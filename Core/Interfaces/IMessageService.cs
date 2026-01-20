using Message_Api.Data.Dtos;

namespace Message_Api.Core.Interfaces
{
    public interface IMessageService
    {
        Task<List<ViewRecievedMessagesResponseDto>> GetRecievedMessagesAsync(int senderId);
    }
}