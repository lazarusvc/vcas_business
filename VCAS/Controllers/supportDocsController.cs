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
    public class supportDocsController : Controller
    {
        private ModelContainer db = new ModelContainer();
        private string fileName;

        // GET: supportDocs
        [Authorize]
        public ActionResult Index()
        {
            var roleID = db.VCAS_users.Where(x => x.userName == GlobalSession.User).Select(x => x.FK_userRolesId).FirstOrDefault();
            ViewBag.roleID = db.VCAS_REF_userRoles.Where(x => x.Id == roleID).Select(x => x.name).FirstOrDefault();

            var vCAS_supportDocs = db.VCAS_supportDocs.Include(v => v.VCAS_REF_userRoles).Where(v => v.FK_REF_userRolesId == roleID);     
            return PartialView("_supportDocsIndex", vCAS_supportDocs.ToList());
        }

        // GET: supportDocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_supportDocs vCAS_supportDocs = db.VCAS_supportDocs.Find(id);
            if (vCAS_supportDocs == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_supportDocs);
        }

        // GET: supportDocs/Create
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name");
            return View();
        }

        // POST: supportDocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc,media,media_type,FK_REF_userRolesId")] VCAS_supportDocs vCAS_supportDocs, HttpPostedFileBase media)
        {
            if (ModelState.IsValid)
            {
                // Verify that the user selected a file
                if (media != null && media.ContentLength > 0)
                {
                    // extract only the filename
                    fileName = Path.GetFileName(media.FileName);
                    // store the file inside ~/Content/Uploads folder
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                    media.SaveAs(path);
                }
                db.VCAS_supportDocs.Add(new VCAS_supportDocs { 
                    Id = vCAS_supportDocs.Id,
                    name = vCAS_supportDocs.name,
                    media = fileName,
                    media_type = vCAS_supportDocs.media_type,
                    FK_REF_userRolesId = vCAS_supportDocs.FK_REF_userRolesId
                });
                db.SaveChanges();
                return RedirectToAction("Help", "Home", null);
            }

            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_supportDocs.FK_REF_userRolesId);
            return View(vCAS_supportDocs);
        }

        // GET: supportDocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_supportDocs vCAS_supportDocs = db.VCAS_supportDocs.Find(id);
            if (vCAS_supportDocs == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_supportDocs.FK_REF_userRolesId);
            return View(vCAS_supportDocs);
        }

        // POST: supportDocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc,media,media_type,FK_REF_userRolesId")] VCAS_supportDocs vCAS_supportDocs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_supportDocs).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Help", "Home", null);
            }
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_supportDocs.FK_REF_userRolesId);
            return View(vCAS_supportDocs);
        }

        // GET: supportDocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_supportDocs vCAS_supportDocs = db.VCAS_supportDocs.Find(id);
            if (vCAS_supportDocs == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_supportDocs);
        }

        // POST: supportDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_supportDocs vCAS_supportDocs = db.VCAS_supportDocs.Find(id);
            db.VCAS_supportDocs.Remove(vCAS_supportDocs);
            db.SaveChanges();
            return RedirectToAction("Help", "Home", null);
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
