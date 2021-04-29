using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using conekta;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SCT_iCare.Controllers.Recepcion
{
    public class SolicitudesConektaController : Controller
    {
        public static void GetApiKey()
        {
            conekta.Api.apiKey = ConfigurationManager.AppSettings["conekta"];
            conekta.Api.version = "2.0.0";
            conekta.Api.locale = "es";
        }

        // GET: SolicitudesConekta
        public ActionResult Index()
        {
            GetApiKey();

            var ordenes2 = new Order().where(new JObject());

            var che = new conekta.Models.Checkout().where(new JObject());

            var link = from c in che select c;
            foreach(var item in link)
            {
                string url = item.url;
            }

            return View(ordenes2);
        }
    }
}