using ChatRoom.Domain.Entities.UserAggregate;

namespace ChatRoom.Domain.RepositoryInterfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> GetByUserIdAsync(int userId);
        Task AddAsync(UserProfile profile);
        Task UpdateAsync(UserProfile profile);
        Task SaveChangesAsync();
    }
}
