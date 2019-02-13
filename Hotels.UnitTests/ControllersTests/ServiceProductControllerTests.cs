using Hotels.Controllers;
using Hotels.Models;
using Hotels.UnitTests.TestDbSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void AddServiceProduct_WhenCalled_ReturnsValidation()
        {
            var context = new TestHotelsContext();
            var controller = new ServiceProductController();
            controller.ModelState.AddModelError("Name", "Product name is required.");

            var result = controller.AddServiceProduct(MockServiceProduct()) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.ViewData.ModelState.IsValid, "Data entry is valid");
        }

        [TestMethod]
        public void AddServiceProduct_WhenCalled_ReturnsServiceProductList()
        {
            var context = new TestHotelsContext();
            var controller = new ServiceProductController(context);
            var serviceProductCount = context.ServiceProducts.Count();

            var serviceProduct = new ServiceProduct
            {
                Id = serviceProductCount + 1,
                Name = "test",
                Price = 1
            };

            var result = controller.AddServiceProduct(serviceProduct) as RedirectToRouteResult;
            var addedServiceProduct = context.ServiceProducts.Where(sp => sp.Id == serviceProduct.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(addedServiceProduct);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Delete_WhenIdIsNotNull_ReturnsDeleteServiceProductViewById()
        {
            var context = new TestHotelsContext();
            context.ServiceProducts = new TestServiceProductDbSet();
            context.ServiceProducts.Add(MockServiceProduct());
            var controller = new ServiceProductController(context);

            var result = controller.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeleteConfirmed_WhenIdIsNotNull_ReturnsDeleteServiceProductViewById()
        {
            var context = new TestHotelsContext();
            context.ServiceProducts = new TestServiceProductDbSet();
            context.ServiceProducts.Add(MockServiceProduct());
            var controller = new ServiceProductController(context);

            var result = controller.DeleteConfirmed(1) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Delete_WhenServiceProductByIdDoesNotExist_ReturnsHttpNotFound()
        {
            var context = new TestHotelsContext();
            context.ServiceProducts = new TestServiceProductDbSet();
            var controller = new ServiceProductController(context);

            var result = controller.Delete(1) as HttpNotFoundResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }


        private ServiceProduct MockServiceProduct()
        {
            return new ServiceProduct
            {
                Id = 1,
                Name = "TestServiceProductName",
                Price = 1
            };
        }
    }
}
