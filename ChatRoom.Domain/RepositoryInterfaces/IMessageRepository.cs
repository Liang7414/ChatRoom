using ChatRoom.Domain.Entities;

namespace ChatRoom.Domain.RepositoryInterfaces
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task SaveChangesAsync();
        Task<IEnumerable<Message>> GetRecentMessagesAsync(int limit);
        Task<Message?> GetByIdAsync(int id);
    }
}
