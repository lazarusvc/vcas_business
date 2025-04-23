using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class processesController : Controller
    {
        private ModelContainer db = new ModelContainer();

        [HttpPost]
        public ActionResult Action(FormCollection fm)
        {
            string spName = fm["n"].ToString();
            var model = db.VCAS_REF_processes.Include(v => v.VCAS_processes).Where(x => x.VCAS_processes.sp_name == spName && x.VCAS_processes.FK_location == GlobalSession.Location);
            List<string> pKEYS = model.Select(x => x.param_key).ToList();
            List<string> pVALS = model.Select(x => x.param_value).ToList();
            List<string> pDTYPE = model.Select(x => x.param_dataType).ToList();

            // EXEC Stored Procedures
            // =================================================
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["ModelContainerUpload"].ConnectionString;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = spName; 

            for (int i = 0; i < pKEYS.Count; i++)
            {
                string key = pKEYS[i].ToString();
                string val = pVALS[i].ToString();
                string dtype = pDTYPE[i];

                switch (dtype)
                {
                    case "Int":
                        cmd.Parameters.Add(key, SqlDbType.Int).Value = int.Parse(val);
                        break;
                    case "Float":
                        cmd.Parameters.Add(key, SqlDbType.Float).Value = float.Parse(val);
                        break;
                    case "Text":
                        cmd.Parameters.Add(key, SqlDbType.Text).Value = val;
                        break;
                    default:
                        throw new InvalidOperationException($"Unsupported data type: {dtype}");
                }
            }

            cnn.Open();
            ViewData["spResults"] = cmd.ExecuteNonQuery();
            return RedirectToAction("ActionComplete");
        }
        public ActionResult ActionParams(string n)
        {
            ViewBag.n = n;
            return View();
        }
        
        [HttpPost]
        public ActionResult ActionParamsUpdate(int? id, FormCollection fm)
        {
            string pVAL = Convert.ToString(fm["param_value"]);
            string spName = Convert.ToString(fm["spName"]);

            db.Database.ExecuteSqlCommand(String.Format(@"
             UPDATE dbo.VCAS_REF_processes
             SET param_value = '{0}'
             WHERE Id = {1} 
            ", pVAL, id));
            return RedirectToAction("ActionParams", new { n = spName });
        }
        public ActionResult ActionComplete()
        {
            return View();
        }


        // GET: processes
        public ActionResult Index()
        {
            var vCAS_processes = db.VCAS_processes.Include(v => v.VCAS_council);
            return View(vCAS_processes.ToList());
        }

        // GET: processes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_processes vCAS_processes = db.VCAS_processes.Find(id);
            if (vCAS_processes == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_processes);
        }

        // GET: processes/Create
        public ActionResult Create()
        {
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name");
            return View();
        }

        // POST: processes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,sp_name,FK_location")] VCAS_processes vCAS_processes)
        {
            if (ModelState.IsValid)
            {
                db.VCAS_processes.Add(vCAS_processes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_processes.FK_location);
            return View(vCAS_processes);
        }

        // GET: processes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_processes vCAS_processes = db.VCAS_processes.Find(id);
            if (vCAS_processes == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_processes.FK_location);
            return View(vCAS_processes);
        }

        // POST: processes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,sp_name,FK_location")] VCAS_processes vCAS_processes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vCAS_processes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_location = new SelectList(db.VCAS_council, "Id", "name", vCAS_processes.FK_location);
            return View(vCAS_processes);
        }

        // GET: processes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_processes vCAS_processes = db.VCAS_processes.Find(id);
            if (vCAS_processes == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_processes);
        }

        // POST: processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VCAS_processes vCAS_processes = db.VCAS_processes.Find(id);
            db.VCAS_processes.Remove(vCAS_processes);
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
