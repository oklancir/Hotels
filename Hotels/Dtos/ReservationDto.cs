using System;

namespace Hotels.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public int Discount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GuestId { get; set; }

        public int RoomId { get; set; }

        public int ReservationStatusId { get; set; }

        public int InvoiceId { get; set; }
    }
}