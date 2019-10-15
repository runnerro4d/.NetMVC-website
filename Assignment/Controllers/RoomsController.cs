using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPNET_MVC_Samples.Models;
using Assignment.Models;
using Newtonsoft.Json;

namespace Assignment.Controllers
{
    public class RoomsController : Controller
    {
        private newHotelModel db = new newHotelModel();

        // GET: Rooms
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Hotel);
            return View(rooms.ToList());
        }

        public ActionResult BookingInfo()
        {
            var rooms = db.Rooms.Include(r => r.Hotel);
            List<ChartDataPoint> datapts = new List<ChartDataPoint>();
            List<ChartDataPoint> datapts2 = new List<ChartDataPoint>();
            foreach (Room r in rooms)
            {
                datapts.Add(new ChartDataPoint(r.id, r.Bookings.Count()));
                foreach (Booking b in r.Bookings)
                {
                    if (b.Ratings.Count() != 0)
                    {
                        datapts2.Add(new ChartDataPoint(r.id, (b.Ratings.Sum(a => a.rate) / b.Ratings.Count())));
                    }
                    else
                    {
                        datapts2.Add(new ChartDataPoint(r.id, 0));
                    }
                }
            }

            ViewBag.BookingDatapoints = JsonConvert.SerializeObject(datapts);
            ViewBag.RatingDatapoints = JsonConvert.SerializeObject(datapts2);
            return View();
        }


        public ActionResult newIndex(int? id)
        {
            Hotel h = db.Hotels.Find(id);
            ViewBag.Name = h.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rooms = db.Rooms.Include(r => r.Hotel).Where(r => r.hotel_id == id);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.rating = "Not Yet Rated";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var allBookings = db.Bookings.Where(b => b.room_id == id).Include(r => r.Ratings);
            float ratingSum = 0;
            float ratingCount = 0;
            foreach (Booking b in allBookings) {
                foreach (Rating r in b.Ratings) {
                    ratingSum += r.rate;
                    ratingCount++;
                }
            }
            if (ratingCount != 0) {
                ViewBag.rating = Math.Round(ratingSum / ratingCount, 2);
            }

            

            Room room = db.Rooms.Find(id);

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "Name");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Floor,Description,PricePerNight,RoomCapacity,hotel_id")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "Name", room.hotel_id);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "Name", room.hotel_id);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Floor,Description,PricePerNight,RoomCapacity,hotel_id")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hotel_id = new SelectList(db.Hotels, "id", "Name", room.hotel_id);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
