using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment.Models;
using Microsoft.AspNet.Identity;

namespace Assignment.Controllers
{
    public class BookingsController : Controller
    {
        private HotelModel db = new HotelModel();

        // GET: Bookings
        
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var bookings = db.Bookings.Include(b => b.Customer).Include(b => b.Room).Where(b => b.cust_id == userId);
            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                bookings = db.Bookings.Include(b => b.Customer).Include(b => b.Room);
            }
            
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        [Authorize]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.cust_id = new SelectList(db.Customers.Where(c => c.id ==userId), "id", "FName");
            ViewBag.room_id = new SelectList(db.Rooms, "id", "Description");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,StartDate,EndDate,NumberOfPeople,TotalCost,cust_id,room_id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cust_id = new SelectList(db.Customers, "id", "FName", booking.cust_id);
            ViewBag.room_id = new SelectList(db.Rooms, "id", "Description", booking.room_id);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.cust_id = new SelectList(db.Customers, "id", "FName", booking.cust_id);
            ViewBag.room_id = new SelectList(db.Rooms, "id", "Description", booking.room_id);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StartDate,EndDate,NumberOfPeople,TotalCost,cust_id,room_id")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cust_id = new SelectList(db.Customers, "id", "FName", booking.cust_id);
            ViewBag.room_id = new SelectList(db.Rooms, "id", "Description", booking.room_id);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
