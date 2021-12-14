using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FileService : IFileService
    {
        private readonly SearchUpContext _context;

        public FileService(SearchUpContext context)
        {
            _context = context;
        }

        public async Task CreateAvatarAsync(Avatar avatar)
        {
            await _context.Avatars.AddAsync(avatar);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Avatar>> GetAvatarsAsync(int userId)
        {
            return await _context.Avatars
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<Avatar> GetCurrentAvatarAsync(int userId)
        {
            return await _context.Avatars
                .Where(a => a.UserId == userId)
                .SingleOrDefaultAsync();
        }

        public async Task UploadFilesForEvent(EventAttachedFile file)
        {
            await _context.EventFiles.AddAsync(file);
            await _context.SaveChangesAsync();
        }
    }
}
