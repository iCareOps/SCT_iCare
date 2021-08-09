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
        public ActionResult Estatus(string curp, string telefono, string numero, int idGestor)
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

            cita = (from c in db.Cita where c.NoExpediente == numero && c.Doctor != null select c).FirstOrDefault();

            
            
            string usuario = (from u in db.log_InicioGestor where u.idLogInicioGestor == idGestor select u.NombreUsuario).FirstOrDefault();

            var movimiento = new log_Movimientos();


            if (paciente != null && cita != null)
            { 
                movimiento.Movimiento = "El usuario " + usuario + " buscó el dictamen de " + paciente.Nombre + " con número de expediente " +cita.NoExpediente+ " con fecha " +Convert.ToDateTime(cita.FechaCita).ToString("dd-MMMM-yyyy");
                movimiento.idLogInicioGestor = idGestor;

                if (ModelState.IsValid)
                {
                    db.log_Movimientos.Add(movimiento);
                    db.SaveChanges();
                }

                ViewBag.idPaciente = paciente.idPaciente;
                ViewBag.Expediente = cita.NoExpediente;
                TempData["ID"] = idGestor;
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
                movimiento.Movimiento = "El usuario " + usuario + " ingresó el expediente " +numero+ " y el CURP " +curp+ " sin éxito";
                movimiento.idLogInicioGestor = idGestor;

                if (ModelState.IsValid)
                {
                    db.log_Movimientos.Add(movimiento);
                    db.SaveChanges();
                }

                TempData["Estatus"] = "No";
                TempData["ID"] = idGestor;
                return Redirect("Inicio");
            }
        }

        public ActionResult DescargarDictamen(int? id, int idGestor)
        {
            Captura captura = db.Captura.Find(id);
            Dictamen dictamen = new Dictamen();

            string usuario = (from u in db.log_InicioGestor where u.idLogInicioGestor == idGestor select u.NombreUsuario).FirstOrDefault();
            var movimiento = new log_Movimientos();

            movimiento.Movimiento = "El usuario " + usuario + " descargó el dictamen de " +captura.NombrePaciente;
            movimiento.idLogInicioGestor = idGestor;

            if (ModelState.IsValid)
            {
                db.log_Movimientos.Add(movimiento);
                db.SaveChanges();
            }

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