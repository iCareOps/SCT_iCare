using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SCT_iCare.Controllers.Recepcion
{
    public class RecepcionistaController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();
        // GET: Recepcionista
        public ActionResult Index()
        {
            var solicitudes = db.Solicitudes.Include(s => s.Comision).Include(s => s.Consultorios).Include(s => s.EstatusSolicitud).Include(s => s.MetodoPago).Include(s => s.Productos).Include(s => s.TipoTarjeta).Include(s => s.Usuarios);
            return View(solicitudes.ToList());
        }
    }
}