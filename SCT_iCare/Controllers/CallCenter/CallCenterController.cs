using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.CallCenter
{
    public class CallCenterController : Controller
    {
        // GET: CallCenter
        public ActionResult Index()
        {
            return View();
        }

        // GET: CallCenter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CallCenter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallCenter/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CallCenter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CallCenter/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CallCenter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CallCenter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
