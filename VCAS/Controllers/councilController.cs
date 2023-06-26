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
    public class councilController : Controller
    {
        private ModelContainer db = new ModelContainer();
        private string fileName;

        // GET: council
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.VCAS_council.ToList());
        }

        // GET: council/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_council vCAS_council = db.VCAS_council.Find(id);
            if (vCAS_council == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_council);
        }

        // GET: council/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: council/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,name,location,vendor_Id,app_name,app_cover,receipt_logo,receipt_header,receipt_footer,twilio_SID,twilio_TOKEN,twilio_NUMBER,twilio_XML")] VCAS_council vCAS_council, HttpPostedFileBase receipt_logo, HttpPostedFileBase app_cover)
        {

            if (receipt_logo != null && receipt_logo.ContentLength > 0) // Attach Receipt logo
            {
                fileName = Path.GetFileNameWithoutExtension(receipt_logo.FileName);
                string extension = Path.GetExtension(receipt_logo.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_council.receipt_logo = "/Content/Uploads/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                receipt_logo.SaveAs(fileName);
            }
            else if (app_cover != null && app_cover.ContentLength > 0) // Attach App cover image
            {
                fileName = Path.GetFileNameWithoutExtension(app_cover.FileName);
                string extension = Path.GetExtension(app_cover.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_council.app_cover = "/Content/Uploads/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                app_cover.SaveAs(fileName);
            }
            else
            {
                fileName = null;
            }

            if (ModelState.IsValid)
            {
                db.VCAS_council.Add(new VCAS_council
                {
                    Id = vCAS_council.Id,
                    name = vCAS_council.name,
                    location = vCAS_council.location,
                    vendor_Id = vCAS_council.vendor_Id,
                    app_name = vCAS_council.app_name,
                    app_cover = vCAS_council.app_cover,
                    receipt_logo = vCAS_council.receipt_logo,
                    receipt_header = vCAS_council.receipt_header,
                    receipt_footer = vCAS_council.receipt_footer
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            return View(vCAS_council);
        }

        // GET: council/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_council vCAS_council = db.VCAS_council.Find(id);
            if (vCAS_council == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_council);
        }

        // POST: council/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,location,vendor_Id,app_name,app_cover,receipt_logo,receipt_header,receipt_footer,twilio_SID,twilio_TOKEN,twilio_NUMBER,twilio_XML")] VCAS_council vCAS_council, HttpPostedFileBase receipt_logo, HttpPostedFileBase app_cover)
        {

            if (receipt_logo != null && receipt_logo.ContentLength > 0) // Attach Receipt logo
            {
                fileName = Path.GetFileNameWithoutExtension(receipt_logo.FileName);
                string extension = Path.GetExtension(receipt_logo.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_council.receipt_logo = "/Content/Uploads/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                receipt_logo.SaveAs(fileName);
            }
            else if (app_cover != null && app_cover.ContentLength > 0) // Attach App cover image
            {
                fileName = Path.GetFileNameWithoutExtension(app_cover.FileName);
                string extension = Path.GetExtension(app_cover.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_council.app_cover = "/Content/Uploads/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                app_cover.SaveAs(fileName);
            }
            else
            {
                fileName = vCAS_council.receipt_logo;
            }

            if (ModelState.IsValid)
            {
                db.Entry(vCAS_council).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vCAS_council);
        }

        // GET: council/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_council vCAS_council = db.VCAS_council.Find(id);
            if (vCAS_council == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_council);
        }

        // POST: council/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_council vCAS_council = db.VCAS_council.Find(id);
            db.VCAS_council.Remove(vCAS_council);
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