namespace Message_Api.Data.Models
{
    public class Friendship
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int FriendId { get; set; }
        public User Friend { get; set; } = null!;
    }
}