using HoteliApp.UnitTesting.TestDbSets;
using Hotels.Controllers;
using Hotels.Models;
using Hotels.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hotels.UnitTests.ControllersTests
{
    [TestClass]
    public class RoomControllerTests
    {
        [TestMethod]
        public void RoomList_WhenCalled_ReturnsRoomListView()
        {
            var controller = new RoomController();
            var result = controller.RoomList() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Room>));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void AddRoom_WhenCalled_ReturnRoomListView()
        {
            var context = new TestHotelsContext { Rooms = new TestDbSet<Room>(), RoomTypes = new TestDbSet<RoomType>() };
            context.RoomTypes.Add(new RoomType() { Id = 1, Name = "PoolRoom", Price = 25000000000 });
            var controller = new RoomController(context);
            var result = controller.AddRoom(new RoomViewModel { Name = "Room 101", RoomTypeId = 1 }) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual<string>("RoomList", result.RouteValues["action"].ToString());
            Assert.AreEqual<string>("Room", result.RouteValues["controller"].ToString());
        }

        [TestMethod]
        public void Edit_WhenIdIsNotNull_ReturnsRoomEditById()
        {
            var context = new TestHotelsContext();
            var controller = new RoomController(context);

            var result = controller.Edit(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit_WhenCalled_ReturnsEditGuestById()
        {
            var context = new TestHotelsContext();
            context.Rooms.Add(MockRoom());

            var controller = new RoomController(context);

            var result = controller.Edit(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Edit_WhenRoomIdIsNegative_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Rooms.Add(MockRoom());

            var controller = new RoomController(context);

            var result = controller.Edit(-1) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void Edit_WhenRoomIdIsNull_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Rooms.Add(MockRoom());

            var controller = new RoomController(context);

            var result = controller.Edit(null as int?) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void Edit_WhenRoomIsNull_ReturnsRedirectToAction()
        {
            var context = new TestHotelsContext();
            context.Rooms.Add(MockRoom());

            var controller = new RoomController(context);

            var result = controller.Edit(null as Room) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual<string>("RoomList", result.RouteValues["action"].ToString());
            Assert.AreEqual<string>("Room", result.RouteValues["controller"].ToString());
        }



        [TestMethod]
        public void Delete_WhenCalled_ReturnsDeleteRoomById()
        {
            var context = new TestHotelsContext();
            context.Rooms = new TestRoomDbSet();
            context.Rooms.Add(MockRoom());

            var controller = new RoomController(context);

            var result = controller.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Delete_WhenRoomIdIsNull_ReturnsHttpStatusCodeResult()
        {
            var context = new TestHotelsContext();
            context.Rooms = new TestRoomDbSet();

            var controller = new RoomController(context);

            var result = controller.Delete(null as int?) as HttpStatusCodeResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void DeleteConfirmed_WhenRoomIdIsNotNull_ReturnsRedirectToAction()
        {
            var context = new TestHotelsContext();
            context.Rooms = new TestRoomDbSet();
            context.Rooms.Add(MockRoom());

            var controller = new RoomController(context);

            var result = controller.DeleteConfirmed(1) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }


        private Room MockRoom()
        {
            return new Room()
            {
                Id = 1,
                Name = "a",
                RoomTypeId = 1
            };
        }
    }
}
