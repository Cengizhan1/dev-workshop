using Application.Interfaces;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("SqlConnection"), option =>
            {
                option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName()
                    .Name);
            });
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

       // services.AddAutoMapper(typeof(MapProfile));

        return services;
    }
}