using HoteliApp.UnitTesting.TestDbSets;
using Hotels.Controllers;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Hotels.UnitTests
{
    /// <summary>
    /// Summary description for ReservationHelperTests
    /// </summary>
    [TestClass]
    public class ReservationHelperTests
    {
        [TestMethod]
        public void GetAailableRooms_WhenCalled_ReturnsListOfAvailableRoomsByDates()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            var startDate = Convert.ToDateTime("2019-05-05T00:00:00");
            var endDate = Convert.ToDateTime("2019-12-05T00:00:00");
            var helper = new ReservationHelper();
            var result = helper.GetAvailableRooms(startDate, endDate) as IEnumerable<Room>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Room>));
        }

        private Reservation MockReservation()
        {
            var startdate = DateTime.Today;
            var endDate = DateTime.Today;
            return new Reservation
            {
                StartDate = startdate,
                EndDate = endDate,
                GuestId = 1,
                Discount = 20,
            };
        }

        private IEnumerable<Reservation> MockListOfReservations()
        {
            return new List<Reservation>
            {
                new Reservation
                {
                    StartDate = Convert.ToDateTime("2019-05-05T00:00:00"),
                    EndDate = Convert.ToDateTime("2019-12-05T00:00:00"),
                    ReservationStatusId = 1,
                    GuestId = 1,
                    Discount = 50,
                    InvoiceId = 1,
                    RoomId = 1
                },

                new Reservation
                {
                    StartDate = Convert.ToDateTime("2019-06-06T00:00:00"),
                    EndDate = Convert.ToDateTime("2019-13-06T00:00:00"),
                    ReservationStatusId = 2,
                    GuestId = 2,
                    Discount = 0,
                    InvoiceId = 2,
                    RoomId = 1
                },

                new Reservation
                {
                    StartDate = Convert.ToDateTime("2019-06-06T00:00:00"),
                    EndDate = Convert.ToDateTime("2019-13-06T00:00:00"),
                    ReservationStatusId = 2,
                    GuestId = 2,
                    Discount = 0,
                    InvoiceId = 2,
                    RoomId = 2
                },
            };
        }
    }
}
