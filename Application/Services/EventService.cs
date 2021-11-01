using Application.Interfaces;
using Domain;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly SearchUpContext _context;
        public EventService(SearchUpContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Event eventModel)
        {
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Event eventModel)
        {
            _context.Events.Remove(eventModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user.Events;
        }

        public async Task Update(Event eventModel)
        {
            _context.Entry(eventModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
