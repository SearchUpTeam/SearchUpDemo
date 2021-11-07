
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
        public IEnumerable<Event> GetVisitedByUser(int userId)
        {
            var eventsId = _context.EventMemberships
                .Where(m => m.UserId == userId)
                .Select(m => m.EventId);

            return _context.Events.Where(e => eventsId.Contains(e.Id));
        }
        public IEnumerable<Event> GetOrganizedByUser(int userId)
        {
            var eventsId = _context.EventMemberships
                .Where(m => m.UserId == userId && m.MemberType == MemberType.Organizer)
                .Select(m => m.EventId);

            return _context.Events.Where(e => eventsId.Contains(e.Id));
        }
        public IEnumerable<Event> GetVisitedByUserAsParticipant(int userId)
        {
            var eventsId = _context.EventMemberships
                .Where(m => m.UserId == userId && m.MemberType == MemberType.Participant)
                .Select(m => m.EventId);

            return _context.Events.Where(e => eventsId.Contains(e.Id));
        }
        public IEnumerable<Event> GetBySearchRequest(string searchRequest)
        {
            return _context.Events.Where(e => e.Title.Contains(searchRequest));
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
        public async Task CreateAsync(Event eventModel)
        {
            await _context.Events.AddAsync(eventModel);
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
