using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Timers;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
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

        public ActionResult Dictamen(int id)
        {
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

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

            if (revision.Odontologia == null)
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

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return View();
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("ExploracionFisica", "ArchivoClinico", new { id = id });
            }

            ViewBag.idPaciente = id;
            return View();
        }

        [HttpPost]
        public ActionResult CARDIOLOGÍA(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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


        public ActionResult NoPatologicos(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return View();
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("ExploracionFisica", "ArchivoClinico", new { id = id });
            }

            ViewBag.idPaciente = id;
            return View();
        }

        public ActionResult Patologicos(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return View();
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("ExploracionFisica", "ArchivoClinico", new { id = id });
            }

            ViewBag.idPaciente = id;
            return View();
        }

        public ActionResult AparatosSistemas(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return View();
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("ExploracionFisica", "ArchivoClinico", new { id = id });
            }

            ViewBag.idPaciente = id;
            return View();
        }

        public ActionResult ExploracionFisica(int id)
        {
            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            //Inserción para verificación de Carrusel Médico
            var revisionCM = (from r in db.Cita where r.idPaciente == id select r).FirstOrDefault();
            if (revisionCM.CarruselMedico == null)
            {
                var cita = db.Cita.Find(revisionCM.idCita);
                cita.CarruselMedico = "SI";

                if (ModelState.IsValid)
                {
                    db.Entry(cita).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //-----------------------------------------------

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

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return View();
            }

            ViewBag.idPaciente = id;
            return View();
        }

        //Guardar módulos--------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Guardar_SignosVitales(int id, double sistolica, double diastolica, double cardiaca, double respiratoria, double temperatura, double peso, double estatura, double cintura, double cuello, double grasa, string sexo, string sangre)
        {
            EPI_SignosVitales sv = new EPI_SignosVitales();
            sv.idPaciente = id;
            sv.IMC = Math.Round(peso / (Math.Pow((estatura / 100), 2)), 2).ToString();
            sv.Sistolica = sistolica.ToString();
            sv.Diastolica = diastolica.ToString();
            sv.Cardiaca = cardiaca.ToString();
            sv.Respiratoria = respiratoria.ToString();
            sv.Temperatura = temperatura.ToString();
            sv.Peso = peso.ToString();
            sv.Estatura = estatura.ToString();
            sv.Cintura = cintura.ToString();
            sv.Cuello = cuello.ToString();
            sv.Grasa = grasa.ToString();
            sv.GrupoSanguineo = sangre;

            Paciente paciente = db.Paciente.Find(id);

            paciente.Genero = sexo;
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_SignosVitales == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_SignosVitales = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.EPI_SignosVitales.Add(sv);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Audiologia(int id, string patologia, string nota, string graficas, 
            string D125, string D250, string D500, string D1000, string D2000, string D4000, string D8000,
            string I125, string I250, string I500, string I1000, string I2000, string I4000, string I8000)
        {
            EPI_Audiologia audio = new EPI_Audiologia();

            audio.Patologia = patologia;
            audio.Grafica = graficas == "on" ? "ALTERADO" : "NORMAL";
            audio.NotaMedica = nota == "on" ? "ALTERADO" : "NORMAL";
            audio.idPaciente = id;

            audio.D125 = D125;
            audio.D250 = D250;
            audio.D500 = D500;
            audio.D1000 = D1000;
            audio.D2000 = D2000;
            audio.D4000 = D4000;
            audio.D8000 = D8000;

            audio.I125 = I125;
            audio.I250 = I250;
            audio.I500 = I500;
            audio.I1000 = I1000;
            audio.I2000 = I2000;
            audio.I4000 = I4000;
            audio.I8000 = I8000;

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Audiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Audiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.EPI_Audiologia.Add(audio);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        //[HttpPost]
        //public ActionResult Guardar_Oftalmologia(int id)
        //{
        //    var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

        //    if (revision.Fin_Oftalmologia == null)
        //    {
        //        var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
        //        cm.Fin_Oftalmologia = DateTime.Now;

        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(cm).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //    }

        //    ViewBag.idPaciente = id;
        //    return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        //}

        public ActionResult Guardar_Oftalmologia(int id, string izquierdoavc, string izquierdoavm, string izquierdoavl, string derechoavc, string derechoavm, string derechoavl,
        string agudezavisual, string estereopsis, string cartaaccidentes, string campovisual, string movimientosoculares, string derechorp, string izquierdorp,
        string cromatica)
        {

            Epi_Oftalmologia oftal = new Epi_Oftalmologia();
            oftal.IzquierdoAVC = izquierdoavc.ToString();
            oftal.IzquierdoAVM = izquierdoavm.ToString();
            oftal.IzquierdoAVL = izquierdoavl.ToString();
            oftal.DerechoAVC = derechoavc.ToString();
            oftal.DerechoAVM = derechoavm.ToString();
            oftal.DerechoAVL = derechoavl.ToString();
            oftal.AgudezaVisual = agudezavisual == "on" ? "SI" : "NO";
            oftal.Estereopsis = estereopsis.ToString();
            oftal.CartaAccidentes = cartaaccidentes == "on" ? "SI" : "NO";
            oftal.MovimientosOculares = movimientosoculares == "on" ? "NORMAL" : "ALTERADO";
            oftal.CampoVisual = campovisual == "on" ? "NORMAL" : "ALTERADO";
            oftal.DerechoRP = derechorp == "on" ? "NORMAL" : "ALTERADO";
            oftal.IzquierdoRP = izquierdorp == "on" ? "NORMAL" : "ALTERADO";
            oftal.Cromatica = cromatica;
            oftal.idPaciente = id;

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Oftalmologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Oftalmologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Epi_Oftalmologia.Add(oftal);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Odontologia(int id, string exploracion,
        string tc_Diente_18, string tr_Diente_18, string to_Diente_18, string tc_Diente_17, string tr_Diente_17, string to_Diente_17, string tc_Diente_16, string tr_Diente_16, string to_Diente_16, string tc_Diente_15, string tr_Diente_15, string to_Diente_15, string tc_Diente_14, string tr_Diente_14, string to_Diente_14, string tc_Diente_13, string tr_Diente_13, string to_Diente_13, string tc_Diente_12, string tr_Diente_12, string to_Diente_12, string tc_Diente_11, string tr_Diente_11, string to_Diente_11,
        string tc_Diente_28, string tr_Diente_28, string to_Diente_28, string tc_Diente_27, string tr_Diente_27, string to_Diente_27, string tc_Diente_26, string tr_Diente_26, string to_Diente_26, string tc_Diente_25, string tr_Diente_25, string to_Diente_25, string tc_Diente_24, string tr_Diente_24, string to_Diente_24, string tc_Diente_23, string tr_Diente_23, string to_Diente_23, string tc_Diente_22, string tr_Diente_22, string to_Diente_22, string tc_Diente_21, string tr_Diente_21, string to_Diente_21,
        string tc_Diente_38, string tr_Diente_38, string to_Diente_38, string tc_Diente_37, string tr_Diente_37, string to_Diente_37, string tc_Diente_36, string tr_Diente_36, string to_Diente_36, string tc_Diente_35, string tr_Diente_35, string to_Diente_35, string tc_Diente_34, string tr_Diente_34, string to_Diente_34, string tc_Diente_33, string tr_Diente_33, string to_Diente_33, string tc_Diente_32, string tr_Diente_32, string to_Diente_32, string tc_Diente_31, string tr_Diente_31, string to_Diente_31,
        string tc_Diente_48, string tr_Diente_48, string to_Diente_48, string tc_Diente_47, string tr_Diente_47, string to_Diente_47, string tc_Diente_46, string tr_Diente_46, string to_Diente_46, string tc_Diente_45, string tr_Diente_45, string to_Diente_45, string tc_Diente_44, string tr_Diente_44, string to_Diente_44, string tc_Diente_43, string tr_Diente_43, string to_Diente_43, string tc_Diente_42, string tr_Diente_42, string to_Diente_42, string tc_Diente_41, string tr_Diente_41, string to_Diente_41)
        {
            string NotaMedica = "";
            //Inicia el proceso de llenado de dientes (operadores ternarios)-----------------------------

            NotaMedica = tc_Diente_18 != "" ? NotaMedica += tc_Diente_18 + " #18, " : NotaMedica += "";
            NotaMedica = to_Diente_18 != "" ? NotaMedica += to_Diente_18 + " #18, " : NotaMedica += "";
            NotaMedica = tr_Diente_18 != "" ? NotaMedica += tr_Diente_18 + " #18, " : NotaMedica += "";

            NotaMedica = tc_Diente_17 != "" ? NotaMedica += tc_Diente_17 + " #17, " : NotaMedica += "";
            NotaMedica = to_Diente_17 != "" ? NotaMedica += to_Diente_17 + " #17, " : NotaMedica += "";
            NotaMedica = tr_Diente_17 != "" ? NotaMedica += tr_Diente_17 + " #17, " : NotaMedica += "";

            NotaMedica = tc_Diente_16 != "" ? NotaMedica += tc_Diente_16 + " #16, " : NotaMedica += "";
            NotaMedica = to_Diente_16 != "" ? NotaMedica += to_Diente_16 + " #16, " : NotaMedica += "";
            NotaMedica = tr_Diente_16 != "" ? NotaMedica += tr_Diente_16 + " #16, " : NotaMedica += "";

            NotaMedica = tc_Diente_15 != "" ? NotaMedica += tc_Diente_15 + " #15, " : NotaMedica += "";
            NotaMedica = to_Diente_15 != "" ? NotaMedica += to_Diente_15 + " #15, " : NotaMedica += "";
            NotaMedica = tr_Diente_15 != "" ? NotaMedica += tr_Diente_15 + " #15, " : NotaMedica += "";

            NotaMedica = tc_Diente_14 != "" ? NotaMedica += tc_Diente_14 + " #14, " : NotaMedica += "";
            NotaMedica = to_Diente_14 != "" ? NotaMedica += to_Diente_14 + " #14, " : NotaMedica += "";
            NotaMedica = tr_Diente_14 != "" ? NotaMedica += tr_Diente_14 + " #14, " : NotaMedica += "";

            NotaMedica = tc_Diente_13 != "" ? NotaMedica += tc_Diente_13 + " #13, " : NotaMedica += "";
            NotaMedica = to_Diente_13 != "" ? NotaMedica += to_Diente_13 + " #13, " : NotaMedica += "";
            NotaMedica = tr_Diente_13 != "" ? NotaMedica += tr_Diente_13 + " #13, " : NotaMedica += "";

            NotaMedica = tc_Diente_12 != "" ? NotaMedica += tc_Diente_12 + " #12, " : NotaMedica += "";
            NotaMedica = to_Diente_12 != "" ? NotaMedica += to_Diente_12 + " #12, " : NotaMedica += "";
            NotaMedica = tr_Diente_12 != "" ? NotaMedica += tr_Diente_12 + " #12, " : NotaMedica += "";

            NotaMedica = tc_Diente_11 != "" ? NotaMedica += tc_Diente_11 + " #11, " : NotaMedica += "";
            NotaMedica = to_Diente_11 != "" ? NotaMedica += to_Diente_11 + " #11, " : NotaMedica += "";
            NotaMedica = tr_Diente_11 != "" ? NotaMedica += tr_Diente_11 + " #11, " : NotaMedica += "";



            NotaMedica = tc_Diente_28 != "" ? NotaMedica += tc_Diente_28 + " #28, " : NotaMedica += "";
            NotaMedica = to_Diente_28 != "" ? NotaMedica += to_Diente_28 + " #28, " : NotaMedica += "";
            NotaMedica = tr_Diente_28 != "" ? NotaMedica += tr_Diente_28 + " #28, " : NotaMedica += "";

            NotaMedica = tc_Diente_27 != "" ? NotaMedica += tc_Diente_27 + " #27, " : NotaMedica += "";
            NotaMedica = to_Diente_27 != "" ? NotaMedica += to_Diente_27 + " #27, " : NotaMedica += "";
            NotaMedica = tr_Diente_27 != "" ? NotaMedica += tr_Diente_27 + " #27, " : NotaMedica += "";

            NotaMedica = tc_Diente_26 != "" ? NotaMedica += tc_Diente_26 + " #26, " : NotaMedica += "";
            NotaMedica = to_Diente_26 != "" ? NotaMedica += to_Diente_26 + " #26, " : NotaMedica += "";
            NotaMedica = tr_Diente_26 != "" ? NotaMedica += tr_Diente_26 + " #26, " : NotaMedica += "";

            NotaMedica = tc_Diente_25 != "" ? NotaMedica += tc_Diente_25 + " #25, " : NotaMedica += "";
            NotaMedica = to_Diente_25 != "" ? NotaMedica += to_Diente_25 + " #25, " : NotaMedica += "";
            NotaMedica = tr_Diente_25 != "" ? NotaMedica += tr_Diente_25 + " #25, " : NotaMedica += "";

            NotaMedica = tc_Diente_24 != "" ? NotaMedica += tc_Diente_24 + " #24, " : NotaMedica += "";
            NotaMedica = to_Diente_24 != "" ? NotaMedica += to_Diente_24 + " #24, " : NotaMedica += "";
            NotaMedica = tr_Diente_24 != "" ? NotaMedica += tr_Diente_24 + " #24, " : NotaMedica += "";

            NotaMedica = tc_Diente_23 != "" ? NotaMedica += tc_Diente_23 + " #23, " : NotaMedica += "";
            NotaMedica = to_Diente_23 != "" ? NotaMedica += to_Diente_23 + " #23, " : NotaMedica += "";
            NotaMedica = tr_Diente_23 != "" ? NotaMedica += tr_Diente_23 + " #23, " : NotaMedica += "";

            NotaMedica = tc_Diente_22 != "" ? NotaMedica += tc_Diente_22 + " #22, " : NotaMedica += "";
            NotaMedica = to_Diente_22 != "" ? NotaMedica += to_Diente_22 + " #22, " : NotaMedica += "";
            NotaMedica = tr_Diente_22 != "" ? NotaMedica += tr_Diente_22 + " #22, " : NotaMedica += "";

            NotaMedica = tc_Diente_21 != "" ? NotaMedica += tc_Diente_21 + " #21, " : NotaMedica += "";
            NotaMedica = to_Diente_21 != "" ? NotaMedica += to_Diente_21 + " #21, " : NotaMedica += "";
            NotaMedica = tr_Diente_21 != "" ? NotaMedica += tr_Diente_21 + " #21, " : NotaMedica += "";



            NotaMedica = tc_Diente_38 != "" ? NotaMedica += tc_Diente_38 + " #38, " : NotaMedica += "";
            NotaMedica = to_Diente_38 != "" ? NotaMedica += to_Diente_38 + " #38, " : NotaMedica += "";
            NotaMedica = tr_Diente_38 != "" ? NotaMedica += tr_Diente_38 + " #38, " : NotaMedica += "";

            NotaMedica = tc_Diente_37 != "" ? NotaMedica += tc_Diente_37 + " #37, " : NotaMedica += "";
            NotaMedica = to_Diente_37 != "" ? NotaMedica += to_Diente_37 + " #37, " : NotaMedica += "";
            NotaMedica = tr_Diente_37 != "" ? NotaMedica += tr_Diente_37 + " #37, " : NotaMedica += "";

            NotaMedica = tc_Diente_36 != "" ? NotaMedica += tc_Diente_36 + " #36, " : NotaMedica += "";
            NotaMedica = to_Diente_36 != "" ? NotaMedica += to_Diente_36 + " #36, " : NotaMedica += "";
            NotaMedica = tr_Diente_36 != "" ? NotaMedica += tr_Diente_36 + " #36, " : NotaMedica += "";

            NotaMedica = tc_Diente_35 != "" ? NotaMedica += tc_Diente_35 + " #35, " : NotaMedica += "";
            NotaMedica = to_Diente_35 != "" ? NotaMedica += to_Diente_35 + " #35, " : NotaMedica += "";
            NotaMedica = tr_Diente_35 != "" ? NotaMedica += tr_Diente_35 + " #35, " : NotaMedica += "";

            NotaMedica = tc_Diente_34 != "" ? NotaMedica += tc_Diente_34 + " #34, " : NotaMedica += "";
            NotaMedica = to_Diente_34 != "" ? NotaMedica += to_Diente_34 + " #34, " : NotaMedica += "";
            NotaMedica = tr_Diente_34 != "" ? NotaMedica += tr_Diente_34 + " #34, " : NotaMedica += "";

            NotaMedica = tc_Diente_33 != "" ? NotaMedica += tc_Diente_33 + " #33, " : NotaMedica += "";
            NotaMedica = to_Diente_33 != "" ? NotaMedica += to_Diente_33 + " #33, " : NotaMedica += "";
            NotaMedica = tr_Diente_33 != "" ? NotaMedica += tr_Diente_33 + " #33, " : NotaMedica += "";

            NotaMedica = tc_Diente_32 != "" ? NotaMedica += tc_Diente_32 + " #32, " : NotaMedica += "";
            NotaMedica = to_Diente_32 != "" ? NotaMedica += to_Diente_32 + " #32, " : NotaMedica += "";
            NotaMedica = tr_Diente_32 != "" ? NotaMedica += tr_Diente_32 + " #32, " : NotaMedica += "";

            NotaMedica = tc_Diente_31 != "" ? NotaMedica += tc_Diente_31 + " #31, " : NotaMedica += "";
            NotaMedica = to_Diente_31 != "" ? NotaMedica += to_Diente_31 + " #31, " : NotaMedica += "";
            NotaMedica = tr_Diente_31 != "" ? NotaMedica += tr_Diente_31 + " #31, " : NotaMedica += "";


            NotaMedica = tc_Diente_48 != "" ? NotaMedica += tc_Diente_48 + " #48, " : NotaMedica += "";
            NotaMedica = to_Diente_48 != "" ? NotaMedica += to_Diente_48 + " #48, " : NotaMedica += "";
            NotaMedica = tr_Diente_48 != "" ? NotaMedica += tr_Diente_48 + " #48, " : NotaMedica += "";

            NotaMedica = tc_Diente_47 != "" ? NotaMedica += tc_Diente_47 + " #47, " : NotaMedica += "";
            NotaMedica = to_Diente_47 != "" ? NotaMedica += to_Diente_47 + " #47, " : NotaMedica += "";
            NotaMedica = tr_Diente_47 != "" ? NotaMedica += tr_Diente_47 + " #47, " : NotaMedica += "";

            NotaMedica = tc_Diente_46 != "" ? NotaMedica += tc_Diente_46 + " #46, " : NotaMedica += "";
            NotaMedica = to_Diente_46 != "" ? NotaMedica += to_Diente_46 + " #46, " : NotaMedica += "";
            NotaMedica = tr_Diente_46 != "" ? NotaMedica += tr_Diente_46 + " #46, " : NotaMedica += "";

            NotaMedica = tc_Diente_45 != "" ? NotaMedica += tc_Diente_45 + " #45, " : NotaMedica += "";
            NotaMedica = to_Diente_45 != "" ? NotaMedica += to_Diente_45 + " #45, " : NotaMedica += "";
            NotaMedica = tr_Diente_45 != "" ? NotaMedica += tr_Diente_45 + " #45, " : NotaMedica += "";

            NotaMedica = tc_Diente_44 != "" ? NotaMedica += tc_Diente_44 + " #44, " : NotaMedica += "";
            NotaMedica = to_Diente_44 != "" ? NotaMedica += to_Diente_44 + " #44, " : NotaMedica += "";
            NotaMedica = tr_Diente_44 != "" ? NotaMedica += tr_Diente_44 + " #44, " : NotaMedica += "";

            NotaMedica = tc_Diente_43 != "" ? NotaMedica += tc_Diente_43 + " #43, " : NotaMedica += "";
            NotaMedica = to_Diente_43 != "" ? NotaMedica += to_Diente_43 + " #43, " : NotaMedica += "";
            NotaMedica = tr_Diente_43 != "" ? NotaMedica += tr_Diente_43 + " #43, " : NotaMedica += "";

            NotaMedica = tc_Diente_42 != "" ? NotaMedica += tc_Diente_42 + " #42, " : NotaMedica += "";
            NotaMedica = to_Diente_42 != "" ? NotaMedica += to_Diente_42 + " #42, " : NotaMedica += "";
            NotaMedica = tr_Diente_42 != "" ? NotaMedica += tr_Diente_42 + " #42, " : NotaMedica += "";

            NotaMedica = tc_Diente_41 != "" ? NotaMedica += tc_Diente_41 + " #41, " : NotaMedica += "";
            NotaMedica = to_Diente_41 != "" ? NotaMedica += to_Diente_41 + " #41, " : NotaMedica += "";
            NotaMedica = tr_Diente_41 != "" ? NotaMedica += tr_Diente_41 + " #41, " : NotaMedica += "";

            //Acaba el proceso de llenado de dientes------------------------------------------------


            EPI_Odontologia odontologia = new EPI_Odontologia();

            odontologia.Exploracion = exploracion == "on" ? "ALTERADO" : "NORMAL";
            odontologia.NotaOdontologica = NotaMedica == "" ? null : NotaMedica;
            odontologia.idPaciente = id;

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Odontologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Odontologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.EPI_Odontologia.Add(odontologia);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Heredofamiliares(int id, string madrevive, string padrevive, string hermanosviven, string familiagrave, 
            string diabetes, string arterial, string obesidad, string isquemica, string cerebral, string miocardio, string tiroides, string neoplastica,
            string esp_madrevive, string esp_padrevive, string esp_hermanosviven)
        {
            EPI_A_Heredofamiliares heredo = new EPI_A_Heredofamiliares();

            heredo.MadreVive = madrevive == "on" ? "SI" : "NO";
            heredo.PadreVive = padrevive == "on" ? "SI" : "NO";
            heredo.HermanosViven = hermanosviven == "on" ? "SI" : "NO";
            heredo.FamiliaGrave = familiagrave == "on" ? "SI" : "NO";
            heredo.Diabetes = diabetes == "on" ? "SI" : "NO";
            heredo.Hipertension = arterial == "on" ? "SI" : "NO";
            heredo.Obesidad = obesidad == "on" ? "SI" : "NO";
            heredo.Cardiopatia = isquemica == "on" ? "SI" : "NO";
            heredo.VascularCerebral = cerebral == "on" ? "SI" : "NO";
            heredo.Infarto = miocardio == "on" ? "SI" : "NO";
            heredo.Tiroides = tiroides == "on" ? "SI" : "NO";
            heredo.Neoplasticas = neoplastica == "on" ? "SI" : "NO";

            heredo.MadreVive_ESP = esp_madrevive != null ? esp_madrevive : null;
            heredo.PadreVive_ESP = esp_padrevive != null ? esp_madrevive : null;
            heredo.HermanosViven_ESP = esp_hermanosviven != null ? esp_hermanosviven : null;

            heredo.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.EPI_A_Heredofamiliares.Add(heredo);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 != null && revision2 != null && revision3 != null && revision4 != null && revision5 != null)
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

            if(revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("ExploracionFisica", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }

        [HttpPost]
        public ActionResult Guardar_NoPatologicos(int id, string vacunas, string civil, string religion, string escolaridad, string hijos, string padecimiento, string esp_padecimiento)
        {
            EPI_A_NoPatologicos nopat = new EPI_A_NoPatologicos();

            nopat.VacunasCompletas = vacunas == "on" ? "SI" : "NO";
            nopat.EstadoCivil = civil;
            nopat.Religion = religion;
            nopat.Escolaridad = escolaridad;
            nopat.Hijos = hijos;
            nopat.idPaciente = id;
            nopat.PadecimientoActual = padecimiento == "on" ? "SI" : "NO";

            nopat.ESP_PadecimientoActual = esp_padecimiento != null ? esp_padecimiento : null;

            if (ModelState.IsValid)
            {
                db.EPI_A_NoPatologicos.Add(nopat);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 != null && revision2 != null && revision3 != null && revision4 != null && revision5 != null)
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

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("ExploracionFisica", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }

        [HttpPost]
        public ActionResult Guardar_AparatosSistemas(int id, string sintoma, string alteracion, string lentes, string cancer, string audicion,
            string auditivo, string nariz, string boca, string cardiaca, string respiratoria, string endocrino, string urinaria, string venas, 
            string intestinal, string articulaciones, string suenio, string apnea, string neurologica, string cuello, string abdomen, string extremidades)
        {
            EPI_AparatosSistemas apa = new EPI_AparatosSistemas();

            apa.Sintoma = sintoma == "on" ? "SI" : "NO";
            apa.AlteracionVista = alteracion == "on" ? "SI" : "NO";
            apa.Lentes = lentes; 
            apa.Audicion = audicion == "on" ? "SI" : "NO";
            apa.Auditivo = auditivo == "on" ? "SI" : "NO";
            apa.Nariz = nariz == "on" ? "SI" : "NO";
            apa.Gusto = boca == "on" ? "SI" : "NO";
            apa.Cardiaca = cardiaca == "on" ? "SI" : "NO";
            apa.Respiratoria = respiratoria == "on" ? "SI" : "NO";
            apa.Endocrinologica = endocrino == "on" ? "SI" : "NO";
            apa.Urinaria = urinaria == "on" ? "SI" : "NO";
            apa.Venas = venas == "on" ? "SI" : "NO";
            apa.Intestinal = intestinal == "on" ? "SI" : "NO";
            apa.Articulaciones = articulaciones == "on" ? "SI" : "NO";
            apa.Suenio = suenio == "on" ? "SI" : "NO";
            apa.Apnea = apnea == "on" ? "SI" : "NO";
            apa.Neurologica = neurologica == "on" ? "SI" : "NO";
            apa.CabezaCuello = cuello == "on" ? "SI" : "NO";
            apa.Abdomen = abdomen == "on" ? "SI" : "NO";
            apa.Extremidades = extremidades == "on" ? "SI" : "NO";
            apa.CancerSangre = cancer == "on" ? "SI" : "NO";
            apa.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.EPI_AparatosSistemas.Add(apa);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 != null && revision2 != null && revision3 != null && revision4 != null && revision5 != null)
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

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("ExploracionFisica", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }

        [HttpPost]
        public ActionResult Guardar_Patologicos(int id, string congenita, string alergias, string memoria, string obstructiva, string quirurgicos, string transfuncionales,
            string diabetes, string convunsivas, string traumatismo, string oncologicos, string cardiopatias, string hipertension, string fuma, string alcoholicas,
            string drogas, string congenita_esp, string alergias_esp, string memoria_esp, string obstructiva_esp, string quirurgicos_esp, string transfuncionales_esp,
            string diabetes_esp, string convulsivas_esp, string traumatismo_esp, string oncologicos_esp, string cardiopatias_esp, string hipertension_esp)
        {
            EPI_A_Patologicos patologicos = new EPI_A_Patologicos();

            patologicos.EnfermedadCongenita = congenita == "on" ? "SI" : "NO";
            patologicos.Alergias = alergias == "on" ? "SI" : "NO";
            patologicos.TranstornoMemoria = memoria == "on" ? "SI" : "NO";
            patologicos.Pulmonar = obstructiva == "on" ? "SI" : "NO";
            patologicos.Quirurgicos = quirurgicos == "on" ? "SI" : "NO";
            patologicos.Transfuncionales = transfuncionales == "on" ? "SI" : "NO";
            patologicos.Diabetes = diabetes == "on" ? "SI" : "NO";
            patologicos.Convulsivas = convunsivas == "on" ? "SI" : "NO";
            patologicos.Traumatismo = traumatismo == "on" ? "SI" : "NO";
            patologicos.Oncologicos = oncologicos == "on" ? "SI" : "NO";
            patologicos.Cardiopatias = cardiopatias == "on" ? "SI" : "NO";
            patologicos.Hipertension = hipertension == "on" ? "SI" : "NO";
            patologicos.Fuma = fuma == "on" ? "SI" : "NO";
            patologicos.Alcoholismo = alcoholicas == "on" ? "SI" : "NO";
            patologicos.Drogas = drogas == "on" ? "SI" : "NO";
            
            patologicos.EnfermedadCongenita_ESP = congenita_esp != null ? congenita_esp : null;
            patologicos.Alergias_ASP = alergias_esp != null ? alergias_esp : null;
            patologicos.TrastornoMemoria_ESP = memoria_esp != null ? memoria_esp : null;
            patologicos.Pulmonar_ESP = obstructiva_esp != null ? obstructiva_esp : null;
            patologicos.Quirurgicos_ESP = quirurgicos_esp != null ? quirurgicos_esp : null;
            patologicos.Transfuncionales_ESP = transfuncionales_esp != null ? transfuncionales_esp : null;
            patologicos.Diabetes_ESP = diabetes_esp != null ? diabetes_esp : null;
            patologicos.Convulsivas_ESP = convulsivas_esp != null ? convulsivas_esp : null;
            patologicos.Traumatismo_ESP = traumatismo_esp != null ? traumatismo_esp : null;
            patologicos.Oncologicos_ESP = oncologicos_esp != null ? oncologicos_esp : null;
            patologicos.Cardiopatias_ESP = cardiopatias_esp != null ? cardiopatias_esp : null;
            patologicos.Hipertension_ESP = hipertension_esp != null ? hipertension_esp : null;


            patologicos.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.EPI_A_Patologicos.Add(patologicos);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 != null && revision2 != null && revision3 != null && revision4 != null && revision5 != null)
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

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }
            if (revision5 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("ExploracionFisica", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }


        [HttpPost]
        public ActionResult Guardar_ExploracionFisica(int id, string romberg, string nariz, string estrabismo, string soplo, string amputaciones,
            string protesis)
        {
            EPI_ExploracionFisica ef = new EPI_ExploracionFisica();

            ef.Romberg = romberg == "on" ? "SI" : "NO";
            ef.PuntaNariz = nariz == "on" ? "SI" : "NO";
            ef.Estrabismo = estrabismo == "on" ? "SI" : "NO";
            ef.Soplo = soplo == "on" ? "SI" : "NO";
            ef.Amputaciones = amputaciones == "on" ? "SI" : "NO";
            ef.Protesis = protesis == "on" ? "SI" : "NO";

            ef.idPaciente = id;

            if (ModelState.IsValid)
            {
                db.EPI_ExploracionFisica.Add(ef);
                db.SaveChanges();
            }

            var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id select r).FirstOrDefault();
            var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id select r).FirstOrDefault();
            var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id select r).FirstOrDefault();
            var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id select r).FirstOrDefault();

            if (revision1 != null && revision2 != null && revision3 != null && revision4 != null && revision5 != null)
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

            if (revision1 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("HISTORIA_CLÍNICA", "ArchivoClinico", new { id = id });
            }
            if (revision2 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("NoPatologicos", "ArchivoClinico", new { id = id });
            }
            if (revision3 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("Patologicos", "ArchivoClinico", new { id = id });
            }
            if (revision4 == null)
            {
                ViewBag.idPaciente = id;
                return RedirectToAction("AparatosSistemas", "ArchivoClinico", new { id = id });
            }

            return View("Recepcion");
        }

        [HttpPost]
        public ActionResult Guardar_Cardiologia(int id, string ritmo, double frecuencia, double eje, double pr, double qt, double qrs, double ondap, double ondaT)
        {
            EPI_Cardiologia cardio = new EPI_Cardiologia();

            cardio.Ritmo = ritmo;
            cardio.Frecuencia = frecuencia.ToString();
            cardio.Eje = eje.ToString();
            cardio.PR = pr.ToString();
            cardio.QT = qt.ToString();
            cardio.QRS = qrs.ToString();
            cardio.OndaP = ondap.ToString();
            cardio.OndaT = ondaT.ToString();
            cardio.idPaciente = id;

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Cardiologia == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Cardiologia = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.EPI_Cardiologia.Add(cardio);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        [HttpPost]
        public ActionResult Guardar_Laboratorio(int id, int glucosa, int? hemoglobina)
        {
            EPI_Laboratorio lab = new EPI_Laboratorio();

            lab.Glucosa = glucosa.ToString();
            lab.HemoglobinaGlucosilada = hemoglobina == null ? "0" : hemoglobina.ToString();
            lab.idPaciente = id;

            var revision = (from r in db.CarruselMedico where r.idPaciente == id select r).FirstOrDefault();

            if (revision.Fin_Laboratorio == null)
            {
                var cm = db.CarruselMedico.Find(revision.idCarruselMedico);
                cm.Fin_Laboratorio = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.EPI_Laboratorio.Add(lab);
                    db.Entry(cm).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            ViewBag.idPaciente = id;
            return RedirectToAction("Index", "ArchivoClinico", new { id = id });
        }

        public ActionResult Guardar_Dictamen(int id, string nota, string accion)
        {
            var revisionDictamen = (from r in db.EPI_DictamenAptitud where r.idPaciente == id orderby r.idDictamenAptitud descending select r).FirstOrDefault();

            if(revisionDictamen == null)
            {
                EPI_DictamenAptitud aptitud = new EPI_DictamenAptitud();

                aptitud.Motivo = accion;
                aptitud.NotaMedica = nota;
                aptitud.idPaciente = id;

                if (accion == "Concluir examen como NO APTO" || accion == "Enviar a Revaloración" || accion == "Revalorado NO APTO")
                {
                    aptitud.Aptitud = "NO APTO";
                }
                else
                {
                    aptitud.Aptitud = "APTO";
                }

                var revision = (from r in db.EPI_DictamenAptitud where r.idPaciente == id select r).FirstOrDefault();

                if (revision == null)
                {
                    if (ModelState.IsValid)
                    {
                        db.EPI_DictamenAptitud.Add(aptitud);
                        db.SaveChanges();
                    }
                }

                if (accion != "Enviar a Revaloración")
                {
                    var pacienteCaptura = (from c in db.Cita where c.idPaciente == id select c).FirstOrDefault();
                    var pacienteCaptura2 = (from c in db.Paciente where c.idPaciente == id select c).FirstOrDefault();
                    var capturaExistente = (from i in db.Captura where i.idPaciente == id select i).FirstOrDefault();

                    if (pacienteCaptura.NoExpediente != null && pacienteCaptura.Doctor != null && capturaExistente == null)
                    {
                        Captura captura = new Captura();

                        captura.idPaciente = id;
                        captura.NombrePaciente = pacienteCaptura2.Nombre;
                        captura.NoExpediente = pacienteCaptura.NoExpediente;
                        captura.TipoTramite = pacienteCaptura.TipoTramite;
                        captura.EstatusCaptura = "No iniciado";
                        captura.Doctor = pacienteCaptura.Doctor;
                        captura.Sucursal = pacienteCaptura.Sucursal;
                        captura.FechaExpdiente = DateTime.Now;
                        captura.CarruselMedico = "SI";

                        if (ModelState.IsValid)
                        {
                            db.Captura.Add(captura);
                            db.SaveChanges();
                        }
                    }

                    
                }

 
                if(accion == "Enviar a Revaloración")
                {
                    TempData["ID"] = id;
                    return Redirect("Recepcion");
                }

                
            }
            else
            {
                EPI_DictamenAptitud aptitud = db.EPI_DictamenAptitud.Find(revisionDictamen.idDictamenAptitud);

                aptitud.Motivo = accion;
                aptitud.NotaMedica = nota;
                aptitud.idPaciente = id;

                if (accion == "Concluir examen como NO APTO")
                {
                    aptitud.Aptitud = "NO APTO";
                }
                else if (accion == "APTO")
                {
                    aptitud.Aptitud = "APTO";
                }

                if (ModelState.IsValid)
                {
                    db.Entry(aptitud).State = EntityState.Modified;
                    db.SaveChanges();
                }


                if (accion == "Revalorado APTO")
                {
                    EPI_A_Heredofamiliares heredo = new EPI_A_Heredofamiliares();
                    var anteriorHEREDO = (from i in db.EPI_A_Heredofamiliares where i.idPaciente == id orderby i.idHeredofamiliares descending select i).FirstOrDefault();
                    heredo.idPaciente = id;
                    heredo.MadreVive = anteriorHEREDO.MadreVive;
                    heredo.MadreVive_ESP = anteriorHEREDO.MadreVive_ESP;
                    heredo.PadreVive = anteriorHEREDO.PadreVive;
                    heredo.PadreVive_ESP = anteriorHEREDO.PadreVive_ESP;
                    heredo.HermanosViven = anteriorHEREDO.HermanosViven;
                    heredo.HermanosViven_ESP = anteriorHEREDO.HermanosViven_ESP;
                    heredo.FamiliaGrave = anteriorHEREDO.FamiliaGrave;
                    heredo.Diabetes = anteriorHEREDO.Diabetes;
                    heredo.Hipertension = anteriorHEREDO.Hipertension;
                    heredo.Obesidad = anteriorHEREDO.Obesidad;
                    heredo.Cardiopatia = anteriorHEREDO.Cardiopatia;
                    heredo.VascularCerebral = anteriorHEREDO.VascularCerebral;
                    heredo.Infarto = anteriorHEREDO.Infarto;
                    heredo.Tiroides = anteriorHEREDO.Tiroides;
                    heredo.Neoplasticas = anteriorHEREDO.Neoplasticas;

                    if (ModelState.IsValid)
                    {
                        db.EPI_A_Heredofamiliares.Add(heredo);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_A_NoPatologicos nopato = new EPI_A_NoPatologicos();
                    var anteriorNOPATO = (from i in db.EPI_A_NoPatologicos where i.idPaciente == id orderby i.idNoPatologicos descending select i).FirstOrDefault();
                    nopato.idPaciente = id;
                    nopato.VacunasCompletas = anteriorNOPATO.VacunasCompletas;
                    nopato.EstadoCivil = anteriorNOPATO.EstadoCivil;
                    nopato.Religion = anteriorNOPATO.Religion;
                    nopato.Escolaridad = anteriorNOPATO.Escolaridad;
                    nopato.Hijos = anteriorNOPATO.Hijos;
                    nopato.ESP_PadecimientoActual = anteriorNOPATO.ESP_PadecimientoActual;

                    if (ModelState.IsValid)
                    {
                        db.EPI_A_NoPatologicos.Add(nopato);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_A_Patologicos pato = new EPI_A_Patologicos();
                    var anteriorPATO = (from i in db.EPI_A_Patologicos where i.idPaciente == id orderby i.idPatologicos descending select i).FirstOrDefault();
                    pato.idPaciente = id;
                    pato.EnfermedadCongenita = "NO";
                    pato.Alergias = "NO";
                    pato.TranstornoMemoria = "NO";
                    pato.Pulmonar = "NO";
                    pato.Quirurgicos = "NO";
                    pato.Transfuncionales = "NO";
                    pato.Diabetes = "NO";
                    pato.Traumatismo = "NO";
                    pato.Convulsivas = "NO";
                    pato.Oncologicos = "NO";
                    pato.Cardiopatias = "NO";
                    pato.Hipertension = "NO";
                    pato.Alcoholismo = "NO";
                    pato.Fuma = "NO";
                    pato.Drogas = "NO";

                    if (ModelState.IsValid)
                    {
                        db.EPI_A_Patologicos.Add(pato);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_AparatosSistemas aparatos = new EPI_AparatosSistemas();
                    var anteriorAPARATOS = (from i in db.EPI_AparatosSistemas where i.idPaciente == id orderby i.idAparatosSistemas descending select i).FirstOrDefault();
                    aparatos.idPaciente = id;
                    aparatos.Sintoma = "NO";
                    aparatos.AlteracionVista = "NO";
                    aparatos.Lentes = anteriorAPARATOS.Lentes;
                    aparatos.Audicion = "NO";
                    aparatos.Auditivo = "NO";
                    aparatos.Nariz = "NO";
                    aparatos.Gusto = "NO";
                    aparatos.Cardiaca = "NO";
                    aparatos.Respiratoria = "NO";
                    aparatos.Endocrinologica = "NO";
                    aparatos.Urinaria = "NO";
                    aparatos.Venas = "NO";
                    aparatos.Intestinal = "NO";
                    aparatos.CancerSangre = "NO";
                    aparatos.Articulaciones = "NO";
                    aparatos.Suenio = "NO";
                    aparatos.Apnea = "NO";
                    aparatos.Neurologica = "NO";
                    aparatos.CabezaCuello = "NO";
                    aparatos.Abdomen = "NO";
                    aparatos.Extremidades = "NO";

                    if (ModelState.IsValid)
                    {
                        db.EPI_AparatosSistemas.Add(aparatos);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_Audiologia audio = new EPI_Audiologia();
                    var anteriorAUDIO = (from i in db.EPI_Audiologia where i.idPaciente == id orderby i.idAudiologia descending select i).FirstOrDefault();
                    audio.idPaciente = id;
                    audio.Patologia = "NORMAL";
                    audio.Grafica = "NORMAL";
                    audio.NotaMedica = "NORMAL";

                    if (ModelState.IsValid)
                    {
                        db.EPI_Audiologia.Add(audio);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_Cardiologia cardi = new EPI_Cardiologia();
                    var anteriorCARDI = (from i in db.EPI_Cardiologia where i.idPaciente == id orderby i.idCardiologia descending select i).FirstOrDefault();
                    cardi.idPaciente = id;
                    cardi.Ritmo = "Sinusal";
                    cardi.Frecuencia = "70";
                    cardi.Eje = "45";
                    cardi.PR = "180";
                    cardi.QT = "194";
                    cardi.QRS = "330";
                    cardi.OndaP = "120";
                    cardi.OndaT = "200";

                    if (ModelState.IsValid)
                    {
                        db.EPI_Cardiologia.Add(cardi);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_ExploracionFisica explo = new EPI_ExploracionFisica();
                    var anteriorEXPLO = (from i in db.EPI_ExploracionFisica where i.idPaciente == id orderby i.idExploracionFisica descending select i).FirstOrDefault();
                    explo.idPaciente = id;
                    explo.Romberg = "NO";
                    explo.PuntaNariz = "NO";
                    explo.Estrabismo = "NO";
                    explo.Soplo = "NO";
                    explo.Amputaciones = "NO";
                    explo.Protesis = "NO";

                    if (ModelState.IsValid)
                    {
                        db.EPI_ExploracionFisica.Add(explo);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_Laboratorio lab = new EPI_Laboratorio();
                    var anteriorLAB = (from i in db.EPI_Laboratorio where i.idPaciente == id orderby i.idLaboratorio descending select i).FirstOrDefault();
                    lab.idPaciente = id;
                    lab.Glucosa = "70";
                    lab.HemoglobinaGlucosilada = "0";

                    if (ModelState.IsValid)
                    {
                        db.EPI_Laboratorio.Add(lab);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_Odontologia odon = new EPI_Odontologia();
                    var anteriorODON = (from i in db.EPI_Odontologia where i.idPaciente == id orderby i.idOdontologia descending select i).FirstOrDefault();
                    odon.idPaciente = id;
                    odon.Exploracion = "NORMAL";
                    odon.NotaOdontologica = "Todo Normal";

                    if (ModelState.IsValid)
                    {
                        db.EPI_Odontologia.Add(odon);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    Epi_Oftalmologia oft = new Epi_Oftalmologia();
                    var anteriorOFT = (from i in db.Epi_Oftalmologia where i.idPaciente == id orderby i.idOftalmologia descending select i).FirstOrDefault();
                    oft.idPaciente = id;
                    oft.IzquierdoAVC = "1";
                    oft.IzquierdoAVM = "1";
                    oft.IzquierdoAVL = "1";
                    oft.DerechoAVC = "1";
                    oft.DerechoAVM = "1";
                    oft.DerechoAVL = "1";
                    oft.AgudezaVisual = anteriorOFT.AgudezaVisual;
                    oft.Estereopsis = "40";
                    oft.CartaAccidentes = anteriorOFT.CartaAccidentes;
                    oft.MovimientosOculares = "NORMAL";
                    oft.CampoVisual = "NORMAL";
                    oft.DerechoRP = "NORMAL";
                    oft.IzquierdoRP = "NORMAL";
                    oft.Cromatica = "Normal";

                    if (ModelState.IsValid)
                    {
                        db.Epi_Oftalmologia.Add(oft);
                        db.SaveChanges();
                    }

                    //----------------------------------------------------------------------------------------------
                    EPI_SignosVitales sig = new EPI_SignosVitales();
                    var anteriorSIG = (from i in db.EPI_SignosVitales where i.idPaciente == id orderby i.idPaciente descending select i).FirstOrDefault();
                    sig.idPaciente = id;
                    sig.Sistolica = "120";
                    sig.Diastolica = "80";
                    sig.Cardiaca = "70";
                    sig.Respiratoria = "16";
                    sig.Cintura = "80";
                    sig.Cuello = "35";
                    sig.Estatura = "170";
                    sig.Peso = "70";
                    sig.Temperatura = "36";
                    sig.IMC = "24";
                    sig.Grasa = "30";

                    if (ModelState.IsValid)
                    {
                        db.EPI_SignosVitales.Add(sig);
                        db.SaveChanges();
                    }

                }


                if (accion != "Enviar a Revaloración")
                {
                    var pacienteCaptura = (from c in db.Cita where c.idPaciente == id select c).FirstOrDefault();
                    var pacienteCaptura2 = (from c in db.Paciente where c.idPaciente == id select c).FirstOrDefault();

                    Captura captura = new Captura();

                    captura.idPaciente = id;
                    captura.NombrePaciente = pacienteCaptura2.Nombre;
                    captura.NoExpediente = pacienteCaptura.NoExpediente;
                    captura.TipoTramite = pacienteCaptura.TipoTramite;
                    captura.EstatusCaptura = "No iniciado";
                    captura.Doctor = pacienteCaptura.Doctor;
                    captura.Sucursal = pacienteCaptura.Sucursal;
                    captura.FechaExpdiente = DateTime.Now;
                    captura.CarruselMedico = "SI";

                    if (ModelState.IsValid)
                    {
                        db.Captura.Add(captura);
                        db.SaveChanges();
                    }
                }
            }

            

            return Redirect("Recepcion");
        }


        public ActionResult Descargar(int? id)
        {

            System.Diagnostics.Debug.WriteLine("Se entra al metodo descargar");
            System.Console.WriteLine("Se entra al metodo descargar");


            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(
                System.IO.Path.GetDirectoryName(
                  System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            requiredPath = requiredPath.Replace("file:\\", "");

            System.Diagnostics.Debug.WriteLine("Se obtiene la carpeta raiz" + requiredPath);
            System.Console.WriteLine("Se obtiene la carpeta raiz" + requiredPath);

            var headerGMI = requiredPath + ConfigurationManager.AppSettings["headerGMI"];

            var paciente = (from p in db.Paciente where p.idPaciente == id select p).FirstOrDefault();
            var cita = (from p in db.Cita where p.idPaciente == id select p).FirstOrDefault();
            var dictamen = (from p in db.EPI_DictamenAptitud where p.idPaciente == id select p).FirstOrDefault();

            //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
            //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


            /*----------------------------------PDFs Certificados----------------------------------*/

            if (true)
            {
                System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
                System.Console.WriteLine("Entra al llenado del pdf");

                string noEstudio = paciente.idPaciente.ToString();
                string nombre = paciente.Nombre;
                string email = paciente.Email;
                string telefono = paciente.Telefono;
                string CURP = paciente.CURP;
                string Folio = paciente.Folio;
                string HASH = paciente.HASH;

                string noExpediente = cita.NoExpediente;
                string tipoL = cita.TipoLicencia;
                string tipoT = cita.TipoTramite;
                string fechaCita = Convert.ToDateTime(cita.FechaCita).ToString("dd-MMMM-yyyy HH:mm");
                string sucursal = cita.Sucursal;
                string doctor = cita.Doctor;

                string dictamenNOTA = dictamen.NotaMedica;
                string aptitud = dictamen.Aptitud;

                //Comienza el armado del archivo
                Document doc = new Document(PageSize.A4);
                var mem = new MemoryStream();
                iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
                //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                doc.Open();
                System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                System.Console.WriteLine("Se prepara para obtener las imagenes");

                //iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(headerGMI);

                //System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                //System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                //PNG1.Alignment = Image.ALIGN_CENTER;

                //PNG1.ScaleAbsolute(650, 120);

                var color = new BaseColor(128, 128, 128);
                var resultado = new BaseColor(0, 0, 255); //apto
                var resultadoNO = new BaseColor(255, 0, 0); //no apto
                string coloro = "";
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();

                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();

                var font = FontFactory.GetFont(coloro, 11, Font.NORMAL, color);
                var fontA = FontFactory.GetFont(coloro, 11, Font.NORMAL, resultado);
                var fontNA = FontFactory.GetFont(coloro, 11, Font.NORMAL, resultadoNO);

                //Chunk c01 = new Chunk("\n", font);
                Chunk c02 = new Chunk("\n", font);
                Chunk c0 = new Chunk("DATOS DEL PACIENTE\n", font);
                Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
                Chunk c2 = new Chunk("Paciente: " + nombre + "\n", font);
                //Chunk c3 = new Chunk("No. Expediente: " + noExpediente + "\n", font);
                Chunk c4 = new Chunk("Email: " + email + "\n", font);
                Chunk c5 = new Chunk("Teléfono: " + telefono + "\n", font);
                Chunk c6 = new Chunk("CURP: " + CURP + "\n", font);
                Chunk c7 = new Chunk("Folio: " + Folio + "\n", font);
                Chunk c8 = new Chunk("\n", font);
                Chunk c81 = new Chunk("Código Hash: " + HASH + "\n", font);
                Chunk c82 = new Chunk("\n", font);

                //PdfContentByte cb = wri.DirectContent;
                //cb.BeginText();
                //cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 18);
                //cb.SetTextMatrix(46, 175);
                //cb.ShowText("text");
                //cb.EndText();

                //Chunk c10 = new Chunk("DATOS DEL EXAMEN\n", font);
                ////Chunk c11 = new Chunk("Sucursal: " + sucursal + "\n", font);
                ////Chunk c12 = new Chunk("Doctor Dictaminador: " + doctor + "\n", font);
                //Chunk c13 = new Chunk("Tipo de Licencia: " + tipoL + "\n", font);
                //Chunk c14 = new Chunk("Tipo de Trámite: " + tipoT + "\n", font);
                Chunk c15 = new Chunk("Fecha de Examen: " + fechaCita + "\n", font);
                Chunk c16 = new Chunk("\n", font);

                Chunk c20 = new Chunk("DIAGNÓSTICO A REVALORAR\n", font);
                Chunk c201 = new Chunk("\n", font);
                //Chunk c21 = new Chunk();

                //if (aptitud == "APTO")
                //{
                //    c21 = new Chunk(aptitud + "\n", fontA);
                //}
                //else
                //{
                //    c21 = new Chunk(aptitud + "\n", fontNA);
                //}

                var consulta = (from c in db.Epi_Oftalmologia where c.idPaciente == id orderby c.idOftalmologia descending select c).FirstOrDefault();
                var consulta1 = (from c in db.EPI_SignosVitales where c.idPaciente == id orderby c.idSignosVitales descending select c).FirstOrDefault();
                var consulta2 = (from c in db.EPI_Cardiologia where c.idPaciente == id orderby c.idCardiologia descending select c).FirstOrDefault();
                var consulta4 = (from c in db.EPI_Audiologia where c.idPaciente == id orderby c.idAudiologia descending select c).FirstOrDefault();
                var consulta6 = (from c in db.EPI_Odontologia where c.idPaciente == id orderby c.idOdontologia descending select c).FirstOrDefault();
                var consulta5 = (from c in db.EPI_Laboratorio where c.idPaciente == id orderby c.idLaboratorio descending select c).FirstOrDefault();
                var revision1 = (from r in db.EPI_A_Heredofamiliares where r.idPaciente == id orderby r.idHeredofamiliares descending select r).FirstOrDefault();
                var revision2 = (from r in db.EPI_A_NoPatologicos where r.idPaciente == id orderby r.idNoPatologicos descending select r).FirstOrDefault();
                var revision3 = (from r in db.EPI_A_Patologicos where r.idPaciente == id orderby r.idPatologicos descending select r).FirstOrDefault();
                var revision4 = (from r in db.EPI_AparatosSistemas where r.idPaciente == id orderby r.idAparatosSistemas descending select r).FirstOrDefault();
                var revision5 = (from r in db.EPI_ExploracionFisica where r.idPaciente == id orderby r.idExploracionFisica descending select r).FirstOrDefault();

                Chunk c50 = new Chunk("CIE-10(E-11) Diabetes Mellitus Tipo 2 - MEDICINA INTERNA\n", font);
                Chunk c51 = new Chunk("CIE-10 HIPERTENSIÓN ESCENCIAL - MEDICINA INTERNA\n", font);
                Chunk c52 = new Chunk("CIE-10(I45) Otros trastornos de la conducción - CARDIÓLOGO\n", font);
                Chunk c53 = new Chunk("CIE-10(E66) Obesidad - NUTRICIÓN\n", font);
                Chunk c54 = new Chunk("ALTERACION DE LA VISTA NO ESPECIFICADA - OFTALMÓLOGO\n", font);
                Chunk c55 = new Chunk("CIE-10(53.32) Estereopsis Defectuosa - OFTALMÓLOGO\n", font);
                Chunk c56 = new Chunk("CIE-10(H-918) OTRAS HIPOACUSIAS ESPECIFICADAS - MEDICINA INTERNA\n", font);
                Chunk c57 = new Chunk("I-25 ENFERMEDAD ISQUEMICA CRONICA DEL CORAZON - CARDIÓLOGO\n", font);
                Chunk c58 = new Chunk("ALTERACION NEUROLOGICA NO ESPECIFICADA - CARDIÓLOGO\n", font);
                Chunk c59 = new Chunk("E-079 TRASTORNO DE LA GLANDULA TIROIDES, NO ESPECIFICADO - MEDICINA INTERNA\n", font);
                Chunk c60 = new Chunk("H-509 ESTRABISMO, NO ESPECIFICADO - OFTALMÓLOGO\n", font);
                Chunk c61 = new Chunk("H-918 OTRAS HIPOACUSIAS ESPECIFICADAS - MEDICINA INTERNA\n", font);
                Chunk c62 = new Chunk("\n", font);


                Chunk c22 = new Chunk("NOTA: " + dictamenNOTA + "\n", font); //Resultado en azul

                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                //doc.Add(PNG1);
                //p.Add(c01);
                p.Add(c02);
                p.Add(c0);
                p.Add(c1);
                p.Add(c15);
                p.Add(c2);
                //p.Add(c3);
                p.Add(c4);
                p.Add(c5);
                p.Add(c6);
                p.Add(c7);
                p.Add(c8);
                p.Add(c81);
                p.Add(c82);

                //p.Add(c10);
                ////p.Add(c11);
                ////p.Add(c12);
                //p.Add(c13);
                //p.Add(c14);
                
                //p.Add(c16);

                p.Add(c20);
                p.Add(c201);

                if ((Convert.ToInt32(consulta5.Glucosa) > 126 && Convert.ToInt32(consulta5.HemoglobinaGlucosilada) != 0)
                    || (Convert.ToInt32(consulta5.Glucosa) > 200 && Convert.ToInt32(consulta5.HemoglobinaGlucosilada) == 0)
                    || (Convert.ToInt32(consulta5.HemoglobinaGlucosilada) > 7))
                {
                    p.Add(c50);
                }

                if (Convert.ToInt32(consulta1.Sistolica) >= 140 || Convert.ToInt32(consulta1.Diastolica) >= 90
                    || (revision3.Diabetes == "SI" && Convert.ToInt32(consulta1.Sistolica) > 130) || (revision3.Diabetes == "SI" && Convert.ToInt32(consulta1.Diastolica) > 180))
                {
                    p.Add(c51);
                }

                if ((Convert.ToInt32(consulta2.Frecuencia) >= 65 && Convert.ToInt32(consulta2.Frecuencia) <= 99))
                {
                    p.Add(c52);
                }

                if (Convert.ToDouble(consulta1.IMC) >= 30
                || Convert.ToInt32(consulta1.Grasa) >= 35)
                {
                    p.Add(c53);
                }

                if ((Convert.ToDouble(consulta.DerechoAVC) >= 0.7 && Convert.ToDouble(consulta.DerechoAVC) <= 1) || (Convert.ToDouble(consulta.DerechoAVM) >= 0.7 && Convert.ToDouble(consulta.DerechoAVM) <= 1)
                || (Convert.ToDouble(consulta.DerechoAVL) >= 0.7 && Convert.ToDouble(consulta.DerechoAVL) <= 1) || (Convert.ToDouble(consulta.IzquierdoAVC) >= 0.7 && Convert.ToDouble(consulta.IzquierdoAVC) <= 1)
                || (Convert.ToDouble(consulta.IzquierdoAVM) >= 0.7 && Convert.ToDouble(consulta.IzquierdoAVM) <= 1) || (Convert.ToDouble(consulta.IzquierdoAVL) >= 0.7 && Convert.ToDouble(consulta.IzquierdoAVL) <= 1)
                || consulta.CampoVisual != "NORMAL" || consulta.MovimientosOculares != "NORMAL" || consulta.DerechoRP != "NORMAL"
                || consulta.IzquierdoRP != "NORMAL" || consulta.Cromatica == "Ir a SCT")
                {
                    p.Add(c54);
                }

                if (Convert.ToInt32(consulta.Estereopsis) > 60)
                {
                    p.Add(c55);
                }

                if (consulta4.Grafica != "NORMAL")
                {
                    p.Add(c56);
                }

                if (revision1.Cardiopatia == "SI" || revision1.Infarto == "SI" || revision3.Cardiopatias == "SI")
                {
                    p.Add(c57);
                }


                if (revision5.Romberg == "SI" || revision5.PuntaNariz == "SI")
                {
                    p.Add(c58);
                }

                if (revision1.Tiroides == "SI")
                {
                    p.Add(c59);
                }

                if (revision5.Estrabismo == "SI")
                {
                    p.Add(c60);
                }

                if (consulta4.Patologia == "Media unilateral/severa contralateral")
                {
                    p.Add(c61);
                }

                p.Add(c62);
                p.Add(c22);

                doc.Add(p);
                doc.Add(pr);
                PdfPTable table = new PdfPTable(3);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.WidthPercentage = 75f;
                table.AddCell(codeQRImage);
                table.AddCell("");

                System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
                System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

                doc.Add(table);
                doc.Close();
                wri.Close();


                var pdf = mem.ToArray();
                string file = Convert.ToBase64String(pdf);

                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

                mem.Close();

                byte[] bytes2 = mem.ToArray();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
                Response.BinaryWrite(bytes2);
                Response.End();

                return File(bytes2, "application/pdf");
            }

            return Redirect("Index");
        }
    }
}

