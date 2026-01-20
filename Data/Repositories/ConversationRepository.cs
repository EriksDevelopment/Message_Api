using System.Reflection.Metadata.Ecma335;
using Message_Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Message_Api.Data.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly MessageDbContext _context;
        public ConversationRepository(MessageDbContext context)
        {
            _context = context;
        }
        public async Task<bool> TagExistsAsync(string tag) =>
            await _context.Conversations.AnyAsync(u => u.ConversationTag == tag);

        public async Task<Conversation?> GetBetweenUsersAsync(int userAId, int userBId) =>
            await _context.Conversations.FirstOrDefaultAsync(c =>
            (c.UserAId == userAId && c.UserBId == userBId) ||
            (c.UserAId == userBId && c.UserBId == userAId));

        public async Task<Conversation> AddConversationAsync(Conversation conversation)
        {
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }
    }
}