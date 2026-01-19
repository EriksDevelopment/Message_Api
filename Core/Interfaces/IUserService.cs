using Message_Api.Data.Dtos;

namespace Message_Api.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseDto> AddUserAsync(UserRegisterRequestDto dto);

        Task<UserLoginResponseDto> LoginUserAsync(UserLoginRequestDto dto);
    }
}