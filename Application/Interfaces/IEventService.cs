using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IEventService
    {
        IEnumerable<Event> GetVisitedByUser(int userId);
        IEnumerable<Event> GetOrganizedByUser(int userId);
        IEnumerable<Event> GetVisitedByUserAsParticipant(int userId);
        IEnumerable<Event> GetBySearchRequest(string searchRequest);
        Task SubscribeAsync(int eventId, int userId);
        Task UnsubscribeAsync(int eventId, int userId);
        Task KickMemberAsync(int eventId, int userId);
        Task UnkickMemberAsync(int eventId, int userId);
        Task CreateAsync(Event eventModel);
        Task UpdateAsync(Event eventModel);
        Task DeleteAsync(int eventId);
    }
}
