using Message_Api.Data.Dtos;
using Message_Api.Data.Models;

namespace Message_Api.Data.Interfaces
{
    public interface IFriendRequestRepository
    {
        Task<FriendRequest> SaveFriendRequest(FriendRequest friendRequest);
        Task<FriendRequest?> GetByTagAsync(string requestTag);
        Task MarkAcceptedAsync(FriendRequest request);
        Task MarkDeclinedAsync(FriendRequest request);
        Task<List<FriendRequest>> GetPendingForUserAsync(int userId);
        Task<bool> ExistsAsync(int senderId, int receiverId);
        Task<List<FriendRequest>> GetPendingRequestsAsync(int userId);
        Task<bool> TagExistsAsync(string requestTag);
    }
}