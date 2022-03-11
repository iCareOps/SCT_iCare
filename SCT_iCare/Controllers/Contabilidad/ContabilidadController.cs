using SCT_iCare.Filters;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.Contabilidad
{
    public class ContabilidadController : Controller
    {
        GMIEntities db = new GMIEntities();
        // GET: Maqueta
        public ActionResult Index(string canal, string cuenta, string sucursal, DateTime? fecha, string tipoPago)
        {
            if (sucursal == "" || sucursal == null)
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

            if (tipoPago == "" || tipoPago == null)
            {
                ViewBag.Pago = "";
            }
            else
            {
                ViewBag.Pago = tipoPago;
            }

            ViewBag.Fecha = fecha != null ? fecha : null;

            return View();
        }

        public ActionResult CambiarCuenta(int? id, string cuenta, string cuenta2, string comentario, string usuario, string canal, string sucursal, DateTime? fecha)
        {
            var cita = db.Cita.Find(id);

            string historico = cita.CuentaComentario == null ? "" : cita.CuentaComentario + "+";
            string cuentaAnterior = cita.Cuenta == null ? "" : " PROVIENE DE " + cita.Cuenta;
            cita.CuentaComentario = historico + comentario + cuentaAnterior + " " + DateTime.Today.ToString("dd-MMMM-yyyy") + " POR " + usuario;
            cita.Cuenta = cuenta;

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { fecha = fecha, sucursal = sucursal, cuenta = cuenta2, canal = canal });
        }

        public ActionResult CambiarCuentaALT(int? id, string cuenta, string comentario, string usuario, DateTime? fecha, string sucursal,
            string cuenta2, string canal, string pago)
        {
            var cita = db.PacienteESP.Find(id);

            string historico = cita.CuentaComentario == null ? "" : cita.CuentaComentario + "+"; 
            string cuentaAnterior = cita.Cuenta == null ? "" : " PROVIENE DE " + cita.Cuenta;
            cita.CuentaComentario = historico + comentario + cuentaAnterior + " " + DateTime.Today.ToString("dd-MMMM-yyyy") + " POR " + usuario;
            cita.Cuenta = cuenta;

            if(pago != null || pago != "")
            {
                cita.TipoPago = pago;
            }

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { fecha = fecha, sucursal = sucursal, cuenta = cuenta2, canal = canal });
        }
    }
}