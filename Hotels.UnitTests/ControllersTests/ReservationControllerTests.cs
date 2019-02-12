using HoteliApp.UnitTesting.TestDbSets;
using Hotels.Controllers;
using Hotels.Models;
using Hotels.UnitTests.TestDbSets;
using Hotels.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hotels.UnitTests.ControllersTests
{
    [TestClass]
    public class ReservationControllerTests
    {
        [TestMethod]
        public void ReservationList_WhenCalled_ReturnsReservationListView()
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
            var context = new TestHotelsContext { Guests = new TestGuestDbSet() };
            var controller = new ReservationController(context);

            var result = controller.New() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void SaveGuestDate_WithValidData_ReturnsFinalizeReservationView()
        {
            var context = new TestHotelsContext { Reservations = new TestReservationDbSet() };
            var controller = new ReservationController(context);

            var reservationFormViewModel = new ReservationFormViewModel
            {
                StartDate = Convert.ToDateTime("2019-05-05T00:00:00"),
                EndDate = Convert.ToDateTime("2019-12-05T00:00:00"),
                GuestId = 1
            };

            var result = controller.SaveGuestDate(reservationFormViewModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(result.ViewName, "FinalizeReservation");
            Assert.IsInstanceOfType(result.Model, typeof(ReservationFormViewModel));
        }

        [TestMethod]
        public void SaveGuestDate_WithInValidData_ReturnsSelectGuesValidationErrorView()
        {
            var context = new TestHotelsContext { Reservations = new TestReservationDbSet() };
            var controller = new ReservationController(context);

            var reservationFormViewModel = new ReservationFormViewModel
            {
                StartDate = Convert.ToDateTime("2019-05-05T00:00:00")
            };

            var result = controller.SaveGuestDate(reservationFormViewModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(result.ViewName, "FinalizeReservation");
            Assert.IsInstanceOfType(result.Model, typeof(ReservationFormViewModel));
        }

        [TestMethod]
        public void SaveGuestDate_WithValidData_ReturnsFinalizeReservation()
        {
            var controller = new ReservationController();
            controller.ModelState.AddModelError("StartDate", "Start Date is required");
            var result = controller.SaveGuestDate(MockReservationViewModel()) as ViewResult;


            Assert.IsNotNull(result);
            Assert.IsTrue(!result.ViewData.ModelState.IsValid, "Data entry is valid.");
        }

        [TestMethod]
        public void FinalizeReservation_IfReservationViewModelIsValid_ReturnsRedirectToSaveAction()
        {
            var controller = new ReservationController();
            var result = controller.FinalizeReservation(MockReservationViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual<string>("Save", result.RouteValues["action"].ToString());
            Assert.AreEqual<string>("Reservation", result.RouteValues["controller"].ToString());
        }

        [TestMethod]
        public void Save_IfReservationViewModelIsValid_ReturnsRedirectToActionReservationList()
        {
            var context = new TestHotelsContext();
            var controller = new ReservationController(context);

            var result = controller.Save(MockReservationViewModel()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Checkout_WhenCalled_ReturnsCheckoutView()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Invoices = new TestInvoiceDbSet();
            context.Invoices.Add(new Invoice { Id = 1, ReservationId = 1 });
            context.Items = new TestItemDbSet();
            context.Items.Add(new Item { Id = 1, InvoiceId = 1 });
            var controller = new ReservationController(context);
            var reservation = new Reservation { Id = 1, StartDate = DateTime.Now, EndDate = DateTime.Now };
            context.Reservations.Add(reservation);
            var viewModel = new CheckoutViewModel
            {
                Reservation = MockReservation(),
                Invoice = new Invoice(),
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                TotalAmount = 1,
                Discount = reservation.Discount
            };

            var result = controller.Checkout(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Checkout", result.ViewName);
        }

        private Reservation MockReservation()
        {
            var startdate = DateTime.Now;
            var endDate = DateTime.Now;
            return new Reservation
            {
                StartDate = Convert.ToDateTime("2019-05-05T00:00:00"),
                EndDate = Convert.ToDateTime("2019-12-05T00:00:00"),
                GuestId = 1,
                Discount = 20,
            };
        }

        private ReservationFormViewModel MockReservationViewModel()
        {
            return new ReservationFormViewModel
            {
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
                GuestId = 1,
                RoomId = 1,
                ReservationStatusId = 1,
                Discount = 20
            };
        }
    }
}
