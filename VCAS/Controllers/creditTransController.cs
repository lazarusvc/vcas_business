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
    public class creditTransController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // CREDIT GRAPHS
        // ******************************************
        [Authorize]
        public ActionResult CreditsTable(int? d)
        {
            var cvt = db.Database.SqlQuery<VCAS_capture_payments>
                (@"SELECT TOP(0) * FROM VCAS_capture_payments 
                WHERE datepart(month,datetime) = datepart(month,getdate())").ToList();
            if (d != null)
            {
                cvt = db.Database.SqlQuery<VCAS_capture_payments>
                (@"SELECT TOP(100) * FROM VCAS_capture_payments 
                 WHERE datepart(year,datetime) = YEAR(GETDATE()) 
                 AND datepart(month,datetime) = datepart(month,getdate())
                 AND voidCheck != 1   
                 AND FK_location = '" + d + "' ORDER BY datetime ASC").ToList();
            }
            ViewBag.data = d;
            return PartialView("_creditsTable", cvt);
        }
        public ActionResult CreditTransJson(string d)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Database.SqlQuery<CreditsJson>
                (@"WITH cte AS (SELECT *
                    FROM VCAS_capture_payments 
                    WHERE datepart(year,datetime) = YEAR(GETDATE()) 
                    AND datepart(month,datetime) = datepart(month,getdate())
                    AND voidCheck != 1   
                    AND invoice = 0
                    AND FK_location = '" + d + "' ) " +
                    "SELECT datetime, SUM(recieved_amount) as amount " +
                    "FROM cte GROUP BY datetime"), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreditsChart(string d)
        {
            ViewBag.data = d;
            return PartialView("_creditsChart");
        }
        public ActionResult CreditTransJson2(string d)
        {
            SqlParameter[] Parameters = { new SqlParameter("@loc", d) };
            var result = db.Database.SqlQuery<usp_SelectCreditChart_Result>("EXEC usp_SelectCreditChart @loc", Parameters).ToList();

            db.Configuration.ProxyCreationEnabled = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreditsChart2(string d)
        {
            ViewBag.data = d;
            return PartialView("_creditsChart2");
        }

        // GET: creditTrans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_creditTrans vCAS_creditTrans = db.VCAS_creditTrans1.Find(id);
            if (vCAS_creditTrans == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_creditTrans);
        }

        // GET: creditTrans/Create
        public ActionResult Create()
        {
            ViewBag.FK_capture_paymentsId = new SelectList(db.VCAS_capture_payments, "Id", "payer");
            return View();
        }

        // POST: creditTrans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FK_capture_paymentsId")] VCAS_creditTrans vCAS_creditTrans)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_creditTrans1.Add(vCAS_creditTrans);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_capture_paymentsId = new SelectList(db.VCAS_capture_payments, "Id", "payer", vCAS_creditTrans.FK_capture_paymentsId);
            return View(vCAS_creditTrans);
        }

        // GET: creditTrans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_creditTrans vCAS_creditTrans = db.VCAS_creditTrans1.Find(id);
            if (vCAS_creditTrans == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_capture_paymentsId = new SelectList(db.VCAS_capture_payments, "Id", "payer", vCAS_creditTrans.FK_capture_paymentsId);
            return View(vCAS_creditTrans);
        }

        // POST: creditTrans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FK_capture_paymentsId")] VCAS_creditTrans vCAS_creditTrans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_creditTrans).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_capture_paymentsId = new SelectList(db.VCAS_capture_payments, "Id", "payer", vCAS_creditTrans.FK_capture_paymentsId);
            return View(vCAS_creditTrans);
        }

        // GET: creditTrans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_creditTrans vCAS_creditTrans = db.VCAS_creditTrans1.Find(id);
            if (vCAS_creditTrans == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_creditTrans);
        }

        // POST: creditTrans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_creditTrans vCAS_creditTrans = db.VCAS_creditTrans1.Find(id);
            db.VCAS_creditTrans1.Remove(vCAS_creditTrans);
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
