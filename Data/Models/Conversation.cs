using Message_Api.Data.Models;

public class Conversation
{
    public int Id { get; set; }
    public string ConversationTag { get; set; } = null!;

    public int UserAId { get; set; }
    public int UserBId { get; set; }

    public User UserA { get; set; } = null!;
    public User UserB { get; set; } = null!;

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}