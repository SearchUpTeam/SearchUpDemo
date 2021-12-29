using Application.Interfaces;
using Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class FollowingService : IFollowingService
    {
        SearchUpContext _context;
        public FollowingService(SearchUpContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetFollowingsAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> CountFollowingsAsync(int userId)
        {
            return await _context.Following.CountAsync(f => f.FollowerId == userId);
        }

        public async Task<int> CountFollowersAsync(int userId)
        {
            return await _context.Following.CountAsync(f => f.FollowedId == userId);
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsFollowedAsync(int followerId, int followedId)
        {
            if (followerId != followedId)
                return await _context.Following.AnyAsync(f => f.FollowerId == followerId && f.FollowerId == followedId);
            else
                return false;
        }

        public async Task FollowAsync(int followerId, int followedId)
        {
            if(followerId != followedId)
            {
                Following following = new Following() { FollowerId = followerId, FollowedId = followedId };
                await _context.Following.AddAsync(following);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnfollowAsync(int followerId, int followedId)
        {
            Following following = new Following() { FollowerId = followerId, FollowedId = followedId };
            _context.Following.Remove(following);
            await _context.SaveChangesAsync();
        }
    }
}