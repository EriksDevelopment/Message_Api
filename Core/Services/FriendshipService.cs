using Message_Api.Core.Interfaces;
using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;

namespace Message_Api.Core.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepo;
        private readonly IUserRepository _userRepo;
        public FriendshipService(IFriendshipRepository friendshipRepo, IUserRepository userRepo)
        {
            _friendshipRepo = friendshipRepo;
            _userRepo = userRepo;
        }

        public async Task<AddFriendResponseDto> AddFriendAsync(int userId, string friendTag)
        {
            if (string.IsNullOrWhiteSpace(friendTag))
                throw new ArgumentException("Invalid, fields can't be empty.");

            var friend = await _userRepo.GetFriendByTagAsync(friendTag);
            if (friend == null)
                throw new ArgumentException($"No user found with tag '{friendTag}'");

            var currentUser = await _userRepo.GetUserByIdAsync(userId);
            if (currentUser == null)
                throw new ArgumentException("Current user not found.");

            if (await _friendshipRepo.AlreadyFriendsAsync(userId, friend.Id))
                throw new ArgumentException("User is already your friend.");

            await _friendshipRepo.AddFriendAsync(userId, friend.Id);

            return new AddFriendResponseDto
            {
                Message = $"Congratulations! {friend.User_Name} is now your friend."
            };
        }
    }
}