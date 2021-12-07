using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IInterestsService
    {
        Task<IEnumerable<InterestTag>> GetUserInterestsAsync(int userId);
        Task AddInterestsToUserAsync(int userId, params int[] interestsId);
        Task RemoveInterestsFromUserAsync(int userId, params int[] interestsId);

        Task<IEnumerable<InterestTag>> GetEventInterestsAsync(int userId);
        Task AddInterestsToEventAsync(int userId, params int[] interestsId);
        Task RemoveInterestsFromEventAsync(int userId, params int[] interestsId);

        Task CreateInterestTagAsync(InterestTag interestTag);
        Task DeleteInterestTagAsync(int interestId);
        Task EditUserInterestsByIdAsync(int userId, IList<int> interestsId);
    }
}