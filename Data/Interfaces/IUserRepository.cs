using Message_Api.Data.Models;

namespace Message_Api.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> AddUserAsync(User user);

        Task<bool> TagExistsAsync(string tag);
    }
}