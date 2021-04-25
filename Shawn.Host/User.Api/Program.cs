using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace User.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context,configbuild) =>
                {
                    var path = context.HostingEnvironment.ContentRootPath;
                    var environmentname = context.HostingEnvironment.EnvironmentName;

                    configbuild
                        .SetBasePath(path)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    if (!string.IsNullOrEmpty(environmentname))
                    {
                        configbuild.AddJsonFile($"appsettings.{environmentname}.json", optional: true, reloadOnChange: true);
                    }
                    configbuild.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(p => { p.AddConsole(); });
                    webBuilder.UseUrls("http://localhost:5000");
                    webBuilder.UseStartup<Startup>();

                    webBuilder.Configure((webhostcontext, app) =>
                    {
                        if (webhostcontext.HostingEnvironment.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                       
                    });
                });
    }
}
