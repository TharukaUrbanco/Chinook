using Chinook.Models;
using Chinook.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly ChinookContext _dbContext;
        public AlbumService(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Album>> GetAlbumsByArtistIdAsync(int artistId)
        {
            return await _dbContext.Albums.Where(a => a.ArtistId == artistId).ToListAsync();
        }
    }
}
