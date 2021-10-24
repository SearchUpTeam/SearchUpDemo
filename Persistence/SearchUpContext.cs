using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SearchUpContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<InterestTag> InterestTags { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<EventAttachedFile> EventFiles { get; set; }
        public DbSet<MessageAttachedFile> MessageFiles { get; set; }
        public SearchUpContext(DbContextOptions<SearchUpContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
