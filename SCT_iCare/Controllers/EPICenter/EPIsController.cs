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

        public ActionResult CentroControl()
        {
            ViewBag.Date = DateTime.Now.ToString("dd-MMMM-yyyy");

            return View(db.Paciente.ToList());
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
            Captura captura = db.Captura.Find(id);

            captura.EstatusCaptura = "En captura...";
            captura.InicioCaptura = DateTime.Now;
            captura.Capturista = usuario;

            var expediente = (from e in db.Expedientes where e.idPaciente == captura.idPaciente select e).FirstOrDefault();

            var bytesBinary = expediente.Expediente;

            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.SaveChanges();                
            }
            return File(bytesBinary, "application/pdf");
            //return View(captura);


        }

        public ActionResult SubirDictamen(int? id /*[Bind(Include = "idEPI,NombrePaciente,BytesArchivo,NoFolio,TipoEPI,Estatus,FechaExpediente,InicioCaptura,FinalCaptura,Doctor,Sucursal,Usuario,Dictamen")] EPI ePI*/)
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
            ViewBag.ID = id;
            ViewBag.Sucursal = captura.Sucursal;
            return View(captura);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dictaminar(int id, HttpPostedFileBase file)
        {
            //EPI epi = db.EPI.Find(id);
            Captura captura = db.Captura.Find(id);
            Dictamen dictamen = new Dictamen();

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

            captura.EstatusCaptura = "Terminado";
            captura.FinalCaptura = DateTime.Now;
            dictamen.Dictamen1 = bytes2;
            dictamen.idPaciente = captura.idPaciente;
            dictamen.idAptitud = 7;


            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.Dictamen.Add(dictamen);
                db.SaveChanges();
                return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = captura.Sucursal} );
            }

            return View(captura);
        }

        public ActionResult DescargarPDF(int? id)
        {
            Captura captura = db.Captura.Find(id);
            Dictamen dictamen = new Dictamen();

            var documento = (from d in db.Dictamen where captura.idPaciente == d.idPaciente select d.Dictamen1).FirstOrDefault();

            var bytesBinary = documento;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + captura.NombrePaciente + ".pdf");
            Response.BinaryWrite(bytesBinary);
            Response.End();

            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
            //    Response.BinaryWrite(bytesBinary);
            //    //Response.End();
            //HttpContext.ApplicationInstance.CompleteRequest();

            return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = captura.Sucursal.ToString() });
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
        public ActionResult Pendiente(int id, string paciente, string folio, string capturista, string incidencia, string falla)
        {
            Captura captura = db.Captura.Find(id);
            Incidencias incidencias = new Incidencias();

            Dictamen dictamen = new Dictamen();

            dictamen.idPaciente = captura.idPaciente;
            dictamen.idAptitud = Convert.ToInt32(falla);

            captura.EstatusCaptura = "Pendiente";
            captura.FinalCaptura = DateTime.Now;
            

            incidencias.NombrePaciente = paciente;
            incidencias.Expediente = folio;
            incidencias.Capturista = capturista;
            incidencias.Incidencia = incidencia;
            incidencias.FechaIncidencia = DateTime.Now;
            incidencias.idEPI = id;


            if (ModelState.IsValid)
            {
                db.Entry(captura).State = EntityState.Modified;
                db.Incidencias.Add(incidencias);
                db.Dictamen.Add(dictamen);
                db.SaveChanges();
                return RedirectToAction("Captura", "EPIs");
            }

            return RedirectToAction("Captura", "EPIs");
        }


        public JsonResult EjemploJSON()
        {
            List<Paciente> data = db.Paciente.ToList();

            var selected = data.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m }).Select( S => new { S.N.Nombre, S.N.Telefono, S.M.FechaCita, S.M.Referencia });
            var modelo = db.Paciente.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m });

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        public JsonResult General()
        {
            List<Paciente> data = db.Paciente.ToList();
            var selected = data.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m }).Select(S => new { S.N.Nombre, S.N.Telefono, S.M.FechaCita, S.M.Referencia });

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actuales(string palabra)
        {
            List<Paciente> data = db.Paciente.ToList();
            var selected = data.Join(db.Cita, n => n.Folio, m => m.Folio, (n, m) => new { N = n, M = m }).Where(r => r.N.Nombre == palabra).Select(S => new { S.N.Nombre, S.N.Telefono, S.M.FechaCita, S.M.Referencia });

            return Json(selected, JsonRequestBehavior.AllowGet);
        }

    }
}
