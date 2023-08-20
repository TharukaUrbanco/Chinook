using Chinook.Models;
using Chinook.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ChinookContext _dbContext;
        public ArtistService(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Artist?> GetArtistByIdAsync(long artistId)
        {
            return await _dbContext.Artists.FirstOrDefaultAsync(r => r.ArtistId == artistId);
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync(string searchValue)
        {
            if(string.IsNullOrEmpty(searchValue))
                return await _dbContext.Artists.Include(r => r.Albums).ToListAsync();

            return await _dbContext.Artists.Where(r => r.Name.ToLower().Contains(searchValue.ToLower())).Include(r => r.Albums).ToListAsync();
        }

    }
}
