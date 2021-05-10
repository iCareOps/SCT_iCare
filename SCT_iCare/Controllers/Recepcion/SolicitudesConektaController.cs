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
using Newtonsoft.Json.Serialization;

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

            var objeto = JObject.Parse(ConvertirOrden());

            var ordenes3 = new Order().where(new JObject(objeto));

            return View(ordenes2);
        }

        private string ConvertirOrden()
        {
            var orden = new ConektaList(typeof(conekta.ConektaList))
            {
                next_page_url = "https://admin.conekta.com/orders?"
            };

            string jsonOrdenes = JsonConvert.SerializeObject(orden, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return jsonOrdenes;
        }
    }
}