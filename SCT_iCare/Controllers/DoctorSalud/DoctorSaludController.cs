using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.DoctorModulo
{
    public class DoctorSaludController : Controller
    {
        GMIEntities db = new GMIEntities();

        // GET: DoctorSalud
        public ActionResult Index()
        {
            return View();
        }
    }
}