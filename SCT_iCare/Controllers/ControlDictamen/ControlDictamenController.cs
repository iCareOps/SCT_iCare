using System;
using System.Collections.Generic;
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
                .Where(r => (r.N.Nombre.Contains(parametro) || r.M.NoExpediente == parametro) )
                .Select(S => new {
                    S.N.idPaciente,
                    S.N.Nombre,
                    S.M.NoExpediente,
                    S.N.Email,
                    S.N.Telefono,
                    FechaReferencia = Convert.ToDateTime(S.M.FechaExpdiente).ToString("dd-MMMM-yyyy"),
                    S.M.Sucursal
                });


            return Json(selected, JsonRequestBehavior.AllowGet);
        }
    }
}