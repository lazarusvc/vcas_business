using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
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

        // GET: Complete
        public ActionResult Complete(int? id)
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
            ViewBag.id = id;
            // ************* FormData 
            var vCAS_REF_forms = db.VCAS_REF_forms.Include(v => v.VCAS_forms).Where(x => x.FK_formsId == id).ToList().LastOrDefault();
            var stringArray = new string[13] {
                vCAS_REF_forms.txtInput_01,
                vCAS_REF_forms.txtInput_02,
                vCAS_REF_forms.txtInput_03,
                vCAS_REF_forms.txtInput_04,
                vCAS_REF_forms.txtInput_05,
                vCAS_REF_forms.txtInput_06,
                vCAS_REF_forms.txtInput_07,
                vCAS_REF_forms.txtInput_08,
                vCAS_REF_forms.txtInput_09,
                vCAS_REF_forms.txtInput_10,
                vCAS_REF_forms.txtInput_11,
                vCAS_REF_forms.txtInput_12,
                vCAS_REF_forms.txtInput_13
            };
            ViewBag.Collection = stringArray;

            var stringArray2 = new string[3] { 
                vCAS_REF_forms.checkInput_01,
                vCAS_REF_forms.checkInput_02,
                vCAS_REF_forms.checkInput_03
            };
            ViewBag.Collection2 = stringArray2;

            var stringArray3 = new string[3]
            {
                vCAS_REF_forms.txtAreaInput_01,
                vCAS_REF_forms.txtAreaInput_02,
                vCAS_REF_forms.txtAreaInput_03
            };
            ViewBag.Collection3 = stringArray3;

            var stringArray4 = new string[3]
            {
                vCAS_REF_forms.selectInput_01,
                vCAS_REF_forms.selectInput_02,
                vCAS_REF_forms.selectInput_03
            };
            ViewBag.Collection4 = stringArray4; 

            return View(vCAS_forms);
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
            ViewBag.id = id;
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
        public ActionResult Create(VCAS_forms vCAS_forms)
        {

            if (ModelState.IsValid)
            {
                db.VCAS_forms.Add(new VCAS_forms
                {
                    Id = vCAS_forms.Id,
                    name = vCAS_forms.name,
                    desc = vCAS_forms.desc,
                    form = vCAS_forms.form,
                    dateModified = vCAS_forms.dateModified,
                    FK_location = vCAS_forms.FK_location,
                    FK_REF_userRolesId = vCAS_forms.FK_REF_userRolesId
                });
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
