# Scrutor Replace MediatR PoC

![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4)
![C#](https://img.shields.io/badge/C%23-11-239120)
![License](https://img.shields.io/github/license/jaroszek/scrutor-replace-mediatr-poc)

This repository demonstrates how to replace the MediatR library with a custom implementation using Scrutor for automatic dependency registration in .NET 9 applications.

## English

### Overview
With the release of .NET 9, many developers look for ways to simplify their tech stack and reduce external dependencies. This project shows how to replace the popular MediatR library with a custom implementation while maintaining the same behavior and benefits.

The custom implementation leverages Scrutor's assembly scanning capabilities to automatically discover and register command and query handlers, similar to MediatR.

### Features
- **Custom Mediator Pattern Implementation**: Built from scratch to replace MediatR
- **Automatic Handler Registration**: Using Scrutor to scan assemblies and register handlers
- **Clean Architecture Structure**: Proper separation of concerns
- **Minimal API Support**: Modern approach with endpoint definitions
- **Swagger Integration**: API documentation and testing
- **In-Memory Repository**: Simple repository implementation for demo purposes

### Project Structure
```
├── src/
│   ├── Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Domain/
│   ├── Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── Common/Interfaces/Mediator/
│   │   └── Interfaces/
│   ├── Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Infrastructure/
│   │   ├── Mediator/
│   │   └── Repositories/
│   └── Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.WebApi/
└── tests/
```

### Getting Started

#### Prerequisites
- .NET 9 SDK
- Visual Studio 2022 or later / VS Code

#### Installation
1. Clone the repository
```bash
git clone https://github.com/jaroszek/scrutor-replace-mediatr-poc.git
cd scrutor-replace-mediatr-poc
```

2. Build the solution
```bash
dotnet build
```

3. Run the application
```bash
cd src/Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.WebApi
dotnet run
```

4. Open your browser and navigate to:
```
https://localhost:5001/swagger
```

### Usage Examples

#### Creating a New Command
```csharp
// Define a command
public record CreateUserCommand(string Name, string Email) : ICommand;

// Implement a handler
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Name = command.Name,
            Email = command.Email
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}
```

#### Creating a New Query
```csharp
// Define a query
public record GetUserQuery(int UserId) : IQuery<UserDto>;

// Implement a handler
public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User with ID {query.UserId} not found");
        }

        return new UserDto(user.Id, user.Name, user.Email);
    }
}
```

#### Using in Minimal API
```csharp
app.MapPost("/api/users", async (CreateUserCommand command, IMediator mediator) =>
{
    await mediator.SendAsync(command);
    return Results.CreatedAtRoute("GetUser", new { id = 1 }, null);
});
```

### Key Benefits Over MediatR
- **No External Dependencies**: Reduced package dependencies
- **Simplified Implementation**: Core functionality without excess features
- **Full Control**: Ability to modify and extend as needed
- **Performance**: Potential performance improvements with custom implementation
- **Learning Opportunity**: Understand how the mediator pattern works internally

---

## Polski

### Przegląd
Wraz z wydaniem .NET 9, wielu programistów szuka sposobów na uproszczenie swojego stosu technologicznego i zmniejszenie zależności zewnętrznych. Ten projekt pokazuje, jak zastąpić popularną bibliotekę MediatR własną implementacją, zachowując to samo zachowanie i korzyści.

Niestandardowa implementacja wykorzystuje możliwości skanowania assembly przez Scrutor do automatycznego wykrywania i rejestrowania handlerów komend i zapytań, podobnie jak MediatR.

### Funkcje
- **Własna implementacja wzorca Mediatora**: Zbudowana od podstaw, aby zastąpić MediatR
- **Automatyczna rejestracja handlerów**: Wykorzystanie Scrutora do skanowania assembly i rejestrowania handlerów
- **Struktura Clean Architecture**: Odpowiednie rozdzielenie odpowiedzialności
- **Wsparcie dla Minimal API**: Nowoczesne podejście z definicjami endpointów
- **Integracja ze Swaggerem**: Dokumentacja i testowanie API
- **Repozytorium In-Memory**: Prosta implementacja repozytorium dla celów demonstracyjnych

### Struktura projektu
```
├── src/
│   ├── Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Domain/
│   ├── Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── Common/Interfaces/Mediator/
│   │   └── Interfaces/
│   ├── Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Infrastructure/
│   │   ├── Mediator/
│   │   └── Repositories/
│   └── Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.WebApi/
└── tests/
```

### Pierwsze kroki

#### Wymagania
- .NET 9 SDK
- Visual Studio 2022 lub nowszy / VS Code

#### Instalacja
1. Sklonuj repozytorium
```bash
git clone https://github.com/jaroszek/scrutor-replace-mediatr-poc.git
cd scrutor-replace-mediatr-poc
```

2. Zbuduj rozwiązanie
```bash
dotnet build
```

3. Uruchom aplikację
```bash
cd src/Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.WebApi
dotnet run
```

4. Otwórz przeglądarkę i przejdź do:
```
https://localhost:5001/swagger
```

### Przykłady użycia

#### Tworzenie nowej komendy
```csharp
// Definicja komendy
public record CreateUserCommand(string Name, string Email) : ICommand;

// Implementacja handlera
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Name = command.Name,
            Email = command.Email
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}
```

#### Tworzenie nowego zapytania
```csharp
// Definicja zapytania
public record GetUserQuery(int UserId) : IQuery<UserDto>;

// Implementacja handlera
public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User with ID {query.UserId} not found");
        }

        return new UserDto(user.Id, user.Name, user.Email);
    }
}
```

#### Użycie w Minimal API
```csharp
app.MapPost("/api/users", async (CreateUserCommand command, IMediator mediator) =>
{
    await mediator.SendAsync(command);
    return Results.CreatedAtRoute("GetUser", new { id = 1 }, null);
});
```

### Kluczowe zalety w porównaniu z MediatR
- **Brak zależności zewnętrznych**: Zmniejszona liczba zależności od pakietów
- **Uproszczona implementacja**: Podstawowa funkcjonalność bez nadmiarowych funkcji
- **Pełna kontrola**: Możliwość modyfikacji i rozszerzania w razie potrzeby
- **Wydajność**: Potencjalne zwiększenie wydajności dzięki własnej implementacji
- **Możliwość nauki**: Zrozumienie, jak działa wzorzec mediatora wewnętrznie

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
