 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class AccountController : Controller
    {
        // ===========================
        // LOGIN PAGE
        // ===========================
        private ModelContainer db = new ModelContainer();
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            @TempData["Alert"] = "";
            @TempData["Alert-Color"] = "success";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // ============================================================
                // INTERNAL AUTHENTICATION

                // Check & Verify Hash passwords in DB
                string orig_pwd = db.VCAS_users.Where(x => x.userName == model.UserName).Select(x => x.password).FirstOrDefault();
                var IsPwdVerified = BCrypt.Net.BCrypt.Verify(model.Password, orig_pwd);

                // Check Valid user in DB
                bool IsValidUser = db.VCAS_users.Any(u => u.userName == model.UserName);

                if (IsValidUser && IsPwdVerified)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    // Assign Global variable for signed in username
                    var loc = db.VCAS_district.Where(x => x.VCAS_users.userName == model.UserName).Select(x => x.FK_location).FirstOrDefault();
                    var rol = db.VCAS_users.Where(x => x.userName == model.UserName).Select(x => x.VCAS_REF_userRoles.Id).FirstOrDefault();

                    // EXEC Stored Procedure - usp_UpdateSession
                    //SqlParameter[] Parameters =
                    //{
                    //    new SqlParameter("@p_rol", rol),
                    //    new SqlParameter("@p_loc", loc),
                    //    new SqlParameter("@p_usr", model.UserName)
                    //};
                    //db.Database.ExecuteSqlCommand("EXEC usp_UpdateSession @p_rol, @p_loc, @p_usr", Parameters);

                    // Store username in Session
                    Session["u"] = Convert.ToString(model.UserName);

                    // Redirect to last page logoff
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        ViewBag.ReturnUrl = returnUrl;
                        return Redirect(returnUrl);
                    }

                    // Pass: Return to home
                    return RedirectToAction("Index", "Home");
                }

                // ============================================================
                // EXTERNAL LDAP AUTHENTICATION

                // TODO: Uncomment for Active Directory

                //else if (Membership.ValidateUser(model.UserName, model.Password))
                //{
                //    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    
                //    // Assign Global variable for signed in username
                //    var loc = db.VCAS_district.Where(x => x.VCAS_users.userName == model.UserName).Select(x => x.FK_location).FirstOrDefault();
                //    var rol = db.VCAS_users.Where(x => x.userName == model.UserName).Select(x => x.VCAS_REF_userRoles.Id).FirstOrDefault();

                //    // EXEC Stored Procedure - usp_UpdateSession
                //    SqlParameter[] Parameters =
                //    {
                //        new SqlParameter("@p_rol", rol),
                //        new SqlParameter("@p_loc", loc),
                //        new SqlParameter("@p_usr", model.UserName)
                //    };
                //    db.Database.ExecuteSqlCommand("EXEC usp_UpdateSession @p_rol, @p_loc, @p_usr", Parameters);

                //    // Store username in Session
                //    Session["u"] = Convert.ToString(model.UserName);

                //    // Pass: Return to home
                //    return RedirectToAction("Index", "Home");
                //}
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        // ===========================
        // LOGOUT PAGE
        // ===========================
        public ActionResult Logout()
        {
            GlobalSession.User = null;
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        // ===========================
        // Session Timeout
        // ===========================
        [HttpPost]
        public JsonResult SessionTimeout()
        {
            return new JsonResult
            {
                Data = "Beat Generated"
            };
        }
    }
}