using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VCAS.Controllers
{
    public class errorController : Controller
    {
        [HandleError]
        // GET: Error
        public ActionResult BadRequest()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }

        public ActionResult Forbidden()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }

        public ActionResult InternalServerError()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }

        public ActionResult NotFound()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }

        public ActionResult NotImplemented()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }

        public ActionResult ServerBusyOrDown()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }

        public ActionResult ServerUnavailable()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }

        public ActionResult Timeout()
        {
            ViewBag.response = HttpContext.Response.ToString();
            return View();
        }
    }
}