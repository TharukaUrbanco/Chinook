using Chinook.Models;

namespace Chinook.Services.IServices
{
    public interface ITrackService
    {
        Task<Track?> GetTrackByIdAsync(long trackId);
        Task<IEnumerable<Track>> GetTracksWithAlbumInfoByArtistIdAsync(long artistId);
    }
}
