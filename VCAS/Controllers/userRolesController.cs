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
    [CustomAuthorize(Roles = "admin")]
    public class userRolesController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: userRoles
        public ActionResult Index()
        {
            return View(db.VCAS_REF_userRoles.ToList());
        }

        // GET: userRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_userRoles vCAS_REF_userRoles = db.VCAS_REF_userRoles.Find(id);
            if (vCAS_REF_userRoles == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_userRoles);
        }

        // GET: userRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name")] VCAS_REF_userRoles vCAS_REF_userRoles)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_userRoles.Add(vCAS_REF_userRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vCAS_REF_userRoles);
        }

        // GET: userRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_userRoles vCAS_REF_userRoles = db.VCAS_REF_userRoles.Find(id);
            if (vCAS_REF_userRoles == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_userRoles);
        }

        // POST: userRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name")] VCAS_REF_userRoles vCAS_REF_userRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_userRoles).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_REF_userRoles);
        }

        // GET: userRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_userRoles vCAS_REF_userRoles = db.VCAS_REF_userRoles.Find(id);
            if (vCAS_REF_userRoles == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_userRoles);
        }

        // POST: userRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_userRoles vCAS_REF_userRoles = db.VCAS_REF_userRoles.Find(id);
            db.VCAS_REF_userRoles.Remove(vCAS_REF_userRoles);
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
