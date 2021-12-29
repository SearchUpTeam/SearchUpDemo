using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IFollowingService
    {
        Task<IEnumerable<User>> GetFollowingsAsync(int userId);
        Task<int> CountFollowingsAsync(int userId);
        Task<int> CountFollowersAsync(int userId);
        Task<IEnumerable<User>> GetFollowersAsync(int userId);
        Task<bool> IsFollowedAsync(int followerId, int followedId);
        Task FollowAsync(int followerId, int followedId);
        Task UnfollowAsync(int followerId, int followedId);

    }
}