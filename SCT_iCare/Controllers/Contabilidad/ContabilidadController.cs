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
        public ActionResult Index(string canal, string cuenta, string sucursal, DateTime? fechaInicio, DateTime? fechaFinal, string tipoPago, int? referido, string conciliadooo, int? id)
        {//id nos indica si es 1 son Conciliados y si es 2 son NoConciliados
                var Referido = db.Referido.Find(referido);

            if (conciliadooo == "" || conciliadooo == null)
            {
                ViewBag.Conciliado = "";
            }
            else
            {
                ViewBag.Conciliado = conciliadooo;
            }

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

            if (id != null)
            {
                if (id == 1)
                {
                    ViewBag.Conciliados = true;
                }
                else if (id == 2)
                {
                    ViewBag.NoConciliados = true;

                }

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

        public ActionResult Pagos(string canal, DateTime? fechaInicio, DateTime? fechaFinal, string cuenta, string tipoPago, string sucursal, int? referido, int? id)
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


        public ActionResult CambiarCuenta(int? id, string cuenta, string cuenta2, string comentario, string usuario, string canal, string sucursal, 
            DateTime? fechaInicio, DateTime? fechaFinal, DateTime? fechaContable, string pago, int? idGestor, string usarSaldo, string FormadePago)
        {
            var cita = db.Cita.Find(id);

            string historico = cita.CuentaComentario == null ? "" : cita.CuentaComentario + "+";
            string cuentaAnterior = cita.Cuenta == null ? "" : " PROVIENE DE " + cita.Cuenta;
            cita.CuentaComentario = historico + comentario + cuentaAnterior + " " + DateTime.Today.ToString("dd-MM-yy") + " POR " + usuario;
            cita.Cuenta = cuenta;
            cita.FechaContable = fechaContable == null ? DateTime.Now : fechaContable;
            cita.UsarSaldo = usarSaldo;
            cita.FormaPago = FormadePago;

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
            string cuenta2, string canal, string pago, int? idGestor, string FormadePago)
        {
            var cita = db.PacienteESP.Find(id);

            string historico = cita.CuentaComentario == null ? "" : cita.CuentaComentario + "+"; 
            string cuentaAnterior = cita.Cuenta == null ? "" : " PROVIENE DE " + cita.Cuenta;
            cita.CuentaComentario = historico + comentario + cuentaAnterior + " " + DateTime.Today.ToString("dd-MM-yy") + " POR " + usuario;
            cita.Cuenta = cuenta;
            cita.FechaContable = fechaContable == null ? DateTime.Now : fechaContable;
            cita.FormaPago = FormadePago;

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
            return File(bytesBinary, "DownloadName.pdf");
            //return File(bytesBinary, "application/pdf");
            //return File(bytesBinary, "application/vnd.visio");


        }

        public ActionResult Deuda(int? id, string deuda, string canal)
        {
            var gestor = db.Referido.Find(id);

            gestor.Deuda = deuda != null ? deuda : null;

            //variables para filtro
            if (canal == "" || canal == null)
            {
                ViewBag.Canal = "";
            }
            else
            {
                ViewBag.Canal = canal;
            }



            if (ModelState.IsValid)
            {
                db.Entry(gestor).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("Pagos");
        }




        //METODO DE INGRESO DE PAGOS EN LA VISTA DE PAGOS
        public ActionResult EditarEfectivo(int? id, int efectivo, string usuario, DateTime fechaAhora, DateTime? fecha1, DateTime? fecha2)
        {

            ViewBag.FechaInicio = fecha1 != null ? fecha1 : null;
            ViewBag.FechaFinal = fecha2 != null ? fecha2 : null;

            DateTime fechaInicio = ViewBag.FechaInicio;
            DateTime fechaFinal = ViewBag.FechaFinal;

            Referido referido = db.Referido.Find(id);
            PagosGestores pagosGestores = new PagosGestores();

            int sumaEfectivos = 0;

            var pG = (from p in db.PagosGestores where p.idReferido == id select p);

            foreach (var pagosGes in pG)
            {

                sumaEfectivos += Convert.ToInt32(pagosGes.PagoIngresado);

            }

            sumaEfectivos = sumaEfectivos + efectivo;

            referido.Efectivo = Convert.ToString(sumaEfectivos);

            pagosGestores.Gestor = referido.Nombre;
            pagosGestores.idReferido = referido.idReferido;
            pagosGestores.PagoIngresado = Convert.ToString(efectivo);
            pagosGestores.Fecha = fechaAhora;

            if (ModelState.IsValid)
            {
                db.Entry(referido).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (ModelState.IsValid)
            {
                db.PagosGestores.Add(pagosGestores);
                db.SaveChanges();
            }
            
            return RedirectToAction("Pagos", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal});
        }



        //METODO PARA CONCILIAR CON EL INGRESO DE PAGOS EN LA VISTA DE PAGOS
        public ActionResult ConciliarIngresos(int? id, int efectivo, string usuario, DateTime fechaAhora, DateTime? fecha1, DateTime? fecha2)
        {

            ViewBag.FechaInicio = fecha1 != null ? fecha1 : null;
            ViewBag.FechaFinal = fecha2 != null ? fecha2 : null;

            DateTime fechaInicio = ViewBag.FechaInicio;
            DateTime fechaFinal = ViewBag.FechaFinal;

            Referido referido = db.Referido.Find(id);
            PagosGestores pagosGestores = new PagosGestores();

            int efectivoActual = Convert.ToInt32(referido.Efectivo);
            int sumaEfectivos = efectivoActual + efectivo;

            referido.Efectivo = Convert.ToString(sumaEfectivos);

            pagosGestores.Gestor = referido.Nombre;
            pagosGestores.idReferido = referido.idReferido;
            pagosGestores.PagoIngresado = Convert.ToString(efectivo);
            pagosGestores.Fecha = fechaAhora;

            if (ModelState.IsValid)
            {
                db.Entry(referido).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (ModelState.IsValid)
            {
                db.PagosGestores.Add(pagosGestores);
                db.SaveChanges();
            }

            return RedirectToAction("Pagos", new { fechaInicio = fechaInicio, fechaFinal = fechaFinal });
        }

    }
}
