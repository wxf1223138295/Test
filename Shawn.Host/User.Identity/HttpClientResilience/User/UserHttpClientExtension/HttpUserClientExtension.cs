using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;

namespace User.Identity.HttpClientResilience.User.UserHttpClientExtension
{
    public class HttpClientDelegate : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("charset", "utf-8"));
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
    public static class HttpUserClientExtension
    {
        public static IServiceCollection AddUserHttpClient(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient("UserApi")
                .SetHandlerLifetime(TimeSpan.FromMinutes(5)) //Set lifetime to five minutes
                .AddHttpMessageHandler<HttpClientDelegate>()
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true
                    };
                })
                .ConfigureHttpClient(config =>
                {
                    config.Timeout = TimeSpan.FromSeconds(30);
                    config.BaseAddress = new Uri(configuration["HttpClients:UserUrl"]);
                })
                .AddPolicyHandler(GetRetryPolicy(services))
                .AddPolicyHandler(GetCircuitBreakerPolicy(services));
                //.AddPolicyHandler(GetFallBack(services));
          


            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IServiceCollection services)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(new HttpResponseMessage(HttpStatusCode.NotFound))
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(2), (exception, timeSpan, context) =>
                {
                    // Add logic to be executed before each retry, such as logging
                    var ser = services.BuildServiceProvider().GetRequiredService<ILogger<Startup>>();
                    ser.LogError("重试中");
                    Console.WriteLine("重试中");

                });

        }
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(IServiceCollection services)
        {
            Action<DelegateResult<HttpResponseMessage>, CircuitState, TimeSpan, Context> onbreak =
                (mess, state, time, conte) =>
                {
                    Console.WriteLine("熔断");
                };

            Action<Context> onreset = (conte) =>
            {
                Console.WriteLine("恢复");
            };

            Action onhalfopen = () =>
            {
                Console.WriteLine("半开了");
            };
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(8), onbreak, onreset, onhalfopen);
        }


        static IAsyncPolicy<HttpResponseMessage> GetFallBack(IServiceCollection services)
        {
            Func<CancellationToken, Task<HttpResponseMessage>> fun1 = (call) =>
            {
                HttpResponseMessage mes=new HttpResponseMessage();
                mes.StatusCode = HttpStatusCode.Gone;
                mes.Content=new StringContent("fallback");
                return Task.FromResult(mes);
            };

            //Func<DelegateResult<HttpResponseMessage>, Task> fun2 = (dele) =>
            //{
            //    var ser = services.BuildServiceProvider().GetRequiredService<ILogger<Startup>>();
            //    ser.LogError(dele.Exception.Message);
            //    dele.
            //};
                var rr = HttpPolicyExtensions
                .HandleTransientHttpError()
                .FallbackAsync(fun1);

            return rr;
        }
    }
}
