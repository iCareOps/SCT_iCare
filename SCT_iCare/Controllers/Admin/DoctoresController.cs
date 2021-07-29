using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Admin
{
    public class DoctoresController : Controller
    {
        private GMIEntities db = new GMIEntities();

        // GET: Doctores
        public ActionResult Index()
        {
            var doctores = db.Doctores.Include(d => d.Sucursales);
            return View(doctores.ToList());
        }

        // GET: Doctores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctores doctores = db.Doctores.Find(id);
            if (doctores == null)
            {
                return HttpNotFound();
            }
            return View(doctores);
        }

        // GET: Doctores/Create
        public ActionResult Create()
        {
            ViewBag.idSucursal = new SelectList(db.Sucursales, "idSucursal", "Nombre");
            return View();
        }

        // POST: Doctores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDoctor,Nombre,idSucursal")] Doctores doctores)
        {
            if (ModelState.IsValid)
            {
                db.Doctores.Add(doctores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idSucursal = new SelectList(db.Sucursales, "idSucursal", "Nombre", doctores.idSucursal);
            return View(doctores);
        }

        // GET: Doctores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctores doctores = db.Doctores.Find(id);
            if (doctores == null)
            {
                return HttpNotFound();
            }
            ViewBag.idSucursal = new SelectList(db.Sucursales, "idSucursal", "Nombre", doctores.idSucursal);
            return View(doctores);
        }

        // POST: Doctores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDoctor,Nombre,idSucursal")] Doctores doctores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSucursal = new SelectList(db.Sucursales, "idSucursal", "Nombre", doctores.idSucursal);
            return View(doctores);
        }

        // GET: Doctores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctores doctores = db.Doctores.Find(id);
            if (doctores == null)
            {
                return HttpNotFound();
            }
            return View(doctores);
        }

        // POST: Doctores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctores doctores = db.Doctores.Find(id);
            db.Doctores.Remove(doctores);
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
