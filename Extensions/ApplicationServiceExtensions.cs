﻿using CitasApp.Data;
using CitasApp.Interfaces;
using CitasApp.Services;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Extensions;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationsServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        return services;

    }


}
