using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

using System.IO;


namespace SCT_iCare.Controllers.EPICenter
{
    public class EPICenterController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: Capturas
        public ActionResult Index()
        {
            var captura = db.Captura.Include(c => c.Paciente);
            return View(captura.ToList());
        }

        // GET: Capturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Captura captura = db.Captura.Find(id);
            if (captura == null)
            {
                return HttpNotFound();
            }
            return View(captura);
        }

        // GET: Capturas/Create
        public ActionResult Create()
        {
            ViewBag.idPaciente = new SelectList(db.Paciente, "idPaciente", "Nombre");
            return View();
        }

        // POST: Capturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(HttpPostedFileBase file, string paciente, string folio, string doctor, string tipo, string sucursal, string usuario)
        //{
        //    SCT_iCare.Expedientes epi = new SCT_iCare.Expedientes();
        //    SCT_iCare.Captura cap = new SCT_iCare.Captura();

        //    byte[] bytes2 = null;

        //    if (file != null && file.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);

        //        byte[] bytes;
        //        using (BinaryReader br = new BinaryReader(file.InputStream))
        //        {
        //            bytes = br.ReadBytes(file.ContentLength);
        //        }

        //////        bytes2 = bytes;
        //    }

        //    epi.BytesArchivo = bytes2;
        //    epi.NombrePaciente = paciente;
        //    epi.NoFolio = Convert.ToInt32(folio);
        //    epi.TipoEPI = tipo;
        //    epi.Estatus = "No iniciado";
        //    epi.FechaExpediente = DateTime.Now;
        //    epi.Doctor = doctor;
        //    epi.Sucursal = sucursal;
        //    epi.Usuario = usuario;


        //    if (ModelState.IsValid)
        //    {
        //        db.EPI.Add(epi);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(epi);
        //}

        // GET: Capturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Captura captura = db.Captura.Find(id);
            if (captura == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaciente = new SelectList(db.Paciente, "idPaciente", "Nombre", captura.idPaciente);
            return View(captura);
        }

        // POST: Capturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCaptura,NombrePaciente,NoExpediente,TipoTramite,EstatusCaptura,InicioCaptura,FinalCaptura,Doctor,Sucursal,Capturista,idPaciente")] Captura captura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaciente = new SelectList(db.Paciente, "idPaciente", "Nombre", captura.idPaciente);
            return View(captura);
        }

        // GET: Capturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Captura captura = db.Captura.Find(id);
            if (captura == null)
            {
                return HttpNotFound();
            }
            return View(captura);
        }

        // POST: Capturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Captura captura = db.Captura.Find(id);
            db.Captura.Remove(captura);
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
