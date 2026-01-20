using Message_Api.Data.Interfaces;

namespace Message_Api.Core.Services.TagGenerator
{
    public class UserTagGeneratorService
    {
        private readonly IUserRepository _userRepo;
        public UserTagGeneratorService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        private string GenerateRandomUserTag()
        {
            var random = new Random();
            return $"#{random.Next(10000, 99999)}";
        }

        public async Task<string> GenerateUniqueUserTag()
        {
            string tag;
            do
            {
                tag = GenerateRandomUserTag();
            } while (await _userRepo.TagExistsAsync(tag));

            return tag;
        }
    }

    public class ConversationTagGeneratorService
    {
        private readonly IConversationRepository _conversationRepo;
        public ConversationTagGeneratorService(IConversationRepository conversationRepo)
        {
            _conversationRepo = conversationRepo;
        }

        private string GenerateRandomConversationTag()
        {
            var random = new Random();
            return $"#{random.Next(1000000, 9999999)}";
        }

        public async Task<string> GenerateUniqueConversationTag()
        {
            string tag;
            do
            {
                tag = GenerateRandomConversationTag();
            } while (await _conversationRepo.TagExistsAsync(tag));

            return tag;
        }
    }
}