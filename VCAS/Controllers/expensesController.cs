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
    public class expensesController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: expenses
        public ActionResult Index()
        {
            return View(db.VCAS_expenses.ToList());
        }

        // GET: expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_expenses vCAS_expenses = db.VCAS_expenses.Find(id);
            if (vCAS_expenses == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_expenses);
        }

        // GET: expenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc")] VCAS_expenses vCAS_expenses)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_expenses.Add(vCAS_expenses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vCAS_expenses);
        }

        // GET: expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_expenses vCAS_expenses = db.VCAS_expenses.Find(id);
            if (vCAS_expenses == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_expenses);
        }

        // POST: expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc")] VCAS_expenses vCAS_expenses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_expenses).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_expenses);
        }

        // GET: expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_expenses vCAS_expenses = db.VCAS_expenses.Find(id);
            if (vCAS_expenses == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_expenses);
        }

        // POST: expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_expenses vCAS_expenses = db.VCAS_expenses.Find(id);
            db.VCAS_expenses.Remove(vCAS_expenses);
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
