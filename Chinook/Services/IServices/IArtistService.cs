using Chinook.Models;

namespace Chinook.Services.IServices
{
    public interface IArtistService
    {
        Task<Artist?> GetArtistByIdAsync(long artistId);
        Task<IEnumerable<Artist>> GetAllArtistsAsync(string searchValue);
    }
}
