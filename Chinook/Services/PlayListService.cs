using Chinook.Models;
using Chinook.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class PlayListService : IPlayListService
    {
        private ChinookContext _dbContext;
        public PlayListService(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Playlist?> GetPlayListByNameAndUserIdAsync(string name, string userId)
        {
            return await _dbContext.UserPlaylists.Where(r => r.UserId == userId)
                .Select(r => r.Playlist)
                .SingleOrDefaultAsync(r => r.Name == name);
        }

        public async Task<IEnumerable<Playlist>> GetPlayListsByUserIdAsync(string userId)
        {
            return await _dbContext.UserPlaylists.Where(r => r.UserId == userId).Select(r => r.Playlist).ToListAsync();
        }
    }
}
