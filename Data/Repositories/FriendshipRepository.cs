using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Message_Api.Data.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly MessageDbContext _context;
        public FriendshipRepository(MessageDbContext context)
        {
            _context = context;
        }

        public async Task AddFriendAsync(int userId, int friendId)
        {
            var friendship1 = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            var friendship2 = new Friendship
            {
                UserId = friendId,
                FriendId = userId
            };

            _context.Friendships.AddRange(friendship1, friendship2);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AlreadyFriendsAsync(int userId, int friendId) =>
            await _context.Friendships.AnyAsync(f => f.UserId == userId && f.FriendId == friendId);
    }
}