namespace Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User.Models.User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}