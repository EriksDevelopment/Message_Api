using Microsoft.EntityFrameworkCore;

namespace Message_Api.Data.Models
{
    [Index(nameof(User_Name), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public string User_Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Tag { get; set; } = null!;

        public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();
        public ICollection<Friendship> FriendsOf { get; set; } = new List<Friendship>();
    }
}