//projectName.Models is used because Context Name (db18249Entities) could not be found
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
    public class CategoriesController : Controller
    {
        //new object in order to connect with the database.
        public acsc384_dbEntities db = new acsc384_dbEntities();


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Index()
        {
            var categories = db.categories.ToList();
            return View(categories);
        }
        [HttpPost]
        public ActionResult Create(category model)
        {
            if (ModelState.IsValid)
            {
                category newCategory = new category();
                newCategory.name = model.name;
                db.categories.Add(newCategory);
                db.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category selectedCategory = db.categories.Find(id);
            if (selectedCategory == null)
            {
                return HttpNotFound();
            }
            return View(selectedCategory);
        }

        [HttpPost]
        public ActionResult Edit(category model)
        {
            if (ModelState.IsValid)
            {
                //click on the error, press ALT+Enter and click what it suggests (adds library)
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            return View(model);
        }
        //Edit->Advanced->Format Document
        //to fix the format when you copy paste stuff
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category selectedCategory = db.categories.Find(id);
            if (selectedCategory == null)
            {
                return HttpNotFound();
            }
            return View(selectedCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            category selectedCategory = db.categories.Find(id);
            db.categories.Remove(selectedCategory);
            db.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }




    }
}