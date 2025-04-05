using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Interfaces;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.User.Models;

namespace Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Infrastructure.Services;

public class UserRepository: IUserRepository
{
    public Task AddAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}