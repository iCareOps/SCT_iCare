using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
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


        public ActionResult NoPatologicos(int id)
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
        public ActionResult Guardar_SignosVitales(int id, double sistolica, double diastolica, double cardiaca, double respiratoria, double temperatura, double peso, double estatura, double cintura, double cuello, double grasa)
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
        public ActionResult Guardar_Audiologia(int id, string patologia, string nota, string graficas)
        {
            EPI_Audiologia audio = new EPI_Audiologia();

            audio.Patologia = patologia;
            audio.Grafica = graficas == "on" ? "ALTERADO" : "NORMAL";
            audio.NotaMedica = nota == "on" ? "ALTERADO" : "NORMAL";
            audio.idPaciente = id;

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

        public ActionResult Guardar_Oftalmologia(int id, double izquierdoavc, double izquierdoavm, double izquierdoavl, double derechoavc, double derechoavm, double derechoavl,
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
            oftal.MovimientosOculares = movimientosoculares == "on" ? "NORMAL" : "NO";
            oftal.CampoVisual = campovisual == "on" ? "NORMAL" : "NO";
            oftal.DerechoRP = derechorp == "on" ? "NORMAL" : "NO";
            oftal.IzquierdoRP = izquierdorp == "on" ? "NORMAL" : "NO";
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
        public ActionResult Guardar_Odontologia(int id, string exploracion, string nota)
        {
            EPI_Odontologia odontologia = new EPI_Odontologia();

            odontologia.Exploracion = exploracion == "on" ? "ALTERADO" : "NORMAL";
            odontologia.NotaOdontologica = nota == "" ? null : nota;
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
        public ActionResult Guardar_Heredofamiliares(int id, string madrevive, string padrevive, string hermanosviven, string familiagrave, string diabetes, string arterial, string obesidad, string isquemica, string cerebral, string miocardio, string tiroides, string neoplastica)
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
        public ActionResult Guardar_NoPatologicos(int id, string vacunas, string civil, string religion, string escolaridad, string hijos)
        {
            EPI_A_NoPatologicos nopat = new EPI_A_NoPatologicos();

            nopat.VacunasCompletas = vacunas == "on" ? "SI" : "NO";
            nopat.EstadoCivil = civil;
            nopat.Religion = religion;
            nopat.Escolaridad = escolaridad;
            nopat.Hijos = hijos;
            nopat.idPaciente = id;

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
            string drogas)
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
            lab.HemoglobinaGlucosilada = hemoglobina.ToString();
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

                if (accion == "Liberar como NO APTO" || accion == "Enviar a Revaloración")
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

                if (accion == "Liberar como NO APTO")
                {
                    aptitud.Aptitud = "NO APTO";
                }
                else if (accion == "Liberar Dictamen")
                {
                    aptitud.Aptitud = "APTO";
                }

                if (ModelState.IsValid)
                {
                    db.Entry(aptitud).State = EntityState.Modified;
                    db.SaveChanges();
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

                Chunk c01 = new Chunk("\n", font);
                Chunk c02 = new Chunk("\n", font);
                Chunk c0 = new Chunk("DATOS DEL PACIENTE\n", font);
                Chunk c1 = new Chunk("N° de estudio: " + noEstudio + "\n", font);
                Chunk c2 = new Chunk("Paciente: " + nombre + "\n", font);
                Chunk c3 = new Chunk("No. Expediente: " + noExpediente + "\n", font);
                Chunk c4 = new Chunk("Email: " + email + "\n", font);
                Chunk c5 = new Chunk("Teléfono: " + telefono + "\n", font);
                Chunk c6 = new Chunk("CURP: " + CURP + "\n", font);
                Chunk c7 = new Chunk("Folio: " + Folio + "\n", font);
                Chunk c8 = new Chunk("\n", font);
                Chunk c81 = new Chunk("Código Hash: " + HASH + "\n", font);
                Chunk c82 = new Chunk("\n", font);

                Chunk c10 = new Chunk("DATOS DEL EXAMEN\n", font);
                //Chunk c11 = new Chunk("Sucursal: " + sucursal + "\n", font);
                //Chunk c12 = new Chunk("Doctor Dictaminador: " + doctor + "\n", font);
                Chunk c13 = new Chunk("Tipo de Licencia: " + tipoL + "\n", font);
                Chunk c14 = new Chunk("Tipo de Trámite: " + tipoT + "\n", font);
                Chunk c15 = new Chunk("Fecha de Examen: " + fechaCita + "\n", font);
                Chunk c16 = new Chunk("\n", font);

                Chunk c20 = new Chunk("RESULTADOS\n", font);
                Chunk c201 = new Chunk("APTITUD:\n", font);
                Chunk c21 = new Chunk();

                if (aptitud == "APTO")
                {
                    c21 = new Chunk(aptitud + "\n", fontA);
                }
                else
                {
                    c21 = new Chunk(aptitud + "\n", fontNA);
                }

                Chunk c22 = new Chunk("NOTA MÉDICA: " + dictamenNOTA + "\n", font); //Resultado en azul

                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                System.Console.WriteLine("Se prepara para generar el QR");

                iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                Image codeQRImage = barcodeQRCode.GetImage();
                codeQRImage.ScaleAbsolute(150, 150);
                codeQRImage.Alignment = Image.ALIGN_LEFT;
                //doc.Add(PNG1);
                p.Add(c01);
                p.Add(c02);
                p.Add(c0);
                p.Add(c1);
                p.Add(c2);
                p.Add(c3);
                p.Add(c4);
                p.Add(c5);
                p.Add(c6);
                p.Add(c7);
                p.Add(c8);
                p.Add(c81);
                p.Add(c82);

                p.Add(c10);
                //p.Add(c11);
                //p.Add(c12);
                p.Add(c13);
                p.Add(c14);
                p.Add(c15);
                p.Add(c16);

                p.Add(c20);
                p.Add(c201);
                p.Add(c21);
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