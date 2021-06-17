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

namespace SCT_iCare.Controllers.EPICenter
{
    public class EPIsController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: EPIs
        public ActionResult Index(int? pageSize, int? page)
        {

            return View(/*db.EPI.ToList()*/);
        }

        // GET: EPIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //EPI ePI = db.EPI.Find(id);
            var ePI = (from e in db.EPI where e.idEPI == id select e).Select(i => new { i.Capturista, i.Dictamen, i.Doctor, i.Estatus, i.FechaExpediente, i.FinalCaptura, i.idEPI, i.InicioCaptura, i.NoFolio, i.NombrePaciente, i.Sucursal, i.TipoEPI, i.Usuario }).FirstOrDefault();
            if (ePI == null)
            {
                return HttpNotFound();
            }
            return View(ePI);
        }

        // GET: EPIs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EPIs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, string paciente, string folio, string doctor, string tipo, string sucursal, string usuario/*[Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI*/)
        {
            EPI epi = new EPI();

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

            epi.BytesArchivo = bytes2;
            epi.NombrePaciente = paciente;
            epi.NoFolio = Convert.ToInt32(folio);
            epi.TipoEPI = tipo;
            epi.Estatus = "No iniciado";
            epi.FechaExpediente = DateTime.Now;
            epi.Doctor = doctor;
            epi.Sucursal = sucursal;
            epi.Usuario = usuario;


            if (ModelState.IsValid)
            {
                db.EPI.Add(epi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(epi);
        }

        // GET: EPIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPI ePI = db.EPI.Find(id);
            if (ePI == null)
            {
                return HttpNotFound();
            }
            TempData["Modal"] = "existe";
            return RedirectToAction("Index");
        }

        // POST: EPIs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ePI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ePI);
        }

        public ActionResult EditExpediente(int? id, string usuario /*[Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI*/)
        {
                //DescargarPDF(id);
                EPI ePI = db.EPI.Find(id);

                ePI.Estatus = "En captura...";
                ePI.InicioCaptura = DateTime.Now;
                ePI.Capturista = usuario;



                //if(true)
                //{
                var bytesBinary = (from b in db.EPI where b.idEPI == id select b.BytesArchivo).FirstOrDefault();
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
            //    Response.BinaryWrite(bytesBinary);
            //    //Response.End();
            //HttpContext.ApplicationInstance.CompleteRequest();


            //}

                if (ModelState.IsValid)
                {
                    db.Entry(ePI).State = EntityState.Modified;
                    db.SaveChanges();
                //return Redirect("~/EPIs/capturaSucursal?sucursal=" + ePI.Sucursal + "");
                return File(bytesBinary, "application/pdf");
            }
                return View(ePI);

        }

        public ActionResult SubirDictamen(int? id /*[Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI*/)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPI ePI = db.EPI.Find(id);
            if (ePI == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = id;
            ViewBag.Sucursal = ePI.Sucursal;
            return View(ePI);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dictaminar(int id, HttpPostedFileBase file)
        {
            EPI epi = db.EPI.Find(id);

            byte[] bytes2 = null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);

                    bytes2 = bytes;
                }
            }


            epi.Dictamen = bytes2;
            epi.Estatus = "Terminado";
            epi.FinalCaptura = DateTime.Now;


            if (ModelState.IsValid)
            {
                db.Entry(epi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = epi.Sucursal} );
            }

            return View(epi);
        }

        public ActionResult DescargarPDF(int? id)
        {
            EPI ePI = db.EPI.Find(id);

            var bytesBinary = ePI.Dictamen;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
            Response.BinaryWrite(bytesBinary);
            Response.End();

            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
            //    Response.BinaryWrite(bytesBinary);
            //    //Response.End();
            //HttpContext.ApplicationInstance.CompleteRequest();

            return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = ePI.Sucursal.ToString() });
        }

        // GET: EPIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EPI ePI = db.EPI.Find(id);
            if (ePI == null)
            {
                return HttpNotFound();
            }
            return View(ePI);
        }

        // POST: EPIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EPI ePI = db.EPI.Find(id);
            db.EPI.Remove(ePI);
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

        public ActionResult Captura()
        {
            ViewBag.Parameter = "";
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Captura(string parameter)
        //{
        //    ViewBag.Parameter = parameter;
        //    return View();
        //}

        public ActionResult capturaSucursal(string sucursal)
        {
            ViewBag.Sucursal = sucursal;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pendiente(int id, string paciente, string folio, string capturista, string incidencia)
        {
            EPI epi = db.EPI.Find(id);
            Incidencias incidencias = new Incidencias();

            epi.Estatus = "Pendiente";
            epi.FinalCaptura = DateTime.Now;

            incidencias.NombrePaciente = paciente;
            incidencias.Expediente = folio;
            incidencias.Capturista = capturista;
            incidencias.Incidencia = incidencia;
            incidencias.FechaIncidencia = DateTime.Now;
            incidencias.idEPI = id;


            if (ModelState.IsValid)
            {
                db.Entry(epi).State = EntityState.Modified;
                db.Incidencias.Add(incidencias);
                db.SaveChanges();
                return RedirectToAction("Captura", "EPIs");
            }

            return RedirectToAction("Captura", "EPIs");
        }
    }
}
