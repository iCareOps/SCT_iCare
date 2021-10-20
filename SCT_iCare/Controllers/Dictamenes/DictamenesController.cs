using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Controllers.Dictamenes
{
    public class DictamenesController : Controller
    {
        GMIEntities db = new GMIEntities();

        // GET: Dictamenes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Citas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create1(string nombre, string usuario, string sucursal, string cantidad, string cantidadAereo, string referido)
        {
            PacienteESP paciente1 = new PacienteESP();

            string canal = null;

            int cantidadN;
            int cantidadA;

            if (cantidad == "")
            {
                cantidadN = 0;
            }
            else
            {
                cantidadN = Convert.ToInt32(cantidad);
            }

            if (cantidadAereo == "")
            {
                cantidadA = 0;
            }
            else
            {
                cantidadA = Convert.ToInt32(cantidadAereo);
            }


            if ((cantidadN + cantidadA) == 1)
            {
                PacienteESP paciente = new PacienteESP();
                paciente.Nombre = nombre.ToUpper()/*.Normalize(System.Text.NormalizationForm.FormD).Replace(@"´¨", "")*/;
                paciente.FechaCita = DateTime.Now;
                paciente.Sucursal = sucursal;
                paciente.Usuario = usuario;
                paciente.ReferidoPor = referido.ToUpper();

                string TIPOLIC = null;
                if (cantidadA != 0)
                {
                    TIPOLIC = "AEREO";
                }
                paciente.TipoLicencia = TIPOLIC;


                if (ModelState.IsValid)
                {
                    db.PacienteESP.Add(paciente);

                    db.SaveChanges();
                }

            }
            else
            {
                //return View(detallesOrden);
                for (int n = 1; n <= Convert.ToInt32((cantidadN + cantidadA)); n++)
                {
                    PacienteESP paciente = new PacienteESP();
                    paciente.Nombre = nombre.ToUpper() + " " + n;
                    paciente.Sucursal = sucursal;
                    paciente.Usuario = usuario;
                    paciente.FechaCita = DateTime.Now;
                    paciente.ReferidoPor = referido.ToUpper();


                    if (n > cantidadN)
                    {
                        paciente.TipoLicencia = "AEREO";
                    }


                    if (ModelState.IsValid)
                    {
                        db.PacienteESP.Add(paciente);
                        db.SaveChanges();
                    }
                }
            }

            return Redirect("Citas"); ;
        }

        [HttpPost]
        public ActionResult SubirHuellas(HttpPostedFileBase h2, HttpPostedFileBase h3, HttpPostedFileBase h7, HttpPostedFileBase h8)
        {
            byte[] bytesh2 = null;
            byte[] bytesh3 = null;
            byte[] bytesh7 = null;
            byte[] bytesh8 = null;

            if (h3 != null && h3.ContentLength > 0)
            {
                var fileName = Path.GetFileName(h3.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(h3.InputStream))
                {
                    bytes = br.ReadBytes(h3.ContentLength);

                    bytesh3 = bytes;
                }
            }

            if (h2 != null && h2.ContentLength > 0)
            {
                var fileName = Path.GetFileName(h2.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(h2.InputStream))
                {
                    bytes = br.ReadBytes(h2.ContentLength);

                    bytesh2 = bytes;
                }
            }

            if (h7 != null && h7.ContentLength > 0)
            {
                var fileName = Path.GetFileName(h7.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(h7.InputStream))
                {
                    bytes = br.ReadBytes(h7.ContentLength);

                    bytesh7 = bytes;
                }
            }

            if (h8 != null && h8.ContentLength > 0)
            {
                var fileName = Path.GetFileName(h8.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(h8.InputStream))
                {
                    bytes = br.ReadBytes(h8.ContentLength);

                    bytesh8 = bytes;
                }
            }

            HuellasRandom huellas = new HuellasRandom();
            huellas.Huella2 = bytesh2;
            huellas.Huella3 = bytesh3;
            huellas.Huella7 = bytesh7;
            huellas.Huella8 = bytesh8;

            if (ModelState.IsValid)
            {
                db.HuellasRandom.Add(huellas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SubirEKG(HttpPostedFileBase EKG)
        {
            byte[] bytes2 = null;

            if (EKG != null && EKG.ContentLength > 0)
            {
                var fileName = Path.GetFileName(EKG.FileName);

                byte[] bytes;

                using (BinaryReader br = new BinaryReader(EKG.InputStream))
                {
                    bytes = br.ReadBytes(EKG.ContentLength);

                    bytes2 = bytes;
                }
            }

            EKGRandom ekg = new EKGRandom();
            ekg.EKG = bytes2;

            if (ModelState.IsValid)
            {
                db.EKGRandom.Add(ekg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}