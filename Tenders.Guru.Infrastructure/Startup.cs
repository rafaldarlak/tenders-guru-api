using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tenders.Guru.Domain.Services;
using Tenders.Guru.Http.Client;
using Tenders.Guru.Infrastructure.Services;

namespace Tenders.Guru.Infrastructure;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TendersGuruDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TendersGuruDbConnectionString")));

        services.AddTendersGuruHttpClient(configuration);
        
        services.Configure<TenderSyncOptions>(
            configuration.GetSection(TenderSyncOptions.SectionName));
        
        services.AddScoped<ITenderSyncService, TenderSyncService>();
        
        services.AddHostedService<TenderSyncBackgroundService>();
    }
}