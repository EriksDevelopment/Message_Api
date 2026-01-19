using Message_Api.Core.Interfaces;
using Message_Api.Core.Services.Jwt;
using Message_Api.Core.Services.TagGenerator;
using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Message_Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtService _jwt;
        public UserService(IUserRepository userRepo, JwtService jwt)
        {
            _userRepo = userRepo;
            _jwt = jwt;
        }

        public async Task<UserRegisterResponseDto> AddUserAsync(UserRegisterRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Invalid, fields can't be empty.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var tagGenerator = new TagGeneratorService(_userRepo);
            var uniqueTag = await tagGenerator.GenerateUniqueTag();

            var user = new User
            {
                User_Name = dto.UserName,
                Email = dto.Email,
                Password = hashedPassword,
                Tag = uniqueTag
            };

            await _userRepo.AddUserAsync(user);

            return new UserRegisterResponseDto
            {
                Message = "Registration successfull, welcome to Message!",
                Tag = user.Tag,
                UserName = user.User_Name,
                Email = user.Email
            };
        }

        public async Task<UserLoginResponseDto> LoginUserAsync(UserLoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Invalid, fields can't be empty.");

            var user = await _userRepo.GetUserByUserNameAsync(dto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid username or password.");

            var token = _jwt.GenerateToken(user.Id, "User");

            return new UserLoginResponseDto
            {
                AccessKey = token
            };
        }
    }
}