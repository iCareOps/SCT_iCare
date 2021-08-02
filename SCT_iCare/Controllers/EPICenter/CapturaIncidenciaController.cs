using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.EPICenter
{
    public class CapturaIncidenciaController : Controller
    {
        GMIEntities db = new GMIEntities();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pausa(int idCaptura, string motivo)
        {
            CapturaIncidencia comentario = new CapturaIncidencia();
            Captura captura = db.Captura.Find(idCaptura);

            comentario.idCaptura = idCaptura;
            comentario.Comentario = motivo;
            comentario.PausaInicio = DateTime.Now;

            captura.EstatusCaptura = "Pausado";

            if (ModelState.IsValid)
            {
                db.CapturaIncidencia.Add(comentario);
                db.SaveChanges();
                return Redirect("Captura");
            }

            return Redirect("Captura");
        }
    }
}