using Hotels.Models;
using Hotels.ViewModels;
using NLog;
using System;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HotelsContext Context = new HotelsContext();
        private readonly Logger Logger = LogManager.GetLogger("HotelsDbLogger");

        // GET: List of Reservations
        public ActionResult Reservations()
        {
            //TODO: List all reservations
            var viewModel = new ReservationViewModel
            {
            };
            return View();
        }

        public ActionResult ReservationForm(ReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var reservation = new Reservation()
            {
                Guest = Context.Guests.Find(model.Guest),
                Room = Context.Rooms.Find(model.Room),
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Discount = model.Discount
            };

            Context.Reservations.Add(reservation);
            try
            {
                Context.SaveChanges();
                return RedirectToAction("Reservations", "Reservation");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "ReservationForm"));
            }
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