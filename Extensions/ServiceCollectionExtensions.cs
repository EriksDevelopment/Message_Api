using Message_Api.Data.Interfaces;
using Message_Api.Data.Repositories;
using Message_Api.Core.Interfaces;
using Message_Api.Core.Services;
using Message_Api.Core.Services.Jwt;
using Message_Api.Core.Services.TagGenerator;
namespace Message_Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<JwtService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IFriendshipRepository, FriendshipRepository>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IConversationRepository, ConversationRepository>();

            services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
            services.AddScoped<IFriendRequestService, FriendRequestService>();

            services.AddScoped<UserTagGeneratorService>();
            services.AddScoped<ConversationTagGeneratorService>();
            services.AddScoped<FriendRequestTagGeneratorService>();

            return services;
        }
    }
}