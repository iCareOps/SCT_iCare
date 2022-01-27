using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult TablaDinamica(string gestor)
        {
            if (gestor == "Diarias")
            {
                return Redirect("TablaMetasDiarias");
            }
            else if (gestor == "Semanales")
            {
                return Redirect("TablaMetas");
            }
            else if (gestor == "Sucursales")
            {
                return Redirect("TablaDinamica");
            }
            else if (gestor == "CallCenter")
            {
                return Redirect("TablaCallCenter");
            }
            else if (gestor == "Gestores")
            {
                return Redirect("TablaGestores");
            }
            else if (gestor == "Alternativos")
            {
                return Redirect("TablaAlternativos");
            }
            else if (gestor == "Doctores")
            {
                return Redirect("ConteoDoctores");
            }
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else
            {
                return View();
            }
        }

        public ActionResult TablaAlternativos(string gestor)
        {
            if (gestor == "Diarias")
            {
                return Redirect("TablaMetasDiarias");
            }
            else if (gestor == "Semanales")
            {
                return Redirect("TablaMetas");
            }
            else if (gestor == "Sucursales")
            {
                return Redirect("TablaDinamica");
            }
            else if (gestor == "CallCenter")
            {
                return Redirect("TablaCallCenter");
            }
            else if (gestor == "Gestores")
            {
                return Redirect("TablaGestores");
            }
            else if (gestor == "Alternativos")
            {
                return Redirect("TablaAlternativos");
            }
            else if (gestor == "Doctores")
            {
                return Redirect("ConteoDoctores");
            }
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else
            {
                return View();
            }
        }

        public ActionResult TablaCallCenter(string gestor)
        {
            if (gestor == "Diarias")
            {
                return Redirect("TablaMetasDiarias");
            }
            else if (gestor == "Semanales")
            {
                return Redirect("TablaMetas");
            }
            else if (gestor == "Sucursales")
            {
                return Redirect("TablaDinamica");
            }
            else if (gestor == "CallCenter")
            {
                return Redirect("TablaCallCenter");
            }
            else if (gestor == "Gestores")
            {
                return Redirect("TablaGestores");
            }
            else if (gestor == "Alternativos")
            {
                return Redirect("TablaAlternativos");
            }
            else if (gestor == "Doctores")
            {
                return Redirect("ConteoDoctores");
            }
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else
            {
                return View();
            }
        }

        public ActionResult TablaGestores(string gestor)
        {
            if (gestor == "Diarias")
            {
                return Redirect("TablaMetasDiarias");
            }
            else if (gestor == "Semanales")
            {
                return Redirect("TablaMetas");
            }
            else if (gestor == "Sucursales")
            {
                return Redirect("TablaDinamica");
            }
            else if (gestor == "CallCenter")
            {
                return Redirect("TablaCallCenter");
            }
            else if (gestor == "Gestores")
            {
                return Redirect("TablaGestores");
            }
            else if (gestor == "Alternativos")
            {
                return Redirect("TablaAlternativos");
            }
            else if (gestor == "Doctores")
            {
                return Redirect("ConteoDoctores");
            }
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else
            {
                return View();
            }
        }

        public ActionResult TablaMetas(string gestor)
        {
            if (gestor == "Diarias")
            {
                return Redirect("TablaMetasDiarias");
            }
            else if (gestor == "Semanales")
            {
                return Redirect("TablaMetas");
            }
            else if (gestor == "Sucursales")
            {
                return Redirect("TablaDinamica");
            }
            else if (gestor == "CallCenter")
            {
                return Redirect("TablaCallCenter");
            }
            else if (gestor == "Gestores")
            {
                return Redirect("TablaGestores");
            }
            else if (gestor == "Alternativos")
            {
                return Redirect("TablaAlternativos");
            }
            else if (gestor == "Doctores")
            {
                return Redirect("ConteoDoctores");
            }
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else
            {
                return View();
            }
        }

        public ActionResult TablaMetasDiarias(string gestor)
        {
            if (gestor == "Diarias")
            {
                return Redirect("TablaMetasDiarias");
            }
            else if (gestor == "Semanales")
            {
                return Redirect("TablaMetas");
            }
            else if (gestor == "Sucursales")
            {
                return Redirect("TablaDinamica");
            }
            else if (gestor == "CallCenter")
            {
                return Redirect("TablaCallCenter");
            }
            else if (gestor == "Gestores")
            {
                return Redirect("TablaGestores");
            }
            else if (gestor == "Alternativos")
            {
                return Redirect("TablaAlternativos");
            }
            else if (gestor == "Doctores")
            {
                return Redirect("ConteoDoctores");
            }
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ConteoDoctores(string gestor)
        {
            if (gestor == "Diarias")
            {
                return Redirect("TablaMetasDiarias");
            }
            else if (gestor == "Semanales")
            {
                return Redirect("TablaMetas");
            }
            else if (gestor == "Sucursales")
            {
                return Redirect("TablaDinamica");
            }
            else if (gestor == "CallCenter")
            {
                return Redirect("TablaCallCenter");
            }
            else if (gestor == "Gestores")
            {
                return Redirect("TablaGestores");
            }
            else if (gestor == "Alternativos")
            {
                return Redirect("TablaAlternativos");
            }
            else if (gestor == "Doctores")
            {
                return Redirect("ConteoDoctores");
            }
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else
            {
                return View();
            }
        }

        public ActionResult TablaComparacion(string gestor)
        {
            if (gestor == "Diarias")
            {
                return Redirect("TablaMetasDiarias");
            }
            else if (gestor == "Semanales")
            {
                return Redirect("TablaMetas");
            }
            else if (gestor == "Sucursales")
            {
                return Redirect("TablaDinamica");
            }
            else if (gestor == "CallCenter")
            {
                return Redirect("TablaCallCenter");
            }
            else if (gestor == "Gestores")
            {
                return Redirect("TablaGestores");
            }
            else if (gestor == "Alternativos")
            {
                return Redirect("TablaAlternativos");
            }
            else if (gestor == "Doctores")
            {
                return Redirect("ConteoDoctores");
            }
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EditarMeta(int? id, int? meta)
        {
            Canales canales = db.Canales.Find(id);

            canales.Meta = meta == null ? canales.Meta : meta;

            if(ModelState.IsValid)
            {
                db.Entry(canales).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("TablaMetas");
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

                List<Paciente> data = db.Paciente.ToList();
                var selected = data.Join(db.Captura, n => n.idPaciente, m => m.idPaciente, (n, m) => new { N = n, M = m })
                    .Where(r => r.M.NoExpediente == parametro)
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
    }
}