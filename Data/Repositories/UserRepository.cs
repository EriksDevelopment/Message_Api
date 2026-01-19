using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Message_Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MessageDbContext _context;
        public UserRepository(MessageDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> TagExistsAsync(string tag) =>
            await _context.Users.AnyAsync(u => u.Tag == tag);
    }
}