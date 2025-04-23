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
    public class processes_paramsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: processes_params
        public ActionResult Index()
        {
            var vCAS_REF_processes = db.VCAS_REF_processes.Include(v => v.VCAS_processes);
            return View(vCAS_REF_processes.ToList());
        }
        public ActionResult IndexPartial(string n)
        {
            var vCAS_REF_processes = db.VCAS_REF_processes.Include(v => v.VCAS_processes).Where(x => x.VCAS_processes.sp_name == n);
            return PartialView("_processesParamsIndex", vCAS_REF_processes.ToList());
        }

        // GET: processes_params/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_processes vCAS_REF_processes = db.VCAS_REF_processes.Find(id);
            if (vCAS_REF_processes == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_processes);
        }

        // GET: processes_params/Create
        public ActionResult Create()
        {
            ViewBag.FK_processesId = new SelectList(db.VCAS_processes, "Id", "sp_name");
            return View();
        }

        // POST: processes_params/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,param_key,param_value,param_dataType,FK_processesId")] VCAS_REF_processes vCAS_REF_processes)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_REF_processes.Add(vCAS_REF_processes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_processesId = new SelectList(db.VCAS_processes, "Id", "sp_name", vCAS_REF_processes.FK_processesId);
            return View(vCAS_REF_processes);
        }

        // GET: processes_params/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_processes vCAS_REF_processes = db.VCAS_REF_processes.Find(id);
            if (vCAS_REF_processes == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_processesId = new SelectList(db.VCAS_processes, "Id", "sp_name", vCAS_REF_processes.FK_processesId);
            return View(vCAS_REF_processes);
        }

        // POST: processes_params/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,param_key,param_value,param_dataType,FK_processesId")] VCAS_REF_processes vCAS_REF_processes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_processes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_processesId = new SelectList(db.VCAS_processes, "Id", "sp_name", vCAS_REF_processes.FK_processesId);
            return View(vCAS_REF_processes);
        }

        // GET: processes_params/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_REF_processes vCAS_REF_processes = db.VCAS_REF_processes.Find(id);
            if (vCAS_REF_processes == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_processes);
        }

        // POST: processes_params/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_REF_processes vCAS_REF_processes = db.VCAS_REF_processes.Find(id);
            db.VCAS_REF_processes.Remove(vCAS_REF_processes);
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
