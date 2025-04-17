using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class linksController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: links
        public ActionResult Index()
        {
            var vCAS_links = db.VCAS_links.Include(v => v.VCAS_council).Include(v => v.VCAS_REF_userRoles).Where(x => x.FK_location == GlobalSession.Location);
            return View(vCAS_links.ToList());
        }

        // GET: links/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_links vCAS_links = db.VCAS_links.Find(id);
            if (vCAS_links == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_links);
        }

        // GET: links/Create
        public ActionResult Create()
        {
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/internal_url.json")))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Link> lk = serializer.Deserialize<List<Link>>(reader.ReadToEnd());
                ViewData["responseLinks"] = lk;
            }            

            ViewBag.FK_location = new SelectList(db.VCAS_council.Where(x => x.Id == GlobalSession.Location), "Id", "name");
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name");
            return View();
        }

        // POST: links/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,text,action,controller,FK_location,FK_REF_userRolesId")] VCAS_links vCAS_links)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_links.Add(vCAS_links);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_links.FK_location);
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_links.FK_REF_userRolesId);
            return View(vCAS_links);
        }

        // GET: links/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_links vCAS_links = db.VCAS_links.Find(id);
            if (vCAS_links == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_links.FK_location);
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_links.FK_REF_userRolesId);
            return View(vCAS_links);
        }

        // POST: links/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,text,action,controller,FK_location,FK_REF_userRolesId")] VCAS_links vCAS_links)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_links).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_links.FK_location);
            ViewBag.FK_REF_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_links.FK_REF_userRolesId);
            return View(vCAS_links);
        }

        // GET: links/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_links vCAS_links = db.VCAS_links.Find(id);
            if (vCAS_links == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_links);
        }

        // POST: links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_links vCAS_links = db.VCAS_links.Find(id);
            db.VCAS_links.Remove(vCAS_links);
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
