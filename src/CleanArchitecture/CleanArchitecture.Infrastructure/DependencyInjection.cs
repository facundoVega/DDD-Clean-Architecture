using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Infrastructure.Clock;
using CleanArchitecture.Applications.Abstractions.Clock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Abstractions;
using Dapper;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Applications.Abstractions.Data;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();
        
        var connectionString = configuration.GetConnectionString("Database")
        ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IHireRepository, HireRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }
}