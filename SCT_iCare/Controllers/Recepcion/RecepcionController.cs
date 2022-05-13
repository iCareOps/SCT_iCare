using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using conekta;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SCT_iCare;

using System.IO;
using System.Text;
using System.Globalization;

using MessagingToolkit.QRCode.Codec;
using System.Drawing;

namespace SCT_iCare.Controllers.Recepcion
{
    public class RecepcionController : Controller
    {
        private GMIEntities db = new GMIEntities();

        public static void GetApiKey()
        {
            conekta.Api.apiKey = ConfigurationManager.AppSettings["conekta"];
            conekta.Api.version = "2.0.0";
            conekta.Api.locale = "es";
        }

        // GET: Pacientes
        public ActionResult Index(DateTime? inicio, DateTime? final, string sucursal)
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
            ViewBag.Sucursal = sucursal;

            ViewBag.Parameter = "";

            return View(db.Paciente.ToList());
        }

        public ActionResult Dashboard()
        {
            return View(db.Paciente.ToList());
        }

        public ActionResult NextDay()
        {
            return View(db.Paciente.ToList());
        }

        public ActionResult Foto()
        {
            return View(db.Paciente.ToList());
        }


        public ActionResult CargarFoto(HttpPostedFileBase file, string id)
        {
            int ide = Convert.ToInt32(id);

            var revisionFoto = (from i in db.Biometricos where i.idPaciente == ide select i).FirstOrDefault();

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
            }

