using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.Admin
{
    public class AdminController : Controller
    {
        GMIEntities db = new GMIEntities();

        // GET: Admin
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

            inicio = (inicio ?? start1);
            final = (final ?? finish1);

            ViewBag.Inicio = inicio;
            ViewBag.Final = final;
            ViewBag.Estado = nulos;

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
        public JsonResult ChartSucursal()
        {
            int diaHoy = DateTime.Now.Day;
            int mesHoy = DateTime.Now.Month;
            int anioHoy = DateTime.Now.Year;

            DateTime dateToday = new DateTime(anioHoy, mesHoy, diaHoy);

            int diaManana = DateTime.Now.AddDays(1).Day;
            int mesManana = DateTime.Now.AddDays(1).Month;
            int anioManana = DateTime.Now.AddDays(1).Year;

            DateTime dateTomorrow = new DateTime(anioManana, mesManana, diaManana);

            List<Cita> data = db.Cita.ToList();

            var mejorSucursal = (from s in data where s.FechaCita >= dateToday && s.FechaCita < dateTomorrow && s.Doctor != null select s).GroupBy(o => o.Sucursal);
            var result = (from m in mejorSucursal select m);


            return Json(result, JsonRequestBehavior.AllowGet);
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
    }
}