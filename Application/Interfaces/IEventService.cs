using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IEventService
    {
        Task<Event> GetEventByIdAsync(int eventId);
        Task<IEnumerable<Event>> GetVisitedByUserAsync(int userId);
        Task<IEnumerable<Event>> GetOrganizedByUserAsync(int userId);
        Task<IEnumerable<Event>> GetVisitedByUserAsParticipantAsync(int userId);
        Task<IEnumerable<Event>> GetBySearchRequestAsync(string searchRequest, int skip, int take);
        Task SubscribeAsync(int eventId, int userId);
        Task UnsubscribeAsync(int eventId, int userId);
        Task KickMemberAsync(int eventId, int userId);
        Task UnkickMemberAsync(int eventId, int userId);
        Task CreateAsync(Event eventModel, int creatorId);
        Task UpdateAsync(Event eventModel);
        Task DeleteAsync(int eventId);
    }
}
