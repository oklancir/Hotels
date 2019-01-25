using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Hotels.Controllers;
using Hotels.Models;
using Hotels.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreApp.Tests;

namespace Hotels.UnitTests.ControllersTests
{
    /// <summary>
    /// Summary description for RoomControllerTests
    /// </summary>
    [TestClass]
    public class RoomControllerTests
    {
        public RoomControllerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddRoom_WhenCalled_ReturnRoomListView()
        {
            var context = new TestHotelsContext {Rooms = new TestDbSet<Room>(), RoomTypes = new TestDbSet<RoomType>()};
            context.RoomTypes.Add( new RoomType() { Id = 1, Name = "PoolRoom", Price = 25000000000}); 
            var controller = new RoomController(context);
            var result = controller.AddRoom(new RoomViewModel {Name = "Room 101", RoomTypeId = 1}) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual<string>("RoomList", result.RouteValues["action"].ToString());
            Assert.AreEqual<string>("Room", result.RouteValues["controller"].ToString());
        }
    }
}
