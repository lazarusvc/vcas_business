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
    public class items_locationController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: items_location
        public ActionResult Index()
        {
            var vCAS_REF_items_location = db.VCAS_REF_items_location.Include(v => v.VCAS_council).Include(v => v.VCAS_REF_items);
            return View(vCAS_REF_items_location.ToList());
        }

        // GET: items_location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_items_location vCAS_REF_items_location = db.VCAS_REF_items_location.Find(id);
            if (vCAS_REF_items_location == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_items_location);
        }

        // GET: items_location/Create
        public ActionResult Create()
        {
            ViewBag.FK_councilId = new SelectList(db.VCAS_council, "Id", "name");
            ViewBag.FK_REF_itemsId = new SelectList(db.VCAS_REF_items, "Id", "name");
            return View();
        }

        // POST: items_location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FK_councilId,FK_REF_itemsId")] VCAS_REF_items_location vCAS_REF_items_location)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_items_location.Add(vCAS_REF_items_location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_councilId = new SelectList(db.VCAS_council, "Id", "name", vCAS_REF_items_location.FK_councilId);
            ViewBag.FK_REF_itemsId = new SelectList(db.VCAS_REF_items, "Id", "name", vCAS_REF_items_location.FK_REF_itemsId);
            return View(vCAS_REF_items_location);
        }

        // GET: items_location/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_items_location vCAS_REF_items_location = db.VCAS_REF_items_location.Find(id);
            if (vCAS_REF_items_location == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_councilId = new SelectList(db.VCAS_council, "Id", "name", vCAS_REF_items_location.FK_councilId);
            ViewBag.FK_REF_itemsId = new SelectList(db.VCAS_REF_items, "Id", "name", vCAS_REF_items_location.FK_REF_itemsId);
            return View(vCAS_REF_items_location);
        }

        // POST: items_location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FK_councilId,FK_REF_itemsId")] VCAS_REF_items_location vCAS_REF_items_location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_items_location).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_councilId = new SelectList(db.VCAS_council, "Id", "name", vCAS_REF_items_location.FK_councilId);
            ViewBag.FK_REF_itemsId = new SelectList(db.VCAS_REF_items, "Id", "name", vCAS_REF_items_location.FK_REF_itemsId);
            return View(vCAS_REF_items_location);
        }

        // GET: items_location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_items_location vCAS_REF_items_location = db.VCAS_REF_items_location.Find(id);
            if (vCAS_REF_items_location == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_items_location);
        }

        // POST: items_location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_items_location vCAS_REF_items_location = db.VCAS_REF_items_location.Find(id);
            db.VCAS_REF_items_location.Remove(vCAS_REF_items_location);
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
