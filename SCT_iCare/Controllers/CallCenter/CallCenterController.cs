using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using conekta;
using System.Configuration;
using Newtonsoft.Json.Serialization;

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
            var consultorios = from c in db.Consultorios select c;

            return View(consultorios);
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

        private string ConvertirProductos(string consultorio)
        {
            var product = new LineItem()
            {
                name = "EPI SCT " + consultorio,
                unit_price = 258800,
                quantity = 1
            };

            string jsonProductos = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return jsonProductos;
        }

        private long FechaExpira()
        {
            DateTime treintaDias = DateTime.Now.AddDays(2);
            long marcaTiempo = (Int64)(treintaDias.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            //string tiempo = marcaTiempo.ToString();
            return marcaTiempo;
        }


        public ActionResult Orden(string nombre, string email, string telefono, string consultorio)
        {
            GetApiKey();

            //Por ahora se llena hace un switch para seleccionar el nombre del producto concatenado con el consultorio ya que se debe hacer un método para 
            //serializar el arreglo y así traer todo desde base de datos

            switch (consultorio)
            {
                case "VERACRUZ":
                    Order order = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(nombre, email, telefono) + @",
                      ""line_items"": [{
                      ""name"": ""Consulta EPI (VERACRUZ)"",
                      ""unit_price"": 258800,
                      ""quantity"": 1
                      }]
                      }");

                    order.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""oxxo_cash""
                    },
                    ""amount"": 258800
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

                    return View(detallesOrden);

                case "LINDAVISTA":
                    Order order1 = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(nombre, email, telefono) + @",
                      ""line_items"": [{
                      ""name"": ""Consulta EPI (HOSPITAL ANGELES LINDAVISTA)"",
                      ""unit_price"": 258800,
                      ""quantity"": 1
                      }]
                      }");

                    order1.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""oxxo_cash""
                    },
                    ""amount"": 258800
                    }");

                    var orden1 = new Order().find(order1.id);

                    var detallesOrden1 = new Order()
                    {
                        id = orden1.id,
                        customer_info = orden1.customer_info,
                        line_items = orden1.line_items,
                        amount = orden1.amount,
                        charges = orden1.charges
                    };

                    ViewBag.Orden = order1.id;

                    return View(detallesOrden1);

                case "UNIVERSIDAD":
                    Order order2 = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(nombre, email, telefono) + @",
                      ""line_items"": [{
                      ""name"": ""Consulta EPI (CLINICA UNIVERSIDAD)"",
                      ""unit_price"": 258800,
                      ""quantity"": 1
                      }]
                      }");

                    order2.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""oxxo_cash""
                    },
                    ""amount"": 258800
                    }");

                    var orden2 = new Order().find(order2.id);

                    var detallesOrden2 = new Order()
                    {
                        id = orden2.id,
                        customer_info = orden2.customer_info,
                        line_items = orden2.line_items,
                        amount = orden2.amount,
                        charges = orden2.charges
                    };

                    ViewBag.Orden = order2.id;

                    return View(detallesOrden2);

                case "SATELITE":
                    Order order3 = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(nombre, email, telefono) + @",
                      ""line_items"": [{
                      ""name"": ""Consulta EPI (CLINICA SATELITE)"",
                      ""unit_price"": 258800,
                      ""quantity"": 1
                      }]
                      }");

                    order3.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""oxxo_cash""
                    },
                    ""amount"": 258800
                    }");

                    var orden3 = new Order().find(order3.id);

                    var detallesOrden3 = new Order()
                    {
                        id = orden3.id,
                        customer_info = orden3.customer_info,
                        line_items = orden3.line_items,
                        amount = orden3.amount,
                        charges = orden3.charges
                    };

                    ViewBag.Orden = order3.id;

                    return View(detallesOrden3);

                default:
                    return View();
            }



            //Order order = new conekta.Order().create(@"{
            //          ""currency"":""MXN"",
            //          ""customer_info"": " + ConvertirCliente(nombre, email, telefono) + @",
            //          ""line_items"": ["+ ConvertirProductos(consultorio) + @"]
            //          }");

            //order.createCharge(@"{
            //        ""payment_method"": {
            //        ""type"": ""oxxo_cash""
            //        },
            //        ""amount"": 258800
            //        }");

            //return View();
        }


