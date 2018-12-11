using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotels.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Room Room { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set; }

    }
}