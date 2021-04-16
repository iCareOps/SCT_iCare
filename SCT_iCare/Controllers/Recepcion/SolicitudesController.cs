using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;
using conekta;
using System.Configuration;
using Newtonsoft.Json;

namespace SCT_iCare.Controllers.Recepcion
{
    public class SolicitudesController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        public static void GetApiKey()
        {
            conekta.Api.apiKey = ConfigurationManager.AppSettings["conekta"];
            conekta.Api.version = "2.0.0";
            conekta.Api.locale = "es";
        }

        // GET: Solicitudes
        public ActionResult Index()
        {
            var solicitudes = db.Solicitudes.Include(s => s.Comision).Include(s => s.Consultorios).Include(s => s.EstatusSolicitud).Include(s => s.MetodoPago).Include(s => s.Productos).Include(s => s.TipoTarjeta).Include(s => s.Usuarios);
            return View(solicitudes.ToList());
        }

        // GET: Solicitudes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }

            return View(solicitudes);
        }

        // GET: Solicitudes/Create
        public ActionResult Create()
        {
            ViewBag.idComision = new SelectList(db.Comision, "idComision", "idComision");
            ViewBag.idConsultorio = new SelectList(db.Consultorios, "id", "Consultorio");
            ViewBag.idEstatusSolicitud = new SelectList(db.EstatusSolicitud, "idEstatusSolicitud", "Estatus");
            ViewBag.idMetodoPago = new SelectList(db.MetodoPago, "idMetodoPago", "MetodoPago1");
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "NombreProducto");
            ViewBag.idTipoTarjeta = new SelectList(db.TipoTarjeta, "idTipoTarjeta", "TipoTarjeta1");
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "Nombre");
            return View();
        }

        // POST: Solicitudes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSolicitud,Nombre,idProducto,Email,Telefono,idConsultorio,FechaSolicitud,idMetodoPago,ReferenciaOXXO,ReferenciaSPEI,NoOrden,idEstatusSolicitud,TerminacionTarjeta,idTipoTarjeta,CodigoAutorizacion,Subtotal,idComision,Total,idUsuario")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                solicitudes.FechaSolicitud = DateTime.Now;
                ////string usuario = Convert.ToString((Usuarios)HttpContext.Session["User"]);
                //var usuario = Convert.ToString((Usuarios)HttpContext.Session["User"]);


                //solicitudes.idUsuario = Convert.ToInt32((from d in db.Solicitudes where d.Nombre == usuario.ToString() select d.idUsuario));

                if (solicitudes.idMetodoPago == 1)
                {
                    solicitudes.idComision = 1;
                    solicitudes.Subtotal = 2400;
                    solicitudes.Total = 2400 - 98;
                    //solicitudes.idEstatusSolicitud = 1;
                }
                else if (solicitudes.idMetodoPago == 2)
                {
                    solicitudes.idComision = 2;
                    solicitudes.Subtotal = 2400;
                    solicitudes.Total = 2400 - 73;
                    //solicitudes.idEstatusSolicitud = 1;
                }

                db.Solicitudes.Add(solicitudes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idComision = new SelectList(db.Comision, "idComision", "idComision", solicitudes.idComision);
            ViewBag.idConsultorio = new SelectList(db.Consultorios, "id", "Consultorio", solicitudes.idConsultorio);
            ViewBag.idEstatusSolicitud = new SelectList(db.EstatusSolicitud, "idEstatusSolicitud", "Estatus", solicitudes.idEstatusSolicitud);
            ViewBag.idMetodoPago = new SelectList(db.MetodoPago, "idMetodoPago", "MetodoPago1", solicitudes.idMetodoPago);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "NombreProducto", solicitudes.idProducto);
            ViewBag.idTipoTarjeta = new SelectList(db.TipoTarjeta, "idTipoTarjeta", "TipoTarjeta1", solicitudes.idTipoTarjeta);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "Nombre", solicitudes.idUsuario);
            return View(solicitudes);
        }

        // GET: Solicitudes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idComision = new SelectList(db.Comision, "idComision", "idComision", solicitudes.idComision);
            ViewBag.idConsultorio = new SelectList(db.Consultorios, "id", "Consultorio", solicitudes.idConsultorio);
            ViewBag.idEstatusSolicitud = new SelectList(db.EstatusSolicitud, "idEstatusSolicitud", "Estatus", solicitudes.idEstatusSolicitud);
            ViewBag.idMetodoPago = new SelectList(db.MetodoPago, "idMetodoPago", "MetodoPago1", solicitudes.idMetodoPago);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "NombreProducto", solicitudes.idProducto);
            ViewBag.idTipoTarjeta = new SelectList(db.TipoTarjeta, "idTipoTarjeta", "TipoTarjeta1", solicitudes.idTipoTarjeta);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "Nombre", solicitudes.idUsuario);
            return View(solicitudes);
        }

        // POST: Solicitudes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSolicitud,Nombre,idProducto,Email,Telefono,idConsultorio,FechaSolicitud,idMetodoPago,ReferenciaOXXO,ReferenciaSPEI,NoOrden,idEstatusSolicitud,TerminacionTarjeta,idTipoTarjeta,CodigoAutorizacion,Subtotal,idComision,Total,idUsuario")] Solicitudes solicitudes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idComision = new SelectList(db.Comision, "idComision", "idComision", solicitudes.idComision);
            ViewBag.idConsultorio = new SelectList(db.Consultorios, "id", "Consultorio", solicitudes.idConsultorio);
            ViewBag.idEstatusSolicitud = new SelectList(db.EstatusSolicitud, "idEstatusSolicitud", "Estatus", solicitudes.idEstatusSolicitud);
            ViewBag.idMetodoPago = new SelectList(db.MetodoPago, "idMetodoPago", "MetodoPago1", solicitudes.idMetodoPago);
            ViewBag.idProducto = new SelectList(db.Productos, "idProducto", "NombreProducto", solicitudes.idProducto);
            ViewBag.idTipoTarjeta = new SelectList(db.TipoTarjeta, "idTipoTarjeta", "TipoTarjeta1", solicitudes.idTipoTarjeta);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "Nombre", solicitudes.idUsuario);
            return View(solicitudes);
        }

        // GET: Solicitudes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        // POST: Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            db.Solicitudes.Remove(solicitudes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
