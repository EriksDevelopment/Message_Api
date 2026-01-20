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

        public async Task<List<Message>> GetMessageBySenderIdAsync(int senderId)
        {
            var message = await _context.Messages
                .Where(m => m.SenderId == senderId)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();

            return message;
        }
    }
}