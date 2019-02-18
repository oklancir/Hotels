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
            var context = new TestHotelsContext();
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
            var context = new TestHotelsContext();
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
            Assert.AreEqual<string>("Save", result.RouteValues["action"].ToString(), "Returned Save Action");
            Assert.AreEqual<string>("Reservation", result.RouteValues["controller"].ToString(), "Returned Reservation Controller");
        }

        [TestMethod]
        public void Save_IfReservationViewModelIsValid_ReturnsRedirectToActionReservationList()
        {
            var context = new TestHotelsContext();
            var controller = new ReservationController(context);

            var result = controller.Save(MockReservationViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual<string>("ReservationList", result.RouteValues["action"].ToString(), "Returned ReservationList Action");
            Assert.AreEqual<string>("Reservation", result.RouteValues["controller"].ToString(), "Returned Reservation Controller");
        }

        [TestMethod]
        public void Checkout_WhenCalled_ReturnsCheckoutView()
        {
            const double roomPrice = 10;

            var context = new TestHotelsContext();
            context.Items = new TestDbSet<Item>();

            context.RoomTypes = new TestRoomTypeDbSet();
            var roomType = new RoomType() { Id = 1, Name = "TestRoomType", Price = roomPrice };
            context.RoomTypes.Add(roomType);

            context.Rooms = new TestRoomDbSet();
            var room = new Room() { Id = 1, IsAvailable = true, Name = "Soba 103", RoomType = roomType };
            context.Rooms.Add(room);

            context.Reservations = new TestReservationDbSet();
            context.Invoices = new TestInvoiceDbSet();
            context.Invoices.Add(new Invoice { Id = 1, ReservationId = 1 });


            var reservation = new Reservation
            {
                Id = 1,
                RoomId = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(3)
            };
            context.Reservations.Add(reservation);

            var controller = new ReservationController(context);
            var result = controller.Checkout(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = result.Model as CheckoutViewModel;
            Assert.AreEqual(model.TotalAmount, 3 * roomPrice, "Returned expected TotalAmount");
        }

        [TestMethod]
        public void ConfirmCheckout_IfNotNull_ReturnsRedirectToAction()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());
            context.Invoices = new TestInvoiceDbSet();
            context.Invoices.Add(new Invoice { Id = 1, IsPaid = false, ReservationId = 1 });

            var controller = new ReservationController(context);

            var result = controller.ConfirmCheckout(1) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Redirected to ReservationList View");
        }

        [TestMethod]
        public void Details_WhenIdIsNotNull_ReturnsReservationDetailsById()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());
            var controller = new ReservationController(context);

            var result = controller.Details(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Returned Reservation Details with valid Id");
        }

        [TestMethod]
        public void Details_WhenIdIsNegative_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            var controller = new ReservationController(context);

            var result = controller.Details(-1) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Page not found");
        }

        [TestMethod]
        public void Edit_WhenCalled_ReturnsEditGuestById()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());

            var controller = new ReservationController(context);

            var result = controller.Edit(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Returned Edit by Id View");
        }

        [TestMethod]
        public void Edit_WhenGuestIdIsNegative_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());

            var controller = new ReservationController(context);

            var result = controller.Edit(-1) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Bad request ");
        }

        [TestMethod]
        public void Edit_WhenGuestIdIsNull_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());

            var controller = new ReservationController(context);

            var result = controller.Edit(null as int?) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Page not found");
        }

        [TestMethod]
        public void Delete_WhenCalled_ReturnsDeleteGuestById()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());

            var controller = new ReservationController(context);

            var result = controller.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Returned Delete by Id View");
        }

        [TestMethod]
        public void Delete_WhenGuestIdIsNegative_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());

            var controller = new ReservationController(context);

            var result = controller.Delete(-1) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Bad request");
        }

        [TestMethod]
        public void Delete_WhenGuestIdIsNull_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());

            var controller = new ReservationController(context);

            var result = controller.Delete(null as int?) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Page not found");
        }

        [TestMethod]
        public void DeleteConfirmed_WhenReservationIdIsNotNull_ReturnsRedirectToAction()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(MockReservation());

            var controller = new ReservationController(context);

            var result = controller.DeleteConfirmed(1) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Returned Redirect to ReservationList");
        }

        private Reservation MockReservation()
        {
            return new Reservation
            {
                Id = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(3),
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
