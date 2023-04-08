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
    public class ordersController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET Partial: inventory
        public ActionResult IndexPartial()
        {
            return PartialView("_ordersIndex", db.VCAS_orders.Where(x => x.FK_location == GlobalSession.Location).Include(v => v.VCAS_customer).Include(v => v.VCAS_inventory).Include(v => v.VCAS_REF_order_status).ToList());
        }

        // GET: orders
        public ActionResult Index()
        {
            var vCAS_orders = db.VCAS_orders.Where(x => x.FK_location == GlobalSession.Location).Include(v => v.VCAS_customer).Include(v => v.VCAS_inventory).Include(v => v.VCAS_REF_order_status);
            return View(vCAS_orders.ToList());
        }

        // GET: CustomerNew
        public ActionResult CustomerNew()
        {
            return View();
        }

        // GET: orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_orders vCAS_orders = db.VCAS_orders.Find(id);
            if (vCAS_orders == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_orders);
        }

        // GET: orders/Create01
        public ActionResult Create01()
        {
            return View();
        }

        // POST: orders/Create01
        [HttpPost]
        public ActionResult Create01([Bind(Include = "Id,firstName,lastName,address,state,phone,email")] VCAS_customer vCAS_customer, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_customer.Add(new VCAS_customer
                {
                    Id = vCAS_customer.Id,
                    firstName = form["firstName"],
                    lastName = form["lastName"],
                    address = form["address"],
                    state = form["state"],
                    phone = form["phone"],
                    email = form["email"]
                });                
                db.SaveChanges();
                return RedirectToAction("Create02", "orders", "#step-2" );
            }            
            return View(vCAS_customer);
        }

        // GET: orders/Create02
        public ActionResult Create02()
        {
            // var cID = db.VCAS_customer.Select(x => x.Id).LastOrDefault();
            int? cID = (from c in db.VCAS_customer orderby c.Id descending select c.Id).Take(1).SingleOrDefault();
            ViewBag.FK_customerId = cID;
            ViewBag.FK_customerName = db.VCAS_customer.Where(x => x.Id == cID).Select(x => x.firstName + " " + x.lastName).FirstOrDefault();
            // ViewBag.FK_inventoryId = new SelectList(db.VCAS_inventory, "Id", "name");
            ViewBag.FK_inventoryId = new SelectList((
                from i in db.VCAS_inventory
                select new {
                    id = i.Id,
                    name = "<img src='" + i.image + "' width='50'/>" + i.name + " - " + i.desc }), 
                "Id", "name");
            ViewBag.FK_order_statusId = new SelectList(db.VCAS_REF_order_status, "Id", "name");
            return View();
        }

        // POST: orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create02([Bind(Include = "Id,datetime,quantity,comment,FK_order_statusId,FK_customerId,FK_inventoryId,FK_location")] VCAS_orders vCAS_orders)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_orders.Add(vCAS_orders);
                db.SaveChanges();
                return RedirectToAction("Create03", "orders");
            }

            ViewBag.FK_customerId = new SelectList(db.VCAS_customer, "Id", "firstName", vCAS_orders.FK_customerId);
            ViewBag.FK_inventoryId = new SelectList(db.VCAS_inventory, "Id", "name", vCAS_orders.FK_inventoryId);
            ViewBag.FK_order_statusId = new SelectList(db.VCAS_REF_order_status, "Id", "name", vCAS_orders.FK_order_statusId);
            return View(vCAS_orders);
        }

        // GET: orders/Create03
        public ActionResult Create03()
        {
            return View();
        }

        // GET: orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_orders vCAS_orders = db.VCAS_orders.Find(id);
            if (vCAS_orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_customerId = new SelectList(db.VCAS_customer, "Id", "firstName", vCAS_orders.FK_customerId);
            ViewBag.FK_inventoryId = new SelectList(db.VCAS_inventory, "Id", "name", vCAS_orders.FK_inventoryId);
            ViewBag.FK_order_statusId = new SelectList(db.VCAS_REF_order_status, "Id", "name", vCAS_orders.FK_order_statusId);
            return View(vCAS_orders);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,datetime,quantity,comment,FK_order_statusId,FK_customerId,FK_inventoryId,FK_location")] VCAS_orders vCAS_orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_orders).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_customerId = new SelectList(db.VCAS_customer, "Id", "firstName", vCAS_orders.FK_customerId);
            ViewBag.FK_inventoryId = new SelectList(db.VCAS_inventory, "Id", "name", vCAS_orders.FK_inventoryId);
            ViewBag.FK_order_statusId = new SelectList(db.VCAS_REF_order_status, "Id", "name", vCAS_orders.FK_order_statusId);
            return View(vCAS_orders);
        }

        // GET: orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_orders vCAS_orders = db.VCAS_orders.Find(id);
            if (vCAS_orders == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_orders);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_orders vCAS_orders = db.VCAS_orders.Find(id);
            db.VCAS_orders.Remove(vCAS_orders);
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
