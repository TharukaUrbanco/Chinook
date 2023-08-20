using Chinook.ClientModels;
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
                .Include(a => a.Album)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlaylistTrack>> GetTracksWithAlbumAndFavoriteInfoByArtistIdAsync(long artistId, string userId)
        {
            return await _dbContext.Tracks
                .Include(a => a.Album)
                .Where(a => a.Album.ArtistId == artistId)
                .Select(t => new PlaylistTrack()
                {
                    AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId && up.Playlist.Name == "Favorites")).Any()
                })
                .ToListAsync();
        }

        public async Task<List<Track>> GetFavoriteTracksByUserIdAsync(string userId)
        {
            var favoritePlayList = await _dbContext.UserPlaylists
                .Include(r => r.Playlist)
                .ThenInclude(r => r.Tracks)
                .FirstOrDefaultAsync(r => r.UserId == userId && r.Playlist.Name == "Favorites");

            return favoritePlayList?.Playlist?.Tracks?.ToList();
        }

        public async Task UpdateTrackAsUnFavorite(string userId, long trackId)
        {
            try
            {
                var favoritePlaList = await _dbContext.UserPlaylists
                   .Include(r => r.Playlist)
                   .ThenInclude(r => r.Tracks)
                   .FirstOrDefaultAsync(r => r.UserId == userId && r.Playlist.Name == "Favorites");

                var track = await GetTrackByIdAsync(trackId);

                var trakIndb = favoritePlaList.Playlist.Tracks.SingleOrDefault(r => r.TrackId == trackId);
                if (trackId != null)
                {
                    favoritePlaList.Playlist.Tracks.Remove(track);
                    await _dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
            }
        }

        public async Task UpdateTrackAsFavorite(string userId, long trackId)
        {
            try
            {
                var favoritePlaList = await _dbContext.UserPlaylists
                .Include(r => r.Playlist)
                .ThenInclude(r => r.Tracks)
                .FirstOrDefaultAsync(r => r.UserId == userId && r.Playlist.Name == "Favorites");

                var track = await GetTrackByIdAsync(trackId);

                if (favoritePlaList != null)
                {
                    if (favoritePlaList.Playlist.Tracks == null)
                    {
                        favoritePlaList.Playlist.Tracks = new List<Track>();
                        favoritePlaList.Playlist?.Tracks.Add(track);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        var trakIndb = favoritePlaList.Playlist.Tracks.SingleOrDefault(r => r.TrackId == trackId);
                        if (trakIndb == null)
                        {
                            favoritePlaList.Playlist.Tracks.Add(track);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    var favPlayList = new Models.Playlist() { Name = "Favorites" };
                    favPlayList.Tracks.Add(track);
                    await _dbContext.Playlists.AddAsync(favPlayList);
                    await _dbContext.SaveChangesAsync();

                    var userPlayList = new UserPlaylist() { PlaylistId = favPlayList.PlaylistId, UserId = userId };
                    await _dbContext.UserPlaylists.AddAsync(userPlayList);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
