using Application.Interfaces;
using Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class InterestsService : IInterestsService
    {
        SearchUpContext _context;

        public InterestsService(SearchUpContext context)
        {
            _context = context;
        }

        public async Task AddInterestsToEventAsync(int userId, params int[] interestsId)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddInterestsToUserAsync(int userId, params int[] interestsId)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateInterestTagAsync(InterestTag interestTag)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteInterestTagAsync(int interestId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<InterestTag>> GetEventInterestsAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<InterestTag>> GetUserInterestsAsync(int userId)
        {
            var user = await _context.User.FindAsync(userId);
            return user.Interests;
        }

        public async Task RemoveInterestsFromEventAsync(int userId, params int[] interestsId)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveInterestsFromUserAsync(int userId, params int[] interestsId)
        {
            throw new System.NotImplementedException();
        }
    }
}