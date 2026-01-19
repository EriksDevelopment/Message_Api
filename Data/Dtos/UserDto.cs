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
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}