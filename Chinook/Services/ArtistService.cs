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
        public async Task<IEnumerable<Artist>> GetArtistsAsync()
        {
            return await _dbContext.Artists.ToListAsync();
        }
    }
}
