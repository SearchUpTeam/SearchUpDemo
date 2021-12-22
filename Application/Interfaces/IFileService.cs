using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task CreateAvatarAsync(Avatar avatar);
        Task<IList<Avatar>> GetAvatarsAsync(int userId);
        Task<Avatar> GetCurrentAvatarAsync(int userId);
        Task UploadFilesForEvent(EventAttachedFile file); 
    }
}
