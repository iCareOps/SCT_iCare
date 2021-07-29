using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SCT_iCare.Controllers.Conciliacion
{
    public class ConciliacionController : Controller
    {
        private GMIEntities db = new GMIEntities();

        // GET: Conciliacion
        public ActionResult Index(DateTime? inicio, DateTime? final, int? pageSize, int? page)
        {
            DateTime start1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime finish1 = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);

            //DateTime start2 = new DateTime(start1.Year, start1.Month, start1.Day);
            //DateTime finish2 = new DateTime(finish1.Year, finish1.Month, finish1.Day);

            //inicio = new DateTime(Convert.ToDateTime(inicio).Year, Convert.ToDateTime(inicio).Month, Convert.ToDateTime(inicio).Day);
            //final = new DateTime(Convert.ToDateTime(final).Year, Convert.ToDateTime(final).Month, Convert.ToDateTime(final).Day);

            int nulos = 0;

            if(inicio != null && final != null)
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
            }
            if(final != null)
            {
                DateTime finish = Convert.ToDateTime(final).AddDays(1);
                int year = finish.Year;
                int month = finish.Month;
                int day = finish.Day;

                final = new DateTime(year, month, day);
            }

            //pageSize = (pageSize ?? 10);
            //page = (page ?? 1);
            inicio = (inicio ?? start1);
            final = (final ?? finish1);

            //ViewBag.PageSize = pageSize;
            ViewBag.Inicio = inicio;
            ViewBag.Final = final;
            ViewBag.Estado = nulos;

            return View(db.Cita.Where(w => w.FechaReferencia >= inicio && w.FechaReferencia < final).OrderByDescending(i => i.FechaReferencia));
        }

        public JsonResult Buscar(string dato)
        {

            List<Paciente> data = db.Paciente.ToList();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var selected = data.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                .Where(r => r.M.Referencia == dato.Trim())
                .Select(S => new {
                    S.N.idPaciente,
                    S.N.Nombre,
                    S.M.TipoPago,
                    S.M.EstatusPago,
                    S.M.TipoLicencia,
                    FechaReferencia = S.M.FechaReferencia.ToString(),
                    S.M.Referencia,
                    S.M.TipoTramite,
                    S.M.Canal1
                });


            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        public void ExportarCSV()
        {
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Paciente o Géstor\", \"EPIs\", \"Estatus de Pago\", \"Fecha de Pago\", \"Tipo de Pago\", \"Referencia\", \"Sucursal\" ");
            Response.ClearContent();
            Response.AddHeader("content-dispotition", "attachment;filename=Conciliacion.csv");
            Response.ContentType = "text/csv";

            var Conciliacion = db.Paciente.Join(db.Cita, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                .Select(S => new {
                    S.N.Nombre,
                    S.M.TipoPago,
                    S.M.EstatusPago,
                    FechaReferencia = S.M.FechaReferencia.ToString(),
                    S.M.Referencia,
                    S.M.Canal1,
                    S.M.Sucursal
                });

            foreach(var item in Conciliacion)
            {
                sw.WriteLine(string.Format("\"0\", \"1\", \"2\", \"3\", \"4\", \"5\", \"6\"",
                    item.Nombre,
                    item.Referencia,
                    item.EstatusPago,
                    item.FechaReferencia,
                    item.TipoPago,
                    item.Referencia,
                    item.Sucursal));
            }

            Response.Write(sw.ToString());
            Response.End();

        }
    }
}