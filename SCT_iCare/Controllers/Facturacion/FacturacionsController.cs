using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Facturacion
{
    public class FacturacionsController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: Facturacions
        public ActionResult Index()
        {
            var facturacion = db.Facturacion.Include(f => f.EstatusFactura);
            return View(facturacion.ToList());
        }

        // GET: Facturacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCT_iCare.Facturacion facturacion = db.Facturacion.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // GET: Facturacions/Create
        public ActionResult Create()
        {
            ViewBag.idEstatusFactura = new SelectList(db.EstatusFactura, "idEstatusFactura", "Estatus");
            return View();
        }

        // POST: Facturacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFactura,RazonSocial,RFC,Fecha,Nombre,FormaPago,MontoPagado,Email,idEstatusFactura")] SCT_iCare.Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Facturacion.Add(facturacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEstatusFactura = new SelectList(db.EstatusFactura, "idEstatusFactura", "Estatus", facturacion.idEstatusFactura);
            return View(facturacion);
        }

        // GET: Facturacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCT_iCare.Facturacion facturacion = db.Facturacion.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEstatusFactura = new SelectList(db.EstatusFactura, "idEstatusFactura", "Estatus", facturacion.idEstatusFactura);
            return View(facturacion);
        }

        // POST: Facturacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFactura,RazonSocial,RFC,Fecha,Nombre,FormaPago,MontoPagado,Email,idEstatusFactura")] SCT_iCare.Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstatusFactura = new SelectList(db.EstatusFactura, "idEstatusFactura", "Estatus", facturacion.idEstatusFactura);
            return View(facturacion);
        }

        // GET: Facturacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCT_iCare.Facturacion facturacion = db.Facturacion.Find(id);
            if (facturacion == null)
            {
                return HttpNotFound();
            }
            return View(facturacion);
        }

        // POST: Facturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SCT_iCare.Facturacion facturacion = db.Facturacion.Find(id);
            db.Facturacion.Remove(facturacion);
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
