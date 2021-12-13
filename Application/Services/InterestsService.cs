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
        public async Task<ICollection<InterestTag>>  GetInterestsBySubstringAsync(string substring, int maxNumOfResults)
        {
            return await _context.InterestTags
                .Where(i => i.Name.Contains(substring))
                .Take(maxNumOfResults)
                .ToListAsync();
        }

        public async Task<ICollection<InterestTag>> GetInterestsById(IEnumerable<int> idArray)
        {
            return await _context.InterestTags
                .Where(i => idArray.Contains(i.Id))
                .ToListAsync();
        }

        public async Task AddInterestsToEventAsync(int eventId, IEnumerable<int> idColl)
        {
            var eventObj = await _context.Events.FindAsync(eventId);
            var topics = await _context.InterestTags.Where(i=>idColl.Contains(i.Id)).ToListAsync();
            
        }

        public async Task CreateInterestTagAsync(InterestTag interestTag)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteInterestTagAsync(int interestId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<InterestTag>> GetUserInterestsAsync(int userId)
        {
            var user = await _context.User.Include(u => u.Interests).SingleAsync(u => u.Id == userId);
            return user.Interests;
        }

        public async Task<IEnumerable<InterestTag>> GetEventInterestsAsync(int eventId)
        {
            throw new System.NotImplementedException();
        }
    }
}