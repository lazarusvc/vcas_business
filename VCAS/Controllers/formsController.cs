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
    public class formsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: forms
        public ActionResult Index()
        {
            var vCAS_forms = db.VCAS_forms.Include(v => v.VCAS_council).Include(v => v.VCAS_REF_userRoles);
            return View(vCAS_forms.ToList());
        }

        // GET: forms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_forms vCAS_forms = db.VCAS_forms.Find(id);
            if (vCAS_forms == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_forms);
        }

        // GET: forms/Create
        public ActionResult Create()
        {
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name");
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name");
            return View();
        }

        // POST: forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc,form,dateModified,FK_location,FK_REF_userRolesId")] VCAS_forms vCAS_forms)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_forms.Add(vCAS_forms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_forms.FK_location);
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_forms.FK_REF_userRolesId);
            return View(vCAS_forms);
        }

        // GET: forms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_forms vCAS_forms = db.VCAS_forms.Find(id);
            if (vCAS_forms == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_forms.FK_location);
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_forms.FK_REF_userRolesId);
            return View(vCAS_forms);
        }

        // POST: forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc,form,dateModified,FK_location,FK_REF_userRolesId")] VCAS_forms vCAS_forms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_forms).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_forms.FK_location);
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_forms.FK_REF_userRolesId);
            return View(vCAS_forms);
        }

        // GET: forms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_forms vCAS_forms = db.VCAS_forms.Find(id);
            if (vCAS_forms == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_forms);
        }

        // POST: forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_forms vCAS_forms = db.VCAS_forms.Find(id);
            db.VCAS_forms.Remove(vCAS_forms);
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
