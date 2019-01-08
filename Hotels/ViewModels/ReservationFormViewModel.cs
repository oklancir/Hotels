using Hotels.Models;
using System;
using System.Collections.Generic;

namespace Hotels.ViewModels
{
    public class ReservationFormViewModel
    {
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public int Discount { get; set; }
        public IEnumerable<ServiceProduct> ServiceProducts { get; set; }
        public IEnumerable<Guest> Guests { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}