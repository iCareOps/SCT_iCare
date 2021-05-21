using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using conekta;
using System.Configuration;
using Newtonsoft.Json.Serialization;
using BarcodeLib;

namespace SCT_iCare.Controllers.CallCenter
{
    public class CallCenterController : Controller
    {
        SCTiCareEntities1 db = new SCTiCareEntities1();

        public static void GetApiKey()
        {
            conekta.Api.apiKey = ConfigurationManager.AppSettings["conekta"];
            conekta.Api.version = "2.0.0";
            conekta.Api.locale = "es";
        }

        // GET: CallCenter
        public ActionResult Index()
        {
            var sucursales = from c in db.Sucursales select c;

            return View(sucursales);
        }

        public ActionResult IndexSPEI()
        {
            var sucursales = from c in db.Sucursales select c;

            return View(sucursales);
        }

        public ActionResult GetRecepcion()
        {
            var consultorios = from c in db.Consultorios select c;

            return View(consultorios);
        }

        private string ConvertirCliente(string nombre, string email, string telefono)
        {
            var newClient = new Customer()
            {
                name = nombre,
                email = email,
                phone = telefono
            };
            string jsonClient = JsonConvert.SerializeObject(newClient, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return jsonClient;
        }

        private string ConvertirProductos1(string consultorio)
        {
            string producto = "Consulta EPI (" + consultorio + ")";
            var product = new LineItem()
            {
                name = "Consulta EPI" + consultorio,
                //unit_price = 258800,
                //quantity = 1
            };

            string jsonProductos = JsonConvert.SerializeObject(producto, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return jsonProductos;
        }

        private int ConvertirProductos2(string precio)
        {
            int producto = Convert.ToInt32(precio) * 100;

            int jsonProductos = Convert.ToInt32(JsonConvert.SerializeObject(producto, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            return jsonProductos;
        }

        private long FechaExpira()
        {
            DateTime treintaDias = DateTime.Now.AddDays(2);
            long marcaTiempo = (Int64)(treintaDias.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            //string tiempo = marcaTiempo.ToString();
            return marcaTiempo;
        }


        public ActionResult Orden(string nombre, string email, string telefono, string consultorio, string precio, string tipoPago)
        {
            GetApiKey();

            if(tipoPago == "OXXO")
            {
                Order order = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(nombre, email, telefono) + @",
                      ""line_items"": [{
                      ""name"": " + ConvertirProductos1(consultorio) + @",
                      ""unit_price"": " + ConvertirProductos2(precio) + @",
                      ""quantity"": 1
                      }]
                      }");

                order.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""oxxo_cash""
                    },
                    ""amount"": " + ConvertirProductos2(precio) + @"
                    }");

                var orden = new Order().find(order.id);

                var detallesOrden = new Order()
                {
                    id = orden.id,
                    customer_info = orden.customer_info,
                    line_items = orden.line_items,
                    amount = orden.amount,
                    charges = orden.charges
                };

                ViewBag.Orden = order.id;
                ViewBag.Metodo = "OXXO";

                return View(detallesOrden);
            }
            else
            {
                Order order = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(nombre, email, telefono) + @",
                      ""line_items"": [{
                      ""name"": " + ConvertirProductos1(consultorio) + @",
                      ""unit_price"": " + ConvertirProductos2(precio) + @",
                      ""quantity"": 1
                      }]
                      }");

                order.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""spei""
                    },
                    ""amount"": " + ConvertirProductos2(precio) + @"
                    }");

                var orden = new Order().find(order.id);

                var detallesOrden = new Order()
                {
                    id = orden.id,
                    customer_info = orden.customer_info,
                    line_items = orden.line_items,
                    amount = orden.amount,
                    charges = orden.charges
                };

                ViewBag.Orden = order.id;
                ViewBag.Metodo = "SPEI";

                return View(detallesOrden);
            }

        }
    }
    
}