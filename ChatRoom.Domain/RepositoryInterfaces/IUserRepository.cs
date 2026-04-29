using ChatRoom.Domain.Entities.UserAggregate;

namespace ChatRoom.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string? username);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task<bool> SaveChangesAsync();
}