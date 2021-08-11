using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.Admin
{
    public class AdminController : Controller
    {
        GMIEntities db = new GMIEntities();

        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }
    }
}