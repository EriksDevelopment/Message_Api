using Message_Api.Data.Dtos;

namespace Message_Api.Core.Interfaces
{
    public interface IFriendshipService
    {
        Task<AddFriendResponseDto> AddFriendAsync(int userId, string friendTag);
    }
}