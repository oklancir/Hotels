using Hotels.Models;
using System.Collections.Generic;

namespace Hotels.ViewModels
{
    public class ReservationFormViewModel
    {
        public Reservation Reservation { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        public IEnumerable<ServiceProduct> ServiceProducts { get; set; }
        public IEnumerable<Guest> Guests { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}