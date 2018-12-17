using System;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int Discount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public virtual Guest Guest { get; set; }

        public virtual Room Room { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }
    }
}