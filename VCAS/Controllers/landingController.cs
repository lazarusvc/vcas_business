using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VCAS.Controllers
{
public class landingController : Controller
{
    // GET: Landing Page
    // ==========================
    public ActionResult Index()
    {
        return View();
    }
}
}