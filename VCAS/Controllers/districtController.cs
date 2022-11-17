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
    public class districtController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: district
        public ActionResult Index()
        {
            var vCAS_district = db.VCAS_district.Include(v => v.VCAS_council).Include(v => v.VCAS_users);
            return View(vCAS_district.ToList());
        }

        // GET: district/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_district vCAS_district = db.VCAS_district.Find(id);
            if (vCAS_district == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_district);
        }

        // GET: district/Create
        public ActionResult Create()
        {
            // USER SIGN UP LOGIC
            // ==============================================================
            int uID = 0;
            if (uID > 0)
            {
                uID = Convert.ToInt32(Session["userID"].ToString());
                ViewBag.FK_usersId = new SelectList(db.VCAS_users.Where(x => x.Id == uID), "Id", "fullName");
            }
            else
            {
                ViewBag.FK_usersId = new SelectList(db.VCAS_users, "Id", "fullName");
                ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name");
            }
            
            return View();
        }

        // POST: district/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,FK_location,FK_usersId")] VCAS_district vCAS_district)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_district.Add(vCAS_district);
                db.SaveChanges();

                Session["userLocation"] = vCAS_district.FK_location;
                return RedirectToAction("Create", "session");
            }

            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_district.FK_location);
            ViewBag.FK_usersId = new SelectList(db.VCAS_users, "Id", "fullName", vCAS_district.FK_usersId);
            return View(vCAS_district);
        }

        // GET: district/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_district vCAS_district = db.VCAS_district.Find(id);
            if (vCAS_district == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_district.FK_location);
            ViewBag.FK_usersId = new SelectList(db.VCAS_users, "Id", "fullName", vCAS_district.FK_usersId);
            return View(vCAS_district);
        }

        // POST: district/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,FK_location,FK_usersId")] VCAS_district vCAS_district)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_district).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_district.FK_location);
            ViewBag.FK_usersId = new SelectList(db.VCAS_users, "Id", "fullName", vCAS_district.FK_usersId);
            return View(vCAS_district);
        }

        // GET: district/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_district vCAS_district = db.VCAS_district.Find(id);
            if (vCAS_district == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_district);
        }

        // POST: district/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_district vCAS_district = db.VCAS_district.Find(id);
            db.VCAS_district.Remove(vCAS_district);
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
