using System.Collections.Generic;

namespace Domain
{
    public class InterestTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
