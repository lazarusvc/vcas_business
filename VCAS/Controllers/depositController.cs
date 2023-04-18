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
    public class depositController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: deposit
        public ActionResult PartialIndex(int? loc)
        {
            // EXEC Stored Procedure - usp_SelectDeposit
            // ********************************************
            SqlParameter[] Parameters = { new SqlParameter("@p_loc", loc) };
            var results = db.Database.SqlQuery<usp_SelectDeposit_Result>("EXEC usp_SelectDeposit @p_loc", Parameters);

            return PartialView("_depositList", results.ToList());
        }
        public ActionResult Index()
        {
            var vCAS_deposit = db.VCAS_deposit.Where(v => v.FK_councilId == GlobalSession.Location);
            return View(vCAS_deposit.ToList());
        }

        public ActionResult Slip(int? id)
        {
            var vCAS_deposit = db.VCAS_deposit.Where(v => v.FK_councilId == GlobalSession.Location && v.Id == id);
            ViewBag.total = vCAS_deposit.Select(v => v.cash_amount).FirstOrDefault();
            ViewBag.dateEnding = vCAS_deposit.Select(v => v.endind_date).FirstOrDefault();
            ViewBag.recLogo = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_logo).FirstOrDefault();
            ViewBag.recHeader = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_header).FirstOrDefault();
            return View();
        }

        // GET: deposit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_deposit vCAS_deposit = db.VCAS_deposit.Find(id);
            if (vCAS_deposit == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_deposit);
        }

        // GET: deposit/Create
        public ActionResult Create(int? loc)
        {
            if (loc == null)
            {
                ViewBag.FK_councilId = new SelectList(db.VCAS_council, "Id", "name");
            }  else
            {
                ViewBag.FK_councilId = new SelectList(db.VCAS_council.Where(x => x.Id == loc), "Id", "name");
            }

            // EXEC Stored Procedure - usp_InsertDeposit
            // ********************************************
            SqlParameter[] Parameters =
            {
                new SqlParameter("@p_location", loc)
            };
            var results = db.Database.SqlQuery<usp_InsertDeposit_Result>("EXEC usp_InsertDeposit @p_location", Parameters).ToList();
            ViewBag.endDate = results.Select(x => x.date_ending).FirstOrDefault();
            ViewBag.cashAmt = results.Select(x => x.cash_amount).FirstOrDefault();
            ViewBag.checkAmt = results.Select(x => x.check_amount).FirstOrDefault();
            ViewBag.visaCAmt = results.Select(x => x.visa_credit_amount).FirstOrDefault();
            ViewBag.visaDAmt = results.Select(x => x.visa_debit_amount).FirstOrDefault();
            ViewBag.btAmt = results.Select(x => x.bt_amount).FirstOrDefault();
            ViewBag.locParam = loc;


            ViewBag.FK_debitAccount = new SelectList(db.VCAS_debitAccounts.Where(x => x.type == "Income" && x.FK_location == GlobalSession.Location), "Id", "name");

            return View();
        }

        // POST: deposit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,endind_date,cash_amount,check_amount,visa_debit_amount,visa_credit_amount,bt_amount,FK_councilId,FK_debitAccount")] VCAS_deposit vCAS_deposit)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_deposit.Add(vCAS_deposit);
                db.SaveChanges();

                // Update (Income) Account with total deposited
                // ********************************************
                Double? curAmt = db.VCAS_debitAccounts.Where(x => x.Id == vCAS_deposit.FK_debitAccount).Select(x => x.amount).FirstOrDefault();
                Double totDepositAmt =
                       Convert.ToDouble(vCAS_deposit.cash_amount) +
                       Convert.ToDouble(vCAS_deposit.check_amount) +
                       Convert.ToDouble(vCAS_deposit.visa_credit_amount) +
                       Convert.ToDouble(vCAS_deposit.visa_debit_amount) +
                       Convert.ToDouble(vCAS_deposit.bt_amount) +
                       Convert.ToDouble(curAmt);
                
                SqlParameter[] Parameters01 = { // EXEC Stored Procedure - usp_SelectDeposit
                    new SqlParameter("@p_amt", totDepositAmt),
                    new SqlParameter("@p_id", vCAS_deposit.FK_debitAccount)
                };
                db.Database.ExecuteSqlCommand("EXEC usp_UpdateAcctAmt @p_amt, @p_id", Parameters01);

                // Clear Undeposited Funds 
                // ***********************            
                SqlParameter[] Parameters02 = { // EXEC Stored Procedure - usp_UpdateUndepositedFundsStatus
                    new SqlParameter("@p_loc", vCAS_deposit.FK_councilId ),
                    new SqlParameter("@p_dID", vCAS_deposit.Id )
                }; 
                db.Database.ExecuteSqlCommand("EXEC usp_UpdateUndepositedFundsStatus @p_loc, @p_dID", Parameters02);

                return RedirectToAction("Slip", new { id = vCAS_deposit.Id });
            }

            ViewBag.FK_councilId = new SelectList(db.VCAS_council, "Id", "name", vCAS_deposit.FK_councilId);
            return View(vCAS_deposit);
        }

        // GET: deposit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_deposit vCAS_deposit = db.VCAS_deposit.Find(id);
            if (vCAS_deposit == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_councilId = new SelectList(db.VCAS_council, "Id", "name", vCAS_deposit.FK_councilId);
            return View(vCAS_deposit);
        }

        // POST: deposit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,endind_date,cash_amount,check_amount,visa_debit_amount,visa_credit_amount,bt_amount,FK_councilId,FK_debitAccount")] VCAS_deposit vCAS_deposit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_deposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_councilId = new SelectList(db.VCAS_council, "Id", "name", vCAS_deposit.FK_councilId);
            return View(vCAS_deposit);
        }

        // GET: deposit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_deposit vCAS_deposit = db.VCAS_deposit.Find(id);
            if (vCAS_deposit == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_deposit);
        }

        // POST: deposit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_deposit vCAS_deposit = db.VCAS_deposit.Find(id);
            db.VCAS_deposit.Remove(vCAS_deposit);
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
