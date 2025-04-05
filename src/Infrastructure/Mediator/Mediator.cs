using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Common.Interfaces.Mediator;

namespace Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Infrastructure.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException($"Nie znaleziono obsługi dla komendy typu {command.GetType().Name}");
        }

        await (Task)handlerType.GetMethod("HandleAsync")!.Invoke(handler, new object[] { command, cancellationToken })!;
    }

    public async Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException($"Nie znaleziono obsługi dla zapytania typu {query.GetType().Name}");
        }

        return await (Task<TResult>)handlerType.GetMethod("HandleAsync")!.Invoke(handler, new object[] { query, cancellationToken })!;
    }
}