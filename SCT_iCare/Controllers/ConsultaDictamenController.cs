using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers
{
    public class ConsultaDictamenController : Controller
    {
        GMIEntities db = new GMIEntities();

        // GET: ConsultaDictamen
        public ActionResult Inicio()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Estatus(string curp, string telefono, string numero)
        {
            var paciente = new Paciente();
            var cita = new Cita();

            if (telefono == "")
            {
                paciente = (from p in db.Paciente where p.CURP == curp.Trim().ToUpper() select p).FirstOrDefault();
            }
            else
            {
                paciente = (from p in db.Paciente where p.CURP == curp.Trim().ToUpper() && p.Telefono == telefono select p).FirstOrDefault();
            }

            cita = (from c in db.Cita where c.NoExpediente == numero select c).FirstOrDefault();

            if (paciente != null && cita != null)
            {
                ViewBag.idPaciente = paciente.idPaciente;
                ViewBag.Expediente = cita.NoExpediente;
                return View();
            }
            //else if(paciente != null && cita == null)
            //{
            //    ViewBag.idPaciente = paciente.idPaciente;
            //    ViewBag.Expediente = null;
            //    return View();
            //}
            else
            {
                TempData["Estatus"] = "No";
                return Redirect("Inicio");
            }
        }

        public ActionResult DescargarDictamen(int? id)
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

            return Redirect("Inicio");
        }

        public ActionResult Proceso(int id, string comentario)
        {
            IncidenciaPaciente incidenciaP = new IncidenciaPaciente();

            incidenciaP.Comentario = comentario;
            incidenciaP.idCaptura = id;

            if (ModelState.IsValid)
            {
                db.IncidenciaPaciente.Add(incidenciaP);
                db.SaveChanges();
                return View();
            }

            return View();
        }
    }
}