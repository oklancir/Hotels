using Hotels.Models;
using System.Collections.Generic;

namespace Hotels.ViewModels
{
    public class BuyViewModel
    {
        public int ReservationId { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }

        public int Amount { get; set; }
    }
}