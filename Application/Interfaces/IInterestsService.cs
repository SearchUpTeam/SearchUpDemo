using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IInterestsService
    {
        Task<ICollection<InterestTag>>  GetInterestsBySubstringAsync(string substring, int maxNumOfResults);
        Task<ICollection<InterestTag>> GetInterestsById(IEnumerable<int> idArray);
        Task AddInterestsToEventAsync(int eventId, IEnumerable<int> idColl);
        Task<ICollection<InterestTag>> GetUserInterestsAsync(int userId);
        Task EditUserInterestsByIdAsync(int userId, IList<int> interestsId);
        Task<IEnumerable<InterestTag>> GetNewForUser(int userId);
    }
}