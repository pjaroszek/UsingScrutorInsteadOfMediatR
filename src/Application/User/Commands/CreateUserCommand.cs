using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Common.Interfaces.Mediator;

namespace Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.User.Commands;

public record CreateUserCommand(string Name, string Email) : ICommand;