//        public EntRespuestaConekta CrearOrdenOxxo(EntOrdenDetalle orden, string fecha, EntUsuarios usuario, int idHorario)
//        {
//            GetApiKey();
//            orden.Total = SumaTotal(orden.Productos).ToString();
//            EntRespuestaConekta respuesta = new EntRespuestaConekta();
//            try
//            {
//                if (orden != null)
//                {
//                    Order nOrder = new Order().create(@"{
//""currency"":""MXN"",
//""customer_info"": " + ConvertirCliente(orden.Cliente) + @",
//""line_items"": " + ConvertirProductos(orden.Productos) + @",
//""charges"": [{
//""payment_method"": {
//""type"": ""oxxo_cash"",
//""expires_at"": " + FechaExpira() + @"
//},
//""amount"": " + orden.Total + @"
//}]
//}");
//                    respuesta.IdOrden = nOrder.id;
//                    respuesta.Total = nOrder.amount.ToString();
//                    respuesta.Moneda = nOrder.currency;
//                    foreach (JObject obj in nOrder.charges.data)
//                    {
//                        var data = JsonConvert.DeserializeObject<Referencia>(obj.GetValue("payment_method").ToString());
//                        respuesta.MetodoPago = data.service_name;
//                        respuesta.Referencia = data.reference;
//                    }
//                    respuesta = FormatoRespuestaConekta(respuesta);
//                    //Todo Ok, se debe actualizar la orden con el idConekta en la tabla Orden
//                    resultadoJson = clsFachada.ActualizarDatosConekta(orden, respuesta, fecha, idHorario);
//                    respuesta.result = true;
//                }
//                else
//                {
//                    respuesta.result = false;
//                    respuesta.ErrorMessage = "Ocurrio un problema interno al guardar la solicitud";
//                }
//                return respuesta;
//            }
//            catch (ConektaException e)
//            {
//                foreach (JObject obj in e.details)
//                {
//                    respuesta.ErrorMessage = obj.GetValue("message").ToString();
//                }
//                return respuesta;
//            }
//        }

//        public EntRespuestaConekta CrearOrdenLink(EntOrdenDetalle orden, string fecha, EntUsuarios usuario, int idHorario)
//        {

//            GetApiKey();
//            orden.Total = SumaTotal(orden.Productos).ToString();
//            EntRespuestaConekta respuesta = new EntRespuestaConekta();
//            try
//            {

//                if (orden != null)
//                {



//                    PaymentLink nOrder = new PaymentLink().create(@"{
//                      ""name"":""Link de pago iCare"",
//                      ""type"":""PaymentLink"",
//                      ""recurrent"": false,
//                      ""expired_at"": " + FechaExpira() + @",
//                      ""allowed_payment_methods"": [""cash"", ""card"", ""bank_transfer""],
//                      ""needs_shipping_contact"": true ,
//                      ""monthly_installments_enabled"": false ,
                       
//                      ""order_template"": {
//                      ""line_items"": " + ConvertirProductos(orden.Productos) + @",

//                      ""currency"":""MXN"",
//                      ""customer_info"": " + ConvertirCliente(orden.Cliente) + @"
                             
//                      }
//                    }");


//                    respuesta.IdOrden = nOrder.id;
//                    respuesta.url = nOrder.url;

//                    respuesta.expires_at = nOrder.expires_at;
//                    respuesta.correo = orden.Cliente.Email;

//                    //Todo Ok, se debe actualizar la orden con el idConekta en la tabla Orden
//                    resultadoJson = clsFachada.ActualizarDatosConekta(orden, respuesta, fecha, idHorario);

//                    //enviar correo 

//                    // recupero el apikewy de sengrid para conectar y el id del template
//                    var apiKey = ConfigurationManager.AppSettings["sengridApiKey"];
//                    var templateIdConfirma = ConfigurationManager.AppSettings["templateIdConfirma"];









//                    respuesta.result = true;
//                }
//                else
//                {
//                    respuesta.result = false;
//                    respuesta.ErrorMessage = "Ocurrio un problema interno al guardar la solicitud";
//                }
//                return respuesta;

//            }
//            catch (ConektaException e)
//            {
//                foreach (JObject obj in e.details)
//                {
//                    respuesta.ErrorMessage = obj.GetValue("message").ToString();
//                }
//                return respuesta;
//            }
//        }


    }
}