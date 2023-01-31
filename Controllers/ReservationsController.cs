using Lab8.Models;
using Lab8.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab8.Controllers
{
    public class ReservationsController : Controller
    {
        public acsc384_dbEntities db = new acsc384_dbEntities();
        // GET: Reservations
        public ActionResult Index()
        {
            var res = db.cust_reservations.ToList();
            return View(res);
        }
        public ActionResult Create()
        {
            ViewBag.BoatID = new SelectList(db.boats, "boatid", "Name");
            ViewBag.CustomerID = new SelectList(db.customers, "customerid", "Name");
            return View();
        }
        // POST: Reservation/Create
        [HttpPost]
        public ActionResult Create(ReservationVM model)
        {
            if (ModelState.IsValid)
            {
                reservation newReservation = new reservation();
                newReservation.fkboatid = model.BoatID;
                newReservation.start_date = model.StartDate;
                newReservation.end_date = model.EndDate;
                var resid = db.reservations.Add(newReservation);
                db.SaveChanges();
                if (resid.bookingid > 0)
                {
                    cust_reservations new_cust_res = new cust_reservations();
                    new_cust_res.fkbookingid = resid.bookingid;
                    new_cust_res.fkcustomerid = model.CustomerID;
                    db.cust_reservations.Add(new_cust_res);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Reservations");
            }
            return View();
        }
        // GET: Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cust_reservations selectedCust_Res = db.cust_reservations.Find(id);
            ReservationVM model = new ReservationVM();
            model.Cust_Res_ID = selectedCust_Res.res_cust_id;
            model.BoatID = selectedCust_Res.reservation.boat.boatid;
            model.CustomerID = selectedCust_Res.customer.customerid;
            model.StartDate = selectedCust_Res.reservation.start_date;
            model.EndDate = selectedCust_Res.reservation.end_date;
            if (selectedCust_Res == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoatID = new SelectList(db.boats, "boatid", "Name");
            ViewBag.CustomerID = new SelectList(db.customers, "customerid", "Name");
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ReservationVM model)
        {
            if (ModelState.IsValid)
            {
                var cust_res = db.cust_reservations.Find(model.Cust_Res_ID);
                var reservetionaki = db.reservations.Find(cust_res.reservation.bookingid);
                reservetionaki.fkboatid = model.BoatID;
                reservetionaki.start_date = model.StartDate;
                reservetionaki.end_date = model.EndDate;
                db.Entry(reservetionaki).State = EntityState.Modified;
                db.SaveChanges();
                cust_res.fkcustomerid = model.CustomerID;
                db.Entry(cust_res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Reservations");
            }
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cust_reservations cust=db.cust_reservations.FirstOrDefault(u=>u.res_cust_id==id);
            db.cust_reservations.Remove(cust);
            return View(cust);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            cust_reservations selectedCustReservation = db.cust_reservations.Find(id);
            db.cust_reservations.Remove(selectedCustReservation);
            db.SaveChanges();
            return RedirectToAction("Index", "Reservations");
        }

    }
}