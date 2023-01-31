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
    public class ColorsController : Controller
    {
        public acsc384_dbEntities db = new acsc384_dbEntities();
        // GET: Colors
        public ActionResult Index()
        {
            var colors = db.boat_colour.ToList();
            return View(colors);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(category model)
        {
            if (ModelState.IsValid)
            {
                boat_colour newColors = new boat_colour();
                newColors.name = model.name;
                db.boat_colour.Add(newColors);
                db.SaveChanges();
                return RedirectToAction("Index", "Colors");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            boat_colour selectedColor = db.boat_colour.Find(id);
            if (selectedColor == null)
            {
                return HttpNotFound();
            }
            return View(selectedColor);
        }
        [HttpPost]
        public ActionResult Edit(boat_colour model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Colors");
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            boat_colour selectedColor = db.boat_colour.Find(id);
            if (selectedColor == null)
            {
                return HttpNotFound();
            }
            return View(selectedColor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            boat_colour selectedColor = db.boat_colour.Find(id);
            db.boat_colour.Remove(selectedColor);
            db.SaveChanges();
            return RedirectToAction("Index", "Colors");
        }


    }
}