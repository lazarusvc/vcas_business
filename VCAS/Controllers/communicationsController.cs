using Newtonsoft.Json;
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
        // DB Config values
        // ********************************************
        private static ModelContainer db = new ModelContainer();
        string sid = db.VCAS_council.Select(x => x.twilio_SID).FirstOrDefault();
        string token = db.VCAS_council.Select(x => x.twilio_TOKEN).FirstOrDefault();
        string number = db.VCAS_council.Select(x => x.twilio_NUMBER).FirstOrDefault();        

        // Home Page
        // ********************************************
        public ActionResult Index()
        {
            // Fix for error: The request was aborted: Could not create SSL/TLS secure channel
            // ********************************************
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Fetch Twilio balance
            // ****************************************
            string url = "https://api.twilio.com/2010-04-01/Accounts/" + sid + "/Balance.json";
            var client = new WebClient();
            client.Credentials = new NetworkCredential(sid, "8d4a250e23a649353d7f22945f5258c7");
            string responseString = client.DownloadString(url);

            dynamic responseObject = JsonConvert.DeserializeObject<object>(responseString);
            double accountBalance = Double.Parse(responseObject["balance"].Value);
            ViewBag.accountBalance = Math.Round((double)accountBalance, 2);

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
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
            string xml = "https://app.vouchcast.com" + this.Url.Action("xml");
            var to = new PhoneNumber(form["tel"].ToString());
            var from = new PhoneNumber(number);
            var call = CallResource.Create(to, from,
               url: new Uri(xml));

            // SMS TEXT
            // ********************************************
            //var message = MessageResource.Create(
            //    new PhoneNumber(form["tel"].ToString()),
            //    from: new PhoneNumber(number),
            //    body: form["body"].ToString()
            //);

            ViewBag.Response = "";
            if (!String.IsNullOrEmpty(call.CallerName))
            {
                ViewBag.Response = call.CallerName;
            }
            
            return RedirectToAction("Index");
        }

        // XML 
        // ********************************************
        public ActionResult xml(string none)
        {
            //Set the Response properties.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/xml";

            //Read the XML file contents as String.
            string xmlFileName = db.VCAS_council.Select(x => x.twilio_XML).FirstOrDefault();
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/Content/Uploads/" + xmlFileName));
            return Content(xml);
        }

        [HttpPost]
        public ActionResult xml()
        {
            //Set the Response properties.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/xml";

            //Read the XML file contents as String.
            string xmlFileName = db.VCAS_council.Select(x => x.twilio_XML).FirstOrDefault();
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/Content/Uploads/" + xmlFileName));
            return Content(xml);
        }
    }
}
