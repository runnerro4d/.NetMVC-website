using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment.Context;
using Assignment.Models;

namespace Assignment.Controllers
{
    public class BookingsController : Controller
    {
        private HotelStuff db = new HotelStuff();

        // GET: Bookings
        
        public ActionResult Index()
        {
            List<Booking> Bookings = db.bookings
                .Include(a => a.room)
                .Include(a => a.room.hotel)
                .Include(a => a.cust).ToList();

            return View(Bookings);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.bookings
                .Include(a => a.room)
                .Include(a => a.cust)
                .Where(a => a.id == id)
                .SingleOrDefault();
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,StartDate,EndDate,NumberOfPeople,TotalCost,cust,room")] Booking booking)
        {
            // extract the room and customer IDs entered by the user.
            int roomId = booking.room.id;
            int custId = booking.cust.id;


            // search for the room and customer data in the database.
            Room r = db.rooms
                .Include(a => a.hotel)
                .Where(a => a.id == roomId)
                .SingleOrDefault();

            Customer c = db.customers.Find(custId);

            // remove room attribute errors from the model state
            if (c != null)
            {
                this.ModelState["cust.FName"].Errors.Clear();
                
            }

            // remove customer attribute errors from the model state
            if (r != null)
            {
                this.ModelState["room.hotel"].Errors.Clear();
                
            }

            // Add the data for the customer and room to the object.
            booking.room = r;
            booking.cust = c;

            if (ModelState.IsValid)
            {
                db.bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StartDate,EndDate,NumberOfPeople,TotalCost")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.bookings.Find(id);
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
            Booking booking = db.bookings.Find(id);
            db.bookings.Remove(booking);
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
