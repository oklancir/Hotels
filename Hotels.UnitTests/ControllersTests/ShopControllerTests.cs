using HoteliApp.UnitTesting.TestDbSets;
using Hotels.Controllers;
using Hotels.Models;
using Hotels.UnitTests.TestDbSets;
using Hotels.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hotels.UnitTests.ControllersTests
{
    [TestClass]
    public class ShopControllerTests
    {
        [TestMethod]
        public void ShopList_WhenCalled_ReturnsShopListView()
        {
            var context = new TestHotelsContext();
            var controller = new ShopController();

            var result = controller.ShopList() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Item>));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void Buy_WhenCalledReturns_SelectItemsView()
        {
            var context = new TestHotelsContext();
            context.Reservations = new TestReservationDbSet();
            context.ServiceProducts = new TestServiceProductDbSet();
            var controller = new ShopController(context);

            var result = controller.Buy() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(result.ViewName, "SelectItems");
        }

        [TestMethod]
        public void SelectItems_WhenCalled_ReturnsRedirectToCompletePurchase()
        {
            var context = new TestHotelsContext();
            var controller = new ShopController(context);
            var viewModel = GetBuyViewModel();

            var result = controller.SelectItems(viewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual<string>("CompletePurchase", result.RouteValues["action"].ToString());
        }

        [TestMethod]
        public void CompletePurchase_WhenCalled_ReturnsRedirectToActionShopList()
        {
            var context = new TestHotelsContext();
            context.Invoices = new TestInvoiceDbSet();
            context.Invoices.Add(new Invoice { Id = 1, ReservationId = 1 });
            context.ServiceProducts = new TestServiceProductDbSet();
            context.ServiceProducts.Add(new ServiceProduct { Id = 1, Name = "a" });
            context.Reservations = new TestReservationDbSet();
            context.Reservations.Add(new Reservation { Id = 1, InvoiceId = 1 });
            context.Items = new TestItemDbSet();

            var controller = new ShopController(context);
            var result = controller.CompletePurchase(GetBuyViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual<string>("ShopList", result.RouteValues["action"].ToString());
        }

        private BuyViewModel GetBuyViewModel()
        {
            return new BuyViewModel
            {
                ReservationId = 1,
                ServiceProductId = 1,
                Quantity = 1
            };
        }

        private Invoice GetInvoice()
        {
            return new Invoice { Id = 1, ReservationId = 1, IsPaid = false, ItemId = 1 };
        }
    }
}
