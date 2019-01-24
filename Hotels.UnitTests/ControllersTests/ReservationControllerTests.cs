using HoteliApp.UnitTesting.TestDbSets;
using Hotels.Controllers;
using Hotels.Models;
using Hotels.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hotels.UnitTests.ControllersTests
{
    /// <summary>
    /// Summary description for ReservationControllerTests
    /// </summary>
    [TestClass]
    public class ReservationControllerTests
    {
        [TestMethod]
        public void ReservationList_WhenCalled_ReturnReservationListView()
        {
            var controller = new ReservationController();
            var result = controller.ReservationList() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Reservation>));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void New_WhenCalled_ReturnView()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            var controller = new ReservationController(context);

            var result = controller.New() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void SaveGuestDate_WithValidData_ReturnFinalizeReservationView()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            var controller = new ReservationController(context);

            var reservationFormViewModel = new ReservationFormViewModel
            {
                StartDate = Convert.ToDateTime("2019-05-05T00:00:00"),
                EndDate = Convert.ToDateTime("2019-12-05T00:00:00"),
                GuestId = 1,
                RoomId = 1,
                Discount = 20
            };

            var result = controller.SaveGuestDate(reservationFormViewModel) as RedirectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.IsInstanceOfType(result, typeof(ReservationFormViewModel));
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
    }
}
