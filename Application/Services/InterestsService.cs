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
        private readonly SearchUpContext _context;

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

        public async Task<ICollection<InterestTag>> GetUserInterestsAsync(int userId)
        {
            var user = await _context.User.Include(u => u.Interests).SingleAsync(u => u.Id == userId);
            return user.Interests;
        }

        public async Task EditUserInterestsByIdAsync(int userId, IList<int> interestsId)
        {
            var user = await _context.Users.Include(u => u.Interests).SingleAsync(u => u.Id == userId);
            List<InterestTag> newInterests = new List<InterestTag>();
            var interests = await _context.InterestTags.ToListAsync();
            foreach(var id in interestsId)
            {
                var item = interests.FirstOrDefault(i => i.Id == id);
                if(item!=null)
                    newInterests.Add(item);
            }
            user.Interests = newInterests;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InterestTag>> GetNewForUser(int userId)
        {
            var userInterests = await GetUserInterestsAsync(userId);
            var interests = await _context.InterestTags.ToListAsync();
            List<InterestTag> result = new List<InterestTag>();
            foreach(var tag in interests)
            {
                if (!userInterests.Contains(tag))
                    result.Add(tag);
            }
            return result;
        }
    }
}