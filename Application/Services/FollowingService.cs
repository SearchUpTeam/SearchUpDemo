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
            return await _context.Followings.CountAsync(f => f.FollowedId == userId);
        }

        public async Task<int> CountFollowersAsync(int userId)
        {
            return await _context.Followings.CountAsync(f => f.FollowerId == userId);
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}