using Domain;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class UserProfileViewModel
    {
        public int AuthorizedUserId { get; set; }
        public int ViewedUserId { get; set; }
        public string Username { get; set; }
        public string About { get; set; }
        public IEnumerable<Avatar> Avatars { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<InterestTag> Interests { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }
        public bool IsFollowedByCurrentUser { get; set; } // false is viewed and current user is same or if not followed
    }
}
