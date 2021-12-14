using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SCT_iCare.Controllers.ControlDictamen
{
    public class ControlDictamenController : Controller
    {
        private GMIEntities db = new GMIEntities();
        // GET: ControlDictamen
        public ActionResult Index(DateTime? fecha)
        {
            if(fecha == null)
            {
                DateTime fechaHoy = DateTime.Now;
                int year = fechaHoy.Year;
                int month = fechaHoy.Month;
                int day = fechaHoy.Day;

                fecha = new DateTime(year, month, day);
            }
            else
            {
                int year = Convert.ToDateTime(fecha).Year;
                int month = Convert.ToDateTime(fecha).Month;
                int day = Convert.ToDateTime(fecha).Day;

                fecha = new DateTime(year, month, day);
            }

            ViewBag.Fecha = fecha;

            return View();
        }

        public ActionResult Sucursal(string sucursal, DateTime? fecha)
        {
            if (fecha == null)
            {
                DateTime fechaHoy = DateTime.Now;
                int year = fechaHoy.Year;
                int month = fechaHoy.Month;
                int day = fechaHoy.Day;

                fecha = new DateTime(year, month, day);
            }
            else
            {
                int year = Convert.ToDateTime(fecha).Year;
                int month = Convert.ToDateTime(fecha).Month;
                int day = Convert.ToDateTime(fecha).Day;

                fecha = new DateTime(year, month, day);
            }

            ViewBag.Fecha = fecha;

            ViewBag.Sucursal = sucursal;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CorreoEnviado(int id, DateTime? fecha)
        {
            int idCaptura = (from d in db.Captura where d.idPaciente == id select d.idCaptura).FirstOrDefault();
            Captura captura = db.Captura.Find(idCaptura);
            DictamenProblema problema = new DictamenProblema();

            var consulta = (from c in db.DictamenProblema where c.idPaciente == captura.idPaciente select c).FirstOrDefault();

            if(consulta == null)
            {
                problema.idPaciente = captura.idPaciente;
                problema.EstatusEnvio = "SI";

                if (ModelState.IsValid)
                {
                    db.DictamenProblema.Add(problema);
                    db.SaveChanges();
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    consulta.idPaciente = captura.idPaciente;
                    consulta.EstatusEnvio = "SI";

                    db.Entry(consulta).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            

            if (fecha == null)
            {
                DateTime fechaHoy = DateTime.Now;
                int year = fechaHoy.Year;
                int month = fechaHoy.Month;
                int day = fechaHoy.Day;

                fecha = new DateTime(year, month, day);
            }
            else
            {
                int year = Convert.ToDateTime(fecha).Year;
                int month = Convert.ToDateTime(fecha).Month;
                int day = Convert.ToDateTime(fecha).Day;

                fecha = new DateTime(year, month, day);
            }

            ViewBag.Fecha = fecha;

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Problema(int id, DateTime? fecha, string problema)
        {
            int idCaptura = (from d in db.Captura where d.idPaciente == id select d.idCaptura).FirstOrDefault();
            Captura captura = db.Captura.Find(idCaptura);
            DictamenProblema problema1 = new DictamenProblema();

            var consulta = (from c in db.DictamenProblema where c.idPaciente == captura.idPaciente select c).FirstOrDefault();

            if (fecha == null)
            {
                DateTime fechaHoy = DateTime.Now;
                int year = fechaHoy.Year;
                int month = fechaHoy.Month;
                int day = fechaHoy.Day;

                fecha = new DateTime(year, month, day);
            }
            else
            {
                int year = Convert.ToDateTime(fecha).Year;
                int month = Convert.ToDateTime(fecha).Month;
                int day = Convert.ToDateTime(fecha).Day;

                fecha = new DateTime(year, month, day);
            }

            if (consulta == null)
            {
                problema1.idPaciente = captura.idPaciente;
                problema1.Problema = problema.ToString(); ;

                if (ModelState.IsValid)
                {
                    db.DictamenProblema.Add(problema1);
                    db.SaveChanges();
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    consulta.idPaciente = captura.idPaciente;
                    consulta.Problema = problema.ToString();

                    db.Entry(consulta).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Fecha = fecha;
                    return View("Index");
                }
            }

            

            ViewBag.Fecha = fecha;

            return View("Index", ViewBag.Fecha);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CorreoEnviadoSucursal(int id, DateTime? fecha, string sucursal)
        {
            int idCaptura = (from d in db.Captura where d.idPaciente == id select d.idCaptura).FirstOrDefault();
            Captura captura = db.Captura.Find(idCaptura);
            DictamenProblema problema = new DictamenProblema();

            var consulta = (from c in db.DictamenProblema where c.idPaciente == captura.idPaciente select c).FirstOrDefault();

            if (consulta == null)
            {
                problema.idPaciente = captura.idPaciente;
                problema.EstatusEnvio = "SI";

                if (ModelState.IsValid)
                {
                    db.DictamenProblema.Add(problema);
                    db.SaveChanges();
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    consulta.idPaciente = captura.idPaciente;
                    consulta.EstatusEnvio = "SI";

                    db.Entry(consulta).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }



            if (fecha == null)
            {
                DateTime fechaHoy = DateTime.Now;
                int year = fechaHoy.Year;
                int month = fechaHoy.Month;
                int day = fechaHoy.Day;

                fecha = new DateTime(year, month, day);
            }
            else
            {
                int year = Convert.ToDateTime(fecha).Year;
                int month = Convert.ToDateTime(fecha).Month;
                int day = Convert.ToDateTime(fecha).Day;

                fecha = new DateTime(year, month, day);
            }

            ViewBag.Sucursal = sucursal;
            ViewBag.Fecha = fecha;

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProblemaSucursal(int id, DateTime? fecha, string problema, string sucursal)
        {
            int idCaptura = (from d in db.Captura where d.idPaciente == id select d.idCaptura).FirstOrDefault();
            Captura captura = db.Captura.Find(idCaptura);
            DictamenProblema problema1 = new DictamenProblema();

            var consulta = (from c in db.DictamenProblema where c.idPaciente == captura.idPaciente select c).FirstOrDefault();

            if (fecha == null)
            {
                DateTime fechaHoy = DateTime.Now;
                int year = fechaHoy.Year;
                int month = fechaHoy.Month;
                int day = fechaHoy.Day;

                fecha = new DateTime(year, month, day);
            }
            else
            {
                int year = Convert.ToDateTime(fecha).Year;
                int month = Convert.ToDateTime(fecha).Month;
                int day = Convert.ToDateTime(fecha).Day;

                fecha = new DateTime(year, month, day);
            }

            if (consulta == null)
            {
                problema1.idPaciente = captura.idPaciente;
                problema1.Problema = problema.ToString(); ;

                if (ModelState.IsValid)
                {
                    db.DictamenProblema.Add(problema1);
                    db.SaveChanges();
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    consulta.idPaciente = captura.idPaciente;
                    consulta.Problema = problema.ToString();

                    db.Entry(consulta).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Fecha = fecha;
                    return View("Index");
                }
            }


            ViewBag.Sucursal = sucursal;
            ViewBag.Fecha = fecha;

            return View("Index", ViewBag.Fecha);
        }

        public ActionResult AbrirEPI_EC(int? id, DateTime? fecha)
        {
            Captura captura = db.Captura.Find(id);

            if (fecha == null)
            {
                DateTime fechaHoy = DateTime.Now;
                int year = fechaHoy.Year;
                int month = fechaHoy.Month;
                int day = fechaHoy.Day;

                fecha = new DateTime(year, month, day);
            }
            else
            {
                int year = Convert.ToDateTime(fecha).Year;
                int month = Convert.ToDateTime(fecha).Month;
                int day = Convert.ToDateTime(fecha).Day;

                fecha = new DateTime(year, month, day);
            }


            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (id != null)
            {
                TempData["ID"] = id;
                return RedirectToAction("Index", new { fecha = fecha });
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("Index", new { fecha = fecha });
            }
        }

        public ActionResult AbrirEPI_S(int? id, string sucursal, DateTime? fecha)
        {
            Captura captura = db.Captura.Find(id);

            if (fecha == null)
            {
                DateTime fechaHoy = DateTime.Now;
                int year = fechaHoy.Year;
                int month = fechaHoy.Month;
                int day = fechaHoy.Day;

                fecha = new DateTime(year, month, day);
            }
            else
            {
                int year = Convert.ToDateTime(fecha).Year;
                int month = Convert.ToDateTime(fecha).Month;
                int day = Convert.ToDateTime(fecha).Day;

                fecha = new DateTime(year, month, day);
            }


            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (id != null)
            {
                TempData["ID"] = id;
                return RedirectToAction("Sucursal", "ControlDictamen", new { sucursal = sucursal, fecha = fecha });
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("Sucursal", "ControlDictamen", new { sucursal = sucursal, fecha = fecha });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDatosDictamen(int id, string email, string telefono)
        {
            var idpaciente = (from p in db.Paciente where p.idPaciente == id select p.idPaciente).FirstOrDefault();

            var paciente = db.Paciente.Find(idpaciente);

            paciente.Email = email != "" ? email : paciente.Email;
            paciente.Telefono = telefono != "" ? telefono : paciente.Telefono;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
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
                var selected = data.Join(db.Captura, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                    .Where(r => (r.M.NoExpediente == parametro))
                    .Select(S => new {
                        S.M.idCaptura,
                        S.N.Nombre,
                        S.M.NoExpediente,
                        S.N.Email,
                        S.N.Telefono,
                        FechaReferencia = Convert.ToDateTime(S.M.FechaExpdiente).ToString("dd-MMMM-yyyy"),
                        S.M.Sucursal
                    });

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
                var selected = data.Join(db.Captura, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                    .Where(r => r.N.Nombre.Contains(parametro))
                    .Select(S => new {
                        S.M.idCaptura,
                        S.N.Nombre,
                        S.M.NoExpediente,
                        S.N.Email,
                        S.N.Telefono,
                        FechaReferencia = Convert.ToDateTime(S.M.FechaExpdiente).ToString("dd-MMMM-yyyy"),
                        S.M.Sucursal
                    });

                return Json(selected, JsonRequestBehavior.AllowGet);
            }

            //List<Paciente> data = db.Paciente.ToList();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //var selected = data.Join(db.Captura, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
            //    .Where(r => (r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro) )
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
    }
}