using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View("~/Views/Index.cshtml");
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Reserve a room.";

            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Contact");
        }
    }
}