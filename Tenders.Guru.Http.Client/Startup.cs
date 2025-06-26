using System.Net;
using System.Net.Mime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Tenders.Guru.Http.Client.Http;

namespace Tenders.Guru.Http.Client;

public static class Startup
{
    public static IServiceCollection AddTenderGuruHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ITenderGuruApiClient>("TenderGuru", client =>
            {
                client.BaseAddress = new Uri(configuration["TenderGuruApi:BaseUrl"]);
                client.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            })        .AddTransientHttpErrorPolicy(policyBuilder =>
                policyBuilder.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
            .AddPolicyHandler((_, _) => Policy<HttpResponseMessage>
                .HandleResult(r => r.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5 * retryAttempt)));;

        return services;
    }
}