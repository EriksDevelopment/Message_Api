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

        public async Task<List<Message>> GetReceivedMessagesAsync(int receiverId)
        {
            var message = await _context.Messages
                .Where(m => m.RecieverId == receiverId)
                .Include(m => m.Conversation)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();

            return message;
        }

        public async Task SendMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}