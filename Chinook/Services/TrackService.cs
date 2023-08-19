using Chinook.Models;
using Chinook.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class TrackService : ITrackService
    {
        private readonly ChinookContext _dbContext;
        public TrackService(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Track?> GetTrackByIdAsync(long trackId)
        {
            return await _dbContext.Tracks.FirstOrDefaultAsync(t => t.TrackId == trackId);
        }

        public async Task<IEnumerable<Track>> GetTracksWithAlbumInfoByArtistIdAsync(long artistId)
        {
            return await _dbContext.Tracks.Where(a => a.Album.ArtistId == artistId)
                .Include(a => a.Album).ToListAsync();
        }
    }
}
