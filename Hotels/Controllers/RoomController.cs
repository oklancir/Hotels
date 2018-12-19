using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult RoomList()
        {
            return View();
        }
    }
}