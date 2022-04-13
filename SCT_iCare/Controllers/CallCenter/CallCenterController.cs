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
using SCT_iCare.Models;

using System.IO;
using System.Text;
using System.Globalization;
using System.Net;
using System.Data.Entity;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;

namespace SCT_iCare.Controllers.CallCenter
{
    public class CallCenterController : Controller
    {

        private GMIEntities db = new GMIEntities();

        public static void GetApiKey()
        {
            conekta.Api.apiKey = ConfigurationManager.AppSettings["conekta"];
            conekta.Api.version = "2.0.0";
            conekta.Api.locale = "es";
        }

        // GET: Pacientes
        public ActionResult Index(DateTime? inicio, DateTime? final)
        {
            DateTime thisDate = new DateTime();
            DateTime tomorrowDate = new DateTime();

            DateTime start1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime finish1 = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);

            int nulos = 0;

            if (inicio != null || final != null)
            {
                nulos = 1;
            }

            if (inicio != null)
            {
                DateTime start = Convert.ToDateTime(inicio);
                int year = start.Year;
                int month = start.Month;
                int day = start.Day;

                inicio = new DateTime(year, month, day);
                thisDate = new DateTime(year, month, day);
            }
            if (final != null)
            {
                DateTime finish = Convert.ToDateTime(final).AddDays(1);
                int year = finish.Year;
                int month = finish.Month;
                int day = finish.Day;

                final = new DateTime(year, month, day);
                tomorrowDate = new DateTime(year, month, day);
            }

            var urge = (from i in db.UrgentesCount select i).FirstOrDefault();
            string mes = DateTime.Now.ToString("MMMM");

