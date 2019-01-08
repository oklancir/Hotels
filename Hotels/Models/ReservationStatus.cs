using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class ReservationStatus
    {
        public int Id { get; set; }

        [DefaultValue("Pending")]
        [Required] public string Name { get; set; }
    }
}