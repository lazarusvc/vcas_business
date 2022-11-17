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
   
    public class usersController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: users
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Index()
        {
            var vCAS_users = db.VCAS_users.Include(v => v.VCAS_REF_userRoles);
            return View(vCAS_users.ToList());
        }

        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_users vCAS_users = db.VCAS_users.Find(id);
            if (vCAS_users == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_users);
        }

        // GET: users/Create
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.FK_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name");
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fullName,userName,email,password,FK_userRolesId")] VCAS_users vCAS_users)
        {
            if (ModelState.IsValid)
            {
                // Hash passwords
                // ==============
                var hash_pwd = BCrypt.Net.BCrypt.HashPassword(vCAS_users.password);

                db.VCAS_users.Add(new VCAS_users {
                    fullName = vCAS_users.fullName,
                    userName = vCAS_users.userName,
                    email = vCAS_users.email,
                    password = hash_pwd,
                    FK_userRolesId = vCAS_users.FK_userRolesId
                });
                db.SaveChanges();

                Session["userID"] = vCAS_users.Id;  
                Session["userName"] = vCAS_users.userName;
                Session["userRoleID"] = vCAS_users.FK_userRolesId;                   

                return RedirectToAction("Create", "district");
            }

            ViewBag.FK_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_users.FK_userRolesId);
            return View(vCAS_users);
        }

        // =======================================================
        // BULK CREATE USERS
        // =======================================================
        [CustomAuthorize(Roles = "admin")]
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
                string path = Server.MapPath("~/Content/Uploads");
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

                DataTable dt = new DataTable();
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
                        sqlBulkCopy.DestinationTableName = "dbo.VCAS_users";

                        // [OPTIONAL]: Map the Excel columns with that of the database table
                        // ================================================================
                        sqlBulkCopy.ColumnMappings.Add("fullName", "fullName");
                        sqlBulkCopy.ColumnMappings.Add("userName", "userName");
                        sqlBulkCopy.ColumnMappings.Add("password", "password");
                        sqlBulkCopy.ColumnMappings.Add("FK_userRolesId", "FK_userRolesId");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // =======================================================
        // DOWNLOAD FILE:: BULK CREATE USERS
        // =======================================================
        [HttpGet]
        public virtual ActionResult DownloadForBulkUsers()
        {
            string fullPath = Path.Combine(Server.MapPath("~/Content/Uploads/VCAS_download_for_bulk_users.xlsx"));
            string fileName = "VCAS_download_for_bulk_users.xlsx";
            return File(fullPath, "application/octet-stream", fileName);
        }

        // =======================================================
        // RESET PASSWORD
        // =======================================================
        [CustomAuthorize(Roles = "cashier, approver")]
        public ActionResult ResetPassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_users vCAS_users = db.VCAS_users.Find(id);
            if (vCAS_users == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_users.FK_userRolesId);
            return View(vCAS_users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword([Bind(Include = "Id,fullName,userName,email,password,FK_userRolesId")] VCAS_users vCAS_users)
        {
            // Hash passwords
            // ==============
            var hash_pwd = BCrypt.Net.BCrypt.HashPassword(vCAS_users.password);
            vCAS_users.password = hash_pwd;

            if (ModelState.IsValid)
            {

                db.Entry(vCAS_users).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }
            ViewBag.FK_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_users.FK_userRolesId);
            return View(vCAS_users);
        }

        // GET: users/Edit/5
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_users vCAS_users = db.VCAS_users.Find(id);
            if (vCAS_users == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_users.FK_userRolesId);
            return View(vCAS_users);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fullName,userName,email,password,FK_userRolesId")] VCAS_users vCAS_users)
        {
            // Hash passwords
            // ==============
            var hash_pwd = BCrypt.Net.BCrypt.HashPassword(vCAS_users.password);
            vCAS_users.password = hash_pwd;

            if (ModelState.IsValid)
            {
                db.Entry(vCAS_users).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_userRolesId = new SelectList(db.VCAS_REF_userRoles, "Id", "name", vCAS_users.FK_userRolesId);
            return View(vCAS_users);
        }

        // GET: users/Delete/5
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_users vCAS_users = db.VCAS_users.Find(id);
            if (vCAS_users == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_users);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_users vCAS_users = db.VCAS_users.Find(id);
            db.VCAS_users.Remove(vCAS_users);
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
