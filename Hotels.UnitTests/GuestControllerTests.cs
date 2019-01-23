using Hotels.Controllers;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hotels.UnitTests
{
    [TestClass]
    class GuestControllerTests
    {
        [TestMethod]
        public void GuestList_WhenCalled_ReturnGuestListView()
        {
            var controller = new GuestController();
            var result = controller.GuestList() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Guest>));
        }

        [TestMethod]
        public void AddGuest_WhenCalled_AddGuestAndRedirectToGuestList()
        {
            var context = new TestHotelsContext();
            var controller = new GuestController(context);
            var guest = MockGuest();

            var result = controller.AddGuest(guest) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
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
