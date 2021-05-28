using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;
using SCT_iCare.Models;

namespace SCT_iCare.Controllers.SubirEPI
{
    public class CheckupEPIController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: CheckupEPI
        public ActionResult Index()
        {
            var checkupEPI = db.Expediente.Include(c => c.Doctores).Include(c => c.Sucursales).Include(c => c.Usuarios);
            return View(checkupEPI.ToList());
        }

        // GET: CheckupEPI/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Expediente expediente = db.Expediente.Find(id);
            //if (expediente == null)
            //{
            //    return HttpNotFound();
            //}

            var archivo = (from a in db.Archivo where a.idArchivo == id select a.Archivo1).FirstOrDefault();
            byte[] bytes = archivo;

            var bytesBinary = bytes;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=MyPDF.pdf");
            Response.BinaryWrite(bytesBinary);
            Response.End();

            return RedirectToAction("Index");
        }

        // GET: CheckupEPI/Create
        public ActionResult Create()
        {
            ViewBag.idDoctor = new SelectList(db.Doctores, "idDoctor", "Nombre");
            ViewBag.idSucursal = new SelectList(db.Sucursales, "idSucursal", "Nombre");
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "Nombre");
            return View();
        }

        // POST: CheckupEPI/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEPI,NombreEPI,Archivo,NoFolio,Tipo,Estatus,Fecha,idDoctor,idSucursal,idUsuario")] Expediente expediente)
        {
            SubirPDF subir = new SubirPDF();

            byte[] archivo = null;

            //if (file != null && file.ContentLength > 0)
            //{
            //    var fileName = Path.GetFileName(file.FileName);

            //    byte[] bytes;
            //    using (BinaryReader br = new BinaryReader(file.InputStream))
            //    {
            //        bytes = br.ReadBytes(file.ContentLength);
            //        archivo = bytes;
            //    }

            //    //var bytesBinary = bytes;
            //    //Response.ContentType = "application/pdf";
            //    //Response.AddHeader("content-disposition", "attachment;filename=MyPDF.pdf");
            //    //Response.BinaryWrite(bytesBinary);
            //    //Response.End();
            //}

            expediente.Estatus = "No iniciado";
            expediente.Fecha = DateTime.Now;
            //expediente.Archivo = archivo;
            //checkupEPI.NombreArchivo = file.ToString();



            if (ModelState.IsValid)
            {
                db.Expediente.Add(expediente);
                db.SaveChanges();
                return RedirectToAction("CheckupEPI");
            }

            //if (checkupEPI.NombreArchivo != null)
            //{
            //    HttpPostedFileBase file = checkupEPI.NombreArchivo;
            //    string rute = Server.MapPath("~/Temp/");
            //    //rute += file.FileName;
            //    subir.SubirArchivo(rute, file);
            //    ViewBag.Error = subir.error;
            //    ViewBag.Correcto = subir.confirmacion;
            //}

            ViewBag.idDoctor = new SelectList(db.Doctores, "idDoctor", "Nombre", expediente.idDoctor);
            ViewBag.idSucursal = new SelectList(db.Sucursales, "idSucursal", "Nombre", expediente.idSucursal);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "Nombre", expediente.idUsuario);
            return RedirectToAction("CheckupEPI");
        }

        public ActionResult CheckupEPI()
        {
            return View();
        }

        public ActionResult SubirArchivo(HttpPostedFileBase file)
        {
            SubirPDF subir = new SubirPDF();
            byte[] bytes2 = null;

            if(file != null && file.ContentLength > 0)
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



            //if (file != null)
            //{
            //    //C:\Inetpub\vhosts\medicinagmi.mx\devsct.medicinagmi.mx\Temp
            //    //string rute = Server.MapPath("~/Temp");
            //    //string fullrute = Path.Combine(rute, file.FileName); //file.FileName;
            //    //subir.SubirArchivo(/*rute*/fullrute, file);

            //    //file.SaveAs(fullrute);

            //    var fileName = Path.GetFileName(file.FileName);

            //    byte[] bytes;
            //    using (BinaryReader br = new BinaryReader(file.InputStream))
            //    {
            //        bytes = br.ReadBytes(file.ContentLength);
            //    }

            //    TempData["Bytes"] = bytes;

            //    ViewBag.Error = subir.error;
            //    ViewBag.Correcto = subir.confirmacion;
            //}
            TempData["Bytes"] = bytes2;
            TempData["Archivo"] = file.FileName;
            
            return Redirect("~/CheckupEPI/Create");
        }

        // GET: CheckupEPI/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expediente checkupEPI = db.Expediente.Find(id);
            if (checkupEPI == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDoctor = new SelectList(db.Doctores, "idDoctor", "Nombre", checkupEPI.idDoctor);
            ViewBag.idSucursal = new SelectList(db.Sucursales, "idSucursal", "Nombre", checkupEPI.idSucursal);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "Nombre", checkupEPI.idUsuario);
            return View(checkupEPI);
        }

        // POST: CheckupEPI/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEPI,NombreEPI,NombreArchivo,NoFolio,Tipo,Estatus,Fecha,idDoctor,idSucursal,idUsuario")] Expediente checkupEPI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkupEPI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDoctor = new SelectList(db.Doctores, "idDoctor", "Nombre", checkupEPI.idDoctor);
            ViewBag.idSucursal = new SelectList(db.Sucursales, "idSucursal", "Nombre", checkupEPI.idSucursal);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "Nombre", checkupEPI.idUsuario);
            return View(checkupEPI);
        }

        // GET: CheckupEPI/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expediente checkupEPI = db.Expediente.Find(id);
            if (checkupEPI == null)
            {
                return HttpNotFound();
            }
            return View(checkupEPI);
        }

        // POST: CheckupEPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expediente checkupEPI = db.Expediente.Find(id);
            db.Expediente.Remove(checkupEPI);
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
