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
        public ActionResult Index()
        {

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
    }
}