namespace Message_Api.Data.Dtos
{
    public class SendFriendRequestRequestDto
    {
        public string FriendTag { get; set; } = null!;
    }

    public class SendFriendRequestResponseDto
    {
        public string Message { get; set; } = null!;
    }

    public class PendingFriendRequestDto
    {
        public string SenderName { get; set; } = null!;
        public string RequestTag { get; set; } = null!;
        public DateTime SentAt { get; set; }
    }
}