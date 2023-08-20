using Chinook.ClientModels;
using Chinook.Models;

namespace Chinook.Services.IServices
{
    public interface ITrackService
    {
        Task<Track?> GetTrackByIdAsync(long trackId);
        Task<IEnumerable<PlaylistTrack>> GetTracksWithAlbumAndFavoriteInfoByArtistIdAsync(long artistId, string userId);
        Task<List<Track>> GetFavoriteTracksByUserIdAsync(string userId);
        Task UpdateTrackAsFavorite(string userId, long trackId);
        Task UpdateTrackAsUnFavorite(string userId, long trackId);
    }
}
