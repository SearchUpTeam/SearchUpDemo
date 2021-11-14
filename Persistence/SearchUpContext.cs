using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SearchUpContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Following> Following {get; set;}
        public DbSet<InterestTag> InterestTags { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<EventAttachedFile> EventFiles { get; set; }
        public DbSet<MessageAttachedFile> MessageFiles { get; set; }
        public DbSet<ChatMembership> ChatMemberships { get; set; }
        public DbSet<EventMembership> EventMemberships { get; set; }

        public SearchUpContext(DbContextOptions<SearchUpContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FOLLOWING CONSTRAINS
            // set composite key
            modelBuilder.Entity<Following>().HasKey(following => new { following.FollowerId, following.FollowedId });
            // references and delete behavior between following and user entities
            modelBuilder.Entity<Following>()
                .HasOne(f => f.Follower)
                .WithMany(follower => follower.Follows)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Following>()
                .HasOne(f => f.Followed)
                .WithMany(followed => followed.Followers)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<EventMembership>().HasKey(membership => new { membership.UserId, membership.EventId});
            modelBuilder.Entity<ChatMembership>().HasKey(membership => new { membership.UserId, membership.ChatId});

            base.OnModelCreating(modelBuilder);
        }
    }
}
