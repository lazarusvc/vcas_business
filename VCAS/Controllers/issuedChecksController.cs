using ClosedXML.Excel;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class issuedChecksController : Controller
    {
        private ModelContainer db = new ModelContainer();
        private string fileName, fileName2, fileName3;

        // STOREAGE PATH
        // **********************************************************
        private static string FilePath()
        {
            return (@"" + ConfigurationManager.ConnectionStrings["StoragePath"].ConnectionString);
        }

        // GET: issuedChecks
        // INDEX PAGE
        // ***********************************************************
        [Authorize]
        public ActionResult Index(int? d)
        {
            var vci = db.VCAS_issuedChecks.SqlQuery("SELECT TOP(0)* FROM VCAS_issuedChecks Order By datetime desc").ToList();
            if (d != null)
            {
                vci = db.VCAS_issuedChecks.SqlQuery("SELECT TOP(500)* FROM VCAS_issuedChecks WHERE FK_location = '" + d + "' Order By datetime desc").ToList();
            }
            return PartialView("_issuedChecksIndex", vci);
        }

        // Filter list of Payments 
        // ***********************************************************
        public ActionResult IndexFilter()
        {
            // Select Organization
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.council_select = new SelectList(dis_user, "Id", "name");
            return View();
        }

        public ActionResult IndexFilterResults(int? loc, string s, string e, string stat)
        {

            var result = db.VCAS_issuedChecks.SqlQuery("SELECT * FROM VCAS_issuedChecks WHERE FK_location = 0 AND datetime between '' and ''").ToList();

            if (loc != null && s != null && e != null)
            {
                switch (stat)
                {
                    case "A":
                        result = db.VCAS_issuedChecks.SqlQuery("SELECT * FROM VCAS_issuedChecks WHERE FK_location = '" + loc + "' AND datetime between '" + s + "' and '" + e + "' AND approval = 1 ").ToList();
                        Session["stat"] = "Approvals";
                        break;
                    case "P":
                        result = db.VCAS_issuedChecks.SqlQuery("SELECT * FROM VCAS_issuedChecks WHERE FK_location = '" + loc + "' AND datetime between '" + s + "' and '" + e + "' AND pending_approval = 1 ").ToList();
                        Session["stat"] = "Pending";
                        break;
                    case "C":
                        result = db.VCAS_issuedChecks.SqlQuery("SELECT * FROM VCAS_issuedChecks WHERE FK_location = '" + loc + "' AND datetime between '" + s + "' and '" + e + "' AND complete = 1 ").ToList();
                        Session["stat"] = "Completed";
                        break;
                }

                Session["print_result"] = result;
                Session["print_loc"] = loc;
                Session["print_startDate"] = s;
                Session["print_endDate"] = e;
            }
            return PartialView("_paymentResultsDashboard", result);
        }

        // INDEX ADMIN
        // ***********************************************************
        [CustomAuthorize(Roles = "admin")]
        public ActionResult IndexAdmin()
        {
            // Select Organization
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.council_select = new SelectList(dis_user, "Id", "name");
            return View();
        }

        public ActionResult IndexAdminResults(int? loc, string s, string e)
        {

            var result = db.VCAS_issuedChecks.SqlQuery("SELECT * FROM VCAS_issuedChecks WHERE FK_location = 0 AND datetime between '' and ''").ToList();

            if (loc != null && s != null && e != null)
            {
                result = db.VCAS_issuedChecks.SqlQuery("SELECT * FROM VCAS_issuedChecks WHERE FK_location = '" + loc + "' AND datetime between '" + s + "' and '" + e + "' ").ToList();
            }
            return PartialView("_paymentAdmin", result);
        }

        // Print Filtered report
        // ***********************************************************
        public ActionResult Print()
        {
            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            int locID = Convert.ToInt32(Session["print_loc"]);
            ViewBag.recLogo = "~" + db.VCAS_council.Where(x => x.Id == loc).Select(x => x.receipt_logo).FirstOrDefault();
            ViewBag.council = db.VCAS_council.Where(x => x.Id == locID).Select(x => x.name).FirstOrDefault();
            ViewBag.startDate = Session["print_startDate"].ToString();
            ViewBag.endDate = Session["print_endDate"].ToString();
            ViewBag.status = Session["stat"].ToString();
            var result = Session["print_result"];
            return View(result);
        }

        // APPROVALS PAGE
        // ***********************************************************
        [CustomAuthorize(Roles = "approver, admin")]
        public ActionResult Approvals()
        {
            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            var a1 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.pending_approvals = a1.Count();
            ViewBag.approvals = new SelectList(a1.ToList(), "Id", "receiverName");
            ViewBag.approvals_default = a1.Select(x => x.Id).FirstOrDefault();

            var a2 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature != null && x.approval == 1 && x.FK_location == loc);
            ViewBag.pending_delivery = a2.Count();
            return View();
        }

        // DELIVERY PAGE
        // ***********************************************************
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult Delivery()
        {
            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            var a1 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.pending_approvals = a1.Count();

            var a2 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature != null && x.approval == 1 && x.FK_location == loc);
            ViewBag.pending_delivery = a2.Count();
            ViewBag.delivery = new SelectList(a2.ToList(), "Id", "receiverName");
            ViewBag.delivery_default = a2.Select(x => x.Id).FirstOrDefault();
            return View();
        }

        // ENTRY PAGE
        // ***********************************************************
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult Entry()
        {
            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            var a1 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.pending_approvals = a1.Count();

            var a2 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature != null && x.approval == 1 && x.FK_location == loc);
            ViewBag.pending_delivery = a2.Count();

            var a3 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.entries = a3.Count();
            ViewBag.entry = new SelectList(a3.ToList(), "Id", "receiverName");
            ViewBag.entry_default = a3.Select(x => x.Id).FirstOrDefault();
            return View();
        }

        // GET: issuedChecks/Details/5
        // DETAILS PAGE
        // ***********************************************************
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_issuedChecks vCAS_issuedChecks = db.VCAS_issuedChecks.Find(id);
            if (vCAS_issuedChecks == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_issuedChecks);
        }

        // DOWNLOAD FILE:: BULK CREATE 
        // ***********************************************************
        public ActionResult DownloadFile_Payments()
        {
            // Select Organization
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.council_select = new SelectList(dis_user, "Id", "name");

            return View();
        }

        public ActionResult DownloadFile_PaymentsResults(int? d, string s, string e)
        {
            // EXEC Stored Procedure - Bulk_payments_to_download
            // ========================================
            SqlParameter[] Parameters1 =
            {
                    new SqlParameter("@p_loc", "0"),
                    new SqlParameter("@p_startDate", ""),
                    new SqlParameter("@p_endDate", "")
            };
            var result = db.Database.SqlQuery<usp_BulkDownloadPayments_Result>("EXEC usp_BulkDownloadPayments @p_loc, @p_startDate, @p_endDate", Parameters1).ToList();


            if (d != null && s != null && e != null)
            {
                SqlParameter[] Parameters2 =
                {
                    new SqlParameter("@p_loc", d),
                    new SqlParameter("@p_startDate", s),
                    new SqlParameter("@p_endDate", e)
                };
                result = db.Database.SqlQuery<usp_BulkDownloadPayments_Result>("EXEC usp_BulkDownloadPayments @p_loc, @p_startDate, @p_endDate", Parameters2).ToList();

                // Params to pass to Download Button
                // =================================
                Session["Dloc"] = d;
                Session["Dstart"] = s;
                Session["Dend"] = e;
            }
            return PartialView("_paymentResults", result);
        }

        [HttpPost]
        public FileResult DownloadFile()
        {
            DataTable dt = new DataTable("Book1");
            dt.Columns.AddRange(new DataColumn[16] {
                new DataColumn("datetime"),
                new DataColumn("amount"),
                new DataColumn("checkNo"),
                new DataColumn("issuer"),
                new DataColumn("receiverName"),
                new DataColumn("FK_receiverID_TypesId"),
                new DataColumn("receiverID"),
                new DataColumn("signature"),
                new DataColumn("FK_debitAccountsId"),
                new DataColumn("attach_ID"),
                new DataColumn("FK_location"),
                new DataColumn("approver"),
                new DataColumn("approval"),
                new DataColumn("pending_approval"),
                new DataColumn("auth_signature"),
                new DataColumn("complete")});

            DateTime Dstart = Convert.ToDateTime(Session["Dstart"].ToString());
            DateTime Dend = Convert.ToDateTime(Session["Dend"].ToString());
            int Dloc = (int)Session["Dloc"];
            var ic = db.VCAS_issuedChecks
                .Where(x => x.datetime >= Dstart)
                .Where(x => x.datetime <= Dend)
                .Where(x => x.FK_location == Dloc)
                .ToList();
            foreach (var ic_data in ic)
            {
                dt.Rows.Add(
                    ic_data.datetime,
                    ic_data.amount,
                    ic_data.checkNo,
                    ic_data.issuer,
                    ic_data.receiverName,
                    ic_data.FK_receiverID_TypesId,
                    ic_data.receiverID,
                    ic_data.signature,
                    ic_data.FK_debitAccountsId,
                    ic_data.attach_ID,
                    ic_data.FK_location,
                    ic_data.approver,
                    ic_data.approval,
                    ic_data.pending_approval,
                    ic_data.auth_signature,
                    ic_data.complete);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                //var ws = wb.Worksheets.Add(dt);
                //ws.Columns(1, 1).Hide();
                //ws.Columns(4, 4).Hide();
                //ws.Columns(8, 16).Hide();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "VCAS_download_for_bulk_payments.xlsx");
                }
            }
        }

        public ActionResult BulkCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BulkCreate(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~//Content//Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                DataTable dt = new DataTable("Book1");

                // Unhide Columns
                // =============================
                //var wb = new XLWorkbook();
                //var ws = wb.Worksheets.Add(dt);
                //ws.Column(1).Unhide();
                //ws.Column(4).Unhide();
                //ws.Column(8).Unhide();
                //ws.Column(9).Unhide();
                //ws.Column(10).Unhide();
                //ws.Column(11).Unhide();
                //ws.Column(12).Unhide();
                //ws.Column(13).Unhide();
                //ws.Column(14).Unhide();
                //ws.Column(15).Unhide();
                //ws.Column(16).Unhide();

                conString = string.Format(conString, filePath);
                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            // Get the name of First Sheet.
                            // =============================
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            // Read Data from First Sheet.
                            // ==========================
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                conString = ConfigurationManager.ConnectionStrings["ModelContainerUpload"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        // Set the database table name.
                        // ===========================
                        sqlBulkCopy.DestinationTableName = "dbo.VCAS_issuedChecks";

                        // [OPTIONAL]: Map the Excel columns with that of the database table
                        // ================================================================

                        //sqlBulkCopy.ColumnMappings.Add("datetime", "datetime");
                        //sqlBulkCopy.ColumnMappings.Add("amount", "amount");
                        //sqlBulkCopy.ColumnMappings.Add("checkNo", "checkNo");
                        //sqlBulkCopy.ColumnMappings.Add("issuer", "issuer");
                        //sqlBulkCopy.ColumnMappings.Add("receiverName", "receiverName");
                        //sqlBulkCopy.ColumnMappings.Add("FK_receiverID_TypesId", "FK_receiverID_TypesId");
                        //sqlBulkCopy.ColumnMappings.Add("receiverID", "receiverID");
                        //sqlBulkCopy.ColumnMappings.Add("signature", "signature");
                        //sqlBulkCopy.ColumnMappings.Add("FK_debitAccountsID", "FK_debitAccountsID");
                        //sqlBulkCopy.ColumnMappings.Add("attach_ID", "attach_ID");
                        //sqlBulkCopy.ColumnMappings.Add("FK_location", "FK_location");
                        //sqlBulkCopy.ColumnMappings.Add("approver", "approver");
                        //sqlBulkCopy.ColumnMappings.Add("approval", "approval");
                        //sqlBulkCopy.ColumnMappings.Add("pending_approval", "pending_approval");
                        //sqlBulkCopy.ColumnMappings.Add("auth_signature", "auth_signature");
                        //sqlBulkCopy.ColumnMappings.Add("complete", "complete");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // START PAGE
        // ***********************************************************
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult Start()
        {
            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            var a1 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.pending_approvals = a1.Count();
            var a2 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature != null && x.approval == 1 && x.FK_location == loc);
            ViewBag.pending_delivery = a2.Count();
            var a3 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.entries = a3.Count();
            return View();
        }

        // GET: issuedChecks/Create
        // CREATE PAGE
        // ***********************************************************
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult Create()
        {

            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name");

            var debitAcctSession = db.VCAS_debitAccounts.Where(x => x.FK_location == loc).ToList();
            ViewBag.FK_debitAccountsId = new SelectList(debitAcctSession, "Id", "name");
            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name");

            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");

            var a1 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.pending_approvals = a1.Count();
            var a2 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature != null && x.approval == 1 && x.FK_location == loc);
            ViewBag.pending_delivery = a2.Count();
            var a3 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.entries = a3.Count();
            return View();
        }

        // POST: issuedChecks/Create
        // CREATE PAGE
        // ***********************************************************
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,datetime,amount,checkNo,issuer,receiverName,FK_receiverID_TypesId,receiverID,signature,FK_debitAccountsId,attach_ID,FK_location,approver, approval, pending_approval, auth_signature, complete, FK_expensesId, attach_Doc, attach_Receipt, comments")] VCAS_issuedChecks vCAS_issuedChecks, HttpPostedFileBase attach_ID, HttpPostedFileBase attach_Doc)
        {
            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            // Attach Receiver's ID
            if (attach_ID != null && attach_ID.ContentLength > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(attach_ID.FileName);
                string extension = Path.GetExtension(attach_ID.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_ID = FilePath() + fileName;
                fileName = Path.Combine(Server.MapPath(FilePath()), fileName);
                attach_ID.SaveAs(fileName);
            }
            else
            {
                fileName = null;
            }


            // Attach Voucher Document
            if (attach_Doc != null && attach_Doc.ContentLength > 0)
            {
                fileName2 = Path.GetFileNameWithoutExtension(attach_Doc.FileName);
                string extension = Path.GetExtension(attach_Doc.FileName);
                fileName2 = fileName2 + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_Doc = FilePath() + fileName2;
                fileName2 = Path.Combine(Server.MapPath(FilePath()), fileName2);
                attach_Doc.SaveAs(fileName2);
            }
            else
            {
                fileName2 = null;
            }


            if (ModelState.IsValid)
            {
                db.VCAS_issuedChecks.Add(new VCAS_issuedChecks
                {
                    Id = vCAS_issuedChecks.Id,
                    datetime = vCAS_issuedChecks.datetime,
                    amount = vCAS_issuedChecks.amount,
                    checkNo = vCAS_issuedChecks.checkNo,
                    issuer = vCAS_issuedChecks.issuer,
                    receiverName = vCAS_issuedChecks.receiverName,
                    FK_receiverID_TypesId = vCAS_issuedChecks.FK_receiverID_TypesId,
                    receiverID = vCAS_issuedChecks.receiverID,
                    signature = vCAS_issuedChecks.signature,
                    FK_debitAccountsId = vCAS_issuedChecks.FK_debitAccountsId,
                    attach_ID = vCAS_issuedChecks.attach_ID,
                    FK_location = vCAS_issuedChecks.FK_location,
                    approver = vCAS_issuedChecks.approver,
                    approval = vCAS_issuedChecks.approval,
                    pending_approval = vCAS_issuedChecks.pending_approval,
                    auth_signature = vCAS_issuedChecks.auth_signature,
                    complete = vCAS_issuedChecks.complete,
                    FK_expensesId = vCAS_issuedChecks.FK_expensesId,
                    attach_Doc = vCAS_issuedChecks.attach_Doc,
                    attach_Receipt = vCAS_issuedChecks.attach_Receipt,
                    comments = vCAS_issuedChecks.comments
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name", vCAS_issuedChecks.FK_receiverID_TypesId);
            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name", vCAS_issuedChecks.FK_expensesId);
            // var fk_locSession = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.FK_location).FirstOrDefault();
            var debitAcctSession = db.VCAS_debitAccounts.Where(x => x.FK_location == loc).ToList();
            ViewBag.FK_debitAccountsId = new SelectList(debitAcctSession, "Id", "name", vCAS_issuedChecks.FK_debitAccountsId);

            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View(vCAS_issuedChecks);
        }


        // GET: issuedChecks/CreateApproved
        // CREATE PAGE
        // ***********************************************************
        [CustomAuthorize(Roles = "admin, approver")]
        public ActionResult CreateApproved()
        {
            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name");

            var debitAcctSession = db.VCAS_debitAccounts.Where(x => x.FK_location == loc).ToList();
            ViewBag.FK_debitAccountsId = new SelectList(debitAcctSession, "Id", "name");
            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name");

            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");

            var a1 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.pending_approvals = a1.Count();
            var a2 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature != null && x.approval == 1 && x.FK_location == loc);
            ViewBag.pending_delivery = a2.Count();
            var a3 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature == null && x.approval == null && x.FK_location == loc);
            ViewBag.entries = a3.Count();
            return View();
        }

        // POST: issuedChecks/CreateApproved
        // CREATE PAGE
        // ***********************************************************
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateApproved([Bind(Include = "Id,datetime,amount,checkNo,issuer,receiverName,FK_receiverID_TypesId,receiverID,signature,FK_debitAccountsId,attach_ID,FK_location,approver, approval, pending_approval, auth_signature, complete, FK_expensesId, attach_Doc, attach_Receipt, comments")] VCAS_issuedChecks vCAS_issuedChecks, HttpPostedFileBase attach_ID, HttpPostedFileBase attach_Doc, HttpPostedFileBase attach_Receipt)
        {
            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            // Attach Receiver's ID
            if (attach_ID != null && attach_ID.ContentLength > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(attach_ID.FileName);
                string extension = Path.GetExtension(attach_ID.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_ID = FilePath() + fileName;
                fileName = Path.Combine(Server.MapPath(FilePath()), fileName);
                attach_ID.SaveAs(fileName);
            }
            else
            {
                fileName = null;
            }


            // Attach Voucher Document
            if (attach_Doc != null && attach_Doc.ContentLength > 0)
            {
                fileName2 = Path.GetFileNameWithoutExtension(attach_Doc.FileName);
                string extension = Path.GetExtension(attach_Doc.FileName);
                fileName2 = fileName2 + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_Doc = FilePath() + fileName2;
                fileName2 = Path.Combine(Server.MapPath(FilePath()), fileName2);
                attach_Doc.SaveAs(fileName2);
            }
            else
            {
                fileName2 = null;
            }

            // Attach Receipt
            if (attach_Receipt != null && attach_Receipt.ContentLength > 0)
            {
                fileName3 = Path.GetFileNameWithoutExtension(attach_Receipt.FileName);
                string extension = Path.GetExtension(attach_Receipt.FileName);
                fileName3 = fileName3 + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_Receipt = FilePath() + fileName3;
                fileName3 = Path.Combine(Server.MapPath(FilePath()), fileName3);
                attach_Receipt.SaveAs(fileName3);
            }
            else
            {
                fileName3 = vCAS_issuedChecks.attach_Receipt;
            }


            if (ModelState.IsValid)
            {
                db.VCAS_issuedChecks.Add(new VCAS_issuedChecks
                {
                    datetime = vCAS_issuedChecks.datetime,
                    amount = vCAS_issuedChecks.amount,
                    checkNo = vCAS_issuedChecks.checkNo,
                    issuer = vCAS_issuedChecks.issuer,
                    receiverName = vCAS_issuedChecks.receiverName,
                    FK_receiverID_TypesId = vCAS_issuedChecks.FK_receiverID_TypesId,
                    receiverID = vCAS_issuedChecks.receiverID,
                    signature = vCAS_issuedChecks.signature,
                    FK_debitAccountsId = vCAS_issuedChecks.FK_debitAccountsId,
                    attach_ID = vCAS_issuedChecks.attach_ID,
                    FK_location = vCAS_issuedChecks.FK_location,
                    approver = vCAS_issuedChecks.approver,
                    approval = vCAS_issuedChecks.approval,
                    pending_approval = vCAS_issuedChecks.pending_approval,
                    auth_signature = vCAS_issuedChecks.auth_signature,
                    complete = vCAS_issuedChecks.complete,
                    FK_expensesId = vCAS_issuedChecks.FK_expensesId,
                    attach_Doc = vCAS_issuedChecks.attach_Doc,
                    attach_Receipt = vCAS_issuedChecks.attach_Receipt,
                    comments = vCAS_issuedChecks.comments
                });
                db.SaveChanges();

                // Reducing Balance on debitAccounts
                // EXEC Stored Procedure - usp_DebitReducingbalance
                //////////////////////////////////////////////////
                int lastID = db.VCAS_issuedChecks.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                SqlParameter[] Parameters =
                {
                    new SqlParameter("@p_amt", vCAS_issuedChecks.amount),
                    new SqlParameter("@p_icID", lastID),
                    new SqlParameter("@p_daID", vCAS_issuedChecks.FK_debitAccountsId)
                };
                db.Database.ExecuteSqlCommand("EXEC usp_DebitReducingbalance @p_amt, @p_icID, @p_daID", Parameters);
                //////////////////////////////////////////////////
                return RedirectToAction("Index", "Home", null);
            }

            ViewBag.issuer = db.VCAS_users.Where(x => x.userName == User.Identity.Name).Select(x => x.fullName).FirstOrDefault();
            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name", vCAS_issuedChecks.FK_receiverID_TypesId);
            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name", vCAS_issuedChecks.FK_expensesId);
            // var fk_locSession = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.FK_location).FirstOrDefault();
            var debitAcctSession = db.VCAS_debitAccounts.Where(x => x.FK_location == loc).ToList();
            ViewBag.FK_debitAccountsId = new SelectList(debitAcctSession, "Id", "name", vCAS_issuedChecks.FK_debitAccountsId);

            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View(vCAS_issuedChecks);
        }


        // GET: issuedChecks/Edit/5
        [CustomAuthorize(Roles = "admin, approver")]
        public ActionResult EditApprovals(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_issuedChecks vCAS_issuedChecks = db.VCAS_issuedChecks.Find(id);
            if (vCAS_issuedChecks == null)
            {
                return HttpNotFound();
            }

            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name", vCAS_issuedChecks.FK_expensesId);
            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name", vCAS_issuedChecks.FK_receiverID_TypesId);
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_issuedChecks.FK_debitAccountsId);
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View(vCAS_issuedChecks);
        }

        // POST: issuedChecks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditApprovals([Bind(Include = "Id,datetime,amount,checkNo,issuer,receiverName,FK_receiverID_TypesId,receiverID,signature,FK_debitAccountsId,attach_ID,FK_location,approver, approval, pending_approval, auth_signature, complete, FK_expensesId, attach_Doc, attach_Receipt, comments")] VCAS_issuedChecks vCAS_issuedChecks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_issuedChecks).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name", vCAS_issuedChecks.FK_expensesId);
            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name", vCAS_issuedChecks.FK_receiverID_TypesId);
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_issuedChecks.FK_debitAccountsId);
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View(vCAS_issuedChecks);
        }

        // GET: issuedChecks/Edit/5
        [CustomAuthorize(Roles = "admin, cashier")]
        public ActionResult EditDelivery(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_issuedChecks vCAS_issuedChecks = db.VCAS_issuedChecks.Find(id);
            if (vCAS_issuedChecks == null)
            {
                return HttpNotFound();
            }

            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name", vCAS_issuedChecks.FK_expensesId);

            var dis_user0 = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.FK_location).FirstOrDefault();
            var a2 = db.VCAS_issuedChecks.Where(x => x.complete == false && x.auth_signature != null && x.approval == 1 && x.FK_location == dis_user0);
            ViewBag.pending_delivery = a2.Count();

            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name", vCAS_issuedChecks.FK_receiverID_TypesId);
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_issuedChecks.FK_debitAccountsId);
            var dis_user1 = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user1, "Id", "name");
            return View(vCAS_issuedChecks);
        }

        // POST: issuedChecks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDelivery([Bind(Include = "Id,datetime,amount,checkNo,issuer,receiverName,FK_receiverID_TypesId,receiverID,signature,FK_debitAccountsId,attach_ID,FK_location,approver, approval, pending_approval, auth_signature, complete, FK_expensesId, attach_Doc, attach_Receipt, comments")] VCAS_issuedChecks vCAS_issuedChecks, HttpPostedFileBase attach_ID, HttpPostedFileBase attach_Doc, HttpPostedFileBase attach_Receipt)
        {
            // Attach Receiver's ID
            if (attach_ID != null && attach_ID.ContentLength > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(attach_ID.FileName);
                string extension = Path.GetExtension(attach_ID.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_ID = FilePath() + fileName;
                fileName = Path.Combine(Server.MapPath(FilePath()), fileName);
                attach_ID.SaveAs(fileName);
            }
            else
            {
                fileName = vCAS_issuedChecks.attach_ID;
            }


            // Attach Voucher Document
            if (attach_Doc != null && attach_Doc.ContentLength > 0)
            {
                fileName2 = Path.GetFileNameWithoutExtension(attach_Doc.FileName);
                string extension = Path.GetExtension(attach_Doc.FileName);
                fileName2 = fileName2 + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_Doc = FilePath() + fileName2;
                fileName2 = Path.Combine(Server.MapPath(FilePath()), fileName2);
                attach_Doc.SaveAs(fileName2);
            }
            else
            {
                fileName2 = vCAS_issuedChecks.attach_Doc;
            }

            // Attach Receipt
            if (attach_Receipt != null && attach_Receipt.ContentLength > 0)
            {
                fileName3 = Path.GetFileNameWithoutExtension(attach_Receipt.FileName);
                string extension = Path.GetExtension(attach_Receipt.FileName);
                fileName3 = fileName3 + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_Receipt = FilePath() + fileName3;
                fileName3 = Path.Combine(Server.MapPath(FilePath()), fileName3);
                attach_Receipt.SaveAs(fileName3);
            }
            else
            {
                fileName3 = vCAS_issuedChecks.attach_Receipt;
            }

            if (ModelState.IsValid)
            {
                db.Entry(vCAS_issuedChecks).State = System.Data.Entity.EntityState.Modified;
                // Reducing Balance on debitAccounts
                // EXEC Stored Procedure - usp_DebitReducingbalance
                //////////////////////////////////////////////////
                SqlParameter[] Parameters =
{
                    new SqlParameter("@p_amt", vCAS_issuedChecks.amount),
                    new SqlParameter("@p_icID", vCAS_issuedChecks.Id),
                    new SqlParameter("@p_daID", vCAS_issuedChecks.FK_debitAccountsId)
                    };
                db.Database.ExecuteSqlCommand("EXEC usp_DebitReducingbalance @p_amt, @p_icID, @p_daID", Parameters);
                //////////////////////////////////////////////////
                db.SaveChanges();
                return RedirectToAction("Delivery");
            }

            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name", vCAS_issuedChecks.FK_expensesId);

            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name", vCAS_issuedChecks.FK_receiverID_TypesId);
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_issuedChecks.FK_debitAccountsId);
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View(vCAS_issuedChecks);
        }

        // GET: issuedChecks/Edit/5
        [CustomAuthorize(Roles = "cashier, admin")]
        public ActionResult EditEntry(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_issuedChecks vCAS_issuedChecks = db.VCAS_issuedChecks.Find(id);
            if (vCAS_issuedChecks == null)
            {
                return HttpNotFound();
            }

            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name", vCAS_issuedChecks.FK_expensesId);

            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name", vCAS_issuedChecks.FK_receiverID_TypesId);
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_issuedChecks.FK_debitAccountsId);
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View(vCAS_issuedChecks);
        }

        // POST: issuedChecks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEntry([Bind(Include = "Id,datetime,amount,checkNo,issuer,receiverName,FK_receiverID_TypesId,receiverID,signature,FK_debitAccountsId,attach_ID,FK_location,approver, approval, pending_approval, auth_signature, complete, FK_expensesId, attach_Doc, attach_Receipt, comments")] VCAS_issuedChecks vCAS_issuedChecks, HttpPostedFileBase attach_ID, HttpPostedFileBase attach_Doc, HttpPostedFileBase attach_Receipt)
        {
            // Attach Receiver's ID
            if (attach_ID != null && attach_ID.ContentLength > 0)
            {
                fileName = Path.GetFileNameWithoutExtension(attach_ID.FileName);
                string extension = Path.GetExtension(attach_ID.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_ID = FilePath() + fileName;
                fileName = Path.Combine(Server.MapPath(FilePath()), fileName);
                attach_ID.SaveAs(fileName);
            }
            else
            {
                fileName = vCAS_issuedChecks.attach_ID;
            }


            // Attach Voucher Document
            if (attach_Doc != null && attach_Doc.ContentLength > 0)
            {
                fileName2 = Path.GetFileNameWithoutExtension(attach_Doc.FileName);
                string extension = Path.GetExtension(attach_Doc.FileName);
                fileName2 = fileName2 + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_Doc = FilePath() + fileName2;
                fileName2 = Path.Combine(Server.MapPath(FilePath()), fileName2);
                attach_Doc.SaveAs(fileName2);
            }
            else
            {
                fileName2 = vCAS_issuedChecks.attach_Doc;
            }

            // Attach Receipt
            if (attach_Receipt != null && attach_Receipt.ContentLength > 0)
            {
                fileName3 = Path.GetFileNameWithoutExtension(attach_Receipt.FileName);
                string extension = Path.GetExtension(attach_Receipt.FileName);
                fileName3 = fileName3 + DateTime.Now.ToString("yymmssffff") + extension;
                vCAS_issuedChecks.attach_Receipt = FilePath() + fileName3;
                fileName3 = Path.Combine(Server.MapPath(FilePath()), fileName3);
                attach_Receipt.SaveAs(fileName3);
            }
            else
            {
                fileName3 = vCAS_issuedChecks.attach_Receipt;
            }

            if (ModelState.IsValid)
            {
                db.Entry(vCAS_issuedChecks).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            // Fixed location
            // ----------------------------------------
            var u = db.VCAS_session.Where(x => x.username == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.username).FirstOrDefault();
            int loc = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            // ----------------------------------------

            ViewBag.FK_expensesId = new SelectList((from e in db.VCAS_REF_expense_location.Where(x => x.FK_councilId == loc) select new { id = e.Id, e.VCAS_expenses.name }), "id", "name", vCAS_issuedChecks.FK_expensesId);

            ViewBag.FK_receiverID_TypesId = new SelectList(db.VCAS_REF_receiverID_Types, "Id", "name", vCAS_issuedChecks.FK_receiverID_TypesId);
            ViewBag.FK_debitAccountsId = new SelectList(db.VCAS_debitAccounts, "Id", "name", vCAS_issuedChecks.FK_debitAccountsId);
            var dis_user = db.VCAS_district.Where(x => x.VCAS_users.userName == System.Web.HttpContext.Current.User.Identity.Name).Select(x => x.VCAS_council).ToList();
            ViewBag.FK_location = new SelectList(dis_user, "Id", "name");
            return View(vCAS_issuedChecks);
        }

        // GET: issuedChecks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_issuedChecks vCAS_issuedChecks = db.VCAS_issuedChecks.Find(id);
            if (vCAS_issuedChecks == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_issuedChecks);
        }

        // POST: issuedChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_issuedChecks vCAS_issuedChecks = db.VCAS_issuedChecks.Find(id);
            db.VCAS_issuedChecks.Remove(vCAS_issuedChecks);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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