using Hotels.Models;
using Hotels.ViewModels;
using Itenso.TimePeriod;
using NLog;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HotelsContext Context = new HotelsContext();
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        // GET: List of Reservations
        public ActionResult ReservationList()
        {
            var reservations = Context.Reservations.ToList();
            return View(reservations);
        }

        public ActionResult New()
        {
            var guests = Context.Guests.ToList();
            var rooms = Context.Rooms.ToList();
            var viewModel = new ReservationFormViewModel
            {
                Guests = guests,
                Rooms = rooms,
            };

            return View("ReservationForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(ReservationFormViewModel reservationFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View("ReservationForm", reservationFormViewModel);
            }

            var reservation = new Reservation()
            {
                StartDate = reservationFormViewModel.Reservation.StartDate,
                EndDate = reservationFormViewModel.Reservation.EndDate,
                Guest = reservationFormViewModel.Reservation.Guest,
                RoomId = reservationFormViewModel.Reservation.RoomId,
                Discount = reservationFormViewModel.Reservation.Discount
            };

            Context.Reservations.Add(reservation);

            var reservationInDb = Context.Reservations.Single(r => r.Id == reservation.Id);
            reservationInDb.Guest.FirstName = reservation.Guest.FirstName;
            reservationInDb.Guest.LastName = reservation.Guest.LastName;
            reservationInDb.StartDate = reservation.StartDate;
            reservationInDb.EndDate = reservation.EndDate;
            reservationInDb.Discount = reservation.Discount;

            try
            {
                Context.SaveChanges();
                return RedirectToAction("ReservationList", "Reservation");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "Save"));
            }
        }

        public ActionResult Details(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.Id == id);

            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }

        //TODO: Napravit metodu za provjeru/prikaz slobodnih soba(LINQ po rezervacijama)
        //private ReservationFormViewModel RoomIsAvailable(int? id, Reservation reservation)
        //{
        //    TimeRange newRange = new TimeRange(reservation.StartDate, reservation.EndDate);

        //    var rooms = Context.Rooms.ToList();

        //    foreach (var room in rooms)
        //    {
        //        foreach (var VARIABLE in room.)
        //        {

        //        }
        //    }

        //    return
        //}
    }
}