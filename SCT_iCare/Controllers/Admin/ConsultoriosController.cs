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
    public class ConsultoriosController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Consultorios.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultorios consultorios = db.Consultorios.Find(id);
            if (consultorios == null)
            {
                return HttpNotFound();
            }
            return View(consultorios);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,NombreDoctor,RFC,CURP,Cedula,Direccion,Colonia,CP,Estado,Ciudad,Consultorio,NoConsultorio")] Consultorios consultorios)
        {
            if (ModelState.IsValid)
            {
                db.Consultorios.Add(consultorios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consultorios);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultorios consultorios = db.Consultorios.Find(id);
            if (consultorios == null)
            {
                return HttpNotFound();
            }
            return View(consultorios);
        }

        // POST: Admin/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,NombreDoctor,RFC,CURP,Cedula,Direccion,Colonia,CP,Estado,Ciudad,Consultorio,NoConsultorio")] Consultorios consultorios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultorios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consultorios);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultorios consultorios = db.Consultorios.Find(id);
            if (consultorios == null)
            {
                return HttpNotFound();
            }
            return View(consultorios);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultorios consultorios = db.Consultorios.Find(id);
            db.Consultorios.Remove(consultorios);
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
