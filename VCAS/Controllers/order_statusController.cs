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
    public class order_statusController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: order_status
        public ActionResult Index()
        {
            return View(db.VCAS_REF_order_status.ToList());
        }

        // GET: order_status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_order_status vCAS_REF_order_status = db.VCAS_REF_order_status.Find(id);
            if (vCAS_REF_order_status == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_order_status);
        }

        // GET: order_status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: order_status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc")] VCAS_REF_order_status vCAS_REF_order_status)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_order_status.Add(vCAS_REF_order_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vCAS_REF_order_status);
        }

        // GET: order_status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_order_status vCAS_REF_order_status = db.VCAS_REF_order_status.Find(id);
            if (vCAS_REF_order_status == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_order_status);
        }

        // POST: order_status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc")] VCAS_REF_order_status vCAS_REF_order_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_order_status).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_REF_order_status);
        }

        // GET: order_status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_order_status vCAS_REF_order_status = db.VCAS_REF_order_status.Find(id);
            if (vCAS_REF_order_status == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_order_status);
        }

        // POST: order_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_order_status vCAS_REF_order_status = db.VCAS_REF_order_status.Find(id);
            db.VCAS_REF_order_status.Remove(vCAS_REF_order_status);
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
