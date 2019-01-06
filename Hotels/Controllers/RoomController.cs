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
    public class RoomController : Controller
    {
        private readonly HotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("HotelsDbLogger");

        public RoomController()
        {
            Context = new HotelsContext();
        }

        // GET: Guest
        public ActionResult RoomList()
        {
            return View(Context.Rooms.ToList());
        }

        // POST
        public ActionResult AddRoom(RoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var room = new Room()
            {
                Name = model.Name
            };

            Context.Rooms.Add(room);
            try
            {
                Context.SaveChanges();
                return RedirectToAction("RoomList", "Room");
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return View("Error", new HandleErrorInfo(e, "Room", "AddRoom"));
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var room = Context.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: GuestList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(guest).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("GuestList");
            }
            return View(guest);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = Context.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest guest = Context.Guests.Find(id);
            Context.Guests.Remove(guest);
            Context.SaveChanges();
            return RedirectToAction("GuestList");
        }

        public ActionResult Details(int? id)
        {
            return View(id);
        }
    }

}