using Hotels.Controllers;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Hotels.UnitTests
{
    [TestClass]
    public class ReservationHelperTests
    {
        [TestMethod]
        public void GetAailableRooms_WhenCalled_ReturnsListOfAvailableRoomsByDates()
        {
            var context = new TestHotelsContext();
            var startDate = Convert.ToDateTime("2019-05-05T00:00:00");
            var endDate = Convert.ToDateTime("2019-12-05T00:00:00");
            var helper = new ReservationHelper(context);
            var result = helper.GetAvailableRooms(startDate, endDate) as IEnumerable<Room>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Room>));
        }
    }
}
