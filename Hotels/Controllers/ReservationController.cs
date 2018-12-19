using Hotels.Models;
using Hotels.ViewModels;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HotelsContext Context = new HotelsContext();
        // GET: List of Reservations
        public ActionResult Reservations()
        {
            //TODO: List all reservations
            var viewModel = new ReservationViewModel
            {
            };
            return View();
        }

        //GET
        public ActionResult EnterGuestDetails()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult EnterGuestDetails(Guest guest)
        {
            return View();
        }

        public ActionResult RoomSelection()
        {
            return View();
        }
    }
}