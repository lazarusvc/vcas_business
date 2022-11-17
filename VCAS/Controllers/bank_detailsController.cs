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
    public class bank_detailsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: bank_details
        public ActionResult Index()
        {
            return View(db.VCAS_REF_bank_details.ToList());
        }

        // GET: bank_details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_bank_details vCAS_REF_bank_details = db.VCAS_REF_bank_details.Find(id);
            if (vCAS_REF_bank_details == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_bank_details);
        }

        // GET: bank_details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: bank_details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name")] VCAS_REF_bank_details vCAS_REF_bank_details)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_bank_details.Add(vCAS_REF_bank_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vCAS_REF_bank_details);
        }

        // GET: bank_details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_bank_details vCAS_REF_bank_details = db.VCAS_REF_bank_details.Find(id);
            if (vCAS_REF_bank_details == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_bank_details);
        }

        // POST: bank_details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name")] VCAS_REF_bank_details vCAS_REF_bank_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_bank_details).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_REF_bank_details);
        }

        // GET: bank_details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_bank_details vCAS_REF_bank_details = db.VCAS_REF_bank_details.Find(id);
            if (vCAS_REF_bank_details == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_bank_details);
        }

        // POST: bank_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_bank_details vCAS_REF_bank_details = db.VCAS_REF_bank_details.Find(id);
            db.VCAS_REF_bank_details.Remove(vCAS_REF_bank_details);
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
