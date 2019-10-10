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
    public class CustomersController : Controller
    {
        private newHotelModel db = new newHotelModel();

        
        // GET: Customers
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var customers = db.Customers.Where(c => c.id == userId).ToList();

            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                customers = db.Customers.ToList();
            }
            return View(customers);
        }

        // GET: Customers/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,FName,LName,DateOfRegistration")] Customer customer)
        {
            customer.id = User.Identity.GetUserId();
            customer.DateOfRegistration = DateTime.Now;

            string userDetails = "<h1>Welcome to JoeStar Hotels.</h1> <br> <h4>Your Account details are as follows:</h4>" +
                "<p> Username:" + User.Identity.GetUserName() + "</p><br>" +
                "<p> First Name:" + customer.FName + "</p><br>" +
                "<p> Last Name" + customer.LName + "</p><br>" +
                "<p> DateOfRegistration:" + customer.DateOfRegistration + "</p><br>";
                
            string email = User.Identity.GetUserName();

            ModelState.Clear();
            TryValidateModel(customer);
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                MailSender m = new MailSender();
                m.Send(email, "Welcome", userDetails);

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,FName,LName,DateOfRegistration")] Customer customer)
        {
            string userDetails = "<h1>Account Details Updated</h1> <br> <h4>Your Account details are as follows:</h4>" +
                "<p> Username:" + User.Identity.GetUserName() + "</p><br>" +
                "<p> First Name:" + customer.FName + "</p><br>" +
                "<p> Last Name" + customer.LName + "</p><br>" +
                "<p> DateOfRegistration:" + customer.DateOfRegistration + "</p><br>";

            string email = User.Identity.GetUserName();

            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                MailSender m = new MailSender();
                m.Send(email, "User Detail Update", userDetails);

                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {


            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();

            string userDetails = "<h1>Account Deleted</h1> <br> <p>Your Account has been deleted:</p>";

            string email = User.Identity.GetUserName();

            MailSender m = new MailSender();
            m.Send(email, "Until Next Time", userDetails);
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
