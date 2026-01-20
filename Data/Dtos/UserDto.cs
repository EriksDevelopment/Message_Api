namespace Message_Api.Data.Dtos
{
    public class UserRegisterRequestDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UserRegisterResponseDto
    {
        public string Message { get; set; } = null!;
        public string Tag { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class UserLoginRequestDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UserLoginResponseDto
    {
        public string AccessKey { get; set; } = null!;
    }

    public class ViewFriendsResponseDto
    {
        public string UserName { get; set; } = null!;
    }

    public class UserDeleteRequestDto
    {
        public string Password { get; set; } = null!;
    }

    public class UserDeleteResponseDto
    {
        public string Message { get; set; } = null!;
    }
}