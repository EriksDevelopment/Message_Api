using Message_Api.Data.Dtos;

namespace Message_Api.Core.Interfaces
{
    public interface IMessageService
    {
        Task<List<AllMessagesResponseDto>> GetRecievedMessagesAsync(int recieverId);

        Task<SendMessageResponseDto> SendMessageAsync(int senderId, string friendUsername, string content);
    }
}