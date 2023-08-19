namespace Chinook.Models;

public class UserPlaylist
{
    public string UserId { get; set; }
    public long PlaylistId { get; set; }
    public virtual ChinookUser User { get; set; }
    public virtual Playlist Playlist { get; set; }
}
