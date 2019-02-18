using Hotels.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace Hotels.UnitTests.ControllersTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_WhenCalled_ReturnsIndexView()
        {
            var controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName, "Returned \"Index\" View");
        }

        [TestMethod]
        public void About_WhenCalled_ReturnsAboutView()
        {
            var controller = new HomeController();

            var result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("About", result.ViewName, "Returned \"About\" View");
        }

        [TestMethod]
        public void Contact_WhenCalled_ReturnsContactView()
        {
            var controller = new HomeController();

            var result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Contact", result.ViewName, "Returned \"Contact\" View");
        }
    }
}
