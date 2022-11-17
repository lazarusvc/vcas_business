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
    public class undepositedFundsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET : JSON API
        public ActionResult IndexJSON(string loc)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Database.SqlQuery<VCAS_undepositedFunds>
                (@"SELECT *
                FROM VCAS_DB.dbo.VCAS_undepositedFunds
                WHERE FK_location = '"+loc+"'"), JsonRequestBehavior.AllowGet);
        }

        // GET: undepositedFunds
        public ActionResult Index()
        {
            var vCAS_undepositedFunds = db.VCAS_undepositedFunds.Include(v => v.VCAS_council);
            return View(vCAS_undepositedFunds.ToList());
        }

        // GET: undepositedFunds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_undepositedFunds vCAS_undepositedFunds = db.VCAS_undepositedFunds.Find(id);
            if (vCAS_undepositedFunds == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_undepositedFunds);
        }

        // GET: undepositedFunds/Create
        public ActionResult Create()
        {
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name");
            return View();
        }

        // POST: undepositedFunds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,amount,datetime,FK_location")] VCAS_undepositedFunds vCAS_undepositedFunds)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_undepositedFunds.Add(vCAS_undepositedFunds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_undepositedFunds.FK_location);
            return View(vCAS_undepositedFunds);
        }

        // GET: undepositedFunds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_undepositedFunds vCAS_undepositedFunds = db.VCAS_undepositedFunds.Find(id);
            if (vCAS_undepositedFunds == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_undepositedFunds.FK_location);
            return View(vCAS_undepositedFunds);
        }

        // POST: undepositedFunds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,amount,datetime,FK_location")] VCAS_undepositedFunds vCAS_undepositedFunds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_undepositedFunds).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_undepositedFunds.FK_location);
            return View(vCAS_undepositedFunds);
        }

        // GET: undepositedFunds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_undepositedFunds vCAS_undepositedFunds = db.VCAS_undepositedFunds.Find(id);
            if (vCAS_undepositedFunds == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_undepositedFunds);
        }

        // POST: undepositedFunds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_undepositedFunds vCAS_undepositedFunds = db.VCAS_undepositedFunds.Find(id);
            db.VCAS_undepositedFunds.Remove(vCAS_undepositedFunds);
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
