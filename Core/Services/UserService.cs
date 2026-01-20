using Message_Api.Core.Interfaces;
using Message_Api.Core.Services.Jwt;
using Message_Api.Core.Services.TagGenerator;
using Message_Api.Data.Dtos;
using Message_Api.Data.Interfaces;
using Message_Api.Data.Models;

namespace Message_Api.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtService _jwt;
        private readonly UserTagGeneratorService _userTagGenerator;
        public UserService(IUserRepository userRepo, JwtService jwt, UserTagGeneratorService userTagGenerator)
        {
            _userRepo = userRepo;
            _jwt = jwt;
            _userTagGenerator = userTagGenerator;
        }

        public async Task<UserRegisterResponseDto> AddUserAsync(UserRegisterRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Invalid, fields can't be empty.");

            var userNameExists = await _userRepo.GetUserByUserNameAsync(dto.UserName);
            if (userNameExists != null)
                throw new ArgumentException("Username already taken.");

            var emailExists = await _userRepo.GetUserByEmailAsync(dto.Email);
            if (emailExists != null)
                throw new ArgumentException("Email already taken.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var uniqueTag = await _userTagGenerator.GenerateUniqueUserTag();

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

        public async Task<List<ViewFriendsResponseDto>> ViewFriendsAsync(int userId)
        {
            var friends = await _userRepo.GetFriendsByIdAsync(userId);
            if (!friends.Any())
                throw new ArgumentException("Oops... No friends found.");

            return friends.Select(f => new ViewFriendsResponseDto
            {
                UserName = f.User_Name
            }).ToList();
        }

        public async Task<UserDeleteResponseDto> DeleteUserAsync(UserDeleteRequestDto dto, int id)
        {
            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Invalid, fields can't be empty.");

            var user = await _userRepo.GetUserByIdAsync(id);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid password.");

            await _userRepo.DeleteUserAsync(user);

            return new UserDeleteResponseDto
            {
                Message = $"User '{user.User_Name}' successfully deleted."
            };
        }
    }
}