            if (urge.Mes != mes)
            {
                urge.Mes = mes;
                urge.Contador = 500;

                if (ModelState.IsValid)
                {
                    db.Entry(urge).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            inicio = (inicio ?? start1);
            final = (final ?? finish1);

            ViewBag.Inicio = inicio;
            ViewBag.Final = final;
            ViewBag.Estado = nulos;

            ViewBag.Parameter = "";

            return View(db.Paciente.ToList());
        }

        public ActionResult Vigencias(DateTime? inicio, DateTime? final)
        {
            DateTime thisDate = new DateTime();
            DateTime tomorrowDate = new DateTime();

            DateTime start1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime finish1 = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);

            int nulos = 0;

            if (inicio != null || final != null)
            {
                nulos = 1;
            }

            if (inicio != null)
            {
                DateTime start = Convert.ToDateTime(inicio);
                int year = start.Year;
                int month = start.Month;
                int day = start.Day;

                inicio = new DateTime(year, month, day);
                thisDate = new DateTime(year, month, day);
            }
            if (final != null)
            {
                DateTime finish = Convert.ToDateTime(final).AddDays(1);
                int year = finish.Year;
                int month = finish.Month;
                int day = finish.Day;

                final = new DateTime(year, month, day);
                tomorrowDate = new DateTime(year, month, day);
            }

            var urge = (from i in db.UrgentesCount select i).FirstOrDefault();
            string mes = DateTime.Now.ToString("MMMM");

            if (urge.Mes != mes)
            {
                urge.Mes = mes;
                urge.Contador = 500;

                if (ModelState.IsValid)
                {
                    db.Entry(urge).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            inicio = (inicio ?? start1);
            final = (final ?? finish1);

            ViewBag.Inicio = inicio;
            ViewBag.Final = final;
            ViewBag.Estado = nulos;

            ViewBag.Parameter = "";

            return View(db.Paciente.ToList());
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult OrdenSAM(int? id)
        {
            ViewBag.idPaciente = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Orden(string nombre, string telefono, string email, string sucursal, string usuario, DateTime fecha, string cantidad, string cantidadAereo, int? referido)
        {
            GetApiKey();

            string mailSeteado = "referenciasoxxo@medicinagmi.mx";

            int cantidadN;
            int cantidadA;

            if (cantidad == "")
            {
                cantidadN = 0;
            }
            else
            {
                cantidadN = Convert.ToInt32(cantidad);
            }

            if (cantidadAereo == "")
            {
                cantidadA = 0;
            }
            else
            {
                cantidadA = Convert.ToInt32(cantidadAereo);
            }

            int precio = (cantidadN * 2842) + (cantidadA * 3480);

            if (precio > 10000)
            {
                precio = 9990;
            }

            if (cantidadAereo == "" && cantidad == "")
            {
                return View("Index");
            }


            Order order = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(nombre, mailSeteado, telefono) + @",
                      ""line_items"": [{
                      ""name"": " + ConvertirProductos1(sucursal) + @",
                      ""unit_price"": " + ConvertirProductos2(Convert.ToString(precio)) + @",
                      ""quantity"": 1
                      }]
                      }");

            order.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""oxxo_cash""
                    },
                    ""amount"": " + ConvertirProductos2(Convert.ToString(precio)) + @"
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

            var referenciaSB = (from r in db.ReferenciasSB where r.EstatusReferencia == "LIBRE" select r.ReferenciaSB).FirstOrDefault();
            ViewBag.ReferenciaSB = referenciaSB;

            ViewBag.Orden = order.id;
            ViewBag.Metodo = "OXXO";

            if ((cantidadN + cantidadA) == 1)
            {
                Paciente paciente = new Paciente();

                paciente.Nombre = nombre.ToUpper();
                paciente.Telefono = telefono;
                paciente.Email = email;

                string hash;
                do
                {
                    Random numero = new Random();
                    int randomize = numero.Next(0, 61);
                    string[] aleatorio = new string[62] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    string get_1;
                    get_1 = aleatorio[randomize];
                    hash = get_1;
                    for (int i = 0; i < 9; i++)
                    {
                        randomize = numero.Next(0, 61);
                        get_1 = aleatorio[randomize];
                        hash += get_1;
                    }
                } while ((from i in db.Paciente where i.HASH == hash select i) == null);

                paciente.HASH = hash;

                //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
                string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
                //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

                //Se obtiene el número del contador desde la base de datos
                int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

                //Contadores por número de citas en cada sucursal
                string contador = "";
                if (num == null)
                {
                    contador = "100";
                }
                else if (num < 10)
                {
                    contador = "00" + Convert.ToString(num);
                }
                else if (num >= 10 && num < 100)
                {
                    contador = "0" + Convert.ToString(num);
                }
                else
                {
                    contador = Convert.ToString(num);
                }



                string mes = DateTime.Now.Month.ToString();
                string dia = DateTime.Now.Day.ToString();
                char[] year = (DateTime.Now.Year.ToString()).ToCharArray();
                string anio = "";

                for (int i = 2; i < year.Length; i++)
                {
                    anio += year[i];
                }

                if (Convert.ToInt32(mes) < 10)
                {
                    mes = "0" + mes;
                }

                if (Convert.ToInt32(dia) < 10)
                {
                    dia = "0" + dia;
                }

                //Se crea el número de Folio
                //string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
                //paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

                string numFolio = dia + mes + anio + SUC + "-" + contador;
                paciente.Folio = dia + mes + anio + SUC + "-" + contador;

                if (ModelState.IsValid)
                {
                    db.Paciente.Add(paciente);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }

                int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

                Sucursales suc = db.Sucursales.Find(idSuc);

                suc.Contador = Convert.ToInt32(num);

                if (ModelState.IsValid)
                {
                    db.Entry(suc).State = EntityState.Modified;
                    db.SaveChanges();
                    //No retorna ya que sigue el proceso
                    //return RedirectToAction("Index");
                }

                var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

                Cita cita = new Cita();

                cita.NoOrden = orden.id;
                cita.TipoPago = "REFERENCIA OXXO";

                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic datosCargo2 = js.Deserialize<dynamic>(orden.charges.data[0].ToString());

                string referencia = datosCargo2["payment_method"]["reference"].ToString();

                cita.Referencia = referencia;

                cita.idPaciente = idPaciente;
                cita.FechaReferencia = DateTime.Now;
                cita.Sucursal = sucursal;
                cita.Recepcionista = usuario;
                cita.EstatusPago = orden.payment_status;
                cita.Folio = numFolio;
                cita.Canal = "Call Center";
                cita.FechaCita = fecha;
                cita.FechaCreacion = DateTime.Now;
                //cita.FechaCita = new DateTime(fecha.Year, fecha.Month, fecha.)

                //Se usa el idCanal para poder hacer que en Recepción se tenga que editar el nombre si viene de gestor
                cita.idCanal = 1;

                if (referido == 22)
                {
                    cita.Referencia = "E1293749";
                }
                if (referido == 23)
                {
                    cita.Referencia = "PL1293750";
                }
                //if (referido == "NATALY FRANCO")
                //{
                //    cita.Referencia = "NF1293751";
                //}
                if (referido == 36)
                {
                    cita.Referencia = "LV1293752";
                }
                if (referido == 21)
                {
                    cita.Referencia = "RS1293753";
                }

                int idRefSB = Convert.ToInt32((from r in db.ReferenciasSB where r.ReferenciaSB == referenciaSB select r.idReferencia).FirstOrDefault());

                ReferenciasSB refe = db.ReferenciasSB.Find(idRefSB);
                refe.EstatusReferencia = "PENDIENTE";
                refe.idPaciente = idPaciente;

                string TIPOLIC = null;
                if(cantidadA != 0)
                {
                    TIPOLIC = "AEREO";
                }
                cita.TipoLicencia = TIPOLIC;


                //if (referido == "NINGUNO" || referido == "OTRO")
                //{
                //    cita.CC = "N/A";
                //}
                //else
                //{
                //    var referidoTipo = (from r in db.Referido where r.Nombre == referido select r.Tipo).FirstOrDefault();
                //    cita.CC = referidoTipo;
                //}

                var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                cita.CC = referidoTipo.Tipo;
                cita.CanalTipo = referidoTipo.Tipo;

                cita.ReferidoPor = referidoTipo.Nombre;

                if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                    referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                    referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136)
                {
                    cita.Cuenta = "CORPORATIVO";
                }

                if (ModelState.IsValid)
                {
                    db.Cita.Add(cita);
                    db.Entry(refe).State = EntityState.Modified;
                    db.SaveChanges();
                    //no regresa ya que se debe ver la pantalla de Orden
                    //return RedirectToAction("Index");
                }
            }
            else
            {
                //return View(detallesOrden);
                for (int n = 1; n <= Convert.ToInt32((cantidadN + cantidadA)); n++)
                {
                    Paciente paciente = new Paciente();

                    paciente.Nombre = nombre.ToUpper() + " " + n;
                    paciente.Telefono = telefono;
                    paciente.Email = email;

                    string hash;
                    do
                    {
                        Random numero = new Random();
                        int randomize = numero.Next(0, 61);
                        string[] aleatorio = new string[62] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                        string get_1;
                        get_1 = aleatorio[randomize];
                        hash = get_1;
                        for (int i = 0; i < 9; i++)
                        {
                            randomize = numero.Next(0, 61);
                            get_1 = aleatorio[randomize];
                            hash += get_1;
                        }
                    } while ((from i in db.Paciente where i.HASH == hash select i) == null);

                    paciente.HASH = hash;

                    //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
                    string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
                    //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

                    //Se obtiene el número del contador desde la base de datos
                    int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

                    //Contadores por número de citas en cada sucursal
                    string contador = "";
                    if (num == null)
                    {
                        contador = "100";
                    }
                    else if (num < 10)
                    {
                        contador = "00" + Convert.ToString(num);
                    }
                    else if (num >= 10 && num < 100)
                    {
                        contador = "0" + Convert.ToString(num);
                    }
                    else
                    {
                        contador = Convert.ToString(num);
                    }

                    //Se asigna el número de ID del doctor
                    //if (Convert.ToInt32(doc) < 10)
                    //{
                    //    doc = "0" + doc;
                    //}

                    string mes = DateTime.Now.Month.ToString();
                    string dia = DateTime.Now.Day.ToString();
                    char[] year = (DateTime.Now.Year.ToString()).ToCharArray();
                    string anio = "";

                    for (int i = 2; i < year.Length; i++)
                    {
                        anio += year[i];
                    }

                    if (Convert.ToInt32(mes) < 10)
                    {
                        mes = "0" + mes;
                    }

                    if (Convert.ToInt32(dia) < 10)
                    {
                        dia = "0" + dia;
                    }

                    //Se crea el número de Folio
                    //string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
                    //paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

                    string numFolio = dia + mes + anio + SUC + "-" + contador;
                    paciente.Folio = dia + mes + anio + SUC + "-" + contador;

                    if (ModelState.IsValid)
                    {
                        db.Paciente.Add(paciente);
                        db.SaveChanges();
                        //return RedirectToAction("Index");
                    }

                    int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

                    Sucursales suc = db.Sucursales.Find(idSuc);

                    suc.Contador = Convert.ToInt32(num);

                    if (ModelState.IsValid)
                    {
                        db.Entry(suc).State = EntityState.Modified;
                        db.SaveChanges();
                        //No retorna ya que sigue el proceso
                        //return RedirectToAction("Index");
                    }

                    var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

                    Cita cita = new Cita();

                    cita.FechaCita = fecha;
                    cita.NoOrden = orden.id;
                    cita.CC = usuario;

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    dynamic datosCargo2 = js.Deserialize<dynamic>(orden.charges.data[0].ToString());

                    string referencia = datosCargo2["payment_method"]["reference"].ToString();

                    cita.Referencia = referencia;

                    cita.idPaciente = idPaciente;
                    cita.FechaReferencia = DateTime.Now;
                    cita.Sucursal = sucursal;
                    cita.Recepcionista = usuario;
                    cita.EstatusPago = orden.payment_status;
                    cita.Folio = numFolio;
                    cita.Canal = "Call Center";
                    cita.TipoPago = "REFERENCIA OXXO";
                    cita.FechaCreacion = DateTime.Now;

                    if (referido == 22)
                    {
                        cita.Referencia = "E1293749";
                    }
                    if (referido == 23)
                    {
                        cita.Referencia = "PL1293750";
                    }
                    //if (referido == "NATALY FRANCO")
                    //{
                    //    cita.Referencia = "NF1293751";
                    //}
                    if (referido == 36)
                    {
                        cita.Referencia = "LV1293752";
                    }
                    if (referido == 21)
                    {
                        cita.Referencia = "RS1293753";
                    }

                    if (n > cantidadN)
                    {
                        cita.TipoLicencia = "AEREO";
                    }

                    int idRefSB = Convert.ToInt32((from r in db.ReferenciasSB where r.ReferenciaSB == referenciaSB select r.idReferencia).FirstOrDefault());
                    ReferenciasSB refe = db.ReferenciasSB.Find(idRefSB);
                    refe.EstatusReferencia = "PENDIENTE";
                    refe.idPaciente = idPaciente;

                    //if (referido == "NINGUNO" || referido == "OTRO")
                    //{
                    //    cita.CC = "N/A";
                    //}
                    //else
                    //{
                    //    var referidoTipo = (from r in db.Referido where r.Nombre == referido select r.Tipo).FirstOrDefault();
                    //    cita.CC = referidoTipo;
                    //}

                    var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                    cita.CC = referidoTipo.Tipo;
                    cita.CanalTipo = referidoTipo.Tipo;

                    cita.ReferidoPor = referidoTipo.Nombre;

                    if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                    referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                    referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136)
                    {
                        cita.Cuenta = "CORPORATIVO";
                    }

                    if (ModelState.IsValid)
                    {
                        db.Cita.Add(cita);
                        db.Entry(refe).State = EntityState.Modified;
                        db.SaveChanges();
                        //no regresa ya que se debe ver la pantalla de Orden
                        //return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.AEREO = Convert.ToInt32(cantidadA);
            ViewBag.AUTO = Convert.ToInt32(cantidadN);
            ViewBag.Precio = (Convert.ToInt32(cantidadN) * 2842) + (Convert.ToInt32(cantidadA) * 3480);
            return View(detallesOrden);
        }


        [HttpPost]
        public ActionResult OrdenCC(string nombre, string nombre2, string telefono, string telefono2, string email, string sucursal, string usuario, DateTime fecha, string tipoL)
        {
            GetApiKey();

            string mailSeteado = "referenciasoxxo@medicinagmi.mx";
            int precio = 0;

            if (tipoL == "AEREO")
            {
                precio = 3480;
            }
            else
            {
                precio = 2842;
            }

            string NOMBRE = nombre != "" ? nombre.ToUpper() : nombre2.ToUpper();
            string TELEFONO = telefono != "" ? telefono : telefono2;

            Order order = new conekta.Order().create(@"{
                      ""currency"":""MXN"",
                      ""customer_info"": " + ConvertirCliente(NOMBRE, mailSeteado, TELEFONO) + @",
                      ""line_items"": [{
                      ""name"": " + ConvertirProductos1(sucursal) + @",
                      ""unit_price"": " + ConvertirProductos2(Convert.ToString(precio)) + @",
                      ""quantity"": 1
                      }]
                      }");

            order.createCharge(@"{
                    ""payment_method"": {
                    ""type"": ""oxxo_cash""
                    },
                    ""amount"": " + ConvertirProductos2(Convert.ToString(precio)) + @"
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

            var referenciaSB = (from r in db.ReferenciasSB where r.EstatusReferencia == "LIBRE" select r.ReferenciaSB).FirstOrDefault();
            ViewBag.ReferenciaSB = referenciaSB;

            ViewBag.Orden = order.id;
            ViewBag.Metodo = "OXXO";

            Paciente paciente = new Paciente();

            paciente.Nombre = nombre != "" ? nombre.ToUpper() : nombre2.ToUpper();
            paciente.Telefono = telefono != "" ? telefono : telefono2;
            paciente.Email = email;

            string hash;
            do
            {
                Random numero = new Random();
                int randomize = numero.Next(0, 61);
                string[] aleatorio = new string[62] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                string get_1;
                get_1 = aleatorio[randomize];
                hash = get_1;
                for (int i = 0; i < 9; i++)
                {
                    randomize = numero.Next(0, 61);
                    get_1 = aleatorio[randomize];
                    hash += get_1;
                }
            } while ((from i in db.Paciente where i.HASH == hash select i) == null);

            paciente.HASH = hash;

            //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
            string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
            //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

            //Se obtiene el número del contador desde la base de datos
            int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

            //Contadores por número de citas en cada sucursal
            string contador = "";
            if (num == null)
            {
                contador = "100";
            }
            else if (num < 10)
            {
                contador = "00" + Convert.ToString(num);
            }
            else if (num >= 10 && num < 100)
            {
                contador = "0" + Convert.ToString(num);
            }
            else
            {
                contador = Convert.ToString(num);
            }



            string mes = DateTime.Now.Month.ToString();
            string dia = DateTime.Now.Day.ToString();
            char[] year = (DateTime.Now.Year.ToString()).ToCharArray();
            string anio = "";

            for (int i = 2; i < year.Length; i++)
            {
                anio += year[i];
            }

            if (Convert.ToInt32(mes) < 10)
            {
                mes = "0" + mes;
            }

            if (Convert.ToInt32(dia) < 10)
            {
                dia = "0" + dia;
            }

            string numFolio = dia + mes + anio + SUC + "-" + contador;
            paciente.Folio = dia + mes + anio + SUC + "-" + contador;

            if (ModelState.IsValid)
            {
                db.Paciente.Add(paciente);
                db.SaveChanges();
            }

            int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

            Sucursales suc = db.Sucursales.Find(idSuc);

            suc.Contador = Convert.ToInt32(num);

            if (ModelState.IsValid)
            {
                db.Entry(suc).State = EntityState.Modified;
                db.SaveChanges();
            }

            var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

            Cita cita = new Cita();

            cita.NoOrden = orden.id;
            cita.TipoPago = "REFERENCIA OXXO";

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic datosCargo2 = js.Deserialize<dynamic>(orden.charges.data[0].ToString());

            string referencia = datosCargo2["payment_method"]["reference"].ToString();

            cita.Referencia = referencia;

            cita.idPaciente = idPaciente;
            cita.FechaReferencia = DateTime.Now;
            cita.Sucursal = sucursal;
            cita.Recepcionista = usuario;
            cita.EstatusPago = orden.payment_status;
            cita.Folio = numFolio;
            cita.Canal = "Call Center";
            cita.FechaCita = fecha;
            cita.ReferidoPor = "NINGUNO";
            cita.FechaCreacion = DateTime.Now;
            cita.TipoLicencia = tipoL;
            cita.TipoTramite = "REVALIDACIÓN";

            //Se usa el idCanal para poder hacer que en Recepción se tenga que editar el nombre si viene de gestor
            cita.idCanal = 1;

            int idRefSB = Convert.ToInt32((from r in db.ReferenciasSB where r.ReferenciaSB == referenciaSB select r.idReferencia).FirstOrDefault());

            ReferenciasSB refe = db.ReferenciasSB.Find(idRefSB);
            refe.EstatusReferencia = "PENDIENTE";
            refe.idPaciente = idPaciente;

            if (ModelState.IsValid)
            {
                db.Cita.Add(cita);
                db.Entry(refe).State = EntityState.Modified;
                db.SaveChanges();
                //no regresa ya que se debe ver la pantalla de Orden
                //return RedirectToAction("Index");
            }

            int cantidadA = 0;
            int cantidadN = 0;

            if(tipoL == "AEREO")
            {
                cantidadA = 1;
            }
            else
            {
                cantidadN = 1;
            }

            ViewBag.AEREO = Convert.ToInt32(cantidadA);
            ViewBag.AUTO = Convert.ToInt32(cantidadN);
            ViewBag.Precio = (Convert.ToInt32(cantidadN) * 2842) + (Convert.ToInt32(cantidadA) * 3480);
            return View(detallesOrden);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagoTarjeta(string nombre, string telefono, string email, string usuario, string sucursal, string cantidad, int card, DateTime? fecha, string cantidadAereo, int? referido)
        {
            GetApiKey();

            string mailSeteado = "referenciaoxxo@medicinagmi.mx";

            int cantidadN;
            int cantidadA;

            if (cantidad == "")
            {
                cantidadN = 0;
            }
            else
            {
                cantidadN = Convert.ToInt32(cantidad);
            }

            if (cantidadAereo == "")
            {
                cantidadA = 0;
            }
            else
            {
                cantidadA = Convert.ToInt32(cantidadAereo);
            }

            int precio = (cantidadN * 2842) + (cantidadA * 3480);

            if (precio > 10000)
            {
                precio = 9990;
            }

            PaymentLink nOrder = new PaymentLink().create(@"{
                      ""name"":""Pago con Tarjeta"",
                      ""type"":""PaymentLink"",
                      ""recurrent"": false,
                      ""expired_at"": " + FechaExpira() + @",
                      ""allowed_payment_methods"": [""card""],
                      ""needs_shipping_contact"": false ,
                      ""monthly_installments_enabled"": false ,
                       
                      ""order_template"": {
                      ""line_items"": [{
                      ""name"": ""Checkup EPI"",
                      ""unit_price"": " + ConvertirProductos2(Convert.ToString(precio)) + @",
                      ""quantity"": 1
                      }],

                      ""currency"":""MXN"",
                      ""metadata"":{},
                      ""customer_info"": " + ConvertirCliente(nombre, email, telefono) + @"
                      }
                    }");

            string link = nOrder.url;
            string IDEE = nOrder.id;
            TempData["Link"] = link;

            DateTime FECHA = new DateTime();

            if(fecha == null)
            {
                FECHA = DateTime.Now;
            }
            else
            {
                FECHA = Convert.ToDateTime(fecha);
            }

            if ((cantidadN + cantidadA) == 1)
            {
                Paciente paciente = new Paciente();

                paciente.Nombre = nombre.ToUpper();
                paciente.Telefono = telefono;
                paciente.Email = email;

                string hash;
                do
                {
                    Random numero = new Random();
                    int randomize = numero.Next(0, 61);
                    string[] aleatorio = new string[62] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    string get_1;
                    get_1 = aleatorio[randomize];
                    hash = get_1;
                    for (int i = 0; i < 9; i++)
                    {
                        randomize = numero.Next(0, 61);
                        get_1 = aleatorio[randomize];
                        hash += get_1;
                    }
                } while ((from i in db.Paciente where i.HASH == hash select i) == null);

                paciente.HASH = hash;

                //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
                string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
                //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

                //Se obtiene el número del contador desde la base de datos
                int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

                //Contadores por número de citas en cada sucursal
                string contador = "";
                if (num == null)
                {
                    contador = "100";
                }
                else if (num < 10)
                {
                    contador = "00" + Convert.ToString(num);
                }
                else if (num >= 10 && num < 100)
                {
                    contador = "0" + Convert.ToString(num);
                }
                else
                {
                    contador = Convert.ToString(num);
                }

                //Se asigna el número de ID del doctor
                //if (Convert.ToInt32(doc) < 10)
                //{
                //    doc = "0" + doc;
                //}

                string mes = DateTime.Now.Month.ToString();
                string dia = DateTime.Now.Day.ToString();
                char[] year = (DateTime.Now.Year.ToString()).ToCharArray();
                string anio = "";

                for (int i = 2; i < year.Length; i++)
                {
                    anio += year[i];
                }

                if (Convert.ToInt32(mes) < 10)
                {
                    mes = "0" + mes;
                }

                if (Convert.ToInt32(dia) < 10)
                {
                    dia = "0" + dia;
                }

                //Se crea el número de Folio
                //string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
                //paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

                string numFolio = dia + mes + anio + SUC + "-" + contador;
                paciente.Folio = dia + mes + anio + SUC + "-" + contador;

                if (ModelState.IsValid)
                {
                    db.Paciente.Add(paciente);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }

                int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

                Sucursales suc = db.Sucursales.Find(idSuc);

                suc.Contador = Convert.ToInt32(num);

                if (ModelState.IsValid)
                {
                    db.Entry(suc).State = EntityState.Modified;
                    db.SaveChanges();
                    //No retorna ya que sigue el proceso
                    //return RedirectToAction("Index");
                }

                var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

                Cita cita = new Cita();

                cita.TipoPago = "Pago con Tarjeta";

                cita.NoOrden = IDEE;

                cita.idPaciente = idPaciente;
                cita.FechaReferencia = DateTime.Now;
                cita.Sucursal = sucursal;
                cita.Recepcionista = usuario;
                cita.EstatusPago = "Pendiente";
                cita.Folio = numFolio;
                cita.Canal = "Call Center";
                cita.FechaCita = FECHA;
                cita.NoOrden = link;
                cita.Referencia = Convert.ToString(card);
                cita.CC = usuario;
                cita.FechaCreacion = DateTime.Now;

                //Se usa el idCanal para poder hacer que en Recepción se tenga que editar el nombre si viene de gestor
                cita.idCanal = 1;

                string TIPOLIC = null;
                if (cantidadA != 0)
                {
                    TIPOLIC = "AEREO";
                }
                cita.TipoLicencia = TIPOLIC;

                //if (referido == "NINGUNO" || referido == "OTRO")
                //{
                //    cita.CC = "N/A";
                //}
                //else
                //{
                //    var referidoTipo = (from r in db.Referido where r.Nombre == referido select r.Tipo).FirstOrDefault();
                //    cita.CC = referidoTipo;
                //}

                var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                cita.CC = referidoTipo.Tipo;
                cita.CanalTipo = referidoTipo.Tipo;

                cita.ReferidoPor = referidoTipo.Nombre;

                if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                    referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                    referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136)
                {
                    cita.Cuenta = "CORPORATIVO";
                }

                if (ModelState.IsValid)
                {
                    db.Cita.Add(cita);
                    db.SaveChanges();
                    //no regresa ya que se debe ver la pantalla de Orden
                    //return RedirectToAction("Index");
                }
            }
            else
            {
                //return View(detallesOrden);
                for (int n = 1; n <= Convert.ToInt32((cantidadN + cantidadA)); n++)
                {
                    Paciente paciente = new Paciente();

                    paciente.Nombre = nombre.ToUpper() + " " + n;
                    paciente.Telefono = telefono;
                    paciente.Email = email;

                    string hash;
                    do
                    {
                        Random numero = new Random();
                        int randomize = numero.Next(0, 61);
                        string[] aleatorio = new string[62] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                        string get_1;
                        get_1 = aleatorio[randomize];
                        hash = get_1;
                        for (int i = 0; i < 9; i++)
                        {
                            randomize = numero.Next(0, 61);
                            get_1 = aleatorio[randomize];
                            hash += get_1;
                        }
                    } while ((from i in db.Paciente where i.HASH == hash select i) == null);

                    paciente.HASH = hash;

                    //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
                    string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
                    //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

                    //Se obtiene el número del contador desde la base de datos
                    int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

                    //Contadores por número de citas en cada sucursal
                    string contador = "";
                    if (num == null)
                    {
                        contador = "100";
                    }
                    else if (num < 10)
                    {
                        contador = "00" + Convert.ToString(num);
                    }
                    else if (num >= 10 && num < 100)
                    {
                        contador = "0" + Convert.ToString(num);
                    }
                    else
                    {
                        contador = Convert.ToString(num);
                    }

                    //Se asigna el número de ID del doctor
                    //if (Convert.ToInt32(doc) < 10)
                    //{
                    //    doc = "0" + doc;
                    //}

                    string mes = DateTime.Now.Month.ToString();
                    string dia = DateTime.Now.Day.ToString();
                    char[] year = (DateTime.Now.Year.ToString()).ToCharArray();
                    string anio = "";

                    for (int i = 2; i < year.Length; i++)
                    {
                        anio += year[i];
                    }

                    if (Convert.ToInt32(mes) < 10)
                    {
                        mes = "0" + mes;
                    }

                    if (Convert.ToInt32(dia) < 10)
                    {
                        dia = "0" + dia;
                    }

                    //Se crea el número de Folio
                    //string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
                    //paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

                    string numFolio = dia + mes + anio + SUC + "-" + contador;
                    paciente.Folio = dia + mes + anio + SUC + "-" + contador;

                    if (ModelState.IsValid)
                    {
                        db.Paciente.Add(paciente);
                        db.SaveChanges();
                        //return RedirectToAction("Index");
                    }

                    int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

                    Sucursales suc = db.Sucursales.Find(idSuc);

                    suc.Contador = Convert.ToInt32(num);

                    if (ModelState.IsValid)
                    {
                        db.Entry(suc).State = EntityState.Modified;
                        db.SaveChanges();
                        //No retorna ya que sigue el proceso
                        //return RedirectToAction("Index");
                    }

                    var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

                    Cita cita = new Cita();

                    cita.TipoPago = "Pago con Tarjeta";

                    if (n > cantidadN)
                    {
                        cita.TipoLicencia = "AEREO";
                    }

                    cita.NoOrden = IDEE;
                    cita.idPaciente = idPaciente;
                    cita.FechaReferencia = DateTime.Now;
                    cita.Sucursal = sucursal;
                    cita.Recepcionista = usuario;
                    cita.EstatusPago = "Pendiente";
                    cita.Folio = numFolio;
                    cita.Canal = "Call Center";
                    cita.FechaCita = FECHA;
                    cita.NoOrden = link;
                    cita.Referencia = Convert.ToString(card);
                    cita.CC = usuario;
                    cita.FechaCreacion = DateTime.Now;

                    //if (referido == "NINGUNO" || referido == "OTRO")
                    //{
                    //    cita.CC = "N/A";
                    //}
                    //else
                    //{
                    //    var referidoTipo = (from r in db.Referido where r.Nombre == referido select r.Tipo).FirstOrDefault();
                    //    cita.CC = referidoTipo;
                    //}

                    var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                    cita.CC = referidoTipo.Tipo;
                    cita.CanalTipo = referidoTipo.Tipo;

                    cita.ReferidoPor = referidoTipo.Nombre;

                    if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                    referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                    referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136)
                    {
                        cita.Cuenta = "CORPORATIVO";
                    }

                    if (ModelState.IsValid)
                    {
                        db.Cita.Add(cita);
                        db.SaveChanges();
                        //no regresa ya que se debe ver la pantalla de Orden
                        //return RedirectToAction("Index");
                    }
                }
            }

            return Redirect("Index");
        }


        public ActionResult PagoTarjetaCC(string nombre, string nombre2, string telefono, string telefono2, string email, string usuario, string sucursal, string cantidad, int card, DateTime? fecha, string cantidadAereo, string referido, string tipoL)
        {
            GetApiKey();

            string mailSeteado = "referenciasoxxo@medicinagmi.mx";
            int precio = 0;

            if (tipoL == "AEREO")
            {
                precio = 3480;
            }
            else
            {
                precio = 2842;
            }

            string NOMBRE = nombre != "" ? nombre.ToUpper() : nombre2.ToUpper();
            string TELEFONO = telefono != "" ? telefono : telefono2;

            PaymentLink nOrder = new PaymentLink().create(@"{
                      ""name"":""Pago con Tarjeta"",
                      ""type"":""PaymentLink"",
                      ""recurrent"": false,
                      ""expired_at"": " + FechaExpira() + @",
                      ""allowed_payment_methods"": [""card""],
                      ""needs_shipping_contact"": false ,
                      ""monthly_installments_enabled"": false ,
                       
                      ""order_template"": {
                      ""line_items"": [{
                      ""name"": ""Checkup EPI"",
                      ""unit_price"": " + ConvertirProductos2(Convert.ToString(precio)) + @",
                      ""quantity"": 1
                      }],

                      ""currency"":""MXN"",
                      ""metadata"":{},
                      ""customer_info"": " + ConvertirCliente(NOMBRE, email, TELEFONO) + @"
                      }
                    }");

            string link = nOrder.url;
            string IDEE = nOrder.id;
            TempData["Link"] = link;

            DateTime FECHA = new DateTime();

            if (fecha == null)
            {
                FECHA = DateTime.Now;
            }
            else
            {
                FECHA = Convert.ToDateTime(fecha);
            }

            Paciente paciente = new Paciente();

            paciente.Nombre = nombre.ToUpper();
            paciente.Telefono = telefono;
            paciente.Email = email;

            string hash;
            do
            {
                Random numero = new Random();
                int randomize = numero.Next(0, 61);
                string[] aleatorio = new string[62] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                string get_1;
                get_1 = aleatorio[randomize];
                hash = get_1;
                for (int i = 0; i < 9; i++)
                {
                    randomize = numero.Next(0, 61);
                    get_1 = aleatorio[randomize];
                    hash += get_1;
                }
            } while ((from i in db.Paciente where i.HASH == hash select i) == null);

            paciente.HASH = hash;

            //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
            string SUC = (from S in db.Sucursales where S.Nombre == sucursal select S.SUC).FirstOrDefault();
            //string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

            //Se obtiene el número del contador desde la base de datos
            int? num = (from c in db.Sucursales where c.Nombre == sucursal select c.Contador).FirstOrDefault() + 1;

            //Contadores por número de citas en cada sucursal
            string contador = "";
            if (num == null)
            {
                contador = "100";
            }
            else if (num < 10)
            {
                contador = "00" + Convert.ToString(num);
            }
            else if (num >= 10 && num < 100)
            {
                contador = "0" + Convert.ToString(num);
            }
            else
            {
                contador = Convert.ToString(num);
            }

            //Se asigna el número de ID del doctor
            //if (Convert.ToInt32(doc) < 10)
            //{
            //    doc = "0" + doc;
            //}

            string mes = DateTime.Now.Month.ToString();
            string dia = DateTime.Now.Day.ToString();
            char[] year = (DateTime.Now.Year.ToString()).ToCharArray();
            string anio = "";

            for (int i = 2; i < year.Length; i++)
            {
                anio += year[i];
            }

            if (Convert.ToInt32(mes) < 10)
            {
                mes = "0" + mes;
            }

            if (Convert.ToInt32(dia) < 10)
            {
                dia = "0" + dia;
            }

            //Se crea el número de Folio
            //string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;
            //paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + "-" + contador;

            string numFolio = dia + mes + anio + SUC + "-" + contador;
            paciente.Folio = dia + mes + anio + SUC + "-" + contador;

            if (ModelState.IsValid)
            {
                db.Paciente.Add(paciente);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            int? idSuc = (from i in db.Sucursales where i.Nombre == sucursal select i.idSucursal).FirstOrDefault();

            Sucursales suc = db.Sucursales.Find(idSuc);

            suc.Contador = Convert.ToInt32(num);

            if (ModelState.IsValid)
            {
                db.Entry(suc).State = EntityState.Modified;
                db.SaveChanges();
                //No retorna ya que sigue el proceso
                //return RedirectToAction("Index");
            }

            var idPaciente = (from i in db.Paciente where i.Folio == paciente.Folio select i.idPaciente).FirstOrDefault();

            Cita cita = new Cita();

            cita.TipoPago = "Pago con Tarjeta";

            cita.NoOrden = IDEE;

            cita.idPaciente = idPaciente;
            cita.FechaReferencia = DateTime.Now;
            cita.Sucursal = sucursal;
            cita.Recepcionista = usuario;
            cita.EstatusPago = "Pendiente";
            cita.Folio = numFolio;
            cita.Canal = "Call Center";
            cita.FechaCita = FECHA;
            cita.NoOrden = link;
            cita.Referencia = Convert.ToString(card);
            cita.CC = usuario;
            cita.ReferidoPor = "NINGUNO";
            cita.FechaCreacion = DateTime.Now;
            cita.TipoLicencia = tipoL;
            cita.TipoTramite = "REVALIDACIÓN";

            //Se usa el idCanal para poder hacer que en Recepción se tenga que editar el nombre si viene de gestor
            cita.idCanal = 1;

            if (referido == "NINGUNO" || referido == "OTRO")
            {
                cita.CC = "N/A";
            }
            else
            {
                var referidoTipo = (from r in db.Referido where r.Nombre == referido select r.Tipo).FirstOrDefault();
                cita.CC = referidoTipo;
            }

            if (ModelState.IsValid)
            {
                db.Cita.Add(cita);
                db.SaveChanges();
            }

            return Redirect("Vigencias");
        }

        public ActionResult Tipificacion(int? id, string operador, string tipificacion)
        {
            var cliente = db.CallCenter.Find(id);

            cliente.Llamadas += 1;

            if(tipificacion == "NO ESTÁ INTERESADO" || tipificacion == "NÚMERO EQUIVOCADO")
            {
                cliente.Rechazado = "SI";
            }

            Tipificaciones tipi = new Tipificaciones();

            tipi.Operador = operador;
            tipi.FechaLlamada = DateTime.Now;
            tipi.Tipificacion = tipificacion;
            tipi.Nombre = cliente.Nombre;
            tipi.idCallCenter = id;

            if(ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.Tipificaciones.Add(tipi);
                db.SaveChanges();
            }

            return Redirect("Vigencias");
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaciente,Nombre,Telefono,Email,Folio")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarEstatus(int? id)
        {
            var referencia = (from r in db.Cita where r.idPaciente == id select r.Referencia).FirstOrDefault();
            var consulta = from c in db.Cita where c.Referencia == referencia select c;
            if (ModelState.IsValid)
            {
                foreach (var item in consulta)
                {
                    Cita cita = db.Cita.Find(item.idCita);

                    cita.EstatusPago = "Pagado";
                    db.Entry(cita).State = EntityState.Modified;

                }
                db.SaveChanges();

            }

            return Redirect("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Digitalizar(HttpPostedFileBase file, string id, string usuario, string nombre, string doctor, string numero, string tipoL, string tipoT)
        {
            int ide = Convert.ToInt32(id);

            SCT_iCare.Expedientes exp = new SCT_iCare.Expedientes();
            var cita = (from c in db.Cita where c.idPaciente == ide select c).FirstOrDefault();
            var paciente = (from p in db.Paciente where p.idPaciente == ide select p).FirstOrDefault();
            Captura captura = new Captura();

            byte[] bytes2 = null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }

                bytes2 = bytes;

                //var bytesBinary = bytes;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=MyPDF.pdf");
                //Response.BinaryWrite(bytesBinary);
                //Response.End();
            }

            exp.Expediente = bytes2;
            exp.Recpecionista = usuario;
            exp.idPaciente = ide;
            paciente.Nombre = nombre.ToUpper();
            captura.NombrePaciente = nombre.ToUpper();
            captura.NoExpediente = numero;
            captura.TipoTramite = tipoT;
            captura.EstatusCaptura = "No iniciado";
            captura.Doctor = doctor;
            captura.Sucursal = cita.Sucursal;
            captura.idPaciente = Convert.ToInt32(id);
            captura.FechaExpdiente = DateTime.Now;

            Cita citaModificar = new Cita();
            cita.TipoLicencia = tipoL;
            cita.TipoTramite = tipoT;
            cita.NoExpediente = numero;
            cita.Doctor = doctor;


            //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
            string SUC = (from S in db.Sucursales where S.Nombre == cita.Sucursal select S.SUC).FirstOrDefault();
            string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

            //Se obtiene el número del contador desde la base de datos del último registro de Folio incompleto
            //int? num = (from c in db.Sucursales where c.Nombre == cita.Sucursal select c.Contador).FirstOrDefault() + 1;
            var num = new string(cita.Folio.Reverse().Take(3).Reverse().ToArray());

            //Se asigna el número de ID del doctor
            if (Convert.ToInt32(doc) < 10)
            {
                doc = "0" + doc;
            }

            string mes = DateTime.Now.Month.ToString();

            if (Convert.ToInt32(mes) < 10)
            {
                mes = "0" + mes;
            }

            //Se crea el número de Folio
            string numFolio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + doc + num;
            cita.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + doc + num;
            paciente.Folio = (DateTime.Now.Year).ToString() + mes + (DateTime.Now.Day).ToString() + SUC + doc + num;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                //db.Cita.Add(cita);
                db.Expedientes.Add(exp);
                db.Captura.Add(captura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exp);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Paciente.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paciente paciente = db.Paciente.Find(id);
            db.Paciente.Remove(paciente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

        [HttpPost]
        public ActionResult EditarCompleto(string id, string nombre, string telefono, string email, DateTime? fecha, string sucursal)
        {
            int ide = Convert.ToInt32(id);

            Paciente paciente = db.Paciente.Find(ide);
            var cita = (from c in db.Cita where c.idPaciente == ide select c).FirstOrDefault();
            var expediente = (from c in db.Expedientes where c.idPaciente == ide select c).FirstOrDefault();
            var captura = (from c in db.Captura where c.idPaciente == ide select c).FirstOrDefault();

            string ID = null;
            string NOMBRE = null;
            string TELEFONO = null;
            string EMAIL = null;
            DateTime FECHA = new DateTime();
            string SUCURSAL = null;

            if (id == null)
            {
                ID = paciente.idPaciente.ToString();
            }
            else
            {
                ID = id;
            }

            if (nombre == "")
            {
                NOMBRE = paciente.Nombre;
            }
            else
            {
                NOMBRE = nombre;
            }

            if (telefono == "")
            {
                TELEFONO = paciente.Telefono;
            }
            else
            {
                TELEFONO = telefono;
            }

            if (email == "")
            {
                EMAIL = paciente.Email;
            }
            else
            {
                EMAIL = email;
            }

            if(fecha == null)
            {
                FECHA = Convert.ToDateTime(cita.FechaCita);
            }
            else
            {
                FECHA = Convert.ToDateTime(fecha);
            }

            paciente.Nombre = NOMBRE;
            paciente.Telefono = TELEFONO;
            paciente.Email = EMAIL;

            cita.FechaCita = fecha;
            cita.Sucursal = sucursal;

            if ((from i in db.Cita where i.Referencia == ((from l in db.Cita where i.idPaciente == ide select l.Referencia).FirstOrDefault()) select i).Count() < 2)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(paciente).State = EntityState.Modified;
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                var bloque = from i in db.Cita where i.Referencia == ((from l in db.Cita where l.idPaciente == ide select l.Referencia).FirstOrDefault()) select i;
                int contador = 1;
                foreach(var item in bloque)
                {
                    Cita cita2 = db.Cita.Find(item.idCita);
                    Paciente paciente2 = (from p in db.Paciente where p.idPaciente == cita2.idPaciente select p).FirstOrDefault();

                    if(nombre == "")
                    {
                        paciente2.Telefono = TELEFONO;
                        paciente2.Email = EMAIL;

                        cita2.FechaCita = fecha;
                        db.Entry(cita).State = EntityState.Modified;
                        db.Entry(paciente).State = EntityState.Modified;
                    }
                    else
                    {
                        paciente2.Nombre = nombre.ToUpper() +" " + contador.ToString();
                        paciente2.Telefono = TELEFONO;
                        paciente2.Email = EMAIL;

                        cita2.FechaCita = fecha;
                        cita2.Canal = nombre.ToUpper();
                        db.Entry(cita).State = EntityState.Modified;
                        db.Entry(paciente).State = EntityState.Modified;
                    }
                    contador++;
                }
                db.SaveChanges();

            }

            return Redirect("Index");
        }

        public JsonResult Buscar(string dato)
        {
            string parametro;

            if (dato.All(char.IsDigit))
            {
                parametro = dato;

                List<Paciente> data = db.Paciente.ToList();
                JavaScriptSerializer js = new JavaScriptSerializer();
                var selected = data.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                    //.Where(r => r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro)
                    .Where(r => r.M.NoExpediente == parametro)
                    .Join(db.Captura, o => o.N.idPaciente, p => p.idPaciente, (o, p) => new { O = o, P = p })
                    .Select(S => new {
                        S.O.N.idPaciente,
                        S.O.N.Nombre,
                        S.O.M.Referencia,
                        S.O.N.Email,
                        S.O.N.Telefono,
                        FechaReferencia = Convert.ToDateTime(S.O.M.FechaReferencia).ToString("dd-MMMM-yyyy"),
                        S.O.M.TipoLicencia,
                        S.O.M.NoExpediente,
                        S.O.M.Sucursal
                    });

                return Json(selected, JsonRequestBehavior.AllowGet);


                return Json(selected, JsonRequestBehavior.AllowGet);
            }
            else
            {
                parametro = dato.ToUpper();

                double porcentaje = 1;

                if (db.Paciente.Count() > 5000 && db.Paciente.Count() < 9000)
                {
                    porcentaje = 0.5;
                }
                else if (db.Paciente.Count() >= 9000 && db.Paciente.Count() < 14000)
                {
                    porcentaje = 0.6;
                }
                else if (db.Paciente.Count() >= 14000 && db.Paciente.Count() < 18000)
                {
                    porcentaje = 0.7;
                }
                else if (db.Paciente.Count() >= 18000 && db.Paciente.Count() < 22000)
                {
                    porcentaje = 0.8;
                }
                else if (db.Paciente.Count() >= 22000)
                {
                    porcentaje = 0.9;
                }

                List<Paciente> data = db.Paciente.Where(w => w.idPaciente > (db.Paciente.Count() * porcentaje)).ToList();
                JavaScriptSerializer js = new JavaScriptSerializer();
                var selected = data.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                    //.Where(r => r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro)
                    .Where(r => r.N.Nombre.Contains(parametro))
                    .Join(db.Captura, o => o.N.idPaciente, p => p.idPaciente, (o, p) => new { O = o, P = p })
                    .Select(S => new {
                        S.O.N.idPaciente,
                        S.O.N.Nombre,
                        S.O.M.Referencia,
                        S.O.N.Email,
                        S.O.N.Telefono,
                        FechaReferencia = Convert.ToDateTime(S.O.M.FechaReferencia).ToString("dd-MMMM-yyyy"),
                        S.O.M.TipoLicencia,
                        S.O.M.NoExpediente,
                        S.O.M.Sucursal
                    });

                return Json(selected, JsonRequestBehavior.AllowGet);
            }

            //List<Paciente> data = db.Paciente.Where(w => w.idPaciente > (db.Paciente.Count() / 3)).ToList();
            //var selected = data.Join(db.Captura, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
            //    .Where(r => (r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro))
            //    .Select(S => new {
            //        S.M.idCaptura,
            //        S.N.Nombre,
            //        S.M.NoExpediente,
            //        S.N.Email,
            //        S.N.Telefono,
            //        FechaReferencia = Convert.ToDateTime(S.M.FechaExpdiente).ToString("dd-MMMM-yyyy"),
            //        S.M.Sucursal
            //    });

            //return Json(selected, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarBT()
        {
            List<Paciente> data = db.Paciente.ToList();

            var selected = data.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m }).Select(S => new {id = S.N.idPaciente, S.N.Nombre, S.N.Telefono, Fecha =  S.M.FechaCita.ToString(), S.M.Referencia })/*.Where(w => w.Nombre.Contains(dato1))*/;
            return Json(selected, JsonRequestBehavior.AllowGet);
        }

    }
    
}