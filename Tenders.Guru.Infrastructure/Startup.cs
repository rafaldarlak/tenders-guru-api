using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tenders.Guru.Http.Client;

namespace Tenders.Guru.Infrastructure;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TendersGuruDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TendersGuruDbConnectionString")));

        services.AddTenderGuruHttpClient(configuration);
    }
}