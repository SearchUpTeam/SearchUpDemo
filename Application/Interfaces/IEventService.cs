using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetVisitedByMemberAsync(User user);
        Task<IEnumerable<Event>> GetOrganizedByUserAsync(User user);
        Task<IEnumerable<Event>> GetBySearchRequestAsync(string searchRequest);
        Task SubscribeAsync(Event eventModel, User user);
        Task UnsubscribeAsync(Event eventModel, User user);
        Task KickMemberAsync(Event eventModel, User user);
        Task UnkickMemberAsync(Event eventModel, User user);
        Task CreateAsync(Event eventModel);
        Task UpdateAsync(Event eventModel);
        Task DeleteAsync(Event eventModel);
    }
}
