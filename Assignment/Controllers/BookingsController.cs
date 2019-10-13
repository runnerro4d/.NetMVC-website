using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment.Models;
using Assignment.Utils;
using Microsoft.AspNet.Identity;

namespace Assignment.Controllers
{
    public class BookingsController : Controller
    {

        private newHotelModel db = new newHotelModel();

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
            ViewBag.room_id = new SelectList(db.Rooms, "id", "id");
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
            Room r = db.Rooms.Find(booking.room_id);
            booking.TotalCost = ((booking.EndDate - booking.StartDate).TotalDays) * r.PricePerNight;

            ViewBag.startDateMessage = "";
            ViewBag.endDateMessage = "";
            ViewBag.peopleMessage = "";
            // string r = db.Rooms.Where(a => a.id == booking.room_id).ToString(); 
            if (DateTime.Compare(booking.StartDate, booking.EndDate) > 0 )
            {
                ViewBag.endDateMessage = "End Date must be greater than Start Date";
            }

            if (DateTime.Compare(DateTime.Now, booking.EndDate) >= 0)
            {
                ViewBag.endDateMessage = "End Date is not valid as date is in the past";
            }

            if (DateTime.Compare(DateTime.Now, booking.StartDate) >= 0)
            {
                ViewBag.startDateMessage = "Start Date is not valid as date is in the past";
            }

            if (r.RoomCapacity < booking.NumberOfPeople)
            {
                ViewBag.peopleMessage = "This room only has capacity for " + r.RoomCapacity + " people";
            }

            string bookingDetails ="<h1>You Have a New Booking!!</h1> <br> <h4>Booking Details:</h4>" +
                "<p> Check-in Date:" + booking.StartDate + "</p><br>" +
                "<p>Check-out Date" + booking.EndDate + "</p><br>" +
                "<p>Room Number:" + booking.room_id + "</p><br>" +
                "<p>Number of People:" + booking.NumberOfPeople + "</p><br>" +
                "<p>Total Cost:" + booking.TotalCost + "</p><br>";

            string email = User.Identity.GetUserName();

            ModelState.Clear();
            TryValidateModel(booking);
            if (ModelState.IsValid && ViewBag.startDateMessage == "" && ViewBag.endDateMessage == "" && ViewBag.peopleMessage == "")
            {
                db.Bookings.Add(booking);
                db.SaveChanges();

                MailSender m = new MailSender();

                m.Send(email, "New Booking",bookingDetails);

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
            string bookingDetails = "<h1>Your booking has been updated!!</h1> <br> <h4>Booking Details:</h4>" +
                "<p> Booking id:" + booking.id + "</p><br>" +
                "<p> Check-in Date:" + booking.StartDate + "</p><br>" +
                "<p>Check-out Date" + booking.EndDate + "</p><br>" +
                "<p>Room Number:" + booking.room_id + "</p><br>" +
                "<p>Number of People:" + booking.NumberOfPeople + "</p><br>" +
                "<p>Total Cost:" + booking.TotalCost + "</p><br>";
            string email = User.Identity.GetUserName();

            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();

                MailSender m = new MailSender();

                m.Send(email, "Updated Booking", bookingDetails);

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
            string bookingDetails = "<h1>We're sorry to see you go.</h1> <br> <p>The following Booking has been cancelled:</p>" +
                "<p> Booking id:" + booking.id + "</p><br>" +
                "<p> Check-in Date:" + booking.StartDate + "</p><br>" +
                "<p>Check-out Date" + booking.EndDate + "</p><br>" +
                "<p>Room Number:" + booking.room_id + "</p><br>" +
                "<p>Number of People:" + booking.NumberOfPeople + "</p><br>" +
                "<p>Total Cost:" + booking.TotalCost + "</p><br>";
            string email = User.Identity.GetUserName();

            MailSender m = new MailSender();

            m.Send(email, "Cancellation of Booking", bookingDetails);

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
