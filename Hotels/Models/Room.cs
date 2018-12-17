using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Room
    {
        [Required, Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public virtual RoomType RoomType { get; set; }
    }
}