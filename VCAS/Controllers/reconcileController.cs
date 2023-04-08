using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class reconcileController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: reconcile
        public ActionResult PartialIndex(int? id)
        {
            // EXEC Stored Procedure - usp_SelectRecon
            //////////////////////////////////////////////////
            SqlParameter[] Parameters = { new SqlParameter("@p_dAccount", id) };
            var results = db.Database.SqlQuery<usp_SelectRecon_Result>("EXEC usp_SelectRecon @p_dAccount", Parameters);
            //////////////////////////////////////////////////
            return PartialView("_reconList", results.ToList());
        }
        public ActionResult Index()
        {
            var vCAS_reconcile = db.VCAS_reconcile.Include(v => v.VCAS_debitAccounts);
            return View(vCAS_reconcile.ToList());
        }

        // GET: reconcile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_reconcile vCAS_reconcile = db.VCAS_reconcile.Find(id);
            if (vCAS_reconcile == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_reconcile);
        }

        // GET: reconcile/Create
        public ActionResult JsonAccounts(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Database.SqlQuery<VCAS_debitAccounts>
                (@"SELECT * FROM VCAS_debitAccounts
                 WHERE FK_location = '" + GlobalSession.Location + "' AND Id = '" + id + "' "), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts.Where(x => x.FK_location == GlobalSession.Location), "Id", "name");
            return View();
        }

        // POST: reconcile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FK_debitAccountsId,bank_ending_amt,bank_ending_date,service_charge_amt,service_charge_date,interest_earned_amt,interest_earned_date,difference,reconcile")] VCAS_reconcile vCAS_reconcile)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_reconcile.Add(vCAS_reconcile);
                db.SaveChanges();
                return RedirectToAction("Do", new { id = vCAS_reconcile.Id });
            }

            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_reconcile.FK_debitAccountsId);
            return View(vCAS_reconcile);
        }

        // GET: reconcile/Edit/5
        public ActionResult Do(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_reconcile vCAS_reconcile = db.VCAS_reconcile.Find(id);
            if (vCAS_reconcile == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_reconcile.FK_debitAccountsId);
            return View(vCAS_reconcile);
        }

        // POST: reconcile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Do([Bind(Include = "Id,FK_debitAccountsId,bank_ending_amt,bank_ending_date,service_charge_amt,service_charge_date,interest_earned_amt,interest_earned_date,difference,reconcile")] VCAS_reconcile vCAS_reconcile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_reconcile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_reconcile.FK_debitAccountsId);
            return View(vCAS_reconcile);
        }

        // GET: reconcile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_reconcile vCAS_reconcile = db.VCAS_reconcile.Find(id);
            if (vCAS_reconcile == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_reconcile.FK_debitAccountsId);
            return View(vCAS_reconcile);
        }

        // POST: reconcile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FK_debitAccountsId,bank_ending_amt,bank_ending_date,service_charge_amt,service_charge_date,interest_earned_amt,interest_earned_date,difference,reconcile")] VCAS_reconcile vCAS_reconcile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_reconcile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_reconcile.FK_debitAccountsId);
            return View(vCAS_reconcile);
        }

        // GET: reconcile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_reconcile vCAS_reconcile = db.VCAS_reconcile.Find(id);
            if (vCAS_reconcile == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_reconcile);
        }

        // POST: reconcile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_reconcile vCAS_reconcile = db.VCAS_reconcile.Find(id);
            db.VCAS_reconcile.Remove(vCAS_reconcile);
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
