using Chinook.Models;

namespace Chinook.Services.IServices
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAlbumsByArtistIdAsync(int artistId);
    }
}
