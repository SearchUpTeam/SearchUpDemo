using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IEventService
    {
        Task CreateAsync(EventViewModel eventModel);
        Task<IEnumerable<EventViewModel>> GetEventsAsync(int userId);
        Task Update(EventViewModel eventModel);
        Task DeleteAsync(EventViewModel eventModel);
    }
}
