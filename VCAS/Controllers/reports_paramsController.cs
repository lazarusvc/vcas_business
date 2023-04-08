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
    public class reports_paramsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: reports_params
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Index()
        {
            var vCAS_REF_reports_params = db.VCAS_REF_reports_params.Include(v => v.VCAS_reports);
            return View(vCAS_REF_reports_params.ToList());
        }

        // GET: reports_params/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_reports_params vCAS_REF_reports_params = db.VCAS_REF_reports_params.Find(id);
            if (vCAS_REF_reports_params == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_reports_params);
        }

        // GET: reports_params/Create
        public ActionResult Create()
        {
            ViewBag.FK_REF_reportsId = new SelectList(db.VCAS_reports, "Id", "desc");
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name");
            return View();
        }

        // POST: reports_params/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,param_key, param_value,param_dataType,FK_REF_reportsId,FK_location")] VCAS_REF_reports_params vCAS_REF_reports_params)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_reports_params.Add(vCAS_REF_reports_params);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_REF_reportsId = new SelectList(db.VCAS_reports, "Id", "name", vCAS_REF_reports_params.FK_REF_reportsId);
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_REF_reports_params.FK_location);
            return View(vCAS_REF_reports_params);
        }

        // GET: reports_params/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_reports_params vCAS_REF_reports_params = db.VCAS_REF_reports_params.Find(id);
            if (vCAS_REF_reports_params == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_REF_reportsId = new SelectList(db.VCAS_reports, "Id", "name", vCAS_REF_reports_params.FK_REF_reportsId);
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_REF_reports_params.FK_location);
            return View(vCAS_REF_reports_params);
        }

        // POST: reports_params/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,param_key, param_value,param_dataType,FK_REF_reportsId,FK_location")] VCAS_REF_reports_params vCAS_REF_reports_params)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_reports_params).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_REF_reportsId = new SelectList(db.VCAS_reports, "Id", "name", vCAS_REF_reports_params.FK_REF_reportsId);
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_REF_reports_params.FK_location);
            return View(vCAS_REF_reports_params);
        }

        // GET: reports_params/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_reports_params vCAS_REF_reports_params = db.VCAS_REF_reports_params.Find(id);
            if (vCAS_REF_reports_params == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_reports_params);
        }

        // POST: reports_params/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_reports_params vCAS_REF_reports_params = db.VCAS_REF_reports_params.Find(id);
            db.VCAS_REF_reports_params.Remove(vCAS_REF_reports_params);
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
