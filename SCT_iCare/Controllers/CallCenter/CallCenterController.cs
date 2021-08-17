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
        public ActionResult Index()
        {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Orden(string nombre, string telefono, string email, string sucursal, string usuario, DateTime fecha, string cantidad, string cantidadAereo)
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
                cita.Canal = "SITIO";
                cita.FechaCita = fecha;
                //cita.FechaCita = new DateTime(fecha.Year, fecha.Month, fecha.)

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
                    cita.Canal = nombre.ToUpper();
                    cita.TipoPago = "REFERENCIA OXXO";

                    if (n > cantidadN)
                    {
                        cita.TipoLicencia = "AEREO";
                    }

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
                }
            }

            ViewBag.AEREO = Convert.ToInt32(cantidadA);
            ViewBag.AUTO = Convert.ToInt32(cantidadN);
            ViewBag.Precio = (Convert.ToInt32(cantidadN) * 2842) + (Convert.ToInt32(cantidadA) * 3480);
            return View(detallesOrden);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagoTarjeta(string nombre, string telefono, string email, string usuario, string sucursal, string cantidad, int card, DateTime? fecha, string cantidadAereo)
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
                      ""metadata"":{""my_custom_customer_id"":""202107PEPE""},
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
                cita.Canal = usuario;
                cita.FechaCita = FECHA;
                cita.NoOrden = link;
                cita.Referencia = Convert.ToString(card);
                cita.CC = usuario;

                string TIPOLIC = null;
                if (cantidadA != 0)
                {
                    TIPOLIC = "AEREO";
                }
                cita.TipoLicencia = TIPOLIC;


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
                    cita.Canal = nombre.ToUpper();
                    cita.FechaCita = FECHA;
                    cita.NoOrden = link;
                    cita.Referencia = Convert.ToString(card);
                    cita.CC = usuario;

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

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult EditarCompleto(string id, string nombre, string telefono, string email, DateTime fecha)
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

            paciente.Nombre = NOMBRE;
            paciente.Telefono = TELEFONO;
            paciente.Email = EMAIL;

            cita.FechaCita = fecha;

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
            }
            else
            {
                parametro = dato.ToUpper();
            }

            List<Paciente> data = db.Paciente.ToList();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var selected = data.Join(db.Captura, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                .Where(r => (r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro)).Join(db.Cita, o => o.N.idPaciente, p => p.idPaciente, (o, p) => new { O = o, P = p })
                .Select(S => new {
                    S.O.N.idPaciente,
                    S.O.N.Nombre,
                    S.P.NoExpediente,
                    S.O.N.Email,
                    S.O.N.Telefono,
                    FechaReferencia = Convert.ToDateTime(S.O.M.FechaExpdiente).ToString("dd-MMMM-yyyy"),
                    S.O.M.Sucursal,
                    S.P.Referencia
                });


            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarBT()
        {
            List<Paciente> data = db.Paciente.ToList();

            var selected = data.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m }).Select(S => new {id = S.N.idPaciente, S.N.Nombre, S.N.Telefono, Fecha =  S.M.FechaCita.ToString(), S.M.Referencia })/*.Where(w => w.Nombre.Contains(dato1))*/;
            return Json(selected, JsonRequestBehavior.AllowGet);
        }

    }
    
}