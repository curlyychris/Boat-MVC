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
    public class CustomersController : Controller
    {
        public acsc384_dbEntities db = new acsc384_dbEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.customers.ToList();
            return View(customers);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(customer model)
        {
            if (ModelState.IsValid)
            {
                customer newCustomer = new customer();
                newCustomer.name = model.name;
                newCustomer.surname = model.surname;
                newCustomer.address = model.address;
                newCustomer.telephone = model.telephone;
                newCustomer.licence = model.licence;
                newCustomer.passcode = model.passcode;
                db.customers.Add(newCustomer);
                db.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer selectedCustomer = db.customers.Find(id);
            if (selectedCustomer == null)
            {
                return HttpNotFound();
            }
            return View(selectedCustomer);
        }
        [HttpPost]
        public ActionResult Edit(customer model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer selectedCustomer = db.customers.Find(id);
            if (selectedCustomer == null)
            {
                return HttpNotFound();
            }
            return View(selectedCustomer);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            customer selectedCustomer = db.customers.Find(id);
            db.customers.Remove(selectedCustomer);
            db.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        


    }
}