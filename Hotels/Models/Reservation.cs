using System;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }

        [Required]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public virtual Guest Guest { get; set; }

        public virtual Room Room { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }
    }
}