using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VCAS.Models;

namespace VCAS.Controllers
{
public class landingController : Controller
{
        private ModelContainer db = new ModelContainer();

        // GET: Landing Page
        // ==========================
        public ActionResult Index()
        {
            return View();
        }

        // GET: Landing Page Docu
        // ==========================
        public ActionResult docs(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VCAS_supportDocs vCAS_supportDocs = db.VCAS_supportDocs.Find(id);
            if (vCAS_supportDocs == null)
            {
                return HttpNotFound();
            }
            return View(vCAS_supportDocs);
        }
    }
}