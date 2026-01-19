namespace Message_Api.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; } = null!;
        public int RecieverId { get; set; }
        public User Reciever { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}