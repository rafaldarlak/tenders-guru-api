using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tenders.Guru.Domain;

namespace Tenders.Guru.Infrastructure;

public static class Startup
{
    public static void AddInfrastructure(IServiceCollection services)
    {
        services.AddDbContext<TendersGuruDbContext>(options =>
            options.UseSqlServer("TendersGuruDbConnectionString"));
    }
}