using Message_Api.Core.Interfaces;
using Message_Api.Core.Services.TagGenerator;
using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;

namespace Message_Api.Core.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestRepository _friendRequestRepo;
        private readonly IUserRepository _userRepo;
        private readonly IFriendshipRepository _friendshipRepo;
        private readonly FriendRequestTagGeneratorService _friendRequestTagGenerator;
        public FriendRequestService
        (
            IFriendRequestRepository friendRequestRepo,
            IUserRepository userRepo,
            IFriendshipRepository friendshipRepo,
            FriendRequestTagGeneratorService friendRequestTagGenerator
        )
        {
            _friendRequestRepo = friendRequestRepo;
            _userRepo = userRepo;
            _friendshipRepo = friendshipRepo;
            _friendRequestTagGenerator = friendRequestTagGenerator;
        }

        public async Task AcceptFriendRequestAsync(string requestTag)
        {
            var request = await _friendRequestRepo.GetByTagAsync(requestTag);
            if (request == null)
                throw new ArgumentException("Friend request not found.");

            if (request.IsAccepted || request.IsDeclined)
                throw new InvalidOperationException("Friend request already handled.");

            await _friendRequestRepo.MarkAcceptedAsync(request);

            await _friendshipRepo.AddFriendAsync(request.SenderId, request.RecieverId);
        }

        public async Task DeclineFriendRequestAsync(string requestTag)
        {
            var request = await _friendRequestRepo.GetByTagAsync(requestTag);
            if (request == null)
                throw new ArgumentException("Friend request not found.");

            await _friendRequestRepo.MarkDeclinedAsync(request);
        }

        public async Task<SendFriendRequestResponseDto> SendFriendRequestAsync(int userId, string friendTag)
        {
            var sender = await _userRepo.GetUserByIdAsync(userId);

            var friend = await _userRepo.GetFriendByTagAsync(friendTag);

            if (sender == null || friend == null)
                throw new ArgumentException("User not found.");

            if (sender.Id == friend.Id)
                throw new InvalidOperationException("You cannot add yourself.");

            if (await _friendshipRepo.AlreadyFriendsAsync(sender.Id, friend.Id))
                throw new InvalidOperationException("Already your friend.");

            if (await _friendRequestRepo.ExistsAsync(sender.Id, friend.Id))
                throw new InvalidOperationException("Friend request already sent.");

            var uniqueTag = await _friendRequestTagGenerator.GenerateUniqueFriendRequestTag();

            var request = new FriendRequest
            {
                SentAt = DateTime.UtcNow,
                SenderId = sender.Id,
                RecieverId = friend.Id,
                RequestTag = uniqueTag
            };

            await _friendRequestRepo.SaveFriendRequest(request);

            return new SendFriendRequestResponseDto
            {
                Message = $"Friend request sent to {friend.User_Name} at {request.SentAt.ToLocalTime()}"
            };
        }

        public async Task<List<PendingFriendRequestDto>> GetPendingRequestsAsync(int userId)
        {
            var pending = await _friendRequestRepo.GetPendingForUserAsync(userId);

            if (!pending.Any())
                throw new InvalidOperationException("No friend requests pending.");

            return pending
                .Where(fr => fr.Sender != null)
                .Select(fr => new PendingFriendRequestDto
                {
                    SenderName = fr.Sender!.User_Name,
                    RequestTag = fr.RequestTag,
                    SentAt = fr.SentAt.ToLocalTime()
                })
                .ToList();
        }
    }
}