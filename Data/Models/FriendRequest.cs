namespace Message_Api.Data.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; } = null!;

        public int RecieverId { get; set; }
        public User Reciever { get; set; } = null!;

        public DateTime SentAt { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }

        public string RequestTag { get; set; } = null!;
    }
}