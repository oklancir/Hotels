using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Required]
        public virtual RoomType RoomType { get; set; }
    }
}