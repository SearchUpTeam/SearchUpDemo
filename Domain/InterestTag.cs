using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class InterestTag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<User> InteresteddUsers { get; set; }
        public virtual ICollection<Event> RelatedEvents { get; set; }
    }
}
