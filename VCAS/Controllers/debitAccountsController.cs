using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class debitAccountsController : Controller
    {
        private ModelContainer db = new ModelContainer();
        private string fileName;

        // GET: debitAccounts
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Index()
        {
            var vCAS_debitAccounts = db.VCAS_debitAccounts.Include(v => v.VCAS_REF_payment_type);
            return View(vCAS_debitAccounts.ToList());
        }

        // GET: debitAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_debitAccounts vCAS_debitAccounts = db.VCAS_debitAccounts.Find(id);
            if (vCAS_debitAccounts == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_debitAccounts);
        }

        // GET: debitAccounts/Create
        public ActionResult Create()
        {
            ViewBag.FK_payment_Type = new SelectList(db.VCAS_REF_payment_type, "Id", "name");
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View();
        }

        // POST: debitAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,acctNum,amount,FK_payment_Type,remittance,payee,datetime,attach_statement,FK_location")] VCAS_debitAccounts vCAS_debitAccounts, HttpPostedFileBase attach_statement)
        {
            if (ModelState.IsValid)
            {
                // Verify that the user selected a file
                if (attach_statement != null && attach_statement.ContentLength > 0)
                {
                    // extract only the filename
                    fileName = Path.GetFileName(attach_statement.FileName);
                    // store the file inside ~/Content/Uploads folder
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                    attach_statement.SaveAs(path);
                }

                db.VCAS_debitAccounts.Add(new VCAS_debitAccounts
                {
                    Id = vCAS_debitAccounts.Id,
                    name = vCAS_debitAccounts.name,
                    acctNum = vCAS_debitAccounts.acctNum,
                    amount = vCAS_debitAccounts.amount,
                    FK_payment_Type = vCAS_debitAccounts.FK_payment_Type,
                    remittance = vCAS_debitAccounts.remittance,
                    payee = vCAS_debitAccounts.payee,
                    datetime = vCAS_debitAccounts.datetime,
                    attach_statement = fileName,
                    FK_location = vCAS_debitAccounts.FK_location
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_payment_Type = new SelectList(db.VCAS_REF_payment_type, "Id", "name", vCAS_debitAccounts.FK_payment_Type);
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View(vCAS_debitAccounts);
        }

        // GET: debitAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_debitAccounts vCAS_debitAccounts = db.VCAS_debitAccounts.Find(id);
            if (vCAS_debitAccounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_payment_Type = new SelectList(db.VCAS_REF_payment_type, "Id", "name", vCAS_debitAccounts.FK_payment_Type);
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_debitAccounts.FK_location);
            return View(vCAS_debitAccounts);
        }

        // POST: debitAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,acctNum,amount,FK_payment_Type,remittance,payee,datetime,attach_statement,FK_location")] VCAS_debitAccounts vCAS_debitAccounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_debitAccounts).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_payment_Type = new SelectList(db.VCAS_REF_payment_type, "Id", "name", vCAS_debitAccounts.FK_payment_Type);
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_debitAccounts.FK_location);
            return View(vCAS_debitAccounts);
        }

        // GET: debitAccounts/EditUpload/5
        public ActionResult EditUpload(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_debitAccounts vCAS_debitAccounts = db.VCAS_debitAccounts.Find(id);
            if (vCAS_debitAccounts == null)
            {
                return HttpNotFound();
            }
            Session["daID"] = vCAS_debitAccounts.Id;
            return View(vCAS_debitAccounts);
        }

        // POST: debitAccounts/EditUpload/5
        [HttpPost]
        public ActionResult EditUpload(HttpPostedFileBase attach_statement)
        {
            // Verify that the user selected a file
            if (attach_statement != null && attach_statement.ContentLength > 0)
            {
                // extract only the filename
                fileName = Path.GetFileName(attach_statement.FileName);
                // store the file inside ~/Content/Uploads folder
                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                attach_statement.SaveAs(path);

                // update database
                int Id = Convert.ToInt32(Session["daID"]);
                db.Database.ExecuteSqlCommand("UPDATE dbo.VCAS_debitAccounts SET attach_statement = '" + fileName + "' WHERE Id = '" + Id + "';");
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: debitAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_debitAccounts vCAS_debitAccounts = db.VCAS_debitAccounts.Find(id);
            if (vCAS_debitAccounts == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_debitAccounts);
        }

        // POST: debitAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_debitAccounts vCAS_debitAccounts = db.VCAS_debitAccounts.Find(id);
            db.VCAS_debitAccounts.Remove(vCAS_debitAccounts);
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
