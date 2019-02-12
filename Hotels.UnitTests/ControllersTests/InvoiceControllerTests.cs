using Hotels.Controllers;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hotels.UnitTests.ControllersTests
{
    [TestClass]
    public class InvoiceControllerTests
    {
        [TestMethod]
        public void InvoiceList_WhenCalled_ReturnsInvoiceListView()
        {
            var context = new TestHotelsContext();
            var controller = new InvoiceController();
            var result = controller.InvoiceList() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Invoice>));
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
