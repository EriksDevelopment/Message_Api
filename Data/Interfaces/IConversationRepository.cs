namespace Message_Api.Data.Interfaces
{
    public interface IConversationRepository
    {
        Task<bool> TagExistsAsync(string tag);
        Task<Conversation?> GetBetweenUsersAsync(int userAId, int userBId);
        Task<Conversation> AddConversationAsync(Conversation conversation);
    }
}