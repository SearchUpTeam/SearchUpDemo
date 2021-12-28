using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

            modelBuilder.Entity<InterestTag>().HasAlternateKey(tag => new {tag.Name});

            InterestTag[] tags = new InterestTag[] { 
                new InterestTag() { Id=1, Name="beer"},
                new InterestTag() { Id=2, Name="hard metal"},
                new InterestTag() { Id=3, Name="alternative metal"},
                new InterestTag() { Id=4, Name="alternative rock"},
                new InterestTag() { Id=5, Name="programming"},
                new InterestTag() { Id=6, Name=".net"},
                new InterestTag() { Id=7, Name="python"},
                new InterestTag() { Id=8, Name="jsnode"},
                new InterestTag() { Id=9, Name="onboard games"},
                new InterestTag() { Id=10, Name="space travels"},
                new InterestTag() { Id=11, Name="star wars"},
                new InterestTag() { Id=12, Name="hiking"},
                new InterestTag() { Id=13, Name="tourism"},
                new InterestTag() { Id=14, Name="fishing"},
                new InterestTag() { Id=15, Name="power lifting"},
                new InterestTag() { Id=16, Name="box"}
            };
            foreach (var tag in tags)
            {
                modelBuilder.Entity<InterestTag>().HasData(tag);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
