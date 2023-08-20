using Chinook.Models;

namespace Chinook.Services.IServices
{
    public interface IPlayListService
    {
        Task<IEnumerable<Playlist>> GetPlayListsByUserIdAsync(string userId);
        Task<Playlist?> GetPlayListByNameAndUserIdAsync(string name, string userId);
        Task<string> AddNewPlayListToUser(string playListName, string userId);
    }
}
