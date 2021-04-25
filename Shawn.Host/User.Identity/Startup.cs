using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using User.Identity.Authentication;
using User.Identity.ExceptionFilter;
using User.Identity.HttpClientResilience;
using User.Identity.HttpClientResilience.User;
using User.Identity.HttpClientResilience.User.UserHttpClientExtension;
using User.Identity.Services;
using IApplicationLifetime = Microsoft.Extensions.Hosting.IApplicationLifetime;

namespace User.Identity
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
            services.AddControllers(p => { p.Filters.Add<ShawnExctption>(); });

            services.AddIdentityServer()
                .AddExtensionGrantValidator<SmsAuthCode>()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryIdentityResources(identityResources: IdentityConfig.GetIdentityResources());

            services.AddTransient<HttpClientDelegate>();
            services.AddLogging();
            services.AddUserHttpClient(Configuration);
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IAuthCodeService, AuthCodeService>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
   
            app.UseIdentityServer();
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
