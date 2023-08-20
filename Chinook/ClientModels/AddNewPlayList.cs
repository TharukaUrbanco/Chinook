using System.ComponentModel.DataAnnotations;

namespace Chinook.ClientModels
{
    public class AddNewPlayList
    {
        [Required]
        public string PlayListName { get; set; }
    }
}
