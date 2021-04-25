using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP.Dashboard.NodeDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Order.Api.OrderDb;

namespace Order.Api
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
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
               {
                   Description = "≤‚ ‘",
                   Title = "Order Api",
                   Version = "v1"
                });
            });
            services.AddDbContext<OrderDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseMySql(Configuration["ConnectionStrings:Default"]));
           var builder= services.AddCap(p =>
            {
                p.UseEntityFramework<OrderDbContext>()
                    .UseRabbitMQ(options =>
                    {
                        options.VirtualHost = "my_vhost";
                        options.HostName = "115.159.155.126";
                        options.UserName = "admin";
                        options.Password = "admin";
                        options.Port = 30011;
                    })
                    .UseDashboard(options =>
                    {
                    });
                    //.UseDiscovery(d =>
                    //{
                    //    d.DiscoveryServerHostName = "115.159.155.126";
                    //    d.DiscoveryServerPort = 8500;
                    //    d.CurrentNodeHostName = "localhost";
                    //    d.CurrentNodePort = 5004;
                    //    d.NodeId = "1";
                    //    d.NodeName = "CAP Order Api Node";
                    //});
            });
           
           services.AddSwaggerGen(option => { option.SwaggerDoc("apiorder",new OpenApiInfo
            {
                Title = "Order Web Host Api",
                Version = "v1",
                Description = "Order for Rich",
                TermsOfService = new Uri("http://www.baidu.com")
            }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Web API V1");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
