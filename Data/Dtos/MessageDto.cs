namespace Message_Api.Data.Dtos
{
    public class ViewRecievedMessagesResponseDto
    {
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; } = null!;
        public string Content { get; set; } = null!;
    }

    public class SendMessageRequestDto
    {
        public string ToUsername { get; set; } = null!;
        public string Content { get; set; } = null!;
    }

    public class SendMessageResponseDto
    {
        public string Message { get; set; } = null!;
    }
}