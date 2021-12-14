using Domain;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class FriendsViewModel
    {
        public IEnumerable<User> Followings { get; set; }
        public IEnumerable<User> Followers { get; set; }
    }
}
