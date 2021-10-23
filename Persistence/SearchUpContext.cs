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
        public SearchUpContext(DbContextOptions<SearchUpContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
