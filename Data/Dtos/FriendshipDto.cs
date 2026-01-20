namespace Message_Api.Data.Dtos
{
    public class AddFriendRequestDto
    {
        public string FriendTag { get; set; } = null!;
    }

    public class AddFriendResponseDto
    {
        public string Message { get; set; } = null!;
    }
}