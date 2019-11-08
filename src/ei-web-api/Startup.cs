using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using ei_core.Interfaces;
using ei_infrastructure.Data;
using ei_infrastructure.Data.Queries;
using ei_infrastructure.Logging;
using ei_infrastructure.Web;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ei_web_api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IDbConnection, SqlConnection>(sp =>
                new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(GetAUserAccountByUsername), typeof(ViewModels.MappingProfile));
            services.AddMediatR(typeof(GetAUserAccountByUsername));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(TransactionBehavior<,>));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddSingleton(typeof(ILoggerFactory),
                typeof(LoggerFactory)); // TODO: This won't be necessary when Microsoft.AspNetCore.Identity is incorporated into the Infrastructure project. To be done in feature #161 (https://dev.azure.com/Apptelier/Entrenamiento%20Imaginativo/_workitems/edit/161).
            services.AddSingleton(typeof(IWebStandardsProvider), typeof(WebStandardsProvider));
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => { builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); }); // TODO: Improve security by refining CORS origins specifications.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
