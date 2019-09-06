using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using ei_core.Interfaces;
using ei_infrastructure.Data;
using ei_infrastructure.Data.Queries;
using ei_infrastructure.Logging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ei_web_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDbConnection, SqlConnection>(sp =>
                new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(GetAUserAccountByUsername));
            services.AddMediatR(typeof(GetAUserAccountByUsername));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(TransactionBehavior<,>));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddSingleton(typeof(ILoggerFactory),
                typeof(LoggerFactory)); // TODO: This won't be necessary when Microsoft.AspNetCore.Identity is incorporated into the Infrastructure project. 
            services.AddCors(options =>
            {
                options.AddPolicy("allowAllOriginsHeadersAndMethods",
                    builder => { builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}