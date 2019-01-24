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
    }
}
