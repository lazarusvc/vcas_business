using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class inventoryController : Controller
    {
        private ModelContainer db = new ModelContainer();
        private string fileName;

        // STOREAGE PATH
        // **********************************************************
        private static string FilePath()
        {
            return (@"" + ConfigurationManager.ConnectionStrings["StoragePath"].ConnectionString);
        }

        // JSON
        // **********************************************************
        public ActionResult Json(int? l, int? it)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Database.SqlQuery<VCAS_inventory>
                (@"SELECT * FROM VCAS_inventory 
                 WHERE FK_REF_itemsId = '" + it + "' AND FK_location = '" + l + "' "), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Json2(int? l)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Database.SqlQuery<VCAS_inventory>
                (@"SELECT * FROM VCAS_inventory 
                 WHERE FK_location = '" + l + "' AND partNumber IS NOT NULL"), JsonRequestBehavior.AllowGet);
        }

        // UPDATE COUNT
        // **********************************************************
        public ActionResult updateCountUP(FormCollection fm)
        {
            SqlParameter[] Parameters = {
                        new SqlParameter("@p_id", Convert.ToInt32(fm["id"])),
                        new SqlParameter("@p_ct", Convert.ToInt32(fm["ct"])),
                        new SqlParameter("@p_loc", GlobalSession.Location)
                    };
            db.Database.ExecuteSqlCommand("EXEC usp_UpdateStockUP @p_loc, @p_id, @p_ct", Parameters);
            return RedirectToAction("Inventory", "Home", null);
        }

        // GET Partial: inventory
        public ActionResult IndexPartial()
        {
            return PartialView("_inventoryIndex", db.VCAS_inventory.Where(x => x.FK_location == GlobalSession.Location).ToList());
        }

        // GET: inventory
        public ActionResult Index()
        {
            return View(db.VCAS_inventory.Where(x => x.FK_location == GlobalSession.Location).ToList());
        }

        // GET: inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_inventory vCAS_inventory = db.VCAS_inventory.Find(id);
            if (vCAS_inventory == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_inventory);
        }

        // GET: inventory/Create
        public ActionResult Create()
        {
            ViewBag.FK_REF_itemsId = new SelectList(db.vw_inventoryItems.Where(x => x.FK_councilId == GlobalSession.Location), "Id", "name").ToList();
            ViewBag.FK_location = GlobalSession.Location;
            return View();
        }

        // POST: inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,desc,dateModified,partNumber,label,startStock,currentStock,quantity,size,unit,unitPrice,sellingPrice,image,FK_location,FK_REF_itemsId,FK_location")] VCAS_inventory vCAS_inventory, HttpPostedFileBase image)
        {
            // Attach image
            if (image != null && image.ContentLength > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(image.FileName);
                string extension = Path.GetExtension(image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_inventory.image = FilePath() + fileName;
                fileName = Path.Combine(Server.MapPath(FilePath()), fileName);
                image.SaveAs(fileName);
            }
            else
            {
                fileName = vCAS_inventory.image;
            }

            if (ModelState.IsValid)
            {
                db.VCAS_inventory.Add(vCAS_inventory);
                db.SaveChanges();
                return RedirectToAction("Inventory", "Home");
            }

            ViewBag.FK_REF_itemsId = new SelectList(db.vw_inventoryItems.Where(x => x.FK_councilId == GlobalSession.Location), "Id", "name").ToList();
            ViewBag.FK_location = GlobalSession.Location;
            return View(vCAS_inventory);
        }

        // GET: inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_inventory vCAS_inventory = db.VCAS_inventory.Find(id);
            if (vCAS_inventory == null)
            {
                return HttpNotFound();
            }

            ViewBag.FK_REF_itemsId = new SelectList(
                db.VCAS_REF_items
                .Where(x => x.VCAS_REF_items_location.Select(z => z.FK_councilId)
                .FirstOrDefault() == GlobalSession.Location),
                "Id", "name", vCAS_inventory.FK_REF_itemsId);
            ViewBag.FK_location = GlobalSession.Location;

            return View(vCAS_inventory);
        }

        // POST: inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,desc,dateModified,partNumber,label,startStock,currentStock,quantity,size,unit,unitPrice,sellingPrice,image,FK_location,FK_REF_itemsId,FK_location")] VCAS_inventory vCAS_inventory, HttpPostedFileBase image)
        {
            // Attach image
            if (image != null && image.ContentLength > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(image.FileName);
                string extension = Path.GetExtension(image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_inventory.image = FilePath() + fileName;
                fileName = Path.Combine(Server.MapPath(FilePath()), fileName);
                image.SaveAs(fileName);
            }
            else
            {
                fileName = vCAS_inventory.image;
            }
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_inventory).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Inventory", "Home");
            }

            ViewBag.FK_REF_itemsId = new SelectList(
                db.VCAS_REF_items
                .Where(x => x.VCAS_REF_items_location.Select(z => z.FK_councilId)
                .FirstOrDefault() == GlobalSession.Location), 
                "Id", "name", vCAS_inventory.FK_REF_itemsId);
            ViewBag.FK_location = GlobalSession.Location;

            return View(vCAS_inventory);
        }

        // GET: inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_inventory vCAS_inventory = db.VCAS_inventory.Find(id);
            if (vCAS_inventory == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_inventory);
        }

        // POST: inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_inventory vCAS_inventory = db.VCAS_inventory.Find(id);
            db.VCAS_inventory.Remove(vCAS_inventory);
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
