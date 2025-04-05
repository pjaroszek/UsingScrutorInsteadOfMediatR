namespace Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Common.Interfaces.Mediator;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}