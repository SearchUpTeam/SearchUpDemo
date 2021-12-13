using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IInterestsService
    {
        Task<ICollection<InterestTag>>  GetInterestsBySubstringAsync(string substring, int maxNumOfResults);
        Task<ICollection<InterestTag>> GetInterestsById(IEnumerable<int> idArray);
        Task<ICollection<InterestTag>> GetUserInterestsAsync(int userId);
        Task<IEnumerable<InterestTag>> GetEventInterestsAsync(int userId);
        Task CreateInterestTagAsync(InterestTag interestTag);
        Task DeleteInterestTagAsync(int interestId);
    }
}