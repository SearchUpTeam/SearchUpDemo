using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class InterestTag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<User> InteresteddUsers { get; set; }
        public ICollection<Event> RelatedEvents { get; set; }
    }
}
