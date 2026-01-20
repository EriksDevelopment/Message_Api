using Message_Api.Data.Models;

namespace Message_Api.Data.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetConversationWithUserAsync(int currentUserId, string friendUsername);
        Task SendMessageAsync(Message message);
    }
}