using System.Reflection;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Common.Interfaces.Mediator;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Interfaces;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Rejestracja innych serwisów infrastruktury...
        services.AddScoped<IUserRepository, UserRepository>();
        // Rejestracja mediatora
        services.AddMediator(
            typeof(ICommand).Assembly  // Assembly z Application layer
        );

        return services;
    }

    public static void AddMediator(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScoped<IMediator, Mediator.Mediator>();

        // Rejestruje wszystkie handlery zapytań
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Rejestruje wszystkie handlery komend
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}