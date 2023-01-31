using Lab8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab8.Controllers
{
    public class BoatController : Controller
    {
        public acsc384_dbEntities db = new acsc384_dbEntities();

        // GET: Boat
        public ActionResult Index()
        {
            var boats = db.boats.ToList();
            return View(boats);
        }

        public ActionResult Create()
        {
            ViewBag.fkcolourid = new SelectList(db.boat_colour, "colourid", "Name");
            ViewBag.fkcategoryid = new SelectList(db.categories, "categoryid", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(boat model)
        {
            if (ModelState.IsValid)
            {
                boat newBoat = new boat();
                newBoat.name = model.name;
                newBoat.daily_rate = model.daily_rate;
                newBoat.hour_rate = model.hour_rate;
                newBoat.fkcategoryid = model.fkcategoryid;
                newBoat.fkcolourid = model.fkcolourid;
                db.boats.Add(newBoat);
                db.SaveChanges();
                return RedirectToAction("Index", "Boat");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.fkcolourid = new SelectList(db.boat_colour, "colourid", "Name");
            ViewBag.fkcategoryid = new SelectList(db.categories, "categoryid", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            boat selectedBoat = db.boats.Find(id);
            if (selectedBoat == null)
            {
                return HttpNotFound();
            }
            return View(selectedBoat);
        }

        [HttpPost]
        public ActionResult Edit(boat model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Boat");
            }
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            boat selectedBoat = db.boats.Find(id);
            if (selectedBoat == null)
            {
                return HttpNotFound();
            }
            return View(selectedBoat);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            boat selectedBoat = db.boats.Find(id);
            db.boats.Remove(selectedBoat);
            db.SaveChanges();
            return RedirectToAction("Index", "Boat");
        }

    }

}