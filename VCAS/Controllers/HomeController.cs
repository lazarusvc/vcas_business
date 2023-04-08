using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VCAS.Models;

namespace VCAS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // ==========================================================================================================
        // HOME PAGE
        // =========
        private ModelContainer db = new ModelContainer();

        // App Name
        // ==================
        public static String gloabl_appName()
        {
            ModelContainer db = new ModelContainer();
            if (String.IsNullOrWhiteSpace(db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.app_name).FirstOrDefault()))
            {
                return "";
            }
            else
            {
                return db.VCAS_council.Where(x => x.Id == GlobalSession.Location).Select(x => x.app_name).FirstOrDefault().ToString();
            }
            
        }

        public ActionResult Index()
        {
            // SET Global Session Data
            // ***************************************************************
            var u = db.VCAS_session.Where(x => x.username == User.Identity.Name).Select(x => x.username).FirstOrDefault();
            GlobalSession.User = Convert.ToString(u);
            GlobalSession.Location = Convert.ToInt32(db.VCAS_session.Where(x => x.username == u).Select(x => x.location).FirstOrDefault());
            GlobalSession.Role = Convert.ToString(db.VCAS_session.Where(x => x.username == u).Select(x => x.role).FirstOrDefault());

            // HOME PAGE ACTION BUTTON - BY USER ROLE
            var roleID = db.VCAS_users.Where(x => x.userName == GlobalSession.User).Select(x => x.FK_userRolesId).FirstOrDefault();
            ViewBag.URole = db.VCAS_REF_userRoles.Where(x => x.Id == roleID).Select(x => x.name).FirstOrDefault();


            // Select Organization 
            var districtSession = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.VCAS_council);
            ViewBag.council_select = new SelectList(districtSession.ToList(), "Id", "name");

            // User ID
            ViewBag.userID = db.VCAS_users.Where(x => x.userName == GlobalSession.User).Select(x => x.Id).FirstOrDefault();

            // Form List
            ViewBag.frms = db.VCAS_forms.Where(x => x.FK_location == GlobalSession.Location).ToList();

            // Location ID
            ViewBag.loc = GlobalSession.Location;

            return View();
        }

        public ActionResult IndexJson()
        {
            // Select User Location
            var userloc = db.VCAS_district.Where(x => x.VCAS_users.userName == GlobalSession.User).Select(x => x.FK_location).FirstOrDefault();
            return Json(userloc, JsonRequestBehavior.AllowGet);
        }

        // ==========================================================================================================
        // REPORTS PAGE
        // ============
        [HttpGet]
        public ActionResult Reports()
        {
            var u_role = db.VCAS_users.Where(x => x.userName == GlobalSession.User).Select(x => x.FK_userRolesId).FirstOrDefault();
            ViewBag.rp = new SelectList(db.VCAS_reports.Where(x => x.FK_REF_userRolesId == u_role).ToList(), "name", "desc");

            // Default report page  
            ReportViewer reportViewer = new ReportViewer();
            ViewBag.ReportViewer = reportViewer;

            // Report Name dropdown default item
            if (Session["reportName"] != null)
            {
                ViewBag.rp_default = Session["reportName"];
            } else
            {
                ViewBag.rp_default = "  ";
            }            
            return View();
        }

        [HttpPost]
        public ActionResult Reports(FormCollection form)
        {
            string reportName = Convert.ToString(form["rp"]);
            string rn = db.VCAS_reports.Where(x => x.name == reportName).Select(x => x.name).FirstOrDefault();
            bool paramCheck = db.VCAS_reports.Where(x => x.name == reportName).Select(x => x.paramCheck).FirstOrDefault();
            var u_role = db.VCAS_users.Where(x => x.userName == GlobalSession.User).Select(x => x.FK_userRolesId).FirstOrDefault();
            ViewBag.rp = new SelectList(db.VCAS_reports.Where(x => x.FK_REF_userRolesId == u_role).ToList(), "name", "desc");

            // Report page configurations
            ReportViewer reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true,
                ZoomMode = ZoomMode.PageWidth,
                Width = Unit.Percentage(1000),
                Height = Unit.Pixel(750),
                AsyncRendering = true
            };
            reportViewer.ServerReport.ReportServerUrl = new Uri("http://127.0.0.1/ReportServer/");
            reportViewer.ServerReport.ReportPath = @"/App_Reports/" + rn.Replace(".rdl", "");
            ViewBag.ReportViewer = reportViewer;

            // Fetch report parameters from DB
            foreach (VCAS_REF_reports_params vrp in db.VCAS_REF_reports_params.Where(x => x.VCAS_reports.name == reportName && x.FK_location == GlobalSession.Location))
            {
                if (paramCheck)
                {
                    reportViewer.ServerReport.SetParameters(new ReportParameter(vrp.param_key.Trim(), vrp.param_value.Trim()));
                }
            }

            // Report Name dropdown default item
            Session["reportName"] = reportName;
            return View();
        }

        // ==========================================================================================================
        // INVENTORY PAGE
        // ==============
        public ActionResult Inventory()
        {
            return View();
        }

        // ==========================================================================================================
        // ORDERS PAGE
        // ==============
        public ActionResult Orders()
        {
            return View();
        }
        // ==========================================================================================================
        // ABOUT PAGE
        // ==========
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // ==========================================================================================================
        // CONTACT PAGE
        // ============
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // ==========================================================================================================
        // HELP PAGE
        // =========
        public ActionResult Help()
        {
            return View();
        }

        // ==========================================================================================================
        // ACCESS DENIED PAGE
        // ==================
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}