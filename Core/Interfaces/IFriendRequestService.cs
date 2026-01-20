using Message_Api.Data.Dtos;

namespace Message_Api.Core.Interfaces
{
    public interface IFriendRequestService
    {
        Task<SendFriendRequestResponseDto> SendFriendRequestAsync(int userId, string friendTag);
        Task AcceptFriendRequestAsync(string requestTag);
        Task DeclineFriendRequestAsync(string requestTag);
        Task<List<PendingFriendRequestDto>> GetPendingRequestsAsync(int userId);
    }
}