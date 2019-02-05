using Hotels.Models;
using Hotels.ViewModels;
using NLog;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hotels.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public ReservationController()
        {
            Context = new HotelsContext();
        }

        public ReservationController(IHotelsContext context)
        {
            Context = context;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: List of Reservations
        public ActionResult ReservationList()
        {
            var reservations = Context.Reservations.ToList();
            return View(reservations);
        }

        public ActionResult New()
        {
            var viewModel = new ReservationFormViewModel();
            PopulateReservationFormViewModel(viewModel);

            return View("SelectGuestDate", viewModel);
        }

        private void PopulateReservationFormViewModel(ReservationFormViewModel viewModel)
        {
            if (viewModel.Guests == null)
            {
                viewModel.Guests = Context.Guests.ToList();
            }

            if (viewModel.StartDate != DateTime.MinValue && viewModel.EndDate != DateTime.MinValue)
            {
                var reservationHelper = new ReservationHelper();
                var availableRooms = reservationHelper.GetAvailableRooms(viewModel.StartDate, viewModel.EndDate);
                viewModel.Rooms = availableRooms;
            }
        }

        public ActionResult SaveGuestDate(ReservationFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateReservationFormViewModel(viewModel);
                return View("SelectGuestDate", viewModel);
            }

            //var reservationHelper = new ReservationHelper();
            //var availableRooms = reservationHelper.GetAvailableRooms(viewModel.StartDate, viewModel.EndDate);
            //viewModel.Rooms = availableRooms;

            PopulateReservationFormViewModel(viewModel);

            return View("FinalizeReservation", viewModel);
        }

        public ActionResult FinalizeReservation(ReservationFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("FinalizeReservation", viewModel);
            }

            return RedirectToAction("Save","Reservation", viewModel);
        }

        [HttpPost]
        public ActionResult Save(ReservationFormViewModel reservationFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("FinalizeReservation", reservationFormViewModel);
            }

            var reservation = new Reservation()
            {
                StartDate = reservationFormViewModel.StartDate,
                EndDate = reservationFormViewModel.EndDate,
                GuestId = reservationFormViewModel.GuestId,
                RoomId = reservationFormViewModel.RoomId,
                Discount = reservationFormViewModel.Discount,
                ReservationStatusId = 1
            };

            var invoice = new Invoice()
            {
                Reservation = reservation
            };

            try
            {
                Context.Reservations.Add(reservation);
                Context.Invoices.Add(invoice);
                Context.SaveChanges();
                return RedirectToAction("ReservationList", "Reservation");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "Save"));
            }
        }

        public ActionResult Checkout(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.Id == id);
            var reservationHelper = new ReservationHelper();

            if (reservation == null)
                return HttpNotFound();
            
            var invoice = Context.Invoices.SingleOrDefault(i => i.ReservationId == reservation.Id);
            var items = Context.Items.Where(i => i.InvoiceId == invoice.Id).ToList();
            var totalAmount = reservationHelper.ReservationTotalAmount(reservation);

            if (invoice != null)
            {
                invoice.TotalAmount = totalAmount;
            }

            var viewModel = new CheckoutViewModel
            {
                Reservation = reservation,
                Invoice = invoice,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Items = items,
                TotalAmount = totalAmount,
                Discount = reservation.Discount
            };

            try
            {
                Context.SaveChanges();
                return View(viewModel);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "Checkout"));
            }
        }

        public ActionResult ConfirmCheckout(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.Id == id);
            var invoice = Context.Invoices.SingleOrDefault(i => i.ReservationId == id);


            if (reservation != null && invoice != null)
            {
                invoice.IsPaid = true;
                reservation.ReservationStatusId = 3;
            }

            try
            {
                Context.SaveChanges();
                return RedirectToAction("ReservationList");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "ConfirmCheckout"));
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = Context.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,Guest,Room")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(reservation).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("ReservationList");
            }
            return View(reservation);
        }

        public ActionResult Details(int id)
        {
            var reservation = Context.Reservations.SingleOrDefault(r => r.Id == id);

            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservation = Context.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var reservation = Context.Reservations.Find(id);

            try
            {
                Context.Reservations.Remove(reservation);
                Context.SaveChanges();
                return RedirectToAction("ReservationList");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Reservation", "Delete"));
            }
        }
    }
}
