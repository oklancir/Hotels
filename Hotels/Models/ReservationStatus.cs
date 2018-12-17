using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class ReservationStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}