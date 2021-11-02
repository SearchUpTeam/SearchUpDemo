using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IEventService
    {
        Task CreateAsync(Event eventModel);
        Task<IEnumerable<Event>> GetEventsAsync(int userId);
        Task Update(Event eventModel);
        Task DeleteAsync(Event eventModel);
    }
}
