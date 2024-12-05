 using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class formsDataController : Controller
    {
        private ModelContainer db = new ModelContainer();
        private string fileName1, fileName2;

        // STOREAGE PATH
        // **********************************************************
        private static string FilePath()
        {
            return (@"" + ConfigurationManager.ConnectionStrings["StoragePath"].ConnectionString);
        }

        // GET: formsData
        public ActionResult Json(int? id)
        {
            var vCAS_REF_forms = db.VCAS_REF_forms.Include(v => v.VCAS_forms).Where(x => x.FK_formsId == id);
            db.Configuration.ProxyCreationEnabled = false;
            return Json(vCAS_REF_forms.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var vCAS_REF_forms = db.VCAS_REF_forms.Include(v => v.VCAS_forms);
            return View(vCAS_REF_forms.ToList());
        }
        public ActionResult IndexPartial(int? id)

        {
            var vCAS_REF_forms = db.VCAS_REF_forms.Include(v => v.VCAS_forms).Where(x => x.FK_formsId == id);
            return PartialView("_formsDataTable", vCAS_REF_forms.ToList());
        }

        // GET: formsData/Details/5

        public ActionResult Details(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VCAS_REF_forms vCAS_REF_forms = db.VCAS_REF_forms.Find(id);

            if (vCAS_REF_forms == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_forms);
        }

        // GET: formsData/Create
        public ActionResult Create()
        {

            ViewBag.FK_formsId = new SelectList(db.VCAS_forms, "Id", "name");

            return View();
        }

        // POST: formsData/Create

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 

        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,txtInput_01,txtInput_02,txtInput_03,txtInput_04,txtInput_05,txtInput_06,txtInput_07,txtInput_08,txtInput_09,txtInput_10,txtInput_11,txtInput_12,txtInput_13,txtInput_14,txtInput_15,txtInput_16,txtInput_17,txtInput_18,checkInput_01,checkInput_02,checkInput_03,selectInput_01,selectInput_02,selectInput_03,txtAreaInput_01,txtAreaInput_02,txtAreaInput_03,fileInput_01,fileInput_02,formBtn,FK_formsId,frmHeader,frmFooter,signatureBox")] VCAS_REF_forms vCAS_REF_forms, HttpPostedFileBase fileInput_01, HttpPostedFileBase fileInput_02)

        {
            if (ModelState.IsValid)
            {

                if (fileInput_01 != null && fileInput_01.ContentLength > 0)
                {
                    fileName1 = Path.GetFileNameWithoutExtension(fileInput_01.FileName);
                    string extension = Path.GetExtension(fileInput_01.FileName);
                    fileName1 = fileName1 + DateTime.Now.ToString("yymmssffff") + extension;
                    vCAS_REF_forms.fileInput_01 = FilePath().Replace(@"\", @"\\") + fileName1;
                    fileName1 = Path.Combine(Server.MapPath(FilePath().Replace(@"\", @"\\")), fileName1);
                    fileInput_01.SaveAs(fileName1);
                }
                else
                {
                    fileName1 = null;
                }

                if (fileInput_02 != null && fileInput_02.ContentLength > 0)
                {
                    fileName2 = Path.GetFileNameWithoutExtension(fileInput_02.FileName);
                    string extension = Path.GetExtension(fileInput_02.FileName);
                    fileName2 = fileName2 + DateTime.Now.ToString("yymmssffff") + extension;
                    vCAS_REF_forms.fileInput_02 = FilePath().Replace(@"\", @"\\") + fileName1;
                    fileName2 = Path.Combine(Server.MapPath(FilePath().Replace(@"\", @"\\")), fileName2);
                    fileInput_02.SaveAs(fileName2);
                }
                else
                {
                    fileName2 = null;
                }

                db.VCAS_REF_forms.Add(vCAS_REF_forms);
                db.SaveChanges();

                return RedirectToAction("Complete", "forms", new { id = vCAS_REF_forms.FK_formsId});
            }


            ViewBag.FK_formsId = new SelectList(db.VCAS_forms, "Id", "name", vCAS_REF_forms.FK_formsId);

            return View(vCAS_REF_forms);
        }

        // GET: formsData/Edit/5

        public ActionResult Edit(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VCAS_REF_forms vCAS_REF_forms = db.VCAS_REF_forms.Find(id);

            if (vCAS_REF_forms == null)
            {
                return HttpNotFound();
            }

            ViewBag.FK_formsId = new SelectList(db.VCAS_forms, "Id", "name", vCAS_REF_forms.FK_formsId);

            return View(vCAS_REF_forms);
        }

        // POST: formsData/Edit/5

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 

        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,txtInput_01,txtInput_02,txtInput_03,txtInput_04,txtInput_05,txtInput_06,txtInput_07,txtInput_08,txtInput_09,txtInput_10,txtInput_11,txtInput_12,txtInput_13,txtInput_14,txtInput_15,txtInput_16,txtInput_17,txtInput_18,checkInput_01,checkInput_02,checkInput_03,selectInput_01,selectInput_02,selectInput_03,txtAreaInput_01,txtAreaInput_02,txtAreaInput_03,fileInput_01,fileInput_02,formBtn,FK_formsId,frmHeader,frmFooter,signatureBox")] VCAS_REF_forms vCAS_REF_forms)

        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_REF_forms).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.FK_formsId = new SelectList(db.VCAS_forms, "Id", "name", vCAS_REF_forms.FK_formsId);

            return View(vCAS_REF_forms);
        }

        // GET: formsData/Delete/5

        public ActionResult Delete(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VCAS_REF_forms vCAS_REF_forms = db.VCAS_REF_forms.Find(id);

            if (vCAS_REF_forms == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_REF_forms);
        }

        // POST: formsData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)

        {

            VCAS_REF_forms vCAS_REF_forms = db.VCAS_REF_forms.Find(id);

            db.VCAS_REF_forms.Remove(vCAS_REF_forms);

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
