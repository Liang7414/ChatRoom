using ChatRoom.Domain.Entities.UserAggregate;

namespace ChatRoom.Domain.RepositoryInterfaces
{
    public interface IUserLogRepository
    {
        Task AddAsync(UserLog log);
        Task SaveChangesAsync();

        Task <IEnumerable<UserLog>> GetByUserIdAsync(int userId);
    }
}
