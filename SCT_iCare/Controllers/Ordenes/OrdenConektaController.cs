using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Ordenes
{
    public class OrdenConektaController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: OrdenConektas
        public ActionResult Index()
        {
            return View(db.OrdenConekta.ToList());
        }

        // GET: OrdenConektas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenConekta ordenConekta = db.OrdenConekta.Find(id);
            if (ordenConekta == null)
            {
                return HttpNotFound();
            }
            return View(ordenConekta);
        }

        // GET: OrdenConektas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdenConektas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOrden,Nombre,Email,Producto,FechaCreacion,Descripcion,Concepto,FechaCita,MetodoPago,idCargo,Estatus,FechaActualizacion,Total,idOrdenConekta,ReferenciaOxxo,LinkPago,CheckoutRequestName,idConsultorio,Asignacion")] OrdenConekta ordenConekta)
        {
            if (ModelState.IsValid)
            {
                db.OrdenConekta.Add(ordenConekta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ordenConekta);
        }

        // GET: OrdenConektas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenConekta ordenConekta = db.OrdenConekta.Find(id);
            if (ordenConekta == null)
            {
                return HttpNotFound();
            }
            return View(ordenConekta);
        }

        // POST: OrdenConektas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOrden,Nombre,Email,Producto,FechaCreacion,Descripcion,Concepto,FechaCita,MetodoPago,idCargo,Estatus,FechaActualizacion,Total,idOrdenConekta,ReferenciaOxxo,LinkPago,CheckoutRequestName,idConsultorio,Asignacion")] OrdenConekta ordenConekta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenConekta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ordenConekta);
        }

        // GET: OrdenConektas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenConekta ordenConekta = db.OrdenConekta.Find(id);
            if (ordenConekta == null)
            {
                return HttpNotFound();
            }
            return View(ordenConekta);
        }

        // POST: OrdenConektas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdenConekta ordenConekta = db.OrdenConekta.Find(id);
            db.OrdenConekta.Remove(ordenConekta);
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

        public ActionResult Asignadas()
        {
            var asignadas = from a in db.OrdenConekta where a.Asignacion == "Asignado" select a;
            return View(asignadas);
        }

        public ActionResult Pendiente()
        {
            var pendiente = from a in db.OrdenConekta where a.Asignacion == "No" select a;
            return View(pendiente);
        }

        public ActionResult Terminado()
        {
            var terminado = from a in db.OrdenConekta where a.Asignacion == "Terminado" select a;
            return View(terminado);
        }
    }
}
