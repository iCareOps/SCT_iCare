using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SCT_iCare;

namespace SCT_iCare.Controllers.EPICenter
{
    public class EPIsController : Controller
    {
        private GMIEntities db = new GMIEntities();

        // GET: EPIs
        public ActionResult Index(int? pageSize, int? page, DateTime? inicio, DateTime? final)
        {
            //DateTime start1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //DateTime finish1 = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);

            //int nulos = 0;

            //if (inicio != null && final != null)
            //{
            //    nulos = 1;
            //}

            //if (inicio != null)
            //{
            //    DateTime start = Convert.ToDateTime(inicio);
            //    int year = start.Year;
            //    int month = start.Month;
            //    int day = start.Day;

            //    inicio = new DateTime(year, month, day);
            //}
            //if (final != null)
            //{
            //    DateTime finish = Convert.ToDateTime(final).AddDays(1);
            //    int year = finish.Year;
            //    int month = finish.Month;
            //    int day = finish.Day;

            //    final = new DateTime(year, month, day);
            //}

            //inicio = (inicio ?? start1);
            //final = (final ?? finish1);

            //ViewBag.Inicio = inicio;
            //ViewBag.Final = final;
            //ViewBag.Estado = nulos;

            return View(/*db.EPI.ToList()*/);
        }

        public ActionResult CentroControl()
        {
            ViewBag.Date = DateTime.Now.ToString("dd-MMMM-yyyy");

            return View(db.Paciente.ToList());
        }

        public ActionResult AbrirEPI_EC(int? id)
        {
            Captura captura = db.Captura.Find(id);


            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (id != null)
            {
                TempData["ID"] = id;
                return RedirectToAction("Captura");
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("Captura");
            }
        }

        public ActionResult EditExpedienteEC(int? id, string usuario/*[Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI*/)
        {
            Captura captura = db.Captura.Find(id);

            if(captura.EstatusCaptura == "En captura...")
            {
                TempData["ERROR"] = "ESTE EXPEDIENTE YA HA SIDO TOMADO POR OTRO USUARIO";
                return RedirectToAction("Captura");
            }

            captura.EstatusCaptura = "En captura...";
            captura.InicioCaptura = DateTime.Now;
            captura.Capturista = usuario;

            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.SaveChanges();                
            }

            if(id != null)
            {
                TempData["ID"] = id;
                return RedirectToAction("Captura");
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("Captura");
            }
        }

