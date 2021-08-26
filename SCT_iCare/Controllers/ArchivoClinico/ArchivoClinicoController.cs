using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.ArchivoClinico
{
    public class ArchivoClinicoController : Controller
    {
        GMIEntities db = new GMIEntities();

        public ActionResult Index(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if(revision.InicioCarrusel == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.InicioCarrusel = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        
        public ActionResult Recepcion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SIGNOS_VITALES(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.SignosVitales == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.SignosVitales = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult AUDIOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Audiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Audiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult OFTALMOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Oftalmologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult ODONTOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Odontologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult HISTORIA_CLÍNICA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.HistoriaClinca == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.HistoriaClinca = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult CARDIOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Cardiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Cardiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult LABORATORIO(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Laboratorio == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Laboratorio = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return View();
        }

        //Guardar módulos--------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Guardar_SignosVitales(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_SignosVitales == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_SignosVitales = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Audiologia(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Audiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Audiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Oftalmologia(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Oftalmologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Odontologia(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Odontologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_HistoriaClinica(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_HistoriaClinica == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_HistoriaClinica = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Cardiologia(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Cardiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Cardiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Laboratorio(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Laboratorio == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Laboratorio = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }
    }
}