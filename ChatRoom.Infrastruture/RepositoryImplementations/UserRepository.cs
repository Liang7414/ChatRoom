using ChatRoom.Domain.Entities.UserAggregate;
using ChatRoom.Domain.RepositoryInterfaces;
using ChatRoom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Infrastructure.RepositoryImplementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatRoomDbContext _context;

        public UserRepository(ChatRoomDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string? username)
        {
            return await _context.Users.Include(u => u.Profile)
                                        .FirstOrDefaultAsync(u => u.Name == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}