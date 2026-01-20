using Message_Api.Data.Dtos;

namespace Message_Api.Core.Interfaces
{
    public interface IMessageService
    {
        Task<List<AllMessagesResponseDto>> GetConversationWithUserAsync(int currentUserId, string friendUsername);

        Task<SendMessageResponseDto> SendMessageAsync(int senderId, string friendUsername, string content);
    }
}