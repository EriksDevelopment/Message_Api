using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Message_Api.Data.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly MessageDbContext _context;
        public FriendRequestRepository(MessageDbContext context)
        {
            _context = context;
        }

        public async Task<FriendRequest> SaveFriendRequest(FriendRequest friendRequest)
        {
            _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();
            return friendRequest;
        }

        public async Task<FriendRequest?> GetByTagAsync(string requestTag)
        {
            return await _context.FriendRequests
                .Include(fr => fr.Sender)
                .Include(fr => fr.Reciever)
                .FirstOrDefaultAsync(fr => fr.RequestTag == requestTag);
        }

        public async Task MarkAcceptedAsync(FriendRequest request)
        {
            request.IsAccepted = true;
            await _context.SaveChangesAsync();
        }

        public async Task MarkDeclinedAsync(FriendRequest request)
        {
            request.IsDeclined = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<FriendRequest>> GetPendingForUserAsync(int userId)
        {
            return await _context.FriendRequests
                .Include(fr => fr.Sender)
                .Include(fr => fr.Reciever)
                .Where(fr => fr.RecieverId == userId && !fr.IsAccepted && !fr.IsDeclined)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int senderId, int receiverId)
        {
            return await _context.FriendRequests
                .AnyAsync(fr => fr.SenderId == senderId
                             && fr.RecieverId == receiverId
                             && !fr.IsAccepted
                             && !fr.IsDeclined);
        }

        public async Task<List<FriendRequest>> GetPendingRequestsAsync(int userId)
        {
            return await _context.FriendRequests
                .Include(fr => fr.Sender)
                .Include(fr => fr.Reciever)
                .Where(fr => fr.RecieverId == userId && !fr.IsAccepted && !fr.IsDeclined)
                .ToListAsync();
        }

        public async Task<bool> TagExistsAsync(string requestTag) =>
            await _context.FriendRequests.AnyAsync(u => u.RequestTag == requestTag);
    }
}