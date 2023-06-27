using MvcSiteMapProvider;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using VCAS.Models;

namespace VCAS.Controllers
{

    [Authorize]
    public class capture_paymentsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: capture_payments
        // ======================================================================
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.VCAS_capture_payments.Where(x => x.datetime.Month == System.DateTime.Now.Month).ToList());
        }

        // GET: capture_payments/Details/5
        // ======================================================================
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_capture_payments vCAS_capture_payments = db.VCAS_capture_payments.Find(id);
            if (vCAS_capture_payments == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_capture_payments);
        }

        // GET: capture_payments/Print
        // ======================================================================
        public ActionResult Print(FormCollection collection)
        {
            string recNo = collection["r"].ToString();
            Session["printRec"] = collection["r"].ToString();
            ViewBag.receiptNo = collection["r"].ToString();
            ViewBag.recLogo = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_logo).FirstOrDefault();
            ViewBag.council = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council.name).FirstOrDefault();
            ViewBag.recHeader = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_header).FirstOrDefault();
            ViewBag.recFooter = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_footer).FirstOrDefault();
            var report = db.VCAS_capture_payments.Where(x => x.receiptNo == recNo && x.FK_location == GlobalSession.Location).FirstOrDefault();
            return View(report);
        }
        public ActionResult PrintLast()
        {
            string lastRec = db.VCAS_capture_payments.Where(x => x.FK_location == GlobalSession.Location).Select(x => x.receiptNo).ToList().LastOrDefault();
            Session["printRec"] = lastRec;
            ViewBag.receiptNo = lastRec;
            ViewBag.recLogo = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_logo).FirstOrDefault();
            ViewBag.council = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council.name).FirstOrDefault();
            ViewBag.recHeader = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_header).FirstOrDefault();
            ViewBag.recFooter = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_footer).FirstOrDefault();
            var report = db.VCAS_capture_payments.Where(x => x.receiptNo == lastRec && x.FK_location == GlobalSession.Location).FirstOrDefault();
            return View(report);
        }
        private object model;
        public ActionResult PrintMoreItems(string r)
        {
            if (r != null)
            {
                model = db.VCAS_capture_payments.Where(x => x.receiptNo == r && x.FK_location == GlobalSession.Location).ToList();
                var tot = db.VCAS_capture_payments.Where(x => x.receiptNo == r && x.FK_location == GlobalSession.Location).ToList().Select(x => x.amount).Sum();
                var pay = db.VCAS_capture_payments.Where(x => x.receiptNo == r && x.FK_location == GlobalSession.Location).ToList().Select(x => x.recieved_amount).Sum();
                ViewBag.total = tot;
                ViewBag.payment = pay;
                ViewBag.balance = tot - pay;
            }
            else
            {
                string lastRec = Session["printRec"].ToString();
                model = db.VCAS_capture_payments.Where(x => x.receiptNo == r && x.FK_location == GlobalSession.Location).ToList();
                var tot = db.VCAS_capture_payments.Where(x => x.receiptNo == r && x.FK_location == GlobalSession.Location).ToList().Select(x => x.amount).Sum();
                var pay = db.VCAS_capture_payments.Where(x => x.receiptNo == r && x.FK_location == GlobalSession.Location).ToList().Select(x => x.recieved_amount).Sum();
                ViewBag.total = tot;
                ViewBag.payment = pay;
                ViewBag.balance = tot - pay;
            }
            return PartialView("_capture_paymentsIndex", model);
        }
        public ActionResult RePrint()
        {
            return View();
        }

        // GET: capture_payments/Create
        // ======================================================================
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult Create(string cusName)
        {
            // Algorithm for Unique Receipt number
            // ===================================
            // Check if date is unique, if yes
            // start with number sequence 1, and increment +1 
            // until date is no longer unique

            // Generate Receipt Number
            // =======================
            ViewBag.recNo = "VCR" + DateTime.Now.ToString("MMddyyyy") + 0;
            var rec_dates = db.VCAS_capture_payments.Select(x => x.datetime == DateTime.Today).ToList();
            var cur_date = DateTime.Today.ToString();
            var rec_num = db.VCAS_capture_payments.Select(x => x.receiptNo).ToList().LastOrDefault();
            var recurringNo = Regex.Replace(rec_num ?? "", @"^.{0,11}", "");
            int newRecurringNo;
            if (rec_dates.LastOrDefault().ToString() != cur_date)
            {
                newRecurringNo = 0;
                if (rec_dates.Any())
                {
                    newRecurringNo = Int32.Parse(recurringNo);
                    newRecurringNo += 1;
                    // Generate Receipt Number
                    ViewBag.recNo = "VCR" + DateTime.Now.ToString("MMddyyyy") + newRecurringNo;
                }
            }
            else
            {
                // Generate Receipt Number
                ViewBag.recNo = "VCR" + DateTime.Now.ToString("MMddyyyy") + 0;
            }

            // Fetch Invoice
            ViewBag.invoiceFetches = new SelectList((from i in db.VCAS_capture_payments.Where(x => x.FK_location == GlobalSession.Location && x.invoice == true) select new { id = i.Id, name = i.receiptNo + " :- " + i.payer }), "id", "name");
            // Receipt Logo
            ViewBag.recLogo = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_logo).FirstOrDefault();
            // Issuer
            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            // Default Council
            ViewBag.council = db.VCAS_council.Select(v => v.name).FirstOrDefault();
            // Location
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            // Payment Types
            ViewBag.FK_paymentType = new SelectList(db.VCAS_REF_payment_type, "id", "name");
            // Bank Details
            ViewBag.FK_bankDetails = new SelectList(db.VCAS_REF_bank_details, "id", "name");
            // Items
            ViewBag.FK_items = new SelectList((from s in db.VCAS_REF_items_location.Where(x => x.FK_councilId == GlobalSession.Location) select new { id = s.VCAS_REF_items.Id, name = s.VCAS_REF_items.name }), "id", "name");
            // Customer Information
            var cusName_data = db.VCAS_customer.Where(x => x.firstName == cusName).Select(x => x.firstName).FirstOrDefault();
            ViewBag.cusName = cusName_data + " " + db.VCAS_customer.Where(x => x.firstName == cusName).Select(x => x.lastName).FirstOrDefault();
            var cusID_data = db.VCAS_customer.Where(x => x.firstName == cusName).Select(x => x.Id).FirstOrDefault();
            ViewBag.cusID = cusID_data;
            ViewBag.cusList = new SelectList((from c in db.VCAS_customer select new { id = c.Id, name = c.firstName + " " + c.lastName }), "id", "name");
            // Customer Orders dropdown
            ViewBag.cusOrders = new SelectList((from o in db.VCAS_orders.Where(x => x.FK_customerId == cusID_data && x.FK_order_statusId == 1) select new { id = o.Id, name = "Order #:" + o.Id + " " + o.VCAS_inventory.name }), "id", "name");
            ViewBag.cusOrdersCount = db.VCAS_orders.Where(x => x.FK_customerId == cusID_data && x.FK_order_statusId == 1).Select(x => x.Id).Count();

            return View();
        }

        // POST: capture_payments/Create
        // ======================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,datetime,payer,payerID,orderID,amount,recieved_amount,checkNo,comment,receiptNo,issuer,FK_paymentType,FK_bankDetails,FK_items,FK_location,invoice")] VCAS_capture_payments vCAS_capture_payments, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_capture_payments.Add(vCAS_capture_payments);
                db.SaveChanges();

                // EXEC Stored Procedure - usp_UpdateCusOrderStat
                // ***********************************************
                if (vCAS_capture_payments.orderID > 0 && vCAS_capture_payments.orderID != null)
                {
                    SqlParameter[] Parameters = { new SqlParameter("@p_cusID", vCAS_capture_payments.payerID) };
                    db.Database.ExecuteSqlCommand("EXEC usp_UpdateCusOrderStat @p_cusID", Parameters);
                }

                // EXEC Stored Procedure - usp_UpdateStock
                // ***********************************************
                if (vCAS_capture_payments.recieved_amount > 0 && 
                    vCAS_capture_payments.recieved_amount != null && 
                    Convert.ToInt32(form["itemID"]) > 0 && 
                    Convert.ToInt32(form["inventoryID"]) > 0)
                {
                    int iID = Convert.ToInt32(form["itemID"]);
                    int invID = Convert.ToInt32(form["inventoryID"]);

                    SqlParameter[] Parameters1 = {
                        new SqlParameter("@p_item", iID),
                        new SqlParameter("@p_id", invID),
                        new SqlParameter("@p_loc", GlobalSession.Location)
                    };
                    db.Database.ExecuteSqlCommand("EXEC usp_UpdateStock @p_item, @p_loc, @p_id", Parameters1);
                }

                return RedirectToAction("PrintLast");
            }
            // Fetch Invoice
            ViewBag.invoiceFetches = new SelectList((from i in db.VCAS_capture_payments.Where(x => x.FK_location == GlobalSession.Location && x.invoice == true) select new { id = i.Id, name = i.receiptNo + " :- " + i.payer }), "id", "name");
            // Issuer
            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            // Default Council
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            // Generate Receipt Number
            ViewBag.recNo = "VCR" + DateTime.Now.ToString("MMddyyyy") + 0;
            // Location
            ViewBag.loc = new SelectList(db.VCAS_council, "id", "name");
            // Payment Types 
            ViewBag.FK_paymentType = new SelectList(db.VCAS_REF_payment_type, "id", "name");
            // Bank Details
            ViewBag.FK_bankDetails = new SelectList(db.VCAS_REF_bank_details, "id", "name");
            // Items
            ViewBag.FK_items = new SelectList((from s in db.VCAS_REF_items_location.Where(x => x.FK_councilId == GlobalSession.Location) select new { id = s.VCAS_REF_items.Id, name = s.VCAS_REF_items.name + " - " + s.VCAS_REF_items.desc }), "id", "name", vCAS_capture_payments.VCAS_REF_items);
            // Customer Information
            var cusName_data = db.VCAS_customer.Where(x => x.Id == vCAS_capture_payments.payerID).Select(x => x.firstName).FirstOrDefault();
            ViewBag.cusName = cusName_data + " " + db.VCAS_customer.Where(x => x.Id == vCAS_capture_payments.payerID).Select(x => x.lastName).FirstOrDefault();
            ViewBag.cusID = vCAS_capture_payments.payerID;
            ViewBag.cusList = new SelectList((from c in db.VCAS_customer select new { id = c.Id, name = c.firstName + " " + c.lastName }), "id", "name");
            // Customer Orders dropdown
            ViewBag.cusOrders = new SelectList((from o in db.VCAS_orders.Where(x => x.FK_customerId == vCAS_capture_payments.payerID && x.FK_order_statusId == 1) select new { id = o.Id, name = "Order #:" + o.Id + " " + o.VCAS_inventory.name }), "id", "name");
            ViewBag.cusOrdersCount = db.VCAS_orders.Where(x => x.FK_customerId == vCAS_capture_payments.payerID && x.FK_order_statusId == 1).Select(x => x.Id).Count();
            return View(vCAS_capture_payments);
        }


        // GET: More Items - capture_payments
        // ======================================================================
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult More()
        {
            // Receipt No
            var recNoDB = db.VCAS_capture_payments.Select(x => x.receiptNo).ToList().LastOrDefault();
            ViewBag.recNo = recNoDB;
            Session["lastRecNo"] = recNoDB;
            // Payer
            var payerDB = db.VCAS_capture_payments.Select(x => x.payer).ToList().LastOrDefault();
            ViewBag.payer = payerDB;
            Session["lastPayer"] = payerDB;
            string cusName = Session["lastPayer"].ToString();
            // Issuer
            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            // Default Council
            ViewBag.council = db.VCAS_council.Select(v => v.name).FirstOrDefault();
            // Location
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            // Payment Types
            ViewBag.FK_paymentType = new SelectList(db.VCAS_REF_payment_type, "id", "name");
            // Bank Details
            ViewBag.FK_bankDetails = new SelectList(db.VCAS_REF_bank_details, "id", "name");
            // Items
            ViewBag.FK_items = new SelectList((from s in db.VCAS_REF_items_location.Where(x => x.FK_councilId == GlobalSession.Location) select new { id = s.VCAS_REF_items.Id, name = s.VCAS_REF_items.name }), "id", "name");

            // Customer Information
            var cusName_data = db.VCAS_customer.Where(x => x.firstName == cusName).Select(x => x.firstName).FirstOrDefault();
            ViewBag.cusName = cusName_data + " " + db.VCAS_customer.Where(x => x.firstName == cusName).Select(x => x.lastName).FirstOrDefault();

            var cusID_data = db.VCAS_customer.Where(x => x.firstName == cusName).Select(x => x.Id).FirstOrDefault();
            ViewBag.cusID = cusID_data;

            ViewBag.cusList = new SelectList((from c in db.VCAS_customer select new { id = c.Id, name = c.firstName + " " + c.lastName }), "id", "name");

            // Customer Orders dropdown
            ViewBag.cusOrders = new SelectList((from o in db.VCAS_orders.Where(x => x.FK_customerId == cusID_data && x.FK_order_statusId == 1) select new { id = o.Id, name = "Order #:" + o.Id + " " + o.VCAS_inventory.name }), "id", "name");
            ViewBag.cusOrdersCount = db.VCAS_orders.Where(x => x.FK_customerId == cusID_data && x.FK_order_statusId == 1).Select(x => x.Id).Count();

            return View();
        }

        // POST: More Items - capture_payments
        // ======================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult More([Bind(Include = "Id,datetime,payer,payerID,orderID,amount,recieved_amount,checkNo,comment,receiptNo,issuer,FK_paymentType,FK_bankDetails,FK_items,FK_location,invoice")] VCAS_capture_payments vCAS_capture_payments)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_capture_payments.Add(new VCAS_capture_payments
                {
                    datetime = vCAS_capture_payments.datetime,
                    payer = Session["lastPayer"].ToString(),
                    payerID = vCAS_capture_payments.payerID,
                    orderID = vCAS_capture_payments.orderID,
                    amount = vCAS_capture_payments.amount,
                    checkNo = vCAS_capture_payments.checkNo,
                    comment = vCAS_capture_payments.comment,
                    receiptNo = Session["lastRecNo"].ToString(),
                    issuer = vCAS_capture_payments.issuer,
                    FK_paymentType = vCAS_capture_payments.FK_paymentType,
                    FK_bankDetails = vCAS_capture_payments.FK_bankDetails,
                    FK_items = vCAS_capture_payments.FK_items,
                    FK_location = vCAS_capture_payments.FK_location,
                    invoice = vCAS_capture_payments.invoice
                });
                db.SaveChanges();

                // EXEC Stored Procedure - usp_UpdateCusOrderStat
                if (vCAS_capture_payments.orderID > 0 && vCAS_capture_payments.orderID != null)
                {
                    SqlParameter[] Parameters = { new SqlParameter("@p_cusID", vCAS_capture_payments.payerID) };
                    db.Database.ExecuteSqlCommand("EXEC usp_UpdateCusOrderStat @p_cusID", Parameters);
                }
                return RedirectToAction("More");
            }
            // Receipt No
            ViewBag.recNo = db.VCAS_capture_payments.Select(x => x.receiptNo).ToList().LastOrDefault();
            // Payer
            ViewBag.payer = db.VCAS_capture_payments.Select(x => x.payer).ToList().LastOrDefault();
            // Issuer
            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            // Default Council
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            // Location
            ViewBag.loc = new SelectList(db.VCAS_council, "id", "name");
            // Payment Types 
            ViewBag.FK_paymentType = new SelectList(db.VCAS_REF_payment_type, "id", "name");
            // Bank Details
            ViewBag.FK_bankDetails = new SelectList(db.VCAS_REF_bank_details, "id", "name");
            // Items
            ViewBag.FK_items = new SelectList((from s in db.VCAS_REF_items select new { id = s.Id, name = s.name + " - " + s.desc }), "id", "name", vCAS_capture_payments.VCAS_REF_items);
            return View(vCAS_capture_payments);
        }

        // GET: capture_payments/Edit/5
        // ======================================================================
        [CustomAuthorize(Roles = "admin,cashier")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_capture_payments vCAS_capture_payments = db.VCAS_capture_payments.Find(id);
            if (vCAS_capture_payments == null)
            {
                return HttpNotFound();
            }

            // Receipt Logo
            ViewBag.recLogo = db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.receipt_logo).FirstOrDefault();
            // Issuer
            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            // Default Council
            ViewBag.council = db.VCAS_council.Select(v => v.name).FirstOrDefault();
            // Location
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            // Payment Types
            ViewBag.FK_paymentType = new SelectList(db.VCAS_REF_payment_type, "id", "name");
            // Bank Details
            ViewBag.FK_bankDetails = new SelectList(db.VCAS_REF_bank_details, "id", "name");
            // Items
            ViewBag.FK_items = new SelectList((from s in db.VCAS_REF_items_location.Where(x => x.FK_councilId == GlobalSession.Location) select new { Id = s.VCAS_REF_items.Id, name = s.VCAS_REF_items.name }), "Id", "name");
            return View(vCAS_capture_payments);
        }

        // POST: capture_payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,datetime,payer,payerID,orderID,amount,recieved_amount,checkNo,comment,receiptNo,issuer,FK_paymentType,FK_bankDetails,FK_items,FK_location,invoice")] VCAS_capture_payments vCAS_capture_payments, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_capture_payments).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // EXEC Stored Procedure - usp_UpdateStock
                // ***********************************************
                if (vCAS_capture_payments.recieved_amount > 0 &&
                    vCAS_capture_payments.recieved_amount != null &&
                    Convert.ToInt32(form["itemID"]) > 0 &&
                    Convert.ToInt32(form["inventoryID"]) > 0)
                {
                    int iID = Convert.ToInt32(form["itemID"]);
                    int invID = Convert.ToInt32(form["inventoryID"]);

                    SqlParameter[] Parameters1 = {
                        new SqlParameter("@p_item", iID),
                        new SqlParameter("@p_id", invID),
                        new SqlParameter("@p_loc", GlobalSession.Location)
                    };
                    db.Database.ExecuteSqlCommand("EXEC usp_UpdateStock @p_item, @p_loc, @p_id", Parameters1);
                }

                return RedirectToAction("Create");
            }

            // Issuer
            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            // Default Council
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            // Generate Receipt Number
            ViewBag.recNo = "VCR" + DateTime.Now.ToString("MMddyyyy") + 0;
            // Location
            ViewBag.loc = new SelectList(db.VCAS_council, "id", "name");
            // Payment Types 
            ViewBag.FK_paymentType = new SelectList(db.VCAS_REF_payment_type, "id", "name");
            // Bank Details
            ViewBag.FK_bankDetails = new SelectList(db.VCAS_REF_bank_details, "id", "name");
            // Items
            ViewBag.FK_items = new SelectList((from s in db.VCAS_REF_items_location.Where(x => x.FK_councilId == GlobalSession.Location) select new { Id = s.VCAS_REF_items.Id, name = s.VCAS_REF_items.name + " - " + s.VCAS_REF_items.desc }), "Id", "name");
            return View(vCAS_capture_payments);
        }

        // VOID: capture_payments
        // ======================================================================
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Void(int? id)
        {
            // Reducing Balance on debitAccounts
            // EXEC Stored Procedure - usp_DeleteCapturedReceipt
            // =================================================
            SqlParameter[] Parameters =
            {
            new SqlParameter("@p_Id", id)
            };
            db.Database.ExecuteSqlCommand("EXEC usp_VoidCapturedReceipt @p_Id", Parameters);
            return RedirectToAction("Index");
        }

        // DELETE: capture_payments
        // ======================================================================
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            // Reducing Balance on debitAccounts
            // EXEC Stored Procedure - usp_DeleteCapturedReceipt
            // =================================================
            SqlParameter[] Parameters =
            {
                new SqlParameter("@p_Id", id)
            };
            db.Database.ExecuteSqlCommand("EXEC usp_DeleteCapturedReceipt @p_Id", Parameters);
            return RedirectToAction("Index");
        }
    }
}
