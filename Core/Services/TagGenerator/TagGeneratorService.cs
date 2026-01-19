using Message_Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Message_Api.Core.Services.TagGenerator
{
    public class TagGeneratorService
    {
        private readonly IUserRepository _userRepo;
        public TagGeneratorService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        private string GenerateRandomTag()
        {
            var random = new Random();
            return $"#{random.Next(10000, 99999)}";
        }

        public async Task<string> GenerateUniqueTag()
        {
            string tag;
            do
            {
                tag = GenerateRandomTag();
            } while (await _userRepo.TagExistsAsync(tag));

            return tag;
        }
    }
}