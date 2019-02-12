using Hotels.Controllers;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hotels.UnitTests.ControllersTests
{
    [TestClass]
    public class ServiceProductControllerTests
    {
        [TestMethod]
        public void ServiceProductList_WhenCalled_ReturnsServiceProductListView()
        {
            var controller = new ServiceProductController();
            var result = controller.ServiceProductList() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<ServiceProduct>));
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
