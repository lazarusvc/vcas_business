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
    public class itemsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: items
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult Index()
        {
            return View(db.VCAS_REF_items.ToList());
        }

        // GET: items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_items vCAS_REF_items = db.VCAS_REF_items.Find(id);
            if (vCAS_REF_items == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_items);
        }

        // GET: items/Create
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc")] VCAS_REF_items vCAS_REF_items)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_items.Add(vCAS_REF_items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vCAS_REF_items);
        }

        // GET: items/Edit/5
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_items vCAS_REF_items = db.VCAS_REF_items.Find(id);
            if (vCAS_REF_items == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_items);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc")] VCAS_REF_items vCAS_REF_items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_items).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_REF_items);
        }

        // GET: items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_items vCAS_REF_items = db.VCAS_REF_items.Find(id);
            if (vCAS_REF_items == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_items);
        }

        // POST: items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_items vCAS_REF_items = db.VCAS_REF_items.Find(id);
            db.VCAS_REF_items.Remove(vCAS_REF_items);
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
