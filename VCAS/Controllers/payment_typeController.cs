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
    [CustomAuthorize(Roles = "admin")]
    public class payment_typeController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: payment_type
        public ActionResult Index()
        {
            return View(db.VCAS_REF_payment_type.ToList());
        }

        // GET: payment_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_payment_type vCAS_REF_payment_type = db.VCAS_REF_payment_type.Find(id);
            if (vCAS_REF_payment_type == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_payment_type);
        }

        // GET: payment_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: payment_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name")] VCAS_REF_payment_type vCAS_REF_payment_type)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_payment_type.Add(vCAS_REF_payment_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vCAS_REF_payment_type);
        }

        // GET: payment_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_payment_type vCAS_REF_payment_type = db.VCAS_REF_payment_type.Find(id);
            if (vCAS_REF_payment_type == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_payment_type);
        }

        // POST: payment_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name")] VCAS_REF_payment_type vCAS_REF_payment_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_payment_type).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_REF_payment_type);
        }

        // GET: payment_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_payment_type vCAS_REF_payment_type = db.VCAS_REF_payment_type.Find(id);
            if (vCAS_REF_payment_type == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_payment_type);
        }

        // POST: payment_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_payment_type vCAS_REF_payment_type = db.VCAS_REF_payment_type.Find(id);
            db.VCAS_REF_payment_type.Remove(vCAS_REF_payment_type);
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
