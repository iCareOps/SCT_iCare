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
        public ActionResult Index(string canal, string cuenta, string sucursal, DateTime? fechaInicio, DateTime? fechaFinal, string tipoPago, int? referido)
        {
            var Referido = db.Referido.Find(referido);

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

            if (referido == null)
            {
                ViewBag.Referido = "";
                ViewBag.idReferido = 0;
            }
            else
            {
                ViewBag.Referido = Referido.Nombre;
                ViewBag.idReferido = Referido.idReferido;
            }

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;

            return View();
        }

        public ActionResult Conciliados(string canal, string cuenta, string sucursal, DateTime? fechaInicio, DateTime? fechaFinal, string tipoPago, int? referido)
        {
             var Referido = db.Referido.Find(referido);

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

            if (referido == null)
            {
                ViewBag.Referido = "";
                ViewBag.idReferido = 0;
            }
            else
            {
                ViewBag.Referido = Referido.Nombre;
                ViewBag.idReferido = Referido.idReferido;
            }

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;

            return View();
        }


        ///Conciliacion
        public ActionResult Conciliacion(DateTime? fechaInicio, DateTime? fechaFinal)
        {

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;

            return View();
        }

        public ActionResult Pagos(DateTime? fechaInicio, DateTime? fechaFinal)
        {

            ViewBag.FechaInicio = fechaInicio != null ? fechaInicio : null;
            ViewBag.FechaFinal = fechaFinal != null ? fechaFinal : null;

            return View();
        }

        public ActionResult CambiarCuenta(int? id, string cuenta, string cuenta2, string comentario, string usuario, string canal, string sucursal, 
            DateTime? fechaInicio, DateTime? fechaFinal, DateTime? fechaContable, string pago, int? idGestor)
        {
            var cita = db.Cita.Find(id);

            string historico = cita.CuentaComentario == null ? "" : cita.CuentaComentario + "+";
            string cuentaAnterior = cita.Cuenta == null ? "" : " PROVIENE DE " + cita.Cuenta;
            cita.CuentaComentario = historico + comentario + cuentaAnterior + " " + DateTime.Today.ToString("dd-MM-yy") + " POR " + usuario;
            cita.Cuenta = cuenta;
            cita.FechaContable = fechaContable == null ? DateTime.Now : fechaContable;

            if (pago != null || pago != "")
            {
                cita.TipoPago = pago;
            }

            cita.Conciliado = DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString();

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta2, canal = canal, referido = idGestor });
        }

        public ActionResult CambiarCuentaALT(int? id, string cuenta, string comentario, string usuario, DateTime? fechaInicio, DateTime? fechaFinal, DateTime? fechaContable, string sucursal,
            string cuenta2, string canal, string pago, int? idGestor)
        {
            var cita = db.PacienteESP.Find(id);

            string historico = cita.CuentaComentario == null ? "" : cita.CuentaComentario + "+"; 
            string cuentaAnterior = cita.Cuenta == null ? "" : " PROVIENE DE " + cita.Cuenta;
            cita.CuentaComentario = historico + comentario + cuentaAnterior + " " + DateTime.Today.ToString("dd-MM-yy") + " POR " + usuario;
            cita.Cuenta = cuenta;
            cita.FechaContable = fechaContable == null ? DateTime.Now : fechaContable;

            if(pago != null || pago != "")
            {
                cita.TipoPago = pago;
            }

            cita.Conciliado = DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString();

            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta2, canal = canal, referido = idGestor });
        }

        public ActionResult AbrirTicket(int? idTicket, string cuenta, string canal, string sucursal, string tiporeferido, string tipopago,
            DateTime? fechaInicio, DateTime? fechaFinal)
        {                                          
            if (idTicket != null)
            {                
                TempData["ID"] = idTicket;
                return RedirectToAction("Index", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta, canal = canal, referido = tiporeferido, tipopago = tipopago });
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("Index", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta, canal = canal, referido = tiporeferido, tipopago = tipopago });
            }


        }
        //Abrir ticket vista conciliados
        public ActionResult AbrirTicketC(int? idTicket, string cuenta, string canal, string sucursal, string tiporeferido, string tipopago,
        DateTime? fechaInicio, DateTime? fechaFinal)
        {
            if (idTicket != null)
            {
                TempData["ID"] = idTicket;
                return RedirectToAction("Conciliados", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta, canal = canal, referido = tiporeferido, tipopago = tipopago });
            }
            else
            {
                TempData["ID"] = null;
                return RedirectToAction("Conciliados", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal, sucursal = sucursal, cuenta = cuenta, canal = canal, referido = tiporeferido, tipopago = tipopago });
            }


        }

        public ActionResult AbrirTicket2(int id)
        {
            Tickets ticket = db.Tickets.Find(id);

            var bytesBinary = ticket.Ticket;
            TempData["ID"] = null;
            return File(bytesBinary, "application/pdf");
        }

        public ActionResult Deuda(int? id, string deuda)
        {
            var gestor = db.Referido.Find(id);

            gestor.Deuda = deuda != null ? deuda : null;

            if (ModelState.IsValid)
            {
                db.Entry(gestor).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("Pagos");
        }

        public ActionResult Efectivo(int? id, string efectivo, DateTime? fecha1, DateTime? fecha2)
        {
            var gestor = db.Referido.Find(id);

            gestor.Efectivo = efectivo != null ? efectivo : null;

            if (ModelState.IsValid)
            {
                db.Entry(gestor).State = EntityState.Modified;
                db.SaveChanges();
            }

            int episEquivalentes = 0;

            if(gestor.PrecioNormalconIVA != null)
            {
                episEquivalentes = Convert.ToInt32(efectivo) / Convert.ToInt32(gestor.PrecioNormalconIVA);
            }
            else
            {
                episEquivalentes = Convert.ToInt32(efectivo) / Convert.ToInt32(gestor.PrecioNormal);
            }

            var citasCXC = (from i in db.Cita where i.CanalTipo == gestor.Tipo && i.ReferidoPor == gestor.Nombre && i.TipoPago == "Pendiente de Pago" && i.FechaCita >= fecha1 && i.FechaCita < fecha2 select i).OrderBy(o => o.FechaCita).Take(episEquivalentes);

            foreach(var item in citasCXC)
            {
                var cita = db.Cita.Find(item.idCita);

                cita.Conciliado = DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString();
                cita.TipoPago = "Efectivo";
                cita.Cuenta = "EFECTIVO";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Redirect("Pagos");
        }

    }
}