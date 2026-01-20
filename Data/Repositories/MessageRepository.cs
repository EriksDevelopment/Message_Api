using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Message_Api.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageDbContext _context;
        public MessageRepository(MessageDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetConversationWithUserAsync(int currentUserId, string friendUsername)
        {
            var friend = await _context.Users.FirstOrDefaultAsync(u => u.User_Name == friendUsername);
            if (friend == null)
                throw new ArgumentException("User not found.");

            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Reciever)
                .Where(m => (m.SenderId == currentUserId && m.RecieverId == friend.Id) ||
                            (m.SenderId == friend.Id && m.RecieverId == currentUserId))
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task SendMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}