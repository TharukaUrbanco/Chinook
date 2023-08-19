using Chinook.Models;

namespace Chinook.Services.IServices
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetAllArtistsAsync();
    }
}
