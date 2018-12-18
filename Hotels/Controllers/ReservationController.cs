using Hotels.Models;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ReservationController : Controller
    {
        private HotelsContext Context = new HotelsContext();
        // GET: Reservation
        public ActionResult Reservations()
        {
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