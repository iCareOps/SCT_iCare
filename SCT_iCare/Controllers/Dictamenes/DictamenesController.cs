using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.Dictamenes
{
    public class DictamenesController : Controller
    {
        GMIEntities db = new GMIEntities();

        // GET: Dictamenes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Citas(DateTime? inicio, DateTime? final)
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

            return View();
        }

        public ActionResult Captura(DateTime? inicio, DateTime? final)
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

            return View();
        }

        public ActionResult VentasAlternativas(DateTime? inicio, DateTime? final, string gestor, string estatus)
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

            if(urge.Mes != mes)
            {
                urge.Mes = mes;
                urge.Contador = 500;

                if(ModelState.IsValid)
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
            ViewBag.Gestor = gestor;
            ViewBag.Estatus = estatus;

            ViewBag.Parameter = "";

            return View();
        }


        public ActionResult Dashboard(DateTime? inicio, DateTime? final)
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

            inicio = (inicio ?? start1);
            final = (final ?? finish1);

            ViewBag.Inicio = inicio;
            ViewBag.Final = final;
            ViewBag.Estado = nulos;

            ViewBag.Parameter = "";

            return View();
        }

        [HttpPost]
        public ActionResult Create1(string nombre, string usuario, /*string sucursal, */string cantidad, string cantidadAereo, int? referido)
        {
            PacienteESP paciente1 = new PacienteESP();

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
                PacienteESP paciente = new PacienteESP();
                paciente.Nombre = nombre.ToUpper()/*.Normalize(System.Text.NormalizationForm.FormD).Replace(@"´¨", "")*/;
                paciente.FechaCita = DateTime.Now;
                paciente.FechaSolicitud = DateTime.Now;
                //paciente.Sucursal = sucursal;
                paciente.Solicita = usuario;


                string TIPOLIC = null;
                if (cantidadA != 0)
                {
                    TIPOLIC = "AEREO";
                }
                else
                {
                    TIPOLIC = "AUTOTRANSPORTE";
                }
                paciente.TipoLicencia = TIPOLIC;

                var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                paciente.CanalTipo = referidoTipo.Tipo;

                paciente.ReferidoPor = referidoTipo.Nombre;

                paciente.Cuenta = "CUENTAS X COBRAR";
                paciente.TipoPago = "Pendiente de Pago";

                if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 138 || referidoTipo.idReferido == 139)
                {
                    paciente.Cuenta = "CORPORATIVO";
                }

                if (ModelState.IsValid)
                {
                    db.PacienteESP.Add(paciente);

                    db.SaveChanges();
                }

            }
            else
            {
                //return View(detallesOrden);
                for (int n = 1; n <= Convert.ToInt32((cantidadN + cantidadA)); n++)
                {
                    PacienteESP paciente = new PacienteESP();
                    paciente.Nombre = nombre.ToUpper() + " " + n;
                    //paciente.Sucursal = sucursal;
                    paciente.Solicita = usuario;
                    paciente.FechaCita = DateTime.Now;
                    paciente.FechaSolicitud = DateTime.Now;


                    if (n > cantidadN)
                    {
                        paciente.TipoLicencia = "AEREO";
                    }
                    else
                    {
                        paciente.TipoLicencia = "AUTOTRANSPORTE";
                    }

                    var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                    paciente.CanalTipo = referidoTipo.Tipo;

                    paciente.ReferidoPor = referidoTipo.Nombre;

                    paciente.Cuenta = "CUENTAS X COBRAR";
                    paciente.TipoPago = "Pendiente de Pago";

                    if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                    referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                    referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 138 || referidoTipo.idReferido == 139)
                    {
                        paciente.Cuenta = "CORPORATIVO";
                    }

                    if (ModelState.IsValid)
                    {
                        db.PacienteESP.Add(paciente);
                        db.SaveChanges();
                    }
                }
            }

            return Redirect("Citas"); ;
        }


        [HttpPost]
        public ActionResult CreateVentas(string nombre, string usuario, /*string sucursal, */string cantidad, string cantidadAereo, int? referido, string urgente, string pagoGestor)
        {
            PacienteESP paciente1 = new PacienteESP();

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
                PacienteESP paciente = new PacienteESP();
                paciente.Nombre = nombre.ToUpper()/*.Normalize(System.Text.NormalizationForm.FormD).Replace(@"´¨", "")*/;
                paciente.FechaCita = DateTime.Now;
                paciente.FechaSolicitud = DateTime.Now;
                //paciente.Sucursal = sucursal;
                paciente.Solicita = usuario;

                if(urgente == "on")
                {
                    paciente.CancelaComentario = "Urgente";
                    var urge = (from i in db.UrgentesCount select i).FirstOrDefault();

                    if(urge.Contador >= 1)
                    {
                        urge.Contador -= 1;

                        if (ModelState.IsValid)
                        {
                            db.Entry(urge).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }          
                }

                string TIPOLIC = null;
                if (cantidadA != 0)
                {
                    TIPOLIC = "AEREO";
                }
                else
                {
                    TIPOLIC = "AUTOTRANSPORTE";
                }
                paciente.TipoLicencia = TIPOLIC;

                var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                paciente.CanalTipo = referidoTipo.Tipo;

                paciente.ReferidoPor = referidoTipo.Nombre;

                paciente.Cuenta = "CUENTAS X COBRAR";
                paciente.TipoPago = "Pendiente de Pago";

                if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 138 || referidoTipo.idReferido == 139)
                {
                    paciente.Cuenta = "CORPORATIVO";
                }


                if (ModelState.IsValid)
                {
                    db.PacienteESP.Add(paciente);

                    db.SaveChanges();
                }

            }
            else
            {
                for (int n = 1; n <= Convert.ToInt32((cantidadN + cantidadA)); n++)
                {
                    PacienteESP paciente = new PacienteESP();
                    paciente.Nombre = nombre.ToUpper() + " " + n;
                    //paciente.Sucursal = sucursal;
                    paciente.Solicita = usuario;
                    paciente.FechaCita = DateTime.Now;
                    paciente.FechaSolicitud = DateTime.Now;

                    if (urgente == "on")
                    {
                        paciente.CancelaComentario = "Urgente";
                        var urge = (from i in db.UrgentesCount select i).FirstOrDefault();

                        if (urge.Contador >= Convert.ToInt32((cantidadN + cantidadA)))
                        {
                            urge.Contador -= Convert.ToInt32((cantidadN + cantidadA));

                            if (ModelState.IsValid)
                            {
                                db.Entry(urge).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }

                    if (n > cantidadN)
                    {
                        paciente.TipoLicencia = "AEREO";
                    }
                    else
                    {
                        paciente.TipoLicencia = "AUTOTRANSPORTE";
                    }

                    var referidoTipo = (from r in db.Referido where r.idReferido == referido select r).FirstOrDefault();
                    paciente.CanalTipo = referidoTipo.Tipo;

                    paciente.ReferidoPor = referidoTipo.Nombre;

                    paciente.Cuenta = "CUENTAS X COBRAR";
                    paciente.TipoPago = "Pendiente de Pago";

                    if (referidoTipo.idReferido == 7 || referidoTipo.idReferido == 8 || referidoTipo.idReferido == 9 || referidoTipo.idReferido == 10 || referidoTipo.idReferido == 12 ||
                    referidoTipo.idReferido == 13 || referidoTipo.idReferido == 20 || referidoTipo.idReferido == 26 || referidoTipo.idReferido == 27 || referidoTipo.idReferido == 29 ||
                    referidoTipo.idReferido == 36 || referidoTipo.idReferido == 52 || referidoTipo.idReferido == 53 || referidoTipo.idReferido == 54 || referidoTipo.idReferido == 55 ||
                    referidoTipo.idReferido == 56 || referidoTipo.idReferido == 59 || referidoTipo.idReferido == 130 || referidoTipo.idReferido == 138 || referidoTipo.idReferido == 139)
                    {
                        paciente.Cuenta = "CORPORATIVO";
                    }

                    if (ModelState.IsValid)
                    {
                        db.PacienteESP.Add(paciente);
                        db.SaveChanges();
                    }
                }
            }

            return Redirect("VentasAlternativas"); 
        }

        [HttpPost]
        public ActionResult SubirHuellas(HttpPostedFileBase h2, HttpPostedFileBase h3, HttpPostedFileBase h7, HttpPostedFileBase h8)
        {
            byte[] bytesh2 = null;
            byte[] bytesh3 = null;
            byte[] bytesh7 = null;
            byte[] bytesh8 = null;

            if (h3 != null && h3.ContentLength > 0)
            {
                var fileName = Path.GetFileName(h3.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(h3.InputStream))
                {
                    bytes = br.ReadBytes(h3.ContentLength);

                    bytesh3 = bytes;
                }
            }

            if (h2 != null && h2.ContentLength > 0)
            {
                var fileName = Path.GetFileName(h2.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(h2.InputStream))
                {
                    bytes = br.ReadBytes(h2.ContentLength);

                    bytesh2 = bytes;
                }
            }

            if (h7 != null && h7.ContentLength > 0)
            {
                var fileName = Path.GetFileName(h7.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(h7.InputStream))
                {
                    bytes = br.ReadBytes(h7.ContentLength);

                    bytesh7 = bytes;
                }
            }

            if (h8 != null && h8.ContentLength > 0)
            {
                var fileName = Path.GetFileName(h8.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(h8.InputStream))
                {
                    bytes = br.ReadBytes(h8.ContentLength);

                    bytesh8 = bytes;
                }
            }

            HuellasRandom huellas = new HuellasRandom();
            huellas.Huella2 = bytesh2;
            huellas.Huella3 = bytesh3;
            huellas.Huella7 = bytesh7;
            huellas.Huella8 = bytesh8;

            if (ModelState.IsValid)
            {
                db.HuellasRandom.Add(huellas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SubirEKG(HttpPostedFileBase EKG)
        {
            byte[] bytes2 = null;

            if (EKG != null && EKG.ContentLength > 0)
            {
                var fileName = Path.GetFileName(EKG.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(EKG.InputStream))
                {
                    bytes = br.ReadBytes(EKG.ContentLength);

                    bytes2 = bytes;
                }
            }

            EKGRandom ekg = new EKGRandom();
            ekg.EKG = bytes2;

            if (ModelState.IsValid)
            {
                db.EKGRandom.Add(ekg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        public ActionResult CompletarDatos(int? id, string nombre, string estatura, string curp, string numero, /*string metra,*/ string genero,
            string doctor, /*string tipoL, */string tipoT, HttpPostedFileBase file,
            HttpPostedFileBase documentos, HttpPostedFileBase declaracion, HttpPostedFileBase carta, HttpPostedFileBase glucosilada, string repetido,
            HttpPostedFileBase huella2, HttpPostedFileBase huella3, HttpPostedFileBase huella7, HttpPostedFileBase huella8, HttpPostedFileBase firma)
        {
            var revisionPacienteESP = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            EPI_ESP epi = new EPI_ESP();
            var revisionEPI = (from i in db.EPI_ESP where i.idPacienteESP == id select i).FirstOrDefault();
            var idEPI = db.EPI_ESP.Find(0);

            var revisionRepetido = (from i in db.PacienteESP where i.idPacienteESP == id orderby i.idPacienteESP descending select i).FirstOrDefault();

            if (numero.Trim() != ""  && repetido != "Aceptar" && numero.Trim() != revisionRepetido.NoExpediente)
            {
                var revisionExpediente = (from i in db.PacienteESP where i.NoExpediente == numero orderby i.idPacienteESP descending select i).FirstOrDefault();

                if(revisionExpediente != null)
                {
                    var dictamenRepetido = (from i in db.DictamenESP where i.idPacienteESP == revisionExpediente.idPacienteESP orderby i.idPacienteESP descending select i).FirstOrDefault();

                    if (revisionExpediente != null && dictamenRepetido == null)
                    {
                        TempData["Expediente"] = numero.Trim();
                        TempData["id"] = id;
                        return RedirectToAction("Citas");
                    }
                    else if (revisionExpediente != null && dictamenRepetido != null)
                    {
                        TempData["Dictamen"] = "SI";
                        TempData["Expediente"] = numero.Trim();
                        TempData["id"] = id;
                        return RedirectToAction("Citas");
                    }
                }
            }

            /*CARGA DE HUELLAS Y FIRMA*/
            if(firma != null)
            {
                if (firma.ContentLength > 0)
                {
                    byte [] bytesFirma = null;
                    var fileName = Path.GetFileName(firma.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(firma.InputStream))
                    {
                        bytes = br.ReadBytes(firma.ContentLength);
                        bytesFirma = bytes;
                    }

                    if(ModelState.IsValid)
                    {
                        var revisionFirma = (from i in db.FirmaESP where i.idPadienteESP == id select i.idFirmaESP).FirstOrDefault();

                        if(revisionFirma != 0)
                        {
                            var fir = db.FirmaESP.Find(revisionFirma);
                            fir.Firma = bytesFirma;
                            db.Entry(fir).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            FirmaESP fir = new FirmaESP();
                            fir.idPadienteESP = id;
                            fir.Firma = bytesFirma;
                            db.FirmaESP.Add(fir);
                            db.SaveChanges();
                        }
                    }
                }
            }


            if (huella2 != null)
            {
                if (huella2.ContentLength > 0)
                {
                    byte[] bytesHuella2 = null;
                    var fileName = Path.GetFileName(huella2.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(huella2.InputStream))
                    {
                        bytes = br.ReadBytes(huella2.ContentLength);
                        bytesHuella2 = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionHuella2 = (from i in db.HuellasESP where i.idPacienteESP == id select i).FirstOrDefault();

                        if (revisionHuella2 != null)
                        {
                            var hue2 = db.HuellasESP.Find(revisionHuella2.idHuellasESP);
                            hue2.Huella2 = bytesHuella2;
                            db.Entry(hue2).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            HuellasESP hue2 = new HuellasESP();
                            hue2.idPacienteESP = id;
                            hue2.Huella2 = bytesHuella2;
                            db.HuellasESP.Add(hue2);
                            db.SaveChanges();
                        }
                    }
                }
            }

            if (huella3 != null)
            {
                if (huella3.ContentLength > 0)
                {
                    byte[] bytesHuella3 = null;
                    var fileName = Path.GetFileName(huella3.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(huella3.InputStream))
                    {
                        bytes = br.ReadBytes(huella3.ContentLength);
                        bytesHuella3 = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionHuella3 = (from i in db.HuellasESP where i.idPacienteESP == id select i).FirstOrDefault();

                        if (revisionHuella3 != null)
                        {
                            var hue3 = db.HuellasESP.Find(revisionHuella3.idHuellasESP);
                            hue3.Huella3 = bytesHuella3;
                            db.Entry(hue3).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            HuellasESP hue3 = new HuellasESP();
                            hue3.idPacienteESP = id;
                            hue3.Huella3 = bytesHuella3;
                            db.HuellasESP.Add(hue3);
                            db.SaveChanges();
                        }
                    }
                }
            }

            if (huella7 != null)
            {
                if (huella7.ContentLength > 0)
                {
                    byte[] bytesHuella7 = null;
                    var fileName = Path.GetFileName(huella7.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(huella7.InputStream))
                    {
                        bytes = br.ReadBytes(huella7.ContentLength);
                        bytesHuella7 = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionHuella7 = (from i in db.HuellasESP where i.idPacienteESP == id select i).FirstOrDefault();

                        if (revisionHuella7 != null)
                        {
                            var hue7 = db.HuellasESP.Find(revisionHuella7.idHuellasESP);
                            hue7.Huella7 = bytesHuella7;
                            db.Entry(hue7).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            HuellasESP hue7 = new HuellasESP();
                            hue7.idPacienteESP = id;
                            hue7.Huella7 = bytesHuella7;
                            db.HuellasESP.Add(hue7);
                            db.SaveChanges();
                        }
                    }
                }
            }

            if (huella8 != null)
            {
                if (huella8.ContentLength > 0)
                {
                    byte[] bytesHuella8 = null;
                    var fileName = Path.GetFileName(huella8.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(huella8.InputStream))
                    {
                        bytes = br.ReadBytes(huella8.ContentLength);
                        bytesHuella8 = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionHuella8 = (from i in db.HuellasESP where i.idPacienteESP == id select i).FirstOrDefault();

                        if (revisionHuella8 != null)
                        {
                            var hue8 = db.HuellasESP.Find(revisionHuella8.idHuellasESP);
                            hue8.Huella8 = bytesHuella8;
                            db.Entry(hue8).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            HuellasESP hue8 = new HuellasESP();
                            hue8.idPacienteESP = id;
                            hue8.Huella8 = bytesHuella8;
                            db.HuellasESP.Add(hue8);
                            db.SaveChanges();
                        }
                    }
                }
            }
            /*---------------------------------------------------------------------*/


            if (revisionEPI != null)
            {
                idEPI = db.EPI_ESP.Find(revisionEPI.idEpiESP);
            }


            if (revisionEPI == null)
            {
                epi.idPacienteESP = id;

                if (numero.Trim() != null)
                {
                    epi.NoExpediente = numero.Trim();
                }
                else
                {
                    if(revisionPacienteESP.NoExpediente != null)
                    {
                        epi.NoExpediente = revisionPacienteESP.NoExpediente;
                    }
                }

                if (genero != null)
                {
                    epi.Genero = genero;

                    if(genero == "F")
                    {
                        epi.URegla = DateTime.Now.AddDays(-20).ToString("yyyy-MM-dd").Replace("-", "");
                        epi.Embarazos = "0";
                        epi.Abortos = "0";
                        epi.Parto = "0";
                        epi.Cesarea = "0";
                        epi.IVSexual = "0";
                    }
                }
                else
                {
                    if (revisionPacienteESP.Genero!= null)
                    {
                        epi.Genero = revisionPacienteESP.Genero;

                        if (revisionPacienteESP.Genero == "F")
                        {
                            epi.URegla = DateTime.Now.AddDays(-20).ToString("yyyy-MM-dd").Replace("-", "");
                            epi.Embarazos = "0";
                            epi.Abortos = "0";
                            epi.Parto = "0";
                            epi.Cesarea = "0";
                            epi.IVSexual = "0";
                        }
                    }
                }

                Random random = new Random();
                epi.Sistolica = random.Next(111, 132).ToString();
                epi.Diastolica = random.Next(71, 84).ToString();
                epi.Cardiaca = random.Next(66, 98).ToString();
                epi.Respiratoria = random.Next(15, 19).ToString();
                epi.Temperatura = random.Next(36, 37).ToString();

                if (estatura != "")
                {
                    double numeroDecimalRandom = 0;
                    double altura = Convert.ToDouble(estatura);
                    double peso = 0.00;
                    float cadenaPeso;

                    epi.Estatura = estatura;
                    numeroDecimalRandom = random.Next(2223, 2878) / 100.00;

                    peso = numeroDecimalRandom * ((altura / 100) * (altura / 100));
                    cadenaPeso = (float)(Math.Round((double)peso, 2));
                    epi.Peso = Convert.ToInt32(cadenaPeso).ToString();
                }
                else
                {
                    if (revisionEPI != null) //-------------------------------
                    {
                        double numeroDecimalRandom = 0;
                        double altura = Convert.ToDouble(estatura);
                        double peso = 0.00;
                        float cadenaPeso;

                        epi.Estatura = revisionPacienteESP.Estatura;
                        numeroDecimalRandom = random.Next(2223, 2878) / 100.00;

                        peso = numeroDecimalRandom * ((altura / 100) * (altura / 100));
                        cadenaPeso = (float)(Math.Round((double)peso, 2));
                        epi.Peso = Convert.ToInt32(cadenaPeso).ToString();
                    }
                }

                epi.Cintura = "90";
                epi.Cuello = "38";
                epi.Grasa = random.Next(19, 33).ToString();

                epi.Glucosa = "100";
                epi.Hemoglobina = "6.5";

                epi.Ritmo = "SINUSAL";
                epi.Frecuencia = random.Next(66, 99).ToString();
                epi.Eje = random.Next(30, 90).ToString();
                epi.PR = random.Next(120, 200).ToString();
                epi.QT = random.Next(350, 400).ToString();
                epi.QRS = random.Next(60, 100).ToString();
                epi.OndaP = random.Next(90, 110).ToString();
                epi.OndaT = random.Next(120, 130).ToString();

                epi.AVCD = "1";
                epi.AVCI = "1";
                epi.AVID = "1";
                epi.AVII = "1";
                epi.AVLD = "1";
                epi.AVLI = "1";
                epi.Carta = "NO";

                int [] profundidad = { 40, 50, 60 };

                int numeroProfundidad = random.Next(3);
                epi.Estereopsis = profundidad[numeroProfundidad].ToString();

                epi.Lentes = "NO";
                epi.CampoVisual = "NORMAL";
                epi.MovOculares = "NORMAL";
                epi.RPD = "NORMAL";
                epi.RPI = "NORMAL";
                epi.TestVC = "NORMAL";

                string[] notaVisual = { "Sano", "Sano ve bien", "No presenta problema" };
                int numeroNotaVisual = random.Next(3);
                epi.Nota = notaVisual[numeroNotaVisual].ToString();

                epi.MadreVive = "SI";
                epi.PadreVive = "SI";
                epi.HermanosViven = "SI";

                epi.Patologias = "NINGUNO";
                epi.Interpretacion = "NORMAL";
                epi.NotaMedica = "NORMAL";

                string[] tablaAudiologia = {"-5", "0", "5", "10", "15", "20", "25", "30", "35" };

                epi.D125 = tablaAudiologia[random.Next(3,6)];
                epi.D250 = tablaAudiologia[random.Next(3, 6)];
                epi.D500 = tablaAudiologia[random.Next(3, 6)];
                epi.D1000 = tablaAudiologia[random.Next(3, 6)];
                epi.D2000 = tablaAudiologia[random.Next(3, 6)];
                epi.D4000 = tablaAudiologia[random.Next(3, 6)];
                epi.D8000 = tablaAudiologia[random.Next(3, 6)];
                epi.D12000 = "35";
                epi.I125 = tablaAudiologia[random.Next(3, 6)];
                epi.I250 = tablaAudiologia[random.Next(3,6)];
                epi.I500 = tablaAudiologia[random.Next(3,6)];
                epi.I1000 = tablaAudiologia[random.Next(3,6)];
                epi.I2000 = tablaAudiologia[random.Next(3,6)];
                epi.I4000 = tablaAudiologia[random.Next(3,6)];
                epi.I8000 = tablaAudiologia[random.Next(3,6)];
                epi.I12000 = "35";

                epi.VacCompletas = "SI";
                epi.ECivil = "UNIONLIBRE";
                epi.Religion = "OTRO";
                epi.Escolaridad = "SECUNDARIA";
                epi.Hijos = "0";
                epi.ECongenita = "NO";
                epi.EspDiag = "Esta limpio";
                epi.Alergia = "Ninguna";
                epi.Diabetes = "NO";
                epi.DiagHipArt = "NO";
                epi.Alcohol = "NO";
                epi.Fuma = "NO";
                epi.Drogas = "NO";
                epi.UsaLentes = "NO";

                if (ModelState.IsValid)
                {
                    db.EPI_ESP.Add(epi);
                    db.SaveChanges();
                }

                string[] tablaAudio = { epi.D125, epi.D250, epi.D500, epi.D1000, epi.D2000, epi.D4000, epi.D8000, epi.D12000, epi.I125, epi.I250, epi.I500, epi.I1000, epi.I2000, epi.I4000, epi.I8000, epi.I12000 };

                string[] posiciones = new string[16];
                int posicionActual = 0;
                int posicionDeseada = 0;
                int operacion = 0;
                string arribaAbajo = "";

                for (int i = 0; i < 16; i++)
                {
                    if(i == 8)
                    {
                        operacion = 0;
                        posicionActual = 0;
                    }

                    for (int n = 0; n < 9; n++)
                    {
                        if (Convert.ToString(tablaAudiologia[n]) == tablaAudio[i])
                        {
                            posicionDeseada = n + 1;
                            break;
                        }
                    }

                    operacion = posicionActual - posicionDeseada;
                    posicionActual = posicionDeseada;
                    posiciones[i] = operacion.ToString();

                }

                MovimientosAudio mov = new MovimientosAudio();
                mov.D125 = posiciones[0];
                mov.D250 = posiciones[1];
                mov.D500 = posiciones[2];
                mov.D1000 = posiciones[3];
                mov.D2000 = posiciones[4];
                mov.D4000 = posiciones[5];
                mov.D8000 = posiciones[6];
                mov.D12000 = posiciones[7];
                mov.I125 = posiciones[8];
                mov.I250 = posiciones[9];
                mov.I500 = posiciones[10];
                mov.I1000 = posiciones[11];
                mov.I2000 = posiciones[12];
                mov.I4000 = posiciones[13];
                mov.I8000 = posiciones[14];
                mov.I12000 = posiciones[15];
                mov.idPacienteESP = id;

                if (ModelState.IsValid)
                {
                    db.MovimientosAudio.Add(mov);
                    db.SaveChanges();
                }

            }
            else
            {
                if(genero != "")
                {
                    idEPI.Genero = genero;
                    revisionPacienteESP.Genero = genero;
                }
                if(numero.Trim() != "")
                {
                    idEPI.NoExpediente = numero.Trim();
                    revisionPacienteESP.NoExpediente = numero.Trim();
                }
                if(estatura != "")
                {
                    Random random = new Random();
                    idEPI.Estatura = estatura;

                    double numeroDecimalRandom = 0;
                    double altura = Convert.ToDouble(estatura);
                    double peso = 0.00;
                    float cadenaPeso;

                    idEPI.Estatura = estatura;
                    revisionPacienteESP.Estatura = estatura;
                    numeroDecimalRandom = random.Next(2223, 2878) / 100.00;

                    peso = numeroDecimalRandom * ((altura /100 ) * (altura/100));
                    cadenaPeso = (float)(Math.Round((double)peso, 2));
                    idEPI.Peso = Convert.ToInt32(cadenaPeso).ToString();
                }

                if (genero != null)
                {
                    epi.Genero = genero;

                    if (genero == "F")
                    {
                        idEPI.URegla = DateTime.Now.AddDays(-20).ToString("yyyy-MM-dd").Replace("-", "");
                        idEPI.Embarazos = "0";
                        idEPI.Abortos = "0";
                        idEPI.Parto = "0";
                        idEPI.Cesarea = "0";
                        idEPI.IVSexual = "0";
                    }
                }

                if (ModelState.IsValid)
                {
                    db.Entry(idEPI).State = EntityState.Modified;
                    db.Entry(revisionPacienteESP).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

            if(revisionPacienteESP.NoExpediente != null && revisionPacienteESP.TipoLicencia != null && revisionPacienteESP.TipoTramite != null && revisionPacienteESP.Doctor != null 
                && revisionPacienteESP.Estatura != null && revisionPacienteESP.Metra != null 
                && (from i in db.DictamenESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault() == null)
            {
                revisionPacienteESP.EstatusCaptura = "En Proceso";
            }

            if(nombre != "")
            {
                revisionPacienteESP.Nombre = nombre != "" ? nombre.ToUpper() : revisionPacienteESP.Nombre;
            }

            if(estatura != "")
            {
                revisionPacienteESP.Estatura = estatura != "" ? estatura: revisionPacienteESP.Estatura;
            }

            if (curp != "")
            {
                revisionPacienteESP.CURP = curp != "" ? curp.ToUpper() : revisionPacienteESP.CURP;
            }

            if (numero.Trim() != "")
            {
                revisionPacienteESP.NoExpediente = numero.Trim() != "" ? numero.Trim() : revisionPacienteESP.NoExpediente;
            }

            if (doctor != "")
            {
                revisionPacienteESP.Doctor = doctor != "" ? doctor : revisionPacienteESP.Doctor;
            }

            if (tipoT != "")
            {
                revisionPacienteESP.TipoTramite = tipoT != "" ? tipoT : revisionPacienteESP.TipoTramite;
            }

            if (genero != "")
            {
                revisionPacienteESP.Genero = genero != "" ? genero : revisionPacienteESP.Genero;
            }

            byte[] bytes2 = null;
            if (file != null)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(file.InputStream))
                    {
                        bytes = br.ReadBytes(file.ContentLength);
                        bytes2 = bytes;
                    }
                }
            }

            byte[] bytesDOCS = null;
            if (documentos != null)
            {
                if (documentos != null && documentos.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(documentos.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(documentos.InputStream))
                    {
                        bytes = br.ReadBytes(documentos.ContentLength);
                        bytesDOCS = bytes;
                    }
                }
            }

            byte[] bytesCARTA = null;
            if (carta != null)
            {
                if (carta != null && carta.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(carta.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(carta.InputStream))
                    {
                        bytes = br.ReadBytes(carta.ContentLength);
                        bytesCARTA = bytes;
                    }
                }
            }

            byte[] bytesGLUCO = null;
            if (glucosilada != null)
            {
                if (glucosilada != null && glucosilada.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(glucosilada.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(glucosilada.InputStream))
                    {
                        bytes = br.ReadBytes(glucosilada.ContentLength);
                        bytesGLUCO = bytes;
                    }
                }
            }

            byte[] bytesDEC = null;
            if (declaracion != null)
            {
                if (declaracion != null && declaracion.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(declaracion.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(declaracion.InputStream))
                    {
                        bytes = br.ReadBytes(declaracion.ContentLength);
                        bytesDEC = bytes;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var revisionFoto = (from i in db.FotoPacienteESP where i.idPacienteESP == id select i.idFoto).FirstOrDefault();
                var foto = db.FotoPacienteESP.Find(revisionFoto);
                FotoPacienteESP nuevaFoto = new FotoPacienteESP();

                if(revisionFoto != 0 && file != null)
                {
                    foto.FotoESP = bytes2;
                    db.Entry(foto).State = EntityState.Modified;
                }
                else
                {
                    if(file != null)
                    {
                        nuevaFoto.FotoESP = bytes2;
                        nuevaFoto.idPacienteESP = id;
                        db.FotoPacienteESP.Add(nuevaFoto);
                    }
                }

                var revisionDocumentos = (from i in db.DocumentosESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionCartaNO = (from i in db.CartaNoAccidentesESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionDeclaracion = (from i in db.DeclaracionSaludESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionGlucosilada = (from i in db.HemoglobinaGlucosiladaESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();

                if (revisionDocumentos != null)
                {
                    var idDocumentos = (from i in db.DocumentosESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (documentos != null)
                    {
                        var DOCS = db.DocumentosESP.Find(idDocumentos);
                        DOCS.Documentos = bytesDOCS;
                        db.Entry(DOCS).State = EntityState.Modified;
                    }
                }
                else
                {
                    DocumentosESP docs = new DocumentosESP();
                    if (documentos != null)
                    {
                        docs.Documentos = bytesDOCS;
                        docs.idPacienteESP = id;
                        db.DocumentosESP.Add(docs);
                    }
                }

                if (revisionDeclaracion != null)
                {
                    var idDeclaracion = (from i in db.DeclaracionSaludESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (declaracion != null)
                    {
                        var DEC = db.DeclaracionSaludESP.Find(idDeclaracion);
                        DEC.DeclaracionSalud = bytesDEC;
                        db.Entry(DEC).State = EntityState.Modified;
                    }
                }
                else
                {
                    DeclaracionSaludESP dec = new DeclaracionSaludESP();
                    if (declaracion != null)
                    {
                        dec.DeclaracionSalud = bytesDEC;
                        dec.idPacienteESP = id;
                        db.DeclaracionSaludESP.Add(dec);
                    }
                }

                if (revisionCartaNO != null)
                {
                    var idCartaNo = (from i in db.CartaNoAccidentesESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (carta != null)
                    {
                        var CARTA = db.CartaNoAccidentesESP.Find(idCartaNo);
                        CARTA.CartaNoAccidentes = bytesCARTA;
                        db.Entry(CARTA).State = EntityState.Modified;
                    }
                }
                else
                {
                    CartaNoAccidentesESP car = new CartaNoAccidentesESP();
                    if (carta != null)
                    {
                        car.CartaNoAccidentes = bytesCARTA;
                        car.idPacienteESP = id;
                        db.CartaNoAccidentesESP.Add(car);
                    }
                }

                if (revisionGlucosilada != null)
                {
                    var idGluco = (from i in db.HemoglobinaGlucosiladaESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (glucosilada != null)
                    {
                        var GLUCOSILADA = db.HemoglobinaGlucosiladaESP.Find(idGluco);
                        GLUCOSILADA.HemoglobinaGlucosilada = bytesGLUCO;
                        db.Entry(GLUCOSILADA).State = EntityState.Modified;
                    }
                }
                else
                {
                    HemoglobinaGlucosiladaESP hem = new HemoglobinaGlucosiladaESP();
                    if (glucosilada != null)
                    {
                        hem.HemoglobinaGlucosilada = bytesGLUCO;
                        hem.idPacienteESP = id;
                        db.HemoglobinaGlucosiladaESP.Add(hem);
                    }
                }

                db.Entry(revisionPacienteESP).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Citas");
        }

        public ActionResult CompletarDatosRepetidos(int? id, string nombre, string estatura, string curp, string numero, /*string metra,*/ string genero, string usuario,
            string doctor, /*string tipoL, */string tipoT, HttpPostedFileBase file,
            HttpPostedFileBase documentos, HttpPostedFileBase declaracion, HttpPostedFileBase carta, HttpPostedFileBase glucosilada, string repetido,
            HttpPostedFileBase huella2, HttpPostedFileBase huella3, HttpPostedFileBase huella7, HttpPostedFileBase huella8, HttpPostedFileBase firma)
        {
            var revisionPacienteESP = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            EPI_ESP epi = new EPI_ESP();
            var revisionEPI = (from i in db.EPI_ESP where i.idPacienteESP == id select i).FirstOrDefault();
            var idEPI = db.EPI_ESP.Find(0);

            var revisionExpediente = (from i in db.PacienteESP where i.NoExpediente == numero select i).FirstOrDefault();

            var pacienteCancelacion = db.PacienteESP.Find(revisionExpediente.idPacienteESP);

            pacienteCancelacion.EstatusCaptura = "Cancelado";
            pacienteCancelacion.CancelaComentario = "SE VOLVIÓ A CAPTURAR POR EL USUARIO " + usuario;

            if(ModelState.IsValid)
            {
                db.Entry(pacienteCancelacion).State = EntityState.Modified;
                db.SaveChanges();
            }

            /*CARGA DE HUELLAS Y FIRMA*/
            if (firma != null)
            {
                if (firma.ContentLength > 0)
                {
                    byte[] bytesFirma = null;
                    var fileName = Path.GetFileName(firma.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(firma.InputStream))
                    {
                        bytes = br.ReadBytes(firma.ContentLength);
                        bytesFirma = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionFirma = (from i in db.FirmaESP where i.idPadienteESP == id select i.idFirmaESP).FirstOrDefault();

                        if (revisionFirma != 0)
                        {
                            var fir = db.FirmaESP.Find(revisionFirma);
                            fir.Firma = bytesFirma;
                            db.Entry(fir).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            FirmaESP fir = new FirmaESP();
                            fir.idPadienteESP = id;
                            fir.Firma = bytesFirma;
                            db.FirmaESP.Add(fir);
                            db.SaveChanges();
                        }
                    }
                }
            }


            if (huella2 != null)
            {
                if (huella2.ContentLength > 0)
                {
                    byte[] bytesHuella2 = null;
                    var fileName = Path.GetFileName(huella2.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(huella2.InputStream))
                    {
                        bytes = br.ReadBytes(huella2.ContentLength);
                        bytesHuella2 = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionHuella2 = (from i in db.HuellasESP where i.idPacienteESP == id select i).FirstOrDefault();

                        if (revisionHuella2 != null)
                        {
                            var hue2 = db.HuellasESP.Find(revisionHuella2.idHuellasESP);
                            hue2.Huella2 = bytesHuella2;
                            db.Entry(hue2).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            HuellasESP hue2 = new HuellasESP();
                            hue2.idPacienteESP = id;
                            hue2.Huella2 = bytesHuella2;
                            db.HuellasESP.Add(hue2);
                            db.SaveChanges();
                        }
                    }
                }
            }

            if (huella3 != null)
            {
                if (huella3.ContentLength > 0)
                {
                    byte[] bytesHuella3 = null;
                    var fileName = Path.GetFileName(huella3.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(huella3.InputStream))
                    {
                        bytes = br.ReadBytes(huella3.ContentLength);
                        bytesHuella3 = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionHuella3 = (from i in db.HuellasESP where i.idPacienteESP == id select i).FirstOrDefault();

                        if (revisionHuella3 != null)
                        {
                            var hue3 = db.HuellasESP.Find(revisionHuella3.idHuellasESP);
                            hue3.Huella3 = bytesHuella3;
                            db.Entry(hue3).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            HuellasESP hue3 = new HuellasESP();
                            hue3.idPacienteESP = id;
                            hue3.Huella3 = bytesHuella3;
                            db.HuellasESP.Add(hue3);
                            db.SaveChanges();
                        }
                    }
                }
            }

            if (huella7 != null)
            {
                if (huella7.ContentLength > 0)
                {
                    byte[] bytesHuella7 = null;
                    var fileName = Path.GetFileName(huella7.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(huella7.InputStream))
                    {
                        bytes = br.ReadBytes(huella7.ContentLength);
                        bytesHuella7 = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionHuella7 = (from i in db.HuellasESP where i.idPacienteESP == id select i).FirstOrDefault();

                        if (revisionHuella7 != null)
                        {
                            var hue7 = db.HuellasESP.Find(revisionHuella7.idHuellasESP);
                            hue7.Huella7 = bytesHuella7;
                            db.Entry(hue7).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            HuellasESP hue7 = new HuellasESP();
                            hue7.idPacienteESP = id;
                            hue7.Huella7 = bytesHuella7;
                            db.HuellasESP.Add(hue7);
                            db.SaveChanges();
                        }
                    }
                }
            }

            if (huella8 != null)
            {
                if (huella8.ContentLength > 0)
                {
                    byte[] bytesHuella8 = null;
                    var fileName = Path.GetFileName(huella8.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(huella8.InputStream))
                    {
                        bytes = br.ReadBytes(huella8.ContentLength);
                        bytesHuella8 = bytes;
                    }

                    if (ModelState.IsValid)
                    {
                        var revisionHuella8 = (from i in db.HuellasESP where i.idPacienteESP == id select i).FirstOrDefault();

                        if (revisionHuella8 != null)
                        {
                            var hue8 = db.HuellasESP.Find(revisionHuella8.idHuellasESP);
                            hue8.Huella8 = bytesHuella8;
                            db.Entry(hue8).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            HuellasESP hue8 = new HuellasESP();
                            hue8.idPacienteESP = id;
                            hue8.Huella8 = bytesHuella8;
                            db.HuellasESP.Add(hue8);
                            db.SaveChanges();
                        }
                    }
                }
            }
            /*---------------------------------------------------------------------*/


            if (revisionEPI != null)
            {
                idEPI = db.EPI_ESP.Find(revisionEPI.idEpiESP);
            }


            if (revisionEPI == null)
            {
                epi.idPacienteESP = id;

                if (numero.Trim() != null)
                {
                    epi.NoExpediente = numero.Trim();
                }
                else
                {
                    if (revisionPacienteESP.NoExpediente != null)
                    {
                        epi.NoExpediente = revisionPacienteESP.NoExpediente;
                    }
                }

                if (genero != null)
                {
                    epi.Genero = genero;

                    if (genero == "F")
                    {
                        epi.URegla = DateTime.Now.AddDays(-20).ToString("yyyy-MM-dd").Replace("-", "");
                        epi.Embarazos = "0";
                        epi.Abortos = "0";
                        epi.Parto = "0";
                        epi.Cesarea = "0";
                        epi.IVSexual = "0";
                    }
                }
                else
                {
                    if (revisionPacienteESP.Genero != null)
                    {
                        epi.Genero = revisionPacienteESP.Genero;

                        if (revisionPacienteESP.Genero == "F")
                        {
                            epi.URegla = DateTime.Now.AddDays(-20).ToString("yyyy-MM-dd").Replace("-", "");
                            epi.Embarazos = "0";
                            epi.Abortos = "0";
                            epi.Parto = "0";
                            epi.Cesarea = "0";
                            epi.IVSexual = "0";
                        }
                    }
                }

                Random random = new Random();
                epi.Sistolica = random.Next(111, 132).ToString();
                epi.Diastolica = random.Next(71, 84).ToString();
                epi.Cardiaca = random.Next(66, 98).ToString();
                epi.Respiratoria = random.Next(15, 19).ToString();
                epi.Temperatura = random.Next(36, 37).ToString();

                if (estatura != "")
                {
                    double numeroDecimalRandom = 0;
                    double altura = Convert.ToDouble(estatura);
                    double peso = 0.00;
                    float cadenaPeso;

                    epi.Estatura = estatura;
                    numeroDecimalRandom = random.Next(2223, 2878) / 100.00;

                    peso = numeroDecimalRandom * ((altura / 100) * (altura / 100));
                    cadenaPeso = (float)(Math.Round((double)peso, 2));
                    epi.Peso = Convert.ToInt32(cadenaPeso).ToString();
                }
                else
                {
                    if (revisionEPI != null) //-------------------------------
                    {
                        double numeroDecimalRandom = 0;
                        double altura = Convert.ToDouble(estatura);
                        double peso = 0.00;
                        float cadenaPeso;

                        epi.Estatura = revisionPacienteESP.Estatura;
                        numeroDecimalRandom = random.Next(2223, 2878) / 100.00;

                        peso = numeroDecimalRandom * ((altura / 100) * (altura / 100));
                        cadenaPeso = (float)(Math.Round((double)peso, 2));
                        epi.Peso = Convert.ToInt32(cadenaPeso).ToString();
                    }
                }

                epi.Cintura = "90";
                epi.Cuello = "38";
                epi.Grasa = random.Next(19, 33).ToString();

                epi.Glucosa = "100";
                epi.Hemoglobina = "6.5";

                epi.Ritmo = "SINUSAL";
                epi.Frecuencia = random.Next(66, 99).ToString();
                epi.Eje = random.Next(30, 90).ToString();
                epi.PR = random.Next(120, 200).ToString();
                epi.QT = random.Next(350, 400).ToString();
                epi.QRS = random.Next(60, 100).ToString();
                epi.OndaP = random.Next(90, 110).ToString();
                epi.OndaT = random.Next(120, 130).ToString();

                epi.AVCD = "1";
                epi.AVCI = "1";
                epi.AVID = "1";
                epi.AVII = "1";
                epi.AVLD = "1";
                epi.AVLI = "1";
                epi.Carta = "NO";

                int[] profundidad = { 40, 50, 60 };

                int numeroProfundidad = random.Next(3);
                epi.Estereopsis = profundidad[numeroProfundidad].ToString();

                epi.Lentes = "NO";
                epi.CampoVisual = "NORMAL";
                epi.MovOculares = "NORMAL";
                epi.RPD = "NORMAL";
                epi.RPI = "NORMAL";
                epi.TestVC = "NORMAL";

                string[] notaVisual = { "Sano", "Sano ve bien", "No presenta problema" };
                int numeroNotaVisual = random.Next(3);
                epi.Nota = notaVisual[numeroNotaVisual].ToString();

                epi.MadreVive = "SI";
                epi.PadreVive = "SI";
                epi.HermanosViven = "SI";

                epi.Patologias = "NINGUNO";
                epi.Interpretacion = "NORMAL";
                epi.NotaMedica = "NORMAL";

                string[] tablaAudiologia = { "-5", "0", "5", "10", "15", "20", "25", "30", "35" };

                epi.D125 = tablaAudiologia[random.Next(3, 6)];
                epi.D250 = tablaAudiologia[random.Next(3, 6)];
                epi.D500 = tablaAudiologia[random.Next(3, 6)];
                epi.D1000 = tablaAudiologia[random.Next(3, 6)];
                epi.D2000 = tablaAudiologia[random.Next(3, 6)];
                epi.D4000 = tablaAudiologia[random.Next(3, 6)];
                epi.D8000 = tablaAudiologia[random.Next(3, 6)];
                epi.D12000 = "35";
                epi.I125 = tablaAudiologia[random.Next(3, 6)];
                epi.I250 = tablaAudiologia[random.Next(3, 6)];
                epi.I500 = tablaAudiologia[random.Next(3, 6)];
                epi.I1000 = tablaAudiologia[random.Next(3, 6)];
                epi.I2000 = tablaAudiologia[random.Next(3, 6)];
                epi.I4000 = tablaAudiologia[random.Next(3, 6)];
                epi.I8000 = tablaAudiologia[random.Next(3, 6)];
                epi.I12000 = "35";

                epi.VacCompletas = "SI";
                epi.ECivil = "UNIONLIBRE";
                epi.Religion = "OTRO";
                epi.Escolaridad = "SECUNDARIA";
                epi.Hijos = "0";
                epi.ECongenita = "NO";
                epi.EspDiag = "Esta limpio";
                epi.Alergia = "Ninguna";
                epi.Diabetes = "NO";
                epi.DiagHipArt = "NO";
                epi.Alcohol = "NO";
                epi.Fuma = "NO";
                epi.Drogas = "NO";
                epi.UsaLentes = "NO";

                if (ModelState.IsValid)
                {
                    db.EPI_ESP.Add(epi);
                    db.SaveChanges();
                }

                string[] tablaAudio = { epi.D125, epi.D250, epi.D500, epi.D1000, epi.D2000, epi.D4000, epi.D8000, epi.D12000, epi.I125, epi.I250, epi.I500, epi.I1000, epi.I2000, epi.I4000, epi.I8000, epi.I12000 };

                string[] posiciones = new string[16];
                int posicionActual = 0;
                int posicionDeseada = 0;
                int operacion = 0;
                string arribaAbajo = "";

                for (int i = 0; i < 16; i++)
                {
                    if (i == 8)
                    {
                        operacion = 0;
                        posicionActual = 0;
                    }

                    for (int n = 0; n < 9; n++)
                    {
                        if (Convert.ToString(tablaAudiologia[n]) == tablaAudio[i])
                        {
                            posicionDeseada = n + 1;
                            break;
                        }
                    }

                    operacion = posicionActual - posicionDeseada;
                    posicionActual = posicionDeseada;
                    posiciones[i] = operacion.ToString();

                }

                MovimientosAudio mov = new MovimientosAudio();
                mov.D125 = posiciones[0];
                mov.D250 = posiciones[1];
                mov.D500 = posiciones[2];
                mov.D1000 = posiciones[3];
                mov.D2000 = posiciones[4];
                mov.D4000 = posiciones[5];
                mov.D8000 = posiciones[6];
                mov.D12000 = posiciones[7];
                mov.I125 = posiciones[8];
                mov.I250 = posiciones[9];
                mov.I500 = posiciones[10];
                mov.I1000 = posiciones[11];
                mov.I2000 = posiciones[12];
                mov.I4000 = posiciones[13];
                mov.I8000 = posiciones[14];
                mov.I12000 = posiciones[15];
                mov.idPacienteESP = id;

                if (ModelState.IsValid)
                {
                    db.MovimientosAudio.Add(mov);
                    db.SaveChanges();
                }

            }
            else
            {
                if (genero != "")
                {
                    idEPI.Genero = genero;
                    revisionPacienteESP.Genero = genero;
                }
                if (numero.Trim() != "")
                {
                    idEPI.NoExpediente = numero.Trim();
                    revisionPacienteESP.NoExpediente = numero.Trim();
                }
                if (estatura != "")
                {
                    Random random = new Random();
                    idEPI.Estatura = estatura;

                    double numeroDecimalRandom = 0;
                    double altura = Convert.ToDouble(estatura);
                    double peso = 0.00;
                    float cadenaPeso;

                    idEPI.Estatura = estatura;
                    revisionPacienteESP.Estatura = estatura;
                    numeroDecimalRandom = random.Next(2223, 2878) / 100.00;

                    peso = numeroDecimalRandom * ((altura / 100) * (altura / 100));
                    cadenaPeso = (float)(Math.Round((double)peso, 2));
                    idEPI.Peso = Convert.ToInt32(cadenaPeso).ToString();
                }

                if (genero != null)
                {
                    epi.Genero = genero;

                    if (genero == "F")
                    {
                        idEPI.URegla = DateTime.Now.AddDays(-20).ToString("yyyy-MM-dd").Replace("-", "");
                        idEPI.Embarazos = "0";
                        idEPI.Abortos = "0";
                        idEPI.Parto = "0";
                        idEPI.Cesarea = "0";
                        idEPI.IVSexual = "0";
                    }
                }

                if (ModelState.IsValid)
                {
                    db.Entry(idEPI).State = EntityState.Modified;
                    db.Entry(revisionPacienteESP).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

            if (revisionPacienteESP.NoExpediente != null && revisionPacienteESP.TipoLicencia != null && revisionPacienteESP.TipoTramite != null && revisionPacienteESP.Doctor != null
                && revisionPacienteESP.Estatura != null && revisionPacienteESP.Metra != null
                && (from i in db.DictamenESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault() == null)
            {
                revisionPacienteESP.EstatusCaptura = "En Proceso";
            }

            if (nombre != "")
            {
                revisionPacienteESP.Nombre = nombre != "" ? nombre.ToUpper() : revisionPacienteESP.Nombre;
            }

            if (estatura != "")
            {
                revisionPacienteESP.Estatura = estatura != "" ? estatura : revisionPacienteESP.Estatura;
            }

            if (curp != "")
            {
                revisionPacienteESP.CURP = curp != "" ? curp.ToUpper() : revisionPacienteESP.CURP;
            }

            if (numero.Trim() != "")
            {
                revisionPacienteESP.NoExpediente = numero.Trim() != "" ? numero.Trim() : revisionPacienteESP.NoExpediente;
            }

            if (doctor != "")
            {
                revisionPacienteESP.Doctor = doctor != "" ? doctor : revisionPacienteESP.Doctor;
            }

            if (tipoT != "")
            {
                revisionPacienteESP.TipoTramite = tipoT != "" ? tipoT : revisionPacienteESP.TipoTramite;
            }

            if (genero != "")
            {
                revisionPacienteESP.Genero = genero != "" ? genero : revisionPacienteESP.Genero;
            }

            byte[] bytes2 = null;
            if (file != null)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(file.InputStream))
                    {
                        bytes = br.ReadBytes(file.ContentLength);
                        bytes2 = bytes;
                    }
                }
            }

            byte[] bytesDOCS = null;
            if (documentos != null)
            {
                if (documentos != null && documentos.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(documentos.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(documentos.InputStream))
                    {
                        bytes = br.ReadBytes(documentos.ContentLength);
                        bytesDOCS = bytes;
                    }
                }
            }

            byte[] bytesCARTA = null;
            if (carta != null)
            {
                if (carta != null && carta.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(carta.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(carta.InputStream))
                    {
                        bytes = br.ReadBytes(carta.ContentLength);
                        bytesCARTA = bytes;
                    }
                }
            }

            byte[] bytesGLUCO = null;
            if (glucosilada != null)
            {
                if (glucosilada != null && glucosilada.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(glucosilada.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(glucosilada.InputStream))
                    {
                        bytes = br.ReadBytes(glucosilada.ContentLength);
                        bytesGLUCO = bytes;
                    }
                }
            }

            byte[] bytesDEC = null;
            if (declaracion != null)
            {
                if (declaracion != null && declaracion.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(declaracion.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(declaracion.InputStream))
                    {
                        bytes = br.ReadBytes(declaracion.ContentLength);
                        bytesDEC = bytes;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var revisionFoto = (from i in db.FotoPacienteESP where i.idPacienteESP == id select i.idFoto).FirstOrDefault();
                var foto = db.FotoPacienteESP.Find(revisionFoto);
                FotoPacienteESP nuevaFoto = new FotoPacienteESP();

                if (revisionFoto != 0 && file != null)
                {
                    foto.FotoESP = bytes2;
                    db.Entry(foto).State = EntityState.Modified;
                }
                else
                {
                    if (file != null)
                    {
                        nuevaFoto.FotoESP = bytes2;
                        nuevaFoto.idPacienteESP = id;
                        db.FotoPacienteESP.Add(nuevaFoto);
                    }
                }

                var revisionDocumentos = (from i in db.DocumentosESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionCartaNO = (from i in db.CartaNoAccidentesESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionDeclaracion = (from i in db.DeclaracionSaludESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionGlucosilada = (from i in db.HemoglobinaGlucosiladaESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();

                if (revisionDocumentos != null)
                {
                    var idDocumentos = (from i in db.DocumentosESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (documentos != null)
                    {
                        var DOCS = db.DocumentosESP.Find(idDocumentos);
                        DOCS.Documentos = bytesDOCS;
                        db.Entry(DOCS).State = EntityState.Modified;
                    }
                }
                else
                {
                    DocumentosESP docs = new DocumentosESP();
                    if (documentos != null)
                    {
                        docs.Documentos = bytesDOCS;
                        docs.idPacienteESP = id;
                        db.DocumentosESP.Add(docs);
                    }
                }

                if (revisionDeclaracion != null)
                {
                    var idDeclaracion = (from i in db.DeclaracionSaludESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (declaracion != null)
                    {
                        var DEC = db.DeclaracionSaludESP.Find(idDeclaracion);
                        DEC.DeclaracionSalud = bytesDEC;
                        db.Entry(DEC).State = EntityState.Modified;
                    }
                }
                else
                {
                    DeclaracionSaludESP dec = new DeclaracionSaludESP();
                    if (declaracion != null)
                    {
                        dec.DeclaracionSalud = bytesDEC;
                        dec.idPacienteESP = id;
                        db.DeclaracionSaludESP.Add(dec);
                    }
                }

                if (revisionCartaNO != null)
                {
                    var idCartaNo = (from i in db.CartaNoAccidentesESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (carta != null)
                    {
                        var CARTA = db.CartaNoAccidentesESP.Find(idCartaNo);
                        CARTA.CartaNoAccidentes = bytesCARTA;
                        db.Entry(CARTA).State = EntityState.Modified;
                    }
                }
                else
                {
                    CartaNoAccidentesESP car = new CartaNoAccidentesESP();
                    if (carta != null)
                    {
                        car.CartaNoAccidentes = bytesCARTA;
                        car.idPacienteESP = id;
                        db.CartaNoAccidentesESP.Add(car);
                    }
                }

                if (revisionGlucosilada != null)
                {
                    var idGluco = (from i in db.HemoglobinaGlucosiladaESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (glucosilada != null)
                    {
                        var GLUCOSILADA = db.HemoglobinaGlucosiladaESP.Find(idGluco);
                        GLUCOSILADA.HemoglobinaGlucosilada = bytesGLUCO;
                        db.Entry(GLUCOSILADA).State = EntityState.Modified;
                    }
                }
                else
                {
                    HemoglobinaGlucosiladaESP hem = new HemoglobinaGlucosiladaESP();
                    if (glucosilada != null)
                    {
                        hem.HemoglobinaGlucosilada = bytesGLUCO;
                        hem.idPacienteESP = id;
                        db.HemoglobinaGlucosiladaESP.Add(hem);
                    }
                }

                db.Entry(revisionPacienteESP).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Citas");
        }

        public ActionResult Metra(int? id, string usuario)
        {
            var paciente = db.PacienteESP.Find(id);
            paciente.Metra = "SI";
            paciente.Usuario = usuario;

            if(ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Citas");
        }

        public ActionResult IniciarCaptura(int? id, string capturista)
        {
            var paciente = db.PacienteESP.Find(id);

            paciente.EstatusCaptura = "En Proceso";
            paciente.Capturista = capturista;
            paciente.FechaCaptura = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Captura");
        }


        public ActionResult ReportarProblema(int? id, string comentario)
        {
            var paciente = db.PacienteESP.Find(id);

            paciente.EstatusCaptura = "Pausado";
            paciente.Problema = comentario;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Captura");
        }

        public ActionResult AbrirElectro()
        {
            var ekg = (from i in db.EKGRandom select i.idEKG).Count();

            Random ran = new Random();
            int numero = ran.Next(ekg);

            TempData["Electro"] = numero;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarElectro()
        {
            var ekg = (from i in db.EKGRandom select i.idEKG).Count();
            Random ran = new Random();
            int numero = ran.Next(ekg);

            var expediente = (from e in db.EKGRandom where e.idEKG == numero select e).FirstOrDefault();

            var bytesBinary = expediente.EKG;
            TempData["Electro"] = null;
            return File(bytesBinary, "application/pdf");
        }


        public ActionResult AbrirDocumentos(int? id)
        {
            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            TempData["Documentos"] = id;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarDocumentos(int? id)
        {
            var expediente = (from e in db.DocumentosESP where e.idPacienteESP == id select e).FirstOrDefault();

            var bytesBinary = expediente.Documentos;
            TempData["Documentos"] = null;
            return File(bytesBinary, "application/pdf");
        }

        public ActionResult AbrirNoAccidentes(int? id)
        {
            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            TempData["NoAccidentes"] = id;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarNoAccidentes(int? id)
        {
            var expediente = (from e in db.CartaNoAccidentesESP where e.idPacienteESP == id select e).FirstOrDefault();

            var bytesBinary = expediente.CartaNoAccidentes;
            TempData["NoAccidentes"] = null;
            return File(bytesBinary, "application/pdf");
        }


        public ActionResult AbrirDeclaracion(int? id)
        {
            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            TempData["Declaracion"] = id;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarDeclaracion(int? id)
        {
            var expediente = (from e in db.DeclaracionSaludESP where e.idPacienteESP == id select e).FirstOrDefault();

            var bytesBinary = expediente.DeclaracionSalud;
            TempData["Declaracion"] = null;
            return File(bytesBinary, "application/pdf");
        }


        public ActionResult AbrirGlucosilada(int? id)
        {
            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            TempData["Glucosilada"] = id;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarGlucosilada(int? id)
        {
            var expediente = (from e in db.HemoglobinaGlucosiladaESP where e.idPacienteESP == id select e).FirstOrDefault();

            var bytesBinary = expediente.HemoglobinaGlucosilada;
            TempData["Glucosilada"] = null;
            return File(bytesBinary, "application/pdf");
        }


        [HttpPost]
        public ActionResult Dictaminar(HttpPostedFileBase dictamen, int? id, string usuario)
        {

            byte[] bytes2 = null;
            if (dictamen != null)
            {
                if (dictamen != null && dictamen.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(dictamen.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(dictamen.InputStream))
                    {
                        bytes = br.ReadBytes(dictamen.ContentLength);
                        bytes2 = bytes;
                    }
                }
            }

            DictamenESP dic = new DictamenESP();

            dic.DictamenArchivo = bytes2;
            dic.idPacienteESP = id;

            PacienteESP paciente = db.PacienteESP.Find(id);
            paciente.EstatusCaptura = "Terminado";
            paciente.Capturista = usuario;

            if(ModelState.IsValid)
            {
                db.DictamenESP.Add(dic);
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Captura");
        }

        public ActionResult DescargarPDF(int? id)
        {
            PacienteESP paciente = db.PacienteESP.Find(id);

            var documento = (from d in db.DictamenESP where d.idPacienteESP == id  orderby d.idDictamenESP descending select d.DictamenArchivo).FirstOrDefault();

            var bytesBinary = documento;

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + paciente.Nombre + ".pdf");
            Response.BinaryWrite(bytesBinary);
            Response.End();

            return RedirectToAction("Citas");
        }


        public ActionResult DescargarHuellasZIP(int? id, string h2, string h3, string h7, string h8)
        {
            Random ran = new Random();
            //int numeroRandom = ran.Next(90);

            var documento = (from d in db.HuellasRandom where d.idHuella == 1  select d.Huella2).FirstOrDefault();

            if(h2 != null)
            {
                documento = (from d in db.HuellasRandom where d.idHuella == 1 select d.Huella2).FirstOrDefault();
            }

            if (h3 != null)
            {
                documento = (from d in db.HuellasRandom where d.idHuella == 1 select d.Huella3).FirstOrDefault();
            }

            if (h7 != null)
            {
                documento = (from d in db.HuellasRandom where d.idHuella == 1 select d.Huella7).FirstOrDefault();
            }

            if (h8 != null)
            {
                documento = (from d in db.HuellasRandom where d.idHuella == 1 select d.Huella8).FirstOrDefault();
            }


            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();
            var bytesBinary = documento;

            Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile();
            //zip.AddEntry(paciente.NoExpediente + "-h-2.bmp", bytesBinary);

            if(h2 != null)
            {
                zip.AddEntry(paciente.NoExpediente + "-h-2.bmp", bytesBinary);
            }

            if (h3 != null)
            {
                zip.AddEntry(paciente.NoExpediente + "-h-3.bmp", bytesBinary);
            }

            if (h7 != null)
            {
                zip.AddEntry(paciente.NoExpediente + "-h-7.bmp", bytesBinary);
            }

            if (h8 != null)
            {
                zip.AddEntry(paciente.NoExpediente + "-h-8.bmp", bytesBinary);
            }

            using (MemoryStream output = new MemoryStream())
            {
                zip.Save(output);

                if(h2 != null)
                {
                    return File(output.ToArray(), "application/zip", paciente.NoExpediente + "-h-2.zip");
                }

                if (h3 != null)
                {
                    return File(output.ToArray(), "application/zip", paciente.NoExpediente + "-h-3.zip");
                }

                if (h7 != null)
                {
                    return File(output.ToArray(), "application/zip", paciente.NoExpediente + "-h-7.zip");
                }

                if (h8 != null)
                {
                    return File(output.ToArray(), "application/zip", paciente.NoExpediente + "-h-8.zip");
                }

            }

            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=" + paciente.NoExpediente + "-h-2.bmp");

            if(h2 != null)
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + paciente.NoExpediente + "-h-2.bmp");
            }

            if (h3 != null)
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + paciente.NoExpediente + "-h-3.bmp");
            }

            if (h7 != null)
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + paciente.NoExpediente + "-h-7.bmp");
            }

            if (h8 != null)
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + paciente.NoExpediente + "-h-8.bmp");
            }

            Response.BinaryWrite(bytesBinary);
            Response.End();
            HttpContext.ApplicationInstance.CompleteRequest();

            return RedirectToAction("Captura");
        }

        public ActionResult CancelarCita(int? id, string comentario)
        {
            var paciente = db.PacienteESP.Find(id);

            paciente.Asistencia = "NO";
            paciente.CancelaComentario = comentario;
            paciente.EstatusCaptura = "Cancelado";

            if(ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Citas");
        }

        public ActionResult HabilitarDoctores(string id_1, string id_2, string id_3, string id_4, string id_5, string id_6, string id_7, string id_8, string id_9, string id_10, string id_11, string id_12, string id_13, string id_14,
            string id_15, string id_16, string id_17, string id_18, string id_19, string id_20 )
        {
            string[] doctores = { id_1, id_2, id_3, id_4, id_5, id_6, id_7, id_8, id_9, id_10, id_11, id_12, id_13, id_14, id_15, id_16, id_17, id_18, id_19, id_20 };

            for(int i = 1; i <= doctores.Length; i++)
            {
                var doctor = db.Doctores.Find(i);

                doctor.ALT = doctores[i - 1] == "on" ? "SI" : "NO";

                if(ModelState.IsValid)
                {
                    db.Entry(doctor).State = EntityState.Modified;
                }
            }

            db.SaveChanges();

            return Redirect("Citas");
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

            List<PacienteESP> data = db.PacienteESP.ToList();
            var selected = data.Where(r => r.Nombre.Contains(parametro) || r.NoExpediente == parametro)
                .Select(S => new {
                    idPacienteESP = S.idPacienteESP,
                    S.Nombre,
                    S.CURP,
                    S.NoExpediente,
                    S.TipoTramite,
                    FechaCita = Convert.ToDateTime(S.FechaCita).ToString("dd-MMMM-yyyy"),
                    S.EstatusCaptura,
                    S.Estatura,
                    S.Genero,
                    S.Metra,
                    S.Doctor
                }).ToList();

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

    }
}