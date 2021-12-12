
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
        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Event>> GetVisitedByUserAsync(int userId)
        {
            var eventsId = await _context.EventMemberships
                .Where(m => m.UserId == userId)
                .Select(m => m.EventId).ToArrayAsync();
            
            return await _context.Events.Where(e => eventsId.Contains(e.Id))
                .Include(e => e.Topics)
                .ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetOrganizedByUserAsync(int userId)
        {
            var eventsId = await _context.EventMemberships
                .Where(m => m.UserId == userId && m.MemberType == MemberType.Organizer)
                .Select(m => m.EventId).ToArrayAsync();

            return await _context.Events.Where(e => eventsId.Contains(e.Id))
                .Include(e => e.Topics)
                .ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetVisitedByUserAsParticipantAsync(int userId)
        {
            var eventsId = await _context.EventMemberships
                .Where(m => m.UserId == userId && m.MemberType == MemberType.Participant)
                .Select(m => m.EventId).ToArrayAsync();

            return await _context.Events.Where(e => eventsId.Contains(e.Id))
                .Include(e => e.Topics)
                .ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetBySearchRequestAsync(string searchRequest)
        {
            return await _context.Events.Where(e => e.Title.Contains(searchRequest))
                .Include(e => e.Topics)
                .ToListAsync();
        }
        public async Task SubscribeAsync(int eventId, int userId)
        {
            var newMembership = new EventMembership(){
                UserId = userId,
                EventId = eventId,
                MemberType = MemberType.Participant
            };
            await _context.EventMemberships.AddAsync(newMembership);
        }
        public async Task UnsubscribeAsync(int eventId, int userId)
        {
            var membershipToDelete = new EventMembership()
            {
                UserId = userId,
                EventId = eventId
            };
            _context.EventMemberships.Remove(membershipToDelete);
            await _context.SaveChangesAsync();
        }
        public async Task KickMemberAsync(int eventId, int userId)
        {
            var membership = await _context.EventMemberships.FindAsync(userId, eventId);
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
            var membership = await _context.EventMemberships.FindAsync(userId, eventId);
            try{ 
                membership.IsKicked = false;
                _context.Entry(membership).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch(ArgumentException ex){
                
            }
        }
        public async Task CreateAsync(Event eventModel, int creatorId)
        {
            var membership = new EventMembership(){Event = eventModel, UserId = creatorId, MemberType = MemberType.Organizer};
            
            if(eventModel.memberships == null)
                eventModel.memberships = new List<EventMembership>(){membership};
            else
                eventModel.memberships.Add(membership);
            
            _context.Events.Add(eventModel);
            await _context.SaveChangesAsync(); 
        }
        public async Task DeleteAsync(int eventId)
        {
            _context.Events.Remove( await _context.Events.FindAsync(eventId));
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Event eventModel)
        {
            _context.Entry(eventModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
