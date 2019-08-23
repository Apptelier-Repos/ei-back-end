using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using ei_infrastructure.Data.Queries;
using ei_modules;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ei_web_api
{
    public class Startup
    {
        private static

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDbConnection, SqlConnection>(sp => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(Startup));

            var builder = new ContainerBuilder();
            var assembly = typeof(HandlerModule).Assembly;
            builder.RegisterAssemblyModules(assembly);


            services.AddMediatR(typeof(UserAccountQueryHandler));
            // TODO: Add MediatR transaction and logging behavior pipelines below:
            //services.AddScoped(
            //    typeof(IPipelineBehavior<,>),
            //    typeof(TransactionBehavior<,>));
            //services.AddScoped(
            //    typeof(IPipelineBehavior<,>),
            //    typeof(LoggingBehavior<,>));

            // TODO: Add CORS rule to allow all.

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