            if(revisionFoto == null)
            {
                Biometricos bio = new Biometricos();

                bio.Foto = bytes2;
                bio.idPaciente = ide;

                if (ModelState.IsValid)
                {
                    db.Biometricos.Add(bio);
                    db.SaveChanges();
                }
            }
            else
            {
                var bio = db.Biometricos.Find(revisionFoto.idBiometricos);

                bio.Foto = bytes2;

                if (ModelState.IsValid)
                {
                    db.Entry(bio).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Foto");
        }       

        public ActionResult CambiarGestor(int? id, int? referido, string usuario, string gestorAnterior, string tipoPago, string referencia, HttpPostedFileBase ticket, int? ide)
        {
            Cita cita = db.Cita.Find(id);

            var referidoTipo = (from r in db.Referido where r.idReferido == referido  select r).FirstOrDefault();
            cita.CC = referidoTipo.Tipo;
            cita.CanalTipo = referidoTipo.Tipo;

            cita.ReferidoPor = referidoTipo.Nombre;

            cita.CancelaComentario = cita.CancelaComentario + " + " + usuario + " cambió de " + gestorAnterior + " a " + referidoTipo.Nombre + " el " + DateTime.Now.ToString("dd-MM-yy");

            cita.TipoPago = tipoPago;

            cita.Referencia = referencia == "" ? cita.Referencia : referencia;

            if(ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            byte[] bytesTicket = null;

            var consultaTicket = db.Tickets.Where(w => w.idPaciente == ide).Select(s => new { s.idPaciente, s.idTicket }).FirstOrDefault();

            if (ticket != null && ticket.ContentLength > 0)
            {
                var fileName = Path.GetFileName(ticket.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(ticket.InputStream))
                {
                    bytes = br.ReadBytes(ticket.ContentLength);
                }

                bytesTicket = bytes;

                if (consultaTicket != null)
                {
                    Tickets ticketArchivo = db.Tickets.Find(consultaTicket.idTicket);

                    ticketArchivo.FechaCarga = DateTime.Now;
                    ticketArchivo.idPaciente = Convert.ToInt32(id);
                    ticketArchivo.Ticket = bytesTicket;

                    if (ModelState.IsValid)
                    {
                        db.Entry(ticketArchivo).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    Tickets ticketArchivo = new Tickets();

                    ticketArchivo.FechaCarga = DateTime.Now;
                    ticketArchivo.idPaciente = Convert.ToInt32(id);
                    ticketArchivo.Ticket = bytesTicket;

                    if (ModelState.IsValid)
                    {
                        db.Tickets.Add(ticketArchivo);
                        db.SaveChanges();
                    }
                }

            }

            return Redirect("Index");
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(string nombre,  string telefono, string email, string usuario, string sucursal, string cantidad, string cantidadAereo, string pago,string referencia, int? referido, DateTime? fecha)
        {
            Paciente paciente1 = new Paciente();

            //var revisionPaciente = from i in db.Paciente where i.Nombre == nombre.ToUpper() select i ;

            //if(revisionPaciente != null)
            //{
            //    List<Captura> data = db.Captura.ToList();
            //    JavaScriptSerializer js = new JavaScriptSerializer();
            //    var selected = data.Where(r => r.NombrePaciente == nombre.ToUpper())
            //        .Select(S => new {
            //            idCaptura = S.idCaptura,
            //            S.NombrePaciente,
            //            S.TipoTramite,
            //            S.NoExpediente,
            //            S.Sucursal,
            //            S.Doctor,
            //            S.EstatusCaptura
            //        }).FirstOrDefault();

            //    return Json(selected, JsonRequestBehavior.AllowGet);
            //}

            string canal = null;

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


            if ((cantidadN + cantidadA) == 1)
            {
                Paciente paciente = new Paciente();
                paciente.Nombre = nombre.ToUpper()/*.Normalize(System.Text.NormalizationForm.FormD).Replace(@"´¨", "")*/;
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
                //if(Convert.ToInt32(doc) < 10)
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

                CarruselMedico cm = new CarruselMedico();
                cm.idPaciente = paciente.idPaciente;

                if (ModelState.IsValid)
                {
                    db.CarruselMedico.Add(cm);
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

                cita.idPaciente = idPaciente;
                cita.FechaReferencia = DateTime.Now;
                cita.Sucursal = sucursal;
                cita.FechaCita = fecha != null ? fecha : DateTime.Now;
                cita.Recepcionista = usuario;
                cita.EstatusPago = "Pagado";
                cita.Referencia = referencia;
                cita.Folio = numFolio;
                cita.Canal = "Recepción";
                cita.TipoPago = pago;
                cita.FechaCreacion = DateTime.Now;

                //Se usa el idCanal para poder hacer que en Recepción se tenga que editar el nombre si viene de gestor
                cita.idCanal = 1;

                if(referido == 22)
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

                if (pago != "Referencia Scotiabank")
                {
                    var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == referencia select t).FirstOrDefault();

                    if (tipoPago != null)
                    {
                        ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                        refe.EstatusReferencia = "LIBRE";
                        refe.idPaciente = idPaciente;

                        if (ModelState.IsValid)
                        {
                            db.Entry(refe).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    //var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == numero select t).FirstOrDefault();

                    var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == referencia select t).FirstOrDefault();

                    if(tipoPago != null)
                    {
                        ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                        refe.EstatusReferencia = "OCUPADO";

                        if (ModelState.IsValid)
                        {
                            db.Entry(refe).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    
                }

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

                cita.Cuenta = pago == "Pendiente de Pago" ? "CUENTAS X COBRAR" : null;

                var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                cita.CC = referidoTipo.Tipo;
                cita.CanalTipo = referidoTipo.Tipo;

                cita.ReferidoPor = referidoTipo.Nombre;

                if(referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                    referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                    referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136 || referidoTipo.idReferido == 142)
                {
                    cita.Cuenta = "CORPORATIVO";
                }


                //-------------------------------------------------------------
                if (ModelState.IsValid)
                {
                    db.Cita.Add(cita);
                    db.SaveChanges();
                    return RedirectToAction("Index");
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
                    char [] year = (DateTime.Now.Year.ToString()).ToCharArray();
                    string anio = "";

                    for(int i = 2; i < year.Length; i++)
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

                    

                    CarruselMedico cm = new CarruselMedico();
                    cm.idPaciente = paciente.idPaciente;

                    if (ModelState.IsValid)
                    {
                        db.CarruselMedico.Add(cm);
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

                    cita.TipoPago = pago;

                    cita.idPaciente = idPaciente;
                    cita.FechaReferencia = DateTime.Now;
                    cita.Sucursal = sucursal;
                    cita.Recepcionista = usuario;
                    cita.EstatusPago = "Pagado";
                    cita.Folio = numFolio;
                    cita.Referencia = referencia;
                    cita.Canal = "Recepción";
                    cita.FechaCita = fecha != null ? fecha : DateTime.Now;
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


                    //if (referido == "NINGUNO" || referido == "OTRO")
                    //{
                    //    cita.CC = "N/A";
                    //}
                    //else
                    //{
                    //    var referidoTipo = (from r in db.Referido where r.Nombre == referido select r.Tipo).FirstOrDefault();
                    //    cita.CC = referidoTipo;
                    //}

                    cita.Cuenta = pago == "Pendiente de Pago" ? "CUENTAS X COBRAR" : null;

                    var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                    cita.CC = referidoTipo.Tipo;
                    cita.CanalTipo = referidoTipo.Tipo;

                    cita.ReferidoPor = referidoTipo.Nombre;

                    if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                    referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                    referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136 || referidoTipo.idReferido == 142)
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

            return Redirect("Index"); ;
        }

        public ActionResult OrdenSAM(int? id)
        {
            ViewBag.idPaciente = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Orden(string nombre, string telefono, string email, string usuario, string sucursal,  string cantidad, string cantidadAereo, int? referido, DateTime? fecha)
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

                cita.TipoPago = "Referencia OXXO";
                cita.NoOrden = orden.id;
                

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
                cita.Canal = "Recepción";
                cita.FechaCita = fecha != null ? fecha : DateTime.Now;
                cita.FechaCreacion = DateTime.Now;

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
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136 || referidoTipo.idReferido == 142)
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

                    cita.TipoPago = "Referencia OXXO";
                    cita.NoOrden = orden.id;
                 

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
                    cita.Canal = "Recepción";
                    cita.FechaCita = fecha != null ? fecha : DateTime.Now;
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
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136 || referidoTipo.idReferido == 142)
                    {
                        cita.Cuenta = "CORPORATIVO";
                    }


                    if (ModelState.IsValid)
                    {
                        db.Entry(refe).State = EntityState.Modified;
                        db.Cita.Add(cita);
                        db.SaveChanges();
                        //no regresa ya que se debe ver la pantalla de Orden
                        //return RedirectToAction("Index");
                    }
                }
            }

            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap img = encoder.Encode("sdfsdf");
            System.Drawing.Image QR = (System.Drawing.Image)img;

            byte[] imageBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = ms.ToArray();
            }

            ViewBag.QR = imageBytes;

            ViewBag.AEREO = Convert.ToInt32(cantidadA);
            ViewBag.AUTO = Convert.ToInt32(cantidadN);
            ViewBag.Precio = (Convert.ToInt32(cantidadN) * 2842) + (Convert.ToInt32(cantidadA) * 3480);
            return View(detallesOrden);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagoTarjeta(string nombre, string telefono, string email, string usuario, string sucursal, string cantidad, string cantidadAereo, int card, int? referido, DateTime? fecha)
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
                cita.Canal = "Recepción";
                cita.FechaCita = fecha != null ? fecha : DateTime.Now;
                cita.NoOrden = link;
                cita.Referencia = Convert.ToString(card);
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
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136 || referidoTipo.idReferido == 142)
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
                    cita.Canal = "Recepción";
                    cita.FechaCita = fecha != null ? fecha : DateTime.Now;
                    cita.NoOrden = link;
                    cita.Referencia = Convert.ToString(card);
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
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 136 || referidoTipo.idReferido == 142)
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
        public ActionResult EditarNombre(int id, string nombre)
        {
            var paciente = (from i in db.Paciente where i.idPaciente == id select i).FirstOrDefault();
            var cita = (from i in db.Cita where i.idPaciente == id select i).FirstOrDefault();
            paciente.Nombre = nombre.ToUpper();
            cita.idCanal = 1;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarEstatus(int? id, string pago)
        {
            string referenciaNueva = "";
            if (pago != "Referencia Scotiabank")
            {
                //var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == numero select t).FirstOrDefault();

                var referenciaRepetida = (from r in db.Cita where r.idPaciente == id select r.Referencia).FirstOrDefault();
                referenciaNueva = referenciaRepetida;
                var idFlag = (from i in db.Cita where i.Referencia == referenciaRepetida orderby i.idPaciente descending select i).FirstOrDefault();
                var tipoPago = (from t in db.ReferenciasSB where t.idPaciente == idFlag.idPaciente select t).FirstOrDefault();

                if (tipoPago != null)
                {
                    ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                    refe.EstatusReferencia = "LIBRE";
                    refe.idPaciente = 0;

                    if(ModelState.IsValid)
                    {
                        db.Entry(refe).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }


            }
            else
            {
                //var tipoPago = (from t in db.ReferenciasSB where t.ReferenciaSB == numero select t).FirstOrDefault();

                var referenciaRepetida = (from r in db.Cita where r.idPaciente == id select r.Referencia).FirstOrDefault();
                var idFlag = (from i in db.Cita where i.Referencia == referenciaRepetida orderby i.idPaciente descending select i).FirstOrDefault();
                referenciaNueva = (from t in db.ReferenciasSB where t.idPaciente == idFlag.idPaciente select t.ReferenciaSB).FirstOrDefault();
                var tipoPago = (from t in db.ReferenciasSB where t.idPaciente == idFlag.idPaciente select t).FirstOrDefault();

                ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                refe.EstatusReferencia = "OCUPADO";

                if (ModelState.IsValid)
                {
                    db.Entry(refe).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var referencia = (from r in db.Cita where r.idPaciente == id select r.Referencia).FirstOrDefault();
            var consulta = from c in db.Cita where c.Referencia == referencia select c;
            if (ModelState.IsValid)
            {
                CarruselMedico cm = new CarruselMedico();

                foreach (var item in consulta)
                {
                    Cita cita = db.Cita.Find(item.idCita);

                    cm.idPaciente = item.idPaciente;
                    db.CarruselMedico.Add(cm);

                    cita.EstatusPago = "Pagado";
                    cita.TipoPago = pago;
                    cita.Referencia = referenciaNueva;

                    cita.Cuenta = pago == "Pendiente de Pago" ? "CUENTAS X COBRAR" : null;

                    db.Entry(cita).State = EntityState.Modified;
                    
                }
                db.SaveChanges();
            }
            
            return Redirect("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entregado(int? id)
        {
            var idCita = (from i in db.Cita where i.idPaciente == id select i.idCita).FirstOrDefault();
            Cita cita = db.Cita.Find(idCita);

            cita.Entregado = "SI";

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CURP_Expediente(string id, string nombre, string numero, string curp, string tel, string email, string doctor, string tipoL, string tipoT)
        {

            //var noExpedienteRepetido = (from i in db.Cita where i.NoExpediente == numero orderby i.idCita descending select i).FirstOrDefault();


            //if (noExpedienteRepetido != null && noExpedienteRepetido.CancelaComentario != "Sobreescrito")
            //{
            //    var capturaRepetida = (from i in db.Captura where i.idPaciente == noExpedienteRepetido.idPaciente orderby i.idCaptura descending select i).FirstOrDefault();

            //    if(capturaRepetida != null && capturaRepetida.EstatusCaptura != "Terminado")
            //    {
            //        TempData["idCaptura"] = capturaRepetida.idCaptura;
            //        return Redirect("Index");
            //    }
            //    else
            //    {
            //        TempData["idPaciente"] = id;
            //        TempData["Doctor"] = doctor;
            //        TempData["TipoLicencia"] = tipoL;
            //        TempData["TipoTramite"] = tipoT;

            //        return Redirect("Index");
            //    }
            //}

            int ide = Convert.ToInt32(id);

            string NOMBRE = null;
            string TELEFONO = null;
            string EMAIL = null;
            string NOEXP = null;
            string CURP = null;

            var paciente = (from p in db.Paciente where p.idPaciente == ide select p).FirstOrDefault();
            var cita = (from p in db.Cita where p.idPaciente == ide select p).FirstOrDefault();

            if(nombre == "")
            {
                NOMBRE = paciente.Nombre;
            }
            else
            {
                NOMBRE = nombre.ToUpper();
            }

            if (tel == "")
            {
                TELEFONO = paciente.Telefono;
            }
            else
            {
                TELEFONO = tel;
            }

            if(email == "")
            {
                EMAIL = paciente.Email;
            }
            else
            {
                EMAIL = email;
            }

            if (curp == "")
            {
                CURP = paciente.CURP;
            }
            else
            {
                CURP = curp.ToUpper();
            }

            if (numero == "")
            {
                NOEXP = cita.NoExpediente;
            }
            else
            {
                NOEXP = numero;
            }

            paciente.Nombre = NOMBRE.ToUpper();
            paciente.CURP = CURP;
            paciente.Email = EMAIL;
            paciente.Telefono = TELEFONO;
            cita.NoExpediente = NOEXP;

            cita.TipoLicencia = tipoL;
            cita.Doctor = doctor;
            cita.TipoTramite = tipoT;

            //CarruselMedico cm = new CarruselMedico();
            //cm.idPaciente = paciente.idPaciente;

            var capturaExistente = (from i in db.Captura where i.idPaciente == ide select i).FirstOrDefault();
            var epivirtual = (from i in db.EPI_DictamenAptitud where i.idPaciente == ide select i).FirstOrDefault();

            if (capturaExistente == null && epivirtual != null)
            {
                Captura captura = new Captura();

                captura.idPaciente = ide;
                captura.NombrePaciente = NOMBRE.ToUpper();
                captura.NoExpediente = NOEXP;
                captura.TipoTramite = tipoT;
                captura.EstatusCaptura = "No iniciado";
                captura.Doctor = doctor;
                captura.Sucursal = cita.Sucursal;
                captura.FechaExpdiente = DateTime.Now;
                captura.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Captura.Add(captura);
                    db.SaveChanges();
                }
            }

            if (ModelState.IsValid)
            {
                //db.CarruselMedico.Add(cm);
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Digitalizar(HttpPostedFileBase file, string id, string usuario, string nombre, string doctor, string numero, string tipoL, 
            string tipoT, string curp, DateTime? fecha, HttpPostedFileBase ticket)
        {

            //var noExpedienteRepetido = (from i in db.Cita where i.NoExpediente == numero orderby i.idCita descending select i).FirstOrDefault();

            //if (noExpedienteRepetido != null && noExpedienteRepetido.CancelaComentario != "Sobreescrito")
            //{
            //    var capturaRepetida = (from i in db.Captura where i.idPaciente == noExpedienteRepetido.idPaciente orderby i.idCaptura descending select i).FirstOrDefault();

            //    if (capturaRepetida != null && capturaRepetida.EstatusCaptura != "Terminado")
            //    {
            //        TempData["idCaptura"] = capturaRepetida.idCaptura;
            //        return Redirect("Index");
            //    }
            //    else
            //    {
            //        TempData["idPaciente"] = id;
            //        TempData["Doctor"] = doctor;
            //        TempData["TipoLicencia"] = tipoL;
            //        TempData["TipoTramite"] = tipoT;

            //        return Redirect("Index");
            //    }
            //}


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

            byte[] bytesTicket = null;

            var consultaTicket = db.Tickets.Where(w => w.idPaciente == ide).Select(s => new { s.idPaciente, s.idTicket }).FirstOrDefault();

            if (ticket != null && ticket.ContentLength > 0)
            {
                var fileName = Path.GetFileName(ticket.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(ticket.InputStream))
                {
                    bytes = br.ReadBytes(ticket.ContentLength);
                }

                bytesTicket = bytes;

                if(consultaTicket != null)
                {
                    Tickets ticketArchivo = db.Tickets.Find(consultaTicket.idTicket);

                    ticketArchivo.FechaCarga = DateTime.Now;
                    ticketArchivo.idPaciente = Convert.ToInt32(id);
                    ticketArchivo.Ticket = bytesTicket;

                    if (ModelState.IsValid)
                    {
                        db.Entry(ticketArchivo).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    Tickets ticketArchivo = new Tickets();

                    ticketArchivo.FechaCarga = DateTime.Now;
                    ticketArchivo.idPaciente = Convert.ToInt32(id);
                    ticketArchivo.Ticket = bytesTicket;

                    if (ModelState.IsValid)
                    {
                        db.Tickets.Add(ticketArchivo);
                        db.SaveChanges();
                    }
                }
                
            }

            string CURP = null;
            string NOMBRE = null;
            string NOEXPEDIENTE = null;
            DateTime FECHA = DateTime.Now;

            if (nombre == "")
            {
                NOMBRE = paciente.Nombre;
            }
            else
            {
                NOMBRE = nombre.ToUpper();
            }

            if (numero == "")
            {
                NOEXPEDIENTE = cita.NoExpediente;
            }
            else
            {
                NOEXPEDIENTE = numero;
            }

            if (curp == "")
            {
                CURP = paciente.CURP;
            }
            else
            {
                CURP = curp;
            }

            if(fecha == null)
            {
                FECHA = Convert.ToDateTime(cita.FechaCita);
            }

            exp.Expediente = bytes2;
            exp.Recpecionista = usuario;
            exp.idPaciente = ide;
            paciente.Nombre = NOMBRE;
            paciente.CURP = CURP.ToUpper();
            captura.NombrePaciente = NOMBRE;
            captura.NoExpediente = NOEXPEDIENTE;
            captura.TipoTramite = tipoT;
            captura.EstatusCaptura = "No iniciado";
            captura.Doctor = doctor;
            captura.Sucursal = cita.Sucursal;
            captura.idPaciente = Convert.ToInt32(id);
            captura.FechaExpdiente = FECHA;

            Cita citaModificar = new Cita();
            cita.TipoLicencia = tipoL;
            cita.TipoTramite = tipoT;
            cita.NoExpediente = NOEXPEDIENTE;
            cita.Doctor = doctor;


            //Se obtienen las abreviaciónes de Sucursal y el ID del doctor
            string SUC = (from S in db.Sucursales where S.Nombre == cita.Sucursal select S.SUC).FirstOrDefault();
           string doc = (from d in db.Doctores where d.Nombre == doctor select d.idDoctor).FirstOrDefault().ToString();

            //Se obtiene el número del contador desde la base de datos del último registro de Folio incompleto
            int? num = (from c in db.Sucursales where c.Nombre == cita.Sucursal select c.Contador).FirstOrDefault() + 1;
            //var num = new string(cita.Folio.Reverse().Take(3).Reverse().ToArray());

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

            string numFolio = dia + mes + anio + SUC + doc + contador;
            paciente.Folio = dia + mes + anio + SUC + doc + contador;
            cita.Folio = dia + mes + anio + SUC + doc + contador;

            int? idSuc = (from i in db.Sucursales where i.Nombre == cita.Sucursal select i.idSucursal).FirstOrDefault();

            Sucursales suc = db.Sucursales.Find(idSuc);

            suc.Contador = Convert.ToInt32(num);

            if (ModelState.IsValid)
            {
                db.Entry(suc).State = EntityState.Modified;
                db.SaveChanges();
                //No retorna ya que sigue el proceso
                //return RedirectToAction("Index");
            }

            var capturaExistente = (from i in db.Captura where i.idPaciente == ide select i).FirstOrDefault();

            if (capturaExistente != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(captura).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Captura.Add(captura);
                    db.SaveChanges();
                }
            }


            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                //db.Cita.Add(cita);
                db.Expedientes.Add(exp);
                //db.Captura.Add(captura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //return View(exp);
            return RedirectToAction("Index");
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
            DateTime treintaDias = DateTime.Now.AddDays(364);
            long marcaTiempo = (Int64)(treintaDias.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            //string tiempo = marcaTiempo.ToString();
            return marcaTiempo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCompleto(HttpPostedFileBase file, string id, string nombre, string doctor, string numero, string tipoL, string tipoT
            , string pago, string telefono, string email, string referencia, string curp, DateTime? fecha, HttpPostedFileBase ticket)
        {
            int ide = Convert.ToInt32(id);

            Paciente paciente = db.Paciente.Find(ide);
            var cita = (from c in db.Cita where c.idPaciente == ide select c).FirstOrDefault();
            var expediente = (from c in db.Expedientes where c.idPaciente == ide select c).FirstOrDefault();
            var captura = (from c in db.Captura where c.idPaciente == ide select c).FirstOrDefault();

            string ID = null;
            string NOMBRE = null;
            string DOCTOR = null;
            string NUMERO = null;
            string TIPOL = null;
            string TIPOT = null;
            string PAGO = null;
            string TELEFONO = null;
            string EMAIL = null;
            string REFERENCIA = null;
            string CURP = null;
            DateTime FECHA = Convert.ToDateTime(fecha);

            if(id == null)
            {
                ID = paciente.idPaciente.ToString();
            }
            else
            {
                ID = id;
            }

            if (nombre == "")
            {
                NOMBRE = paciente.Nombre.ToUpper();
            }
            else
            {
                NOMBRE = nombre.ToUpper();
            }

            if (doctor == "")
            {
                DOCTOR = cita.Doctor;
            }
            else
            {
                DOCTOR = doctor;
            }

            if (numero == "")
            {
                NUMERO = cita.NoExpediente;
            }
            else
            {
                NUMERO = numero;
            }

            if (tipoL == "")
            {
                TIPOL = cita.TipoLicencia;
            }
            else
            {
                TIPOL = tipoL;
            }

            if (tipoT == "")
            {
                TIPOT = cita.TipoTramite;
            }
            else
            {
                TIPOT = tipoT;
            }

            if (pago == "" || pago == null)
            {
                PAGO = cita.TipoPago;
            }
            else
            {
                PAGO = pago;
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

            if (referencia == "")
            {
                REFERENCIA = cita.Referencia;
            }
            else
            {
                REFERENCIA = referencia;
            }

            if (curp == "")
            {
                CURP = paciente.CURP;
            }
            else
            {
                CURP = curp.ToUpper();
            }

            if(fecha == null)
            {
                FECHA = Convert.ToDateTime(cita.FechaCita);
            }

            if (expediente != null)
            {
                byte[] bytes2 = expediente.Expediente;

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(file.InputStream))
                    {
                        bytes = br.ReadBytes(file.ContentLength);
                    }

                    bytes2 = bytes;
                    expediente.Expediente = bytes2;

                    if (ModelState.IsValid)
                    {
                        db.Entry(expediente).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            byte[] bytesTicket = null;

            var consultaTicket = db.Tickets.Where(w => w.idPaciente == ide).Select(s => new { s.idPaciente, s.idTicket }).FirstOrDefault();

            if (ticket != null && ticket.ContentLength > 0)
            {
                var fileName = Path.GetFileName(ticket.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(ticket.InputStream))
                {
                    bytes = br.ReadBytes(ticket.ContentLength);
                }

                bytesTicket = bytes;

                if (consultaTicket != null)
                {
                    Tickets ticketArchivo = db.Tickets.Find(consultaTicket.idTicket);

                    ticketArchivo.FechaCarga = DateTime.Now;
                    ticketArchivo.idPaciente = Convert.ToInt32(id);
                    ticketArchivo.Ticket = bytesTicket;

                    if (ModelState.IsValid)
                    {
                        db.Entry(ticketArchivo).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    Tickets ticketArchivo = new Tickets();

                    ticketArchivo.FechaCarga = DateTime.Now;
                    ticketArchivo.idPaciente = Convert.ToInt32(id);
                    ticketArchivo.Ticket = bytesTicket;

                    if (ModelState.IsValid)
                    {
                        db.Tickets.Add(ticketArchivo);
                        db.SaveChanges();
                    }
                }

            }

            paciente.Nombre = NOMBRE;
            paciente.Telefono = TELEFONO;
            paciente.Email = EMAIL;
            paciente.CURP = CURP;

            cita.TipoPago = PAGO;
            cita.TipoLicencia = TIPOL;
            cita.NoExpediente = NUMERO;
            cita.TipoTramite = TIPOT;
            //cita.Referencia = REFERENCIA;
            cita.Doctor = DOCTOR;

            captura.TipoTramite = TIPOT;
            captura.NombrePaciente = NOMBRE;
            captura.NoExpediente = NUMERO;
            captura.Doctor = DOCTOR;
            captura.FechaExpdiente = FECHA;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Redirect("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubirArchivosEKG(HttpPostedFileBase EKG, HttpPostedFileBase NoAccidentes, HttpPostedFileBase Glucosilada, HttpPostedFileBase Otros, string id)
        {
            int ide = Convert.ToInt32(id);

            Paciente paciente = db.Paciente.Find(ide);
            var archivos = (from c in db.Archivos where c.idPaciente == ide select c).FirstOrDefault();

            if(EKG != null)
            {
                byte[] bytes2;
                var fileName = Path.GetFileName(EKG.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(EKG.InputStream))
                {
                    bytes = br.ReadBytes(EKG.ContentLength);
                }
                bytes2 = bytes;

                if (archivos == null)
                {
                    Archivos pdf = new Archivos();
                    pdf.ElectroCardiograma = bytes2;
                    pdf.idPaciente = ide;

                    if (ModelState.IsValid)
                    {
                        db.Archivos.Add(pdf);
                        db.SaveChanges();
                    }
                }
                else
                {
                    Archivos pdf = db.Archivos.Find(archivos.idArchivos);
                    pdf.ElectroCardiograma = bytes2;

                    if (ModelState.IsValid)
                    {
                        db.Entry(pdf).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            if (NoAccidentes != null)
            {
                byte[] bytes2;
                var fileName = Path.GetFileName(NoAccidentes.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(NoAccidentes.InputStream))
                {
                    bytes = br.ReadBytes(NoAccidentes.ContentLength);
                }
                bytes2 = bytes;

                if (archivos == null)
                {
                    Archivos pdf = new Archivos();
                    pdf.NoAccidentes = bytes2;
                    pdf.idPaciente = ide;

                    if (ModelState.IsValid)
                    {
                        db.Archivos.Add(pdf);
                        db.SaveChanges();
                    }
                }
                else
                {
                    Archivos pdf = db.Archivos.Find(archivos.idArchivos);
                    pdf.NoAccidentes = bytes2;

                    if (ModelState.IsValid)
                    {
                        db.Entry(pdf).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            if (Glucosilada != null)
            {
                byte[] bytes2;
                var fileName = Path.GetFileName(Glucosilada.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(Glucosilada.InputStream))
                {
                    bytes = br.ReadBytes(Glucosilada.ContentLength);
                }
                bytes2 = bytes;

                if (archivos == null)
                {
                    Archivos pdf = new Archivos();
                    pdf.HGlucosilada = bytes2;
                    pdf.idPaciente = ide;

                    if (ModelState.IsValid)
                    {
                        db.Archivos.Add(pdf);
                        db.SaveChanges();
                    }
                }
                else
                {
                    Archivos pdf = db.Archivos.Find(archivos.idArchivos);
                    pdf.HGlucosilada = bytes2;

                    if (ModelState.IsValid)
                    {
                        db.Entry(pdf).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            if (Otros != null)
            {
                byte[] bytes2;
                var fileName = Path.GetFileName(Otros.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(Otros.InputStream))
                {
                    bytes = br.ReadBytes(Otros.ContentLength);
                }
                bytes2 = bytes;

                if (archivos == null)
                {
                    Archivos pdf = new Archivos();
                    pdf.ArchivosExtra = bytes2;
                    pdf.idPaciente = ide;

                    if (ModelState.IsValid)
                    {
                        db.Archivos.Add(pdf);
                        db.SaveChanges();
                    }
                }
                else
                {
                    Archivos pdf = db.Archivos.Find(archivos.idArchivos);
                    pdf.ArchivosExtra = bytes2;

                    if (ModelState.IsValid)
                    {
                        db.Entry(pdf).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            return Redirect("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubirRevaloracion(HttpPostedFileBase file, int id)
        {
            int ide = Convert.ToInt32(id);

            Paciente paciente = db.Paciente.Find(ide);
            ExpedienteRevaloracion exp = new ExpedienteRevaloracion();

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
            }

            exp.ExpedienteCompleto = bytes2;
            exp.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.ExpedienteRevaloracion.Add(exp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Redirect("Index");
        }

        public ActionResult NoLlego(int id, string comentario, string pago)
        {
            var cita = (from c in db.Cita where c.idPaciente == id select c).FirstOrDefault();
            cita.Asistencia = "NO";
            cita.CancelaComentario = comentario;
            

            //Proceso para cancelar también las referencias Scotiabank que no se utilicen
            var referenciaRepetida = (from r in db.Cita where r.idPaciente == id select r.Referencia).FirstOrDefault();

            var idFlag = (from i in db.Cita where i.Referencia == referenciaRepetida orderby i.idPaciente descending select i).FirstOrDefault();
            var tipoPago = (from t in db.ReferenciasSB where t.idPaciente == idFlag.idPaciente select t).FirstOrDefault();

            if(pago != "Pago con Tarjeta" && pago != "Transferencia vía Scotiabank" && pago != "Transferencia vía Banbajío" && tipoPago != null)
            {
                if ((from r in db.Cita where r.Referencia == referenciaRepetida select r).Count() == 1)
                {
                    ReferenciasSB refe = db.ReferenciasSB.Find(tipoPago.idReferencia);
                    refe.EstatusReferencia = "LIBRE";
                    refe.idPaciente = 0;

                    if (ModelState.IsValid)
                    {
                        db.Entry(refe).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            
            //--------------------------------------------------------------------------------------

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Redirect("Index");
        }


        public JsonResult Buscar(string dato)
        {
            string parametro;

            if(dato.All(char.IsDigit))
            {
                parametro = dato;

                List<Paciente> data = db.Paciente.ToList();
                JavaScriptSerializer js = new JavaScriptSerializer();
                var selected = data.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                    //.Where(r => r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro)
                    .Where(r => r.M.NoExpediente == parametro )
                    .Join(db.Captura, o => o.N.idPaciente, p => p.idPaciente, (o, p) => new { O = o, P = p })
                    .Select(S => new {
                        S.O.N.idPaciente,
                        S.O.N.Nombre,
                        S.O.M.TipoPago,
                        FechaCita = Convert.ToDateTime(S.O.M.FechaCita).ToString("dd-MMMM-yyyy"),
                        S.O.M.TipoLicencia,
                        S.O.M.NoExpediente,
                        S.O.M.Sucursal,
                        S.O.M.TipoTramite,
                        S.P.EstatusCaptura
                    });

                return Json(selected, JsonRequestBehavior.AllowGet);
            }
            else
            {
                parametro = dato.ToUpper();

                double porcentaje = 1;

                if(db.Paciente.Count() > 5000 && db.Paciente.Count() < 9000)
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
                    .Where(r => r.N.Nombre == parametro)
                    .Join(db.Captura, o => o.N.idPaciente, p => p.idPaciente, (o, p) => new { O = o, P = p })
                    .Select(S => new {
                        S.O.N.idPaciente,
                        S.O.N.Nombre,
                        S.O.M.TipoPago,
                        FechaCita = Convert.ToDateTime(S.O.M.FechaCita).ToString("dd-MMMM-yyyy"),
                        S.O.M.TipoLicencia,
                        S.O.M.NoExpediente,
                        S.O.M.Sucursal,
                        S.O.M.TipoTramite,
                        S.P.EstatusCaptura
                    });

                return Json(selected, JsonRequestBehavior.AllowGet);
            }

            //List<Paciente> data = db.Paciente.Where(w => w.idPaciente > (db.Paciente.Count() / 3)).ToList();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //var selected = data.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
            //    //.Where(r => r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro)
            //    .Where(r => r.N.Nombre == parametro || r.M.NoExpediente == parametro)
            //    .Join(db.Captura, o => o.N.idPaciente, p => p.idPaciente, (o, p) => new { O = o, P = p })
            //    .Select(S => new {
            //        S.O.N.idPaciente,
            //        S.O.N.Nombre,
            //        S.O.M.TipoPago,
            //        FechaCita = Convert.ToDateTime(S.O.M.FechaCita).ToString("dd-MMMM-yyyy"),
            //        S.O.M.TipoLicencia,
            //        S.O.M.NoExpediente,
            //        S.O.M.Sucursal,
            //        S.O.M.TipoTramite,
            //        S.P.EstatusCaptura
            //    });

            //return Json(selected, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrdenScotiabank(string nombre, string telefono, string email, string usuario, string sucursal, string cantidad, string cantidadAereo, string referido)
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

            int precio = (cantidadN * 3480) + (cantidadA * 4640);

            if (precio > 10000)
            {
                precio = 9990;
            }

            if (cantidadAereo == "" && cantidad == "")
            {
                return View("Index");
            }


            var referenciaSB = (from r in db.ReferenciasSB where r.EstatusReferencia == "LIBRE" select r.ReferenciaSB).FirstOrDefault();
            ViewBag.ReferenciaSB = referenciaSB;

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

                cita.TipoPago = "Referencia Scotiabank";

                cita.Referencia = referenciaSB;

                cita.idPaciente = idPaciente;
                cita.FechaReferencia = DateTime.Now;
                cita.Sucursal = sucursal;
                cita.Recepcionista = usuario;
                cita.EstatusPago = "pending_payment";
                cita.Folio = numFolio;
                cita.Canal = "Recepción";
                cita.FechaCita = DateTime.Now;
                cita.ReferidoPor = referido.ToUpper();
                cita.FechaCreacion = DateTime.Now;

                //Se usa el idCanal para poder hacer que en Recepción se tenga que editar el nombre si viene de gestor
                cita.idCanal = 1;

                if (referido == "ELIZABETH")
                {
                    cita.Referencia = "E1293749";
                }
                if (referido == "PABLO")
                {
                    cita.Referencia = "PL1293750";
                }
                if (referido == "NATALY FRANCO")
                {
                    cita.Referencia = "NF1293751";
                }
                if (referido == "LUIS VALENCIA")
                {
                    cita.Referencia = "LV1293752";
                }
                if (referido == "ROBERTO SALAZAR")
                {
                    cita.Referencia = "RS1293753";
                }

                int idRefSB = Convert.ToInt32((from r in db.ReferenciasSB where r.ReferenciaSB == referenciaSB select r.idReferencia).FirstOrDefault());
                ReferenciasSB refe = db.ReferenciasSB.Find(idRefSB);
                refe.EstatusReferencia = "PENDIENTE";
                refe.idPaciente = idPaciente;

                string TIPOLIC = null;
                if (cantidadA != 0)
                {
                    TIPOLIC = "AEREO";
                }
                cita.TipoLicencia = TIPOLIC;

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
                    db.Entry(refe).State = EntityState.Modified;
                    db.SaveChanges();
                    //no regresa ya que se debe ver la pantalla de Orden
                    //return RedirectToAction("Index");
                }

                ViewBag.idPaciente = paciente.idPaciente;
                ViewBag.idCita = cita.idCita;
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

                    cita.TipoPago = "Referencia Scotiabank";

                    cita.Referencia = referenciaSB;

                    cita.idPaciente = idPaciente;
                    cita.FechaReferencia = DateTime.Now;
                    cita.Sucursal = sucursal;
                    cita.Recepcionista = usuario;
                    cita.EstatusPago = "pending_payment";
                    cita.Folio = numFolio;
                    cita.Canal = "Recepción";
                    cita.FechaCita = DateTime.Now;
                    cita.ReferidoPor = referido.ToUpper();
                    cita.FechaCreacion = DateTime.Now;

                    if (referido == "ELIZABETH")
                    {
                        cita.Referencia = "E1293749";
                    }
                    if (referido == "PABLO")
                    {
                        cita.Referencia = "PL1293750";
                    }
                    if (referido == "NATALY FRANCO")
                    {
                        cita.Referencia = "NF1293751";
                    }
                    if (referido == "LUIS VALENCIA")
                    {
                        cita.Referencia = "LV1293752";
                    }
                    if (referido == "ROBERTO SALAZAR")
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
                        db.Entry(refe).State = EntityState.Modified;
                        db.Cita.Add(cita);
                        db.SaveChanges();
                        //no regresa ya que se debe ver la pantalla de Orden
                        //return RedirectToAction("Index");
                    }

                    ViewBag.idPaciente = paciente.idPaciente;
                    ViewBag.idCita = cita.idCita;
                }
            }

            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap img = encoder.Encode("sdfsdf");
            System.Drawing.Image QR = (System.Drawing.Image)img;

            byte[] imageBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = ms.ToArray();
            }

            ViewBag.QR = imageBytes;

            ViewBag.AEREO = Convert.ToInt32(cantidadA);
            ViewBag.AUTO = Convert.ToInt32(cantidadN);
            ViewBag.Precio = (Convert.ToInt32(cantidadN) * 2842) + (Convert.ToInt32(cantidadA) * 3480);

            
            return View();
        }

        public JsonResult BuscarDictamen(string dato)
        {
            string parametro;

            if (dato.All(char.IsDigit))
            {
                parametro = dato;

                List<Captura> data = db.Captura.ToList();
                JavaScriptSerializer js = new JavaScriptSerializer();
                var selected = data.Where(r => r.NoExpediente == parametro)
                    .Select(S => new {
                        idCaptura = S.idCaptura,
                        S.NombrePaciente,
                        S.TipoTramite,
                        S.NoExpediente,
                        S.Sucursal,
                        S.Doctor,
                        S.EstatusCaptura
                    }).FirstOrDefault();

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

                List<Captura> data = db.Captura.Where(w => w.idPaciente > (db.Paciente.Count() * porcentaje)).ToList();
                JavaScriptSerializer js = new JavaScriptSerializer();
                var selected = data.Where(r => r.NombrePaciente == parametro)
                    .Select(S => new {
                        idCaptura = S.idCaptura,
                        S.NombrePaciente,
                        S.TipoTramite,
                        S.NoExpediente,
                        S.Sucursal,
                        S.Doctor,
                        S.EstatusCaptura
                    }).FirstOrDefault();

                return Json(selected, JsonRequestBehavior.AllowGet);
            }

            //List<Captura> data = db.Captura.ToList();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //var selected = data.Where(r => r.NombrePaciente == parametro || r.NoExpediente == parametro)
            //    .Select(S => new {
            //        idCaptura = S.idCaptura,
            //        S.NombrePaciente,
            //        S.TipoTramite,
            //        S.NoExpediente,
            //        S.Sucursal,
            //        S.Doctor,
            //        S.EstatusCaptura
            //    }).FirstOrDefault();

            //return Json(selected, JsonRequestBehavior.AllowGet);
        }

    }
}
