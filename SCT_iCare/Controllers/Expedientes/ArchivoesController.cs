using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Expedientes
{
    public class ArchivoesController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: Archivoes
        public ActionResult Index()
        {
            return View(db.Archivo.ToList());
        }

        // GET: Archivoes/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Archivo archivo = db.Archivo.Find(id);
            //if (archivo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(archivo);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Expediente expediente = db.Expediente.Find(id);
            //if (expediente == null)
            //{
            //    return HttpNotFound();
            //}

            var archivo = (from a in db.Archivo where a.idArchivo == id select a.Archivo1).FirstOrDefault();
            var nombre = (from a in db.Archivo where a.idArchivo == id select a.NombreArchivo).FirstOrDefault();
            byte[] bytes = archivo;
            string nombreArchivo = nombre;

            var bytesBinary = bytes;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename="+nombre);
            Response.BinaryWrite(bytesBinary);
            Response.End();

            return RedirectToAction("Index");
        }

        // GET: Archivoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Archivoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "idArchivo,Archivo1,NombreArchivo")] Archivo archivo*/HttpPostedFileBase file)
        {
            Archivo archivo = new Archivo();

            byte[] bytes2 = null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }

                bytes2 = bytes;

                //var bytesBinary = bytes;
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=MyPDF.pdf");
                //Response.BinaryWrite(bytesBinary);
                //Response.End();
            }

            archivo.Archivo1 = bytes2;
            archivo.NombreArchivo = file.FileName;

            if (ModelState.IsValid)
            {
                db.Archivo.Add(archivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(archivo);
        }



        // GET: Archivoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = db.Archivo.Find(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // POST: Archivoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idArchivo,Archivo1,NombreArchivo")] Archivo archivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(archivo);
        }

        // GET: Archivoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = db.Archivo.Find(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // POST: Archivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Archivo archivo = db.Archivo.Find(id);
            db.Archivo.Remove(archivo);
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
