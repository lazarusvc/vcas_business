using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class customerController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: customer
        public ActionResult Index()
        {
            return View(db.VCAS_customer.Where(x => x.FK_Location == GlobalSession.Location).ToList());
        }

        // GET: customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_customer vCAS_customer = db.VCAS_customer.Find(id);
            if (vCAS_customer == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_customer);
        }

        // GET: customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,firstName,lastName,address,state,phone,email")] VCAS_customer vCAS_customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.VCAS_customer.Add(vCAS_customer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(vCAS_customer);
        //}

        // POST: customer/CreateModal
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,firstName,lastName,address,state,phone,email, FK_Location")] VCAS_customer vCAS_customer, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_customer.Add(new VCAS_customer
                {
                    Id = vCAS_customer.Id,
                    firstName = form["firstName"],
                    lastName = form["lastName"],
                    address = form["address"],
                    state = form["state"],
                    phone = form["phone"],
                    email = form["email"],
                    FK_Location = Convert.ToInt16(form["FK_Location"])
                });
                db.SaveChanges();
            }
            return View(vCAS_customer);
        }

        // GET: customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_customer vCAS_customer = db.VCAS_customer.Find(id);
            if (vCAS_customer == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_customer);
        }

        // POST: customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,firstName,lastName,address,state,phone,email, FK_Location")] VCAS_customer vCAS_customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_customer);
        }

        // GET: customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_customer vCAS_customer = db.VCAS_customer.Find(id);
            if (vCAS_customer == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_customer);
        }

        // POST: customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_customer vCAS_customer = db.VCAS_customer.Find(id);
            db.VCAS_customer.Remove(vCAS_customer);
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
