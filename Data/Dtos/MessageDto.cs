namespace Message_Api.Data.Dtos
{
    public class AllMessagesResponseDto
    {
        public string FromUserName { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string ToUserName { get; set; } = null!;
        public DateTime Timestamp { get; set; }
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