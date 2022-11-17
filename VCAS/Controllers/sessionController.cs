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
    public class sessionController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: session
        public ActionResult Index()
        {
            return View(db.VCAS_session.ToList());
        }

        // GET: session/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_session vCAS_session = db.VCAS_session.Find(id);
            if (vCAS_session == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_session);
        }

        // GET: session/Create
        public ActionResult Create()
        {
            // USER SIGN UP LOGIC
            // ==============================================================
            string uName = "";
            int uRole = 0;
            int uLoc = 0;
            if (uName != "" && uRole > 0 && uLoc > 0)
            {
                uName = Convert.ToString(Session["userName"].ToString());
                uRole = Convert.ToInt32(Session["userRoleID"].ToString());
                uLoc = Convert.ToInt32(Session["userLocation"].ToString());
                ViewBag.username = uName;
                ViewBag.role = new SelectList(db.VCAS_REF_userRoles.Where(x => x.Id == uRole), "Id", "name");
                ViewBag.location = new SelectList(db.VCAS_council.Where(x => x.Id == uLoc), "Id", "name");
            }
            else
            {
                // Regular Create code sequence
                ViewBag.role = new SelectList(db.VCAS_REF_userRoles, "Id", "name");
                ViewBag.location = new SelectList(db.VCAS_council, "Id", "name");
            }
            return View();
        }

        // POST: session/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,username,role,location")] VCAS_session vCAS_session)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_session.Add(vCAS_session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.role = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_session.role);
            ViewBag.location = new SelectList(db.VCAS_council, "Id", "name", vCAS_session.location);
            return View(vCAS_session);
        }

        // GET: session/Edit/5
        public ActionResult Edit(int? id)
        {
            // USER SIGN UP LOGIC
            // ==============================================================
            string uName = "";
            int uRole = 0;
            int uLoc = 0;
            if (uName != "" && uRole > 0 && uLoc > 0)
            {
                uName = Convert.ToString(Session["userName"].ToString());
                uRole = Convert.ToInt32(Session["userRoleID"].ToString());
                uLoc = Convert.ToInt32(Session["userLocation"].ToString());
                ViewBag.username = uName;
                ViewBag.role = new SelectList(db.VCAS_REF_userRoles.Where(x => x.Id == uRole), "Id", "name");
                ViewBag.location = new SelectList(db.VCAS_council.Where(x => x.Id == uLoc), "Id", "name");
            }
            else
            {
                // Regular Create code sequence
                ViewBag.role = new SelectList(db.VCAS_REF_userRoles, "Id", "name");
                ViewBag.location = new SelectList(db.VCAS_council, "Id", "name");
            }
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_session vCAS_session = db.VCAS_session.Find(id);
            if (vCAS_session == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_session);
        }

        // POST: session/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,username,role,location")] VCAS_session vCAS_session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_session).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.role = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_session.role);
            ViewBag.location = new SelectList(db.VCAS_council, "Id", "name", vCAS_session.location);
            return View(vCAS_session);
        }

        // GET: session/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_session vCAS_session = db.VCAS_session.Find(id);
            if (vCAS_session == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_session);
        }

        // POST: session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_session vCAS_session = db.VCAS_session.Find(id);
            db.VCAS_session.Remove(vCAS_session);
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
