using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class communicationsController : Controller
    {
        private ModelContainer db = new ModelContainer();

        // GET: communications
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            // DB Config values
            // ********************************************
            string sid = db.VCAS_council.Select(x => x.twilio_SID).FirstOrDefault();
            string token = db.VCAS_council.Select(x => x.twilio_TOKEN).FirstOrDefault();
            string number = db.VCAS_council.Select(x => x.twilio_SID).FirstOrDefault();
            string xml = db.VCAS_council.Select(x => x.twilio_SID).FirstOrDefault();

            // Fix for error: The request was aborted: Could not create SSL/TLS secure channel
            // ********************************************
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Twilio secrets
            // ********************************************
            string TWILIO_ACCOUNT_SID = sid;
            string TWILIO_AUTH_TOKEN = token;
            TwilioClient.Init(TWILIO_ACCOUNT_SID, TWILIO_AUTH_TOKEN);

            // VOICE CALL
            // ********************************************
            var to = new PhoneNumber(form["tel"].ToString());
            var from = new PhoneNumber(number);
            var call = CallResource.Create(to, from,
               url: new Uri(xml));

            // SMS TEXT
            // ********************************************
            var message = MessageResource.Create(
                new PhoneNumber(form["tel"].ToString()),
                from: new PhoneNumber(number),
                body: form["body"].ToString()
            );

            ViewBag.Response = "";
            if (!String.IsNullOrEmpty(call.CallerName))
            {
                ViewBag.Response = call.CallerName;
            }
            
            return RedirectToAction("Index");
        }
    }
}
