using Hotels.Models;
using System;
using System.Collections.Generic;

namespace Hotels.ViewModels
{
    public class CheckoutViewModel
    {
        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}