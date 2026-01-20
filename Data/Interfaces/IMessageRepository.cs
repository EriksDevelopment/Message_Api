using Message_Api.Data.Models;

namespace Message_Api.Data.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetReceivedMessagesAsync(int receiverId);
        Task SendMessageAsync(Message message);
    }
}