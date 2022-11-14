using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Techrunch.TecVas.FulFillment.MtnConsumer.HostedServices;
using Techrunch.TecVas.FulFillment.MtnConsumer.Services;
using Techrunch.TecVas.Fullfillment.Common.Extensions;

namespace Techrunch.TecVas.FulFillment.MtnConsumer
{
    public class Startup
    {
        private readonly IConfiguration _config;
        [Obsolete]
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        [Obsolete]
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            //Configuration = configuration;
            _config = configuration;
            _environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCommonService(_config);
            services.AddSingleton<IConsumerService, ConsumerService>();
            services.AddHostedService<ConsumerHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
