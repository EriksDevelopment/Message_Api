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

        public async Task<User?> GetUserByUserNameAsync(string userName) =>
            await _context.Users.FirstOrDefaultAsync(u => u.User_Name == userName);

        public async Task<User?> GetFriendByTagAsync(string tag) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Tag == tag);

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<List<User>> GetFriendsByIdAsync(int userId)
        {
            var friends = await _context.Friendships
                .Where(f => f.UserId == userId)
                .Include(f => f.Friend)
                .Select(f => f.Friend)
                .ToListAsync();

            return friends;
        }

        public async Task<User> DeleteUserAsync(User user)
        {
            var friendRequests = await _context.FriendRequests
                .Where(fr => fr.SenderId == user.Id || fr.RecieverId == user.Id)
                .ToListAsync();

            _context.FriendRequests.RemoveRange(friendRequests);

            var friendships = await _context.Friendships
                .Where(f => f.UserId == user.Id || f.FriendId == user.Id)
                .ToListAsync();
            _context.Friendships.RemoveRange(friendships);

            var messages = await _context.Messages
                .Where(m => m.SenderId == user.Id || m.RecieverId == user.Id)
                .ToListAsync();
            _context.Messages.RemoveRange(messages);

            var conversations = await _context.Conversations
                .Where(c => c.UserAId == user.Id || c.UserBId == user.Id)
                .ToListAsync();
            _context.Conversations.RemoveRange(conversations);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
            return user;
        }
    }
}