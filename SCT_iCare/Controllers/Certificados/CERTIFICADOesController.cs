using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Certificados
{
    public class CERTIFICADOesController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: CERTIFICADOes
        public ActionResult Index()
        {
            return View(db.CERTIFICADO.ToList());
        }

        // GET: CERTIFICADOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICADO cERTIFICADO = db.CERTIFICADO.Find(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICADO);
        }

        // GET: CERTIFICADOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CERTIFICADOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCertificado,Nombre,Pasaporte,Respuesta,FechaNacimiento,FechaToma,Sexo")] CERTIFICADO cERTIFICADO)
        {
            if (ModelState.IsValid)
            {
                db.CERTIFICADO.Add(cERTIFICADO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cERTIFICADO);
        }

        // GET: CERTIFICADOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICADO cERTIFICADO = db.CERTIFICADO.Find(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICADO);
        }

        // POST: CERTIFICADOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCertificado,Nombre,Pasaporte,Respuesta,FechaNacimiento,FechaToma,Sexo")] CERTIFICADO cERTIFICADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cERTIFICADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cERTIFICADO);
        }

        // GET: CERTIFICADOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICADO cERTIFICADO = db.CERTIFICADO.Find(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICADO);
        }

        // POST: CERTIFICADOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CERTIFICADO cERTIFICADO = db.CERTIFICADO.Find(id);
            db.CERTIFICADO.Remove(cERTIFICADO);
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
