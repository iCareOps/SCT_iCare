using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.PruebasAngel
{
    public class Prueba_1 : Controller
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ConteoDoctores(string gestor, int? pageSize, int? page, DateTime? mes, string grupo)
        {

            TempData["MES"] = mes != null ? Convert.ToDateTime(mes).Month : 0;
            TempData["AÑO"] = mes != null ? Convert.ToDateTime(mes).Year : 0;
            TempData["FECHA"] = mes;

            if(Convert.ToDateTime(mes).Month == DateTime.Today.Month && Convert.ToDateTime(mes).Year == DateTime.Today.Year)
            {
                TempData["MES"] = 0;
                TempData["AÑO"] = 0;
                TempData["FECHA"] = DateTime.Today;
            }

            pageSize = (pageSize ?? 10);
            page = (page ?? 1);
            ViewBag.PageSize = pageSize;

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
            //else if (gestor == "Doctores")
            //{
            //    return Redirect("ConteoDoctores");
            //}
            else if (gestor == "Comparacion")
            {
                return Redirect("TablaComparacion");
            }
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
            }
            else
            {
                if(grupo == "" || grupo == "todos" || grupo == null)
                {
                    ViewBag.Grupo = "";
                    return View(db.Doctores2.OrderByDescending(o => o.idDoctor2).ToPagedList(page.Value, pageSize.Value));
                }
                else
                {
                    ViewBag.Grupo = grupo;
                    return View(db.Doctores2.Where(w => w.Grupo == grupo).OrderByDescending(o => o.idDoctor2).ToPagedList(page.Value, pageSize.Value));
                }
                
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
            }
            else
            {
                return View();
            }
        }

        public ActionResult TablaComparacion2(string gestor)
        {
            return View();
        }

        public ActionResult ActualizarMeta(string gestor)
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ActualizarPrecios(string gestor)
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
            else if (gestor == "Metas")
            {
                return Redirect("ActualizarMeta");
            }
            else if (gestor == "Precios")
            {
                return Redirect("ActualizarPrecios");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Gestores8Columnas()
        {
            return View();
        }

        public ActionResult MediosDigitales8Columnas()
        {
            return View();
        }

        public ActionResult InSitu8Columnas()
        {
            return View();
        }

        public ActionResult ALT8Columnas()
        {
            return View();
        }

        public ActionResult Sindicatos8Columnas()
        {
            return View();
        }

        public ActionResult CallCenter8Columnas()
        {
            return View();
        }

        public ActionResult Empresas8Columnas()
        {
            return View();
        }

        public ActionResult Otros8Columnas()
        {
            return View();
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

            return Redirect("ActualizarMeta");
        }

        public ActionResult EditarPrecios(int? id, string precio, string precioIVA, string usuario)
        {
            Referido referido = db.Referido.Find(id);

            string precioAnterior = referido.PrecioNormal;
            string precioAnteriorIVA = referido.PrecioNormalconIVA;

            string historico = referido.HistorialPrecios == null ? "" : referido.HistorialPrecios + "+";
            referido.HistorialPrecios = historico +  " "+usuario+" en " + DateTime.Today.ToString("dd-MM-yyyy") + " de " + precioAnterior + " a " +precio;

            referido.PrecioNormal = precio == "" ? referido.PrecioNormal : precio;

            if (ModelState.IsValid)
            {
                db.Entry(referido).State = EntityState.Modified;
                db.SaveChanges();
            }

            historico = referido.HistorialPrecios == null ? "" : referido.HistorialPrecios + "+";
            referido.HistorialPrecios = historico + " " + usuario + " en " + DateTime.Today.ToString("dd-MM-yyyy") + " de " + precioAnteriorIVA + " a " + precioIVA;

            referido.PrecioNormalconIVA = precioIVA == "" ? referido.PrecioNormalconIVA : precioIVA;

            if (ModelState.IsValid)
            {
                db.Entry(referido).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("ActualizarPrecios");
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