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
    [Authorize]
    public class reportsPageController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: reportsPage
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Index()
        {
            var vCAS_reports = db.VCAS_reports.Include(v => v.VCAS_REF_userRoles);
            return View(vCAS_reports.ToList());
        }

        // GET: reportsPage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_reports vCAS_reports = db.VCAS_reports.Find(id);
            if (vCAS_reports == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_reports);
        }

        // GET: reportsPage/Create
        public ActionResult Create()
        {
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name");
            return View();
        }

        // POST: reportsPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc,FK_REF_userRolesId,paramCheck")] VCAS_reports vCAS_reports)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_reports.Add(vCAS_reports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_reports.FK_REF_userRolesId);
            return View(vCAS_reports);
        }

        // GET: reportsPage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_reports vCAS_reports = db.VCAS_reports.Find(id);
            if (vCAS_reports == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_reports.FK_REF_userRolesId);
            return View(vCAS_reports);
        }

        // POST: reportsPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc,FK_REF_userRolesId,paramCheck")] VCAS_reports vCAS_reports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_reports).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_reports.FK_REF_userRolesId);
            return View(vCAS_reports);
        }

        // GET: reportsPage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_reports vCAS_reports = db.VCAS_reports.Find(id);
            if (vCAS_reports == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_reports);
        }

        // POST: reportsPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_reports vCAS_reports = db.VCAS_reports.Find(id);
            db.VCAS_reports.Remove(vCAS_reports);
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
