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
using Payment.Api.PaymentDb;
using RabbitMQ.Client;

namespace Payment.Api
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
            services.AddDbContext<PaymentDbContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:Default"]));

            services.AddCap(p =>
            {
                //达到重试次数后人工维护
                //p.FailedThresholdCallback = (ops, s) =>
                //{

                //};
                p.UseEntityFramework<PaymentDbContext>()
                    .UseRabbitMQ(options =>
                    {
                        options.VirtualHost = "my_vhost";
                        options.HostName = "115.159.155.126";
                        options.UserName = "admin";
                        options.Password = "admin";
                        options.Port = 30011;
                    })
                    .UseDashboard(options => { });
                //.UseDiscovery(d =>
                //{
                //    d.DiscoveryServerHostName = "115.159.155.126";
                //    d.DiscoveryServerPort = 8500;
                //    d.CurrentNodeHostName = "localhost";
                //    d.CurrentNodePort = 5003;
                //    d.NodeId = "1";
                //    d.NodeName = "CAP Payment Api Node";
                //});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
