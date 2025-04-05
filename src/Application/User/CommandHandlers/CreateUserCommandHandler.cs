using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Common.Interfaces.Mediator;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Interfaces;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.User.Commands;

namespace Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.User.CommandHandlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new Models.User
        {
            Name = command.Name,
            Email = command.Email
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}