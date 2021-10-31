using Domain;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public string About { get; set; }
        public Avatar Avatar { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
