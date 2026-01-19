using Message_Api.Core.Interfaces;
using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;

namespace Message_Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserRegisterResponseDto> AddUserAsync(UserRegisterRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Invalid, fields can't be empty.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                User_Name = dto.UserName,
                Email = dto.Email,
                Password = hashedPassword
            };

            await _userRepo.AddUserAsync(user);

            return new UserRegisterResponseDto
            {
                Message = "Registration successfull, welcome to Message!",
                UserName = user.User_Name,
                Email = user.Email
            };
        }
    }
}