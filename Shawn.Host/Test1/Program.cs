using Microsoft.Extensions.Hosting;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostweb = Host.CreateDefaultBuilder(args)
                .ConfigureServices(p => { })
                //主机配置
                .ConfigureHostConfiguration(p =>
                {
                })
                //应用配置
                .ConfigureAppConfiguration((hostbuilder,configbuilder) =>
                {
              
                });





            //var host = new HostBuilder()
            //    .ConfigureServices(p => { })
            //    .ConfigureHostConfiguration(p =>
            //    {

            //    }).c



        }
    }
}
