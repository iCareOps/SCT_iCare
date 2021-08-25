using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.ArchivoClinico
{
    public class ArchivoClinicoController : Controller
    {
        GMIEntities db = new GMIEntities();

        // GET: ArchivoClinico
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Recepcion()
        {
            return View();
        }
    }
}