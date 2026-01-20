using Message_Api.Data.Models;

public class Conversation
{
    public int Id { get; set; }
    public string ConversationTag { get; set; } = null!;

    public int UserAId { get; set; }
    public int UserBId { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}