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
using CleanArchitecture.Application.Paginations;
using Asp.Versioning;
using CleanArchitecture.Infrastructure.Outbox;
using Quartz;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Application.Abstractions.Authentication;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        services.AddQuartz();
        services.AddQuartzHostedService(
            options => {
                options.WaitForJobsToComplete = true;
            }
        );

        services.ConfigureOptions<ProcessOutboxMessageSetup>();
        
        services.AddApiVersioning(options => {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddMvc()
        .AddApiExplorer(options => {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl  = true;
        });

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();
        
        var connectionString = configuration.GetConnectionString("ConnectionString")
        ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPaginationRepository, UserRepository>();

        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IHireRepository, HireRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }
}