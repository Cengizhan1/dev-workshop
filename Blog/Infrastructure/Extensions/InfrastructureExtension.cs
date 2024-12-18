﻿using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Infrastructure.Mapping;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
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
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IBlogRepository, BlogRepository>();

        services.AddAutoMapper(typeof(MapProfile));

        return services;
    }
}
