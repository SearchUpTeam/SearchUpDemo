using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string About { get; set; }
        //public File ProfilePicture { get; set; }
        public virtual ICollection<InterestTag> Interests { get; set; }
    }
}
