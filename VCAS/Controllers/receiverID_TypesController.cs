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
    public class receiverID_TypesController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: receiverID_Types
        public ActionResult Index()
        {
            return View(db.VCAS_REF_receiverID_Types.ToList());
        }

        // GET: receiverID_Types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_receiverID_Types vCAS_REF_receiverID_Types = db.VCAS_REF_receiverID_Types.Find(id);
            if (vCAS_REF_receiverID_Types == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_receiverID_Types);
        }

        // GET: receiverID_Types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: receiverID_Types/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name")] VCAS_REF_receiverID_Types vCAS_REF_receiverID_Types)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_receiverID_Types.Add(vCAS_REF_receiverID_Types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vCAS_REF_receiverID_Types);
        }

        // GET: receiverID_Types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_receiverID_Types vCAS_REF_receiverID_Types = db.VCAS_REF_receiverID_Types.Find(id);
            if (vCAS_REF_receiverID_Types == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_receiverID_Types);
        }

        // POST: receiverID_Types/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name")] VCAS_REF_receiverID_Types vCAS_REF_receiverID_Types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_receiverID_Types).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_REF_receiverID_Types);
        }

        // GET: receiverID_Types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_receiverID_Types vCAS_REF_receiverID_Types = db.VCAS_REF_receiverID_Types.Find(id);
            if (vCAS_REF_receiverID_Types == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_receiverID_Types);
        }

        // POST: receiverID_Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_receiverID_Types vCAS_REF_receiverID_Types = db.VCAS_REF_receiverID_Types.Find(id);
            db.VCAS_REF_receiverID_Types.Remove(vCAS_REF_receiverID_Types);
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
