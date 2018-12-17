using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Reservations()
        {
            return View();
        }
    }
}