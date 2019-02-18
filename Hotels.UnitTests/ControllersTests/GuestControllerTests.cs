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
        public void GuestList_WhenCalled_ReturnsGuestListView()
        {
            var controller = new GuestController();
            var result = controller.GuestList() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Guest>));
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Returned GuestList view");
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void AddGuest_WhenCalled_ReturnsValidation()
        {
            var context = new TestHotelsContext();
            var controller = new GuestController(context);
            controller.ModelState.AddModelError("FirstName", "First Name is required");

            var result = controller.AddGuest(MockGuest()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.ViewData.ModelState.IsValid, "Data entry is not valid.");
        }

        [TestMethod]
        public void AddGuest_WhenCalled_ReturnsGuestList()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            var controller = new GuestController(context);
            var guestCount = context.Guests.Count();

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
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), "Redirected to GuestList action");
        }

        [TestMethod]
        public void Details_WhenIdIsNotNull_ReturnsGuestDetailsById()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            context.Guests.Add(MockGuest());
            var controller = new GuestController(context);

            var result = controller.Details(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Returned Guest Details with valid Id");
        }

        [TestMethod]
        public void Details_WhenIdIsNull_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            var controller = new GuestController(context);

            var result = controller.Details(null) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Page not found");
        }

        [TestMethod]
        public void Edit_WhenCalled_ReturnsEditGuestById()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            context.Guests.Add(MockGuest());

            var controller = new GuestController(context);

            var result = controller.Edit(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Returned Edit Guest View by Id");
        }

        [TestMethod]
        public void Edit_WhenGuestIdIsNegative_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            context.Guests.Add(MockGuest());

            var controller = new GuestController(context);

            var result = controller.Edit(-1) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Bad request");
        }

        [TestMethod]
        public void Edit_WhenGuestIdIsNull_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            context.Guests.Add(MockGuest());

            var controller = new GuestController(context);

            var result = controller.Edit(null as int?) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Page not found");
        }

        [TestMethod]
        public void Delete_WhenCalled_ReturnsDeleteGuestById()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            context.Guests.Add(MockGuest());

            var controller = new GuestController(context);

            var result = controller.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult), "Returned Delete Guest View");
        }

        [TestMethod]
        public void Delete_WhenGuestIdIsZero_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            context.Guests.Add(MockGuest());

            var controller = new GuestController(context);

            var result = controller.Delete(0) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Bad request");
        }

        [TestMethod]
        public void Delete_WhenGuestIdIsNull_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Guests = new TestGuestDbSet();
            context.Guests.Add(MockGuest());

            var controller = new GuestController(context);

            var result = controller.Delete(null as int?) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult), "Returned Page not found");
        }

        private Guest MockGuest()
        {
            return new Guest
            {
                Id = 1,
                FirstName = "firstName",
                LastName = "lastName",
                Address = "address",
                Email = "email@email.com",
                PhoneNumber = "09112345678"
            };
        }
    }
}
