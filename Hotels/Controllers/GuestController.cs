using Hotels.Models;
using Hotels.ViewModels;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class GuestController : Controller
    {
        private readonly HotelsContext Context;

        public GuestController()
        {
            Context = new HotelsContext();
        }

        // GET: Guest
        public ActionResult GuestList()
        {
            var viewModel = new GuestViewModel
            {

            };
            return View();
        }

        // POST
        public ActionResult AddGuest()
        {
            return View();
        }
    }
}