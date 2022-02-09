using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.Maqueta
{
    public class MaquetaController : Controller
    {
        GMIEntities db = new GMIEntities();
        // GET: Maqueta
        public ActionResult Index(string canal, string cuenta, string sucursal, DateTime? fecha)
        {
            if(sucursal == "" || sucursal == null)
            {
                ViewBag.Sucursal = "";
            }
            else
            {
                ViewBag.Sucursal = sucursal;
            }

            if (canal == "" || canal == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canal;
            }

            if (cuenta == "" || cuenta == null)
            {
                ViewBag.Cuenta = "";
            }
            else
            {
                ViewBag.Cuenta = cuenta;
            }

            ViewBag.Fecha = fecha != null ? fecha : null;

            return View();
        }

        public ActionResult CambiarCuenta(int? id, string cuenta)
        {
            var cita = db.Cita.Find(id);

            cita.Cuenta = cuenta;

            if(ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("Index");
        }
    }
}