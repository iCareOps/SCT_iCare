using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using conekta;

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

            string ordenReal = "ord_2pYTnMjcr6XeqMQYX";
            string ordenReal2 = "ord_2paV4oQ4sw68AFADV";
            //Intento de traer datos de conekta
            Order ordenes = new Order().find(ordenReal2);
            var nombre = ordenes.customer_info.name;
            string nom = nombre;
            var status = ordenes.payment_status;
            string ps = status;
            //return View(solicitudes);
            return View(ordenes);
        }
    }
}