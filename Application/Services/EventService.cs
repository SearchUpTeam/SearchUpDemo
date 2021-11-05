
using System;
using Application.Interfaces;
using Domain;
using Domain.Enums;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly SearchUpContext _context;
        public EventService(SearchUpContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Event>> GetVisitedByMemberAsync(int userId)
        {
            var eventsId = _context.EventMemberships.Select(m => m.EventId).Where(m => m.UserId == userId);
            return await _context.Events.Where(e => eventsId.Contains(e.Id));
        }
        public async Task<IEnumerable<Event>> GetOrganizedByUserAsync(int userId)
        {
            var eventsId = _context.EventMemberships.Select(m => m.EventId).Where(m => m.UserId == userId && m.MemberType == MemberType.Organizer);
            return await _context.Events.Where(e => eventsId.Contains(e.Id));
        }
        public async Task<IEnumerable<Event>> GetBySearchRequestAsync(string searchRequest)
        {
            return await _context.Events
                .Where(e => e.Title.Contains(searchRequest))
                .ToListAsync();
        }
        public async Task SubscribeAsync(int eventId, int userId)
        {
            var newMembership = new EventMembership(){
                User = user,
                Event = eventModel,
                MemberType = MemberType.Participant
            };
            await _context.EventMemberships.AddAsync(newMembership);
        }
        public async Task UnsubscribeAsync(int eventId, int userId)
        {
            var membershipToDelete = new EventMembership(){
                User = user,
                Event = eventModel,
                MemberType = MemberType.Participant
            };
            _context.EventMemberships.Remove(membershipToDelete);
            await _context.SaveChangesAsync();
        }
        public async Task KickMemberAsync(int eventId, int userId)
        {
            var membership = await _context.EventMemberships.FindAsync(user.Id, eventModel.Id);
            try{ 
                membership.IsKicked = true;
                _context.Entry(membership).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch(ArgumentException ex){
                
            }
        }
        public async Task UnkickMemberAsync(int eventId, int userId)
        {
            var membership = await _context.EventMemberships.FindAsync(user.Id, eventModel.Id);
            try{ 
                membership.IsKicked = false;
                _context.Entry(membership).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch(ArgumentException ex){
                
            }
        }
        public async Task CreateAsync(Event eventModel)
        {
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int eventId)
        {
            _context.Events.Remove(eventModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event eventModel)
        {
            _context.Entry(eventModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
