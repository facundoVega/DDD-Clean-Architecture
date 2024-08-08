using CleanArchitecture.Application.Abstractions.Behaviors;
using CleanArchitecture.Domain.Hires;
using CleanArchitecture.Applications.Abstractions.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => {
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient<PriceService>();
        
        return services;
    }
}