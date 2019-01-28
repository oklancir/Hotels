using HoteliApp.UnitTesting.TestDbSets;
using Hotels.Controllers;
using Hotels.Models;
using Hotels.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using StoreApp.Tests;

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
            var context = new TestHotelsContext {Guests = new TestGuestDbSet()};
            var controller = new ReservationController(context);

            var result = controller.New() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void SaveGuestDate_WithValidData_ReturnFinalizeReservationView()
        {
            var context = new TestHotelsContext {Reservations = new TestReservationDbSet()};
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
        public void FinalizeReservation_WhenCalled_ReturnsRedirectToActionSave()
        {
            var context = new TestHotelsContext();
            var controller = new ReservationController();
            controller.FinalizeReservation(new ReservationFormViewModel());
            var result = controller.FinalizeReservation(new ReservationFormViewModel()) as RedirectToRouteResult;
            
            
            Assert.IsNotNull(result);
            Assert.AreEqual<string>("Save", result.RouteValues["action"].ToString());
            Assert.AreEqual<string>("Reservation", result.RouteValues["controller"].ToString());
        }

        
        //[TestMethod]
        //public void SaveGuestDate_WithInvalidData_ReturnsSelectGuestDateView()
        //{
        //    var context = new TestHotelsContext { Reservations = new TestReservationDbSet() };
        //    var controller = new ReservationController(context);

        //    var reservationFormViewModel = new ReservationFormViewModel
        //    {
        //        StartDate = Convert.ToDateTime("2019-05-05T00:00:00"),
        //        EndDate = Convert.ToDateTime("2019-12-05T00:00:00"),
        //        GuestId = -10
        //    };

        //    var result = controller.SaveGuestDate(reservationFormViewModel) as ViewResult;

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    Assert.AreEqual(result.ViewName, "SelectGuestDate");
        //    Assert.IsInstanceOfType(result.Model, typeof(ReservationFormViewModel));
        //}

        private ReservationFormViewModel MockReservationFormViewModel()
        {
            return new ReservationFormViewModel
            {
               StartDate = DateTime.Now,
               EndDate = DateTime.Today,
               GuestId = 1
            };
        }
        
        private Reservation MockReservation()
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today;
            return new Reservation
            {
                StartDate = startDate,
                EndDate = endDate,
                GuestId = 1,
                Discount = 20,
            };
        }
    }
}
