namespace Domain
{
    public class Following
    {
        public User Follower { get; set; }
        public User Followed { get; set; }
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
    }
}
