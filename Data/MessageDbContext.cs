using Message_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Message_Api.Data
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<FriendRequest> FriendRequests { get; set; } = null!;
        public DbSet<Friendship> Friendships { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Conversation> Conversations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friendship>()
              .HasKey(f => new { f.UserId, f.FriendId });

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany(u => u.FriendsOf)
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasIndex(f => new { f.UserId, f.FriendId })
                .IsUnique();

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Sender)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(fr => fr.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(fr => fr.Reciever)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(fr => fr.RecieverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasIndex(fr => new { fr.SenderId, fr.RecieverId })
                .IsUnique();

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Reciever)
                .WithMany()
                .HasForeignKey(m => m.RecieverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conversation>()
                .HasIndex(c => new { c.UserAId, c.UserBId })
                .IsUnique();

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.UserA)
                .WithMany(u => u.ConversationsAsUserA)
                .HasForeignKey(c => c.UserAId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.UserB)
                .WithMany(u => u.ConversationsAsUserB)
                .HasForeignKey(c => c.UserBId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}