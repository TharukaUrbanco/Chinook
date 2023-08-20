using Chinook.Models;
using Chinook.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class PlayListService : IPlayListService
    {
        private ChinookContext _dbContext;
        public PlayListService(ChinookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AddNewPlayListToUser(string playListName, string userId)
        {
            if (string.IsNullOrEmpty(playListName))
                return "PlayList Name Must Enter";

            var record = await GetPlayListByNameAndUserIdAsync(playListName, userId);
            if (record != null)
                return "PlayList Name Already Exists";

            var newPlayList = new Playlist()
            {
                Name = playListName,
            };
            _dbContext.Playlists.Add(newPlayList);
            _dbContext.SaveChanges();

            var userPlayList = new UserPlaylist()
            {
                UserId = userId,
                PlaylistId = newPlayList.PlaylistId
            };
            _dbContext.UserPlaylists.Add(userPlayList);
            _dbContext.SaveChanges();

            return "Success";
        }

        public async Task<string> AddTrackToPlayList(long playListId, long trackId)
        {
            var playList = await _dbContext.Playlists.FirstOrDefaultAsync(r => r.PlaylistId == playListId);
            if (playList == null)
                return "PlayList Not Found";

            if (playList.Tracks.Any(r => r.TrackId == trackId))
                return "Track already exists";

            var track = await _dbContext.Tracks.FirstOrDefaultAsync(r => r.TrackId == trackId);
            playList.Tracks.Add(track);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<Playlist?> GetPlayListByNameAndUserIdAsync(string name, string userId)
        {
            return await _dbContext.UserPlaylists.Where(r => r.UserId == userId)
                .Select(r => r.Playlist)
                .SingleOrDefaultAsync(r => r.Name == name);
        }

        public async Task<IEnumerable<Playlist>> GetPlayListsByUserIdAsync(string userId)
        {
            return await _dbContext.UserPlaylists.Where(r => r.UserId == userId).Select(r => r.Playlist).ToListAsync();
        }
    }
}
