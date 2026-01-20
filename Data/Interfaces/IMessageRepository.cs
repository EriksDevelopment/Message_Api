using Message_Api.Data.Models;

namespace Message_Api.Data.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetMessageBySenderIdAsync(int senderId);
    }
}