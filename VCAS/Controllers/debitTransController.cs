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
    public class debitTransController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: debitTrans
        public ActionResult Index0()
        {
            var vCAS_debitTrans1 = db.VCAS_debitTrans1.Include(v => v.VCAS_debitAccounts);
            return View(vCAS_debitTrans1.ToList());
        }
        public ActionResult DebitsChart(string d)
        {
            ViewBag.data = d;
            return PartialView("_debitsChart");
        }
        public ActionResult DebitAccountJson(string d)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Database.SqlQuery<VCAS_debitAccounts>("SELECT * FROM VCAS_debitAccounts WHERE FK_location = '"+ d + "' ORDER BY Id DESC"), JsonRequestBehavior.AllowGet);
        }

        // GET: debitTrans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_debitTrans vCAS_debitTrans = db.VCAS_debitTrans1.Find(id);
            if (vCAS_debitTrans == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_debitTrans);
        }

        // GET: debitTrans/Create
        public ActionResult Create()
        {
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name");
            return View();
        }

        // POST: debitTrans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FK_debitAccountsId")] VCAS_debitTrans vCAS_debitTrans)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_debitTrans1.Add(vCAS_debitTrans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_debitTrans.FK_debitAccountsId);
            return View(vCAS_debitTrans);
        }

        // GET: debitTrans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_debitTrans vCAS_debitTrans = db.VCAS_debitTrans1.Find(id);
            if (vCAS_debitTrans == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_debitTrans.FK_debitAccountsId);
            return View(vCAS_debitTrans);
        }

        // POST: debitTrans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FK_debitAccountsId")] VCAS_debitTrans vCAS_debitTrans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_debitTrans).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_debitTrans.FK_debitAccountsId);
            return View(vCAS_debitTrans);
        }

        // GET: debitTrans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_debitTrans vCAS_debitTrans = db.VCAS_debitTrans1.Find(id);
            if (vCAS_debitTrans == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_debitTrans);
        }

        // POST: debitTrans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_debitTrans vCAS_debitTrans = db.VCAS_debitTrans1.Find(id);
            db.VCAS_debitTrans1.Remove(vCAS_debitTrans);
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
