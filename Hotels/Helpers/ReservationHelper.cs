using Hotels.Models;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotels.Controllers
{
    public class ReservationHelper
    {
        private readonly IHotelsContext Context;

        public ReservationHelper()
        {
            Context = new HotelsContext();
        }

        public ReservationHelper(IHotelsContext context)
        {
            Context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            var rangeFromSelect = new TimeRange(startDate, endDate);

            var reservations = Context.Reservations.ToList();

            var unavailableRoomsId = reservations
                .Where(r => rangeFromSelect.IntersectsWith(new TimeRange(r.StartDate, r.EndDate, true)))
                .Select(r => r.RoomId).ToList();

            var availableRooms = Context.Rooms.Where(ar => !unavailableRoomsId.Contains(ar.Id));

            return availableRooms;
        }

        public void SaveReservation(Reservation reservation)
        {
            Context.Reservations.Add(reservation);

            var invoice = new Invoice()
            {
                Reservation = reservation
            };

            Context.Invoices.Add(invoice);
            Context.SaveChanges();
        }

        public double ReservationTotalAmount(Reservation reservation)
        {
            var numberOfDays = (reservation.EndDate - reservation.StartDate).TotalDays;
            var invoice = Context.Invoices.SingleOrDefault(i => i.ReservationId == reservation.Id);
            var items = Context.Items.Where(i => i.InvoiceId == invoice.Id).ToList();

            double totalItemsAmount = 0;

            foreach (var item in items)
            {
                totalItemsAmount += item.ServiceProduct.Price * item.Quantity;
            }

            var totalAmount = totalItemsAmount + reservation.Room.RoomType.Price * numberOfDays;
            totalAmount = totalAmount * (1 - (reservation.Discount * 0.01));

            return totalAmount;
        }
    }
}
