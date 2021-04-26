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
            return RedirectToAction("Index", "Ordenes");
        }
    }
}