        public ActionResult AbrirEPI_S(int? id, string sucursal)
        {
            Captura captura = db.Captura.Find(id);


            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (id != null)
            {
                TempData["ID"] = id;
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = sucursal });
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = sucursal });
            }
        }

        public ActionResult EditExpedienteS(int? id, string usuario, string sucursal/*[Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI*/)
        {
            Captura captura = db.Captura.Find(id);

            if (captura.EstatusCaptura == "En captura...")
            {
                TempData["ERROR"] = "ESTE EXPEDIENTE YA HA SIDO TOMADO POR OTRO USUARIO";
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = sucursal });
            }

            captura.EstatusCaptura = "En captura...";
            captura.InicioCaptura = DateTime.Now;
            captura.Capturista = usuario;

            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (id != null)
            {
                TempData["ID"] = id;
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = sucursal });
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = sucursal });
            }
        }

        public ActionResult AbrirDictamenS(int? id, string sucursal)
        {
            Captura captura = db.Captura.Find(id);

            int idPaciente = Convert.ToInt32(captura.idPaciente);
            var Dictamen = (from d in db.Dictamen where d.idPaciente == idPaciente select d.Dictamen1).FirstOrDefault();

            var bytesBinary = Dictamen;

            if (id != null)
            {
                TempData["ID2"] = id;
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = sucursal });
            }
            else
            {
                TempData["ID2"] = null;
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = sucursal });
            }
        }

        public ActionResult AbrirEPI_D(int? id, string sucursal, string doctor)
        {
            Captura captura = db.Captura.Find(id);


            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (id != null)
            {
                TempData["ID"] = id;
                return capturaDoctor(sucursal, doctor);
            }
            else
            {
                TempData["ID"] = null;
                return capturaDoctor(sucursal, doctor);
            }
        }

        public ActionResult EditExpedienteD(int? id, string usuario, string sucursal, string doctor/*[Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI*/)
        {
            Captura captura = db.Captura.Find(id);

            if (captura.EstatusCaptura == "En captura...")
            {
                TempData["ERROR"] = "ESTE EXPEDIENTE YA HA SIDO TOMADO POR OTRO USUARIO";
                return capturaDoctor(sucursal, doctor);
            }

            captura.EstatusCaptura = "En captura...";
            captura.InicioCaptura = DateTime.Now;
            captura.Capturista = usuario;

            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (id != null)
            {
                TempData["ID"] = id;
                //return RedirectToAction("capturaDoctor", "EPIs", new { sucursal = sucursal, doctor = doctor });
                return capturaDoctor(sucursal, doctor);
            }
            else
            {
                TempData["ID"] = null;
                //return RedirectToAction("capturaDoctor", "EPIs", new { sucursal = sucursal, doctor = doctor });
                return capturaDoctor(sucursal, doctor);
            }
        }

        public ActionResult AbrirDictamenD(int? id, string sucursal, string doctor)
        {
            Captura captura = db.Captura.Find(id);

            int idPaciente = Convert.ToInt32(captura.idPaciente);
            var Dictamen = (from d in db.Dictamen where d.idPaciente == idPaciente select d.Dictamen1).FirstOrDefault();

            var bytesBinary = Dictamen;

            if (id != null)
            {
                TempData["ID2"] = id;
                return capturaDoctor(sucursal, doctor);
            }
            else
            {
                TempData["ID2"] = null;
                return capturaDoctor(sucursal, doctor);
            }
        }

        public ActionResult AbrirEPI(int id)
        {
            Captura captura = db.Captura.Find(id);
            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;
            TempData["ID"] = null;
            return File(bytesBinary, "application/pdf");
        }

        public ActionResult AbrirDictamen(int id)
        {
            Captura captura = db.Captura.Find(id);
            var expediente = (from e in db.Dictamen where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Dictamen1;
            TempData["ID"] = null;
            return File(bytesBinary, "application/pdf");
        }

        public ActionResult SubirDictamen(int? id /*[Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI*/)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Captura captura = db.Captura.Find(id);
            if (captura == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = id;
            ViewBag.Sucursal = captura.Sucursal;
            return View(captura);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dictaminar(int id, HttpPostedFileBase file, string comentario, DateTime? vigencia, string aptitud)
        {
            //EPI epi = db.EPI.Find(id);
            Captura captura = db.Captura.Find(id);
            Dictamen dictamen = new Dictamen();

            byte[] bytes2 = null;

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

            TimeSpan diferencia = DateTime.Now - Convert.ToDateTime(captura.InicioCaptura);
            //int totalCaptura = (DateTime.Now.Minute - Convert.ToDateTime(captura.InicioCaptura).Minute);
            int minutos = Convert.ToInt32(diferencia.TotalMinutes);

            var pausas = (from p in db.CapturaIncidencia where p.idCaptura == id select p).ToList();

            if(pausas != null)
            {
                int diferenciaTotal = 0;
                foreach(var item in pausas)
                {
                    TimeSpan diferencia2 = (Convert.ToDateTime(item.PausaFinal) - Convert.ToDateTime(item.PausaInicio));
                    diferenciaTotal += Convert.ToInt32(diferencia2.TotalMinutes);
                }

                minutos = minutos - diferenciaTotal;
            }

            captura.EstatusCaptura = "Terminado";
            captura.FinalCaptura = DateTime.Now;
            captura.Duracion = minutos;
            dictamen.Dictamen1 = bytes2;
            dictamen.idPaciente = captura.idPaciente;
            dictamen.idAptitud = 1;
            captura.Aptitud = aptitud;
            captura.ComentarioAptitud = comentario;
            captura.FechaVigencia = vigencia;


            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.Dictamen.Add(dictamen);
                db.SaveChanges();
                return RedirectToAction("Captura");
            }

            return RedirectToAction("Captura");
        }

        public ActionResult DescargarPDF(int? id)
        {
            Captura captura = db.Captura.Find(id);
            Dictamen dictamen = new Dictamen();

            var documento = (from d in db.Dictamen where captura.idPaciente == d.idPaciente orderby d.idDictamen descending select d.Dictamen1).FirstOrDefault();

            var bytesBinary = documento;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + captura.NombrePaciente + ".pdf");
            Response.BinaryWrite(bytesBinary);
            Response.End();

            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
            //    Response.BinaryWrite(bytesBinary);
            //    //Response.End();
            //HttpContext.ApplicationInstance.CompleteRequest();

            return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = captura.Sucursal.ToString() });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Captura(int? pageSize, int? page, DateTime? inicio, DateTime? final)
        {
            //DateTime start = DateTime.Now;
            //int year = start.Year;
            //int month = start.Month;
            //int day = start.Day;
            //int tomorrorDay = day + 1;
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


            pageSize = (pageSize ?? 10);
            page = (page ?? 1);
            ViewBag.PageSize = pageSize;

            ViewBag.Parameter = "";
            //return View();
            return View(db.Cita.Where(w => w.FechaCita >= thisDate && w.FechaCita < tomorrowDate).OrderByDescending(i => i.FechaCita).ToPagedList(page.Value, pageSize.Value));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Captura(string parameter)
        //{
        //    ViewBag.Parameter = parameter;
        //    return View();
        //}

        public ActionResult capturaSucursal(string sucursal)
        {
            ViewBag.Sucursal = sucursal;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult capturaDoctor(string sucursal, string doctor)
        {
            ViewBag.Sucursal = sucursal;
            ViewBag.Doctor = doctor;
            TempData["Sucursal"] = sucursal;
            TempData["Doctor"] = doctor;
            return View("capturaDoctor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pendiente(int id, string paciente, string folio, string capturista, string incidencia, string falla)
        {
            Captura captura = db.Captura.Find(id);
            IncidenciaDictamen incidencias = new IncidenciaDictamen();

            Dictamen dictamen = new Dictamen();

            dictamen.idPaciente = captura.idPaciente;
            dictamen.idAptitud = Convert.ToInt32(falla);

            captura.EstatusCaptura = "Pendiente";
            captura.FinalCaptura = DateTime.Now;

            TimeSpan diferencia = DateTime.Now - Convert.ToDateTime(captura.InicioCaptura);
            //int totalCaptura = (DateTime.Now.Minute - Convert.ToDateTime(captura.InicioCaptura).Minute);
            int minutos = Convert.ToInt32(diferencia.TotalMinutes);

            var pausas = (from p in db.CapturaIncidencia where p.idCaptura == id select p).ToList();

            if (pausas != null)
            {
                int diferenciaTotal = 0;
                foreach (var item in pausas)
                {
                    TimeSpan diferencia2 = (Convert.ToDateTime(item.PausaFinal) - Convert.ToDateTime(item.PausaInicio));
                    diferenciaTotal += Convert.ToInt32(diferencia2.TotalMinutes);
                }

                minutos = minutos - diferenciaTotal;
            }

            captura.Duracion = minutos;


            //incidencias.NombrePaciente = paciente;
            incidencias.NoExpediente = folio;
            incidencias.Capturista = capturista;
            incidencias.Incidencia = incidencia;
            incidencias.FechaIncidencia = DateTime.Now;
            incidencias.idCaptura = id;


            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.IncidenciaDictamen.Add(incidencias);
                db.Dictamen.Add(dictamen);
                db.SaveChanges();
                return RedirectToAction("Captura", "EPIs");
            }

            return RedirectToAction("Captura", "EPIs");
        }


        public JsonResult EjemploJSON()
        {
            List<Paciente> data = db.Paciente.ToList();

            var selected = data.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m }).Select( S => new { S.N.Nombre, S.N.Telefono, S.M.FechaCita, S.M.Referencia });
            var modelo = db.Paciente.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m });

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        public JsonResult General()
        {
            List<Paciente> data = db.Paciente.ToList();
            var selected = data.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m }).Select(S => new { S.N.Nombre, S.N.Telefono, S.M.FechaCita, S.M.Referencia });

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actuales(string palabra)
        {
            List<Paciente> data = db.Paciente.ToList();
            var selected = data.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m }).Where(r => r.N.Nombre == palabra).Select(S => new { S.N.Nombre, S.N.Telefono, S.M.FechaCita, S.M.Referencia });

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarTodo(string dato)
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
            var selected = data.Join(db.Captura, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                .Where(r => (r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro))
                .Join(db.Cita, o => o.N.idPaciente, p => p.idPaciente, (o, p) => new { O = o, P = p })
                .Select(S => new {
                    S.O.N.idPaciente,
                    S.O.N.Nombre,
                    S.O.M.NoExpediente,
                    S.O.N.Email,
                    S.O.M.TipoTramite,
                    S.O.M.EstatusCaptura,
                    S.O.N.Telefono,
                    FechaReferencia = Convert.ToDateTime(S.O.M.FechaExpdiente).ToString("dd-MMMM-yyyy"),
                    S.O.M.Sucursal,
                    S.O.M.Doctor,
                    S.P.TipoLicencia,
                    S.O.M.idCaptura
                });

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        
        public ActionResult Pausa(int idCaptura, string motivo)
        {
            CapturaIncidencia comentario = new CapturaIncidencia();
            Captura captura = db.Captura.Find(idCaptura);

            comentario.idCaptura = idCaptura;
            comentario.Comentario = motivo;
            comentario.PausaInicio = DateTime.Now;

            captura.EstatusCaptura = "Pausado";

            if (ModelState.IsValid)
            {
                db.CapturaIncidencia.Add(comentario);
                db.SaveChanges();
                return Redirect("Captura");
            }

            return Redirect("Captura");
        }

        public ActionResult Reanudar(int idCaptura)
        {
            var capturaIncidencia = (from c in db.CapturaIncidencia where c.idCaptura == idCaptura orderby c.idCapturaIncidencia descending select c).FirstOrDefault();
            Captura captura = db.Captura.Find(idCaptura);

            capturaIncidencia.PausaFinal = DateTime.Now;

            captura.EstatusCaptura = "En captura...";

            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.Entry(capturaIncidencia).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("Captura");
            }

            return Redirect("Captura");
        }



    }
}
