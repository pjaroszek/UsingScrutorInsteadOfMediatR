using Microsoft.Extensions.DependencyInjection;

namespace Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Tutaj możesz dodać inne zależności warstwy aplikacji
        // np. komponenty walidacyjne, mappery, itp.

        // AutoMapper
        // services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // FluentValidation
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}