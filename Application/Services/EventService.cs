using Application.Interfaces;
using Persistence;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly SearchUpContext _context;
        public EventService(SearchUpContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(EventViewModel eventModel)
        {
            throw new System.NotImplementedException();
            //some actions
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(EventViewModel eventModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<EventViewModel>> GetEventsAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(EventViewModel eventModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
