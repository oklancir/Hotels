using HoteliApp.UnitTesting.TestDbSets;
using Hotels.Controllers;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotels.UnitTests.ControllersTests
{
    [TestClass]
    public class GuestControllerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void GuestList_WhenCalled_ReturnGuestListView()
        {
            var controller = new GuestController();
            var result = controller.GuestList() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Guest>));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void AddGuest_WhenCalled_ReturnValidation()
        {
            var context = new TestHotelsContext();
            var controller = new GuestController(context);
            controller.ModelState.AddModelError("FirstName", "First Name is required");

            var result = controller.AddGuest(MockGuest()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.ViewData.ModelState.IsValid, "Data entry is valid.");
        }

        [TestMethod]
        public void AddGuest_WhenCalled_ReturnGuestList()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            var controller = new GuestController(context);

            var guest = new Guest
            {
                FirstName = "TestName1",
                LastName = "TestLName1",
                Address = "Test Street",
                PhoneNumber = "2037144124",
                Email = "email@email.com"
            };

            var result = controller.AddGuest(guest) as RedirectToRouteResult;
            var addedGuest = context.Guests.Where(g => g.Id == guest.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(addedGuest);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Details_WhenCalled_ReturnGuestById()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            var controller = new GuestController(context);

            var result = controller.Details(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_WhenCalled_ReturnEditGuestById()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            context.Guests.Add(MockGuest());

            var controller = new GuestController(context);

            var result = controller.Edit(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        private Guest MockGuest()
        {
            return new Guest
            {
                FirstName = "firstName",
                LastName = "lastName",
                Address = "address",
                Email = "email@email.com",
                PhoneNumber = "09112345678"
            };
        }
    }
}
