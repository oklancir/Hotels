using Hotels.Models;
using Hotels.ViewModels;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ServiceProductController : Controller
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("HotelsDbLogger");

        public ServiceProductController()
        {
            Context = new HotelsContext();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: ServiceProduct
        public ActionResult ServiceProductList()
        {
            var serviceProducts = Context.ServiceProducts.ToList();
            return View(serviceProducts);
        }

        public ActionResult AddServiceProduct(ServiceProduct modelServiceProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(modelServiceProduct);
            }
            var serviceProduct = new ServiceProduct()
            {
                Name = modelServiceProduct.Name,
                Price = modelServiceProduct.Price
            };

            Context.ServiceProducts.Add(serviceProduct);
            try
            {
                Context.SaveChanges();
                return RedirectToAction("ServiceProductList", "ServiceProduct");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "ServiceProduct", "AddServiceProduct"));
            }
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProduct serviceProduct = Context.ServiceProducts.Find(id);
            if (serviceProduct == null)
            {
                return HttpNotFound();
            }
            return View(serviceProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceProduct serviceProduct = Context.ServiceProducts.Find(id);
            Context.ServiceProducts.Remove(serviceProduct);
            Context.SaveChanges();
            return RedirectToAction("ServiceProductList");
        }

        public ActionResult Buy(int? id, BuyViewModel viewModel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reservations = Context.Reservations.ToList();

            var item = Context.ServiceProducts.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // POST: GuestList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(int id, BuyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var invoice = new Invoice()
                {
                    ReservationId = viewModel.ReservationId
                };
                Context.SaveChanges();
                return RedirectToAction("ServiceProductList");
            }
            return View(viewModel);
        }
    }
}