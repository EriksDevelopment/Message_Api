using Message_Api.Data.Models;

namespace Message_Api.Data.Interfaces
{
    public interface IFriendshipRepository
    {
        Task AddFriendAsync(int userId, int friendId);

        Task<bool> AlreadyFriendsAsync(int userId, int friendId);
    }
}