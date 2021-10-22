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


        public ActionResult CompletarDatos(int? id, string nombre, string estatura, string curp, string numero, 
            string doctor, string tipoL, string tipoT, HttpPostedFileBase file,
            HttpPostedFileBase documentos, HttpPostedFileBase declaracion, HttpPostedFileBase carta, HttpPostedFileBase glucosilada)
        {
            var revisionPacienteESP = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            int numeroRandom = 0;
            double numeroDecimalRandom = 0;
            double decimalRandom = 0.00;
            double altura = 1.74;
            double peso = 0.00;
            float cadenaPeso;

            for(int i  = 0; i < 10; i++)
            {
                Random ran = new Random();
                numeroRandom = ran.Next(57);

                numeroDecimalRandom = ran.Next(2223, 2878)/100.00;

                peso = numeroDecimalRandom * (altura * altura);
                cadenaPeso = (float)(Math.Round((double)peso, 2));
            }

            if(nombre != "")
            {
                revisionPacienteESP.Nombre = nombre != "" ? nombre : revisionPacienteESP.Nombre;
            }

            if(estatura != "")
            {
                revisionPacienteESP.Estatura = estatura != "" ? estatura: revisionPacienteESP.Estatura;
            }

            if (curp != "")
            {
                revisionPacienteESP.CURP = curp.ToUpper() != "" ? curp : revisionPacienteESP.CURP;
            }

            if (numero != "")
            {
                revisionPacienteESP.NoExpediente = numero != "" ? numero : revisionPacienteESP.NoExpediente;
            }

            if (doctor != "")
            {
                revisionPacienteESP.Doctor = doctor != "" ? doctor : revisionPacienteESP.Doctor;
            }

            if (tipoL != "")
            {
                revisionPacienteESP.TipoLicencia = tipoL != "" ? tipoL : revisionPacienteESP.TipoLicencia;
            }

            if (tipoT != "")
            {
                revisionPacienteESP.TipoTramite = tipoT != "" ? tipoT : revisionPacienteESP.TipoTramite;
            }

            byte[] bytes2 = null;
            if (file != null)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(file.InputStream))
                    {
                        bytes = br.ReadBytes(file.ContentLength);
                        bytes2 = bytes;
                    }
                }
            }

            byte[] bytesDOCS = null;
            if (documentos != null)
            {
                if (documentos != null && documentos.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(documentos.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(documentos.InputStream))
                    {
                        bytes = br.ReadBytes(documentos.ContentLength);
                        bytesDOCS = bytes;
                    }
                }
            }

            byte[] bytesCARTA = null;
            if (carta != null)
            {
                if (carta != null && carta.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(carta.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(carta.InputStream))
                    {
                        bytes = br.ReadBytes(carta.ContentLength);
                        bytesCARTA = bytes;
                    }
                }
            }

            byte[] bytesGLUCO = null;
            if (glucosilada != null)
            {
                if (glucosilada != null && glucosilada.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(glucosilada.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(glucosilada.InputStream))
                    {
                        bytes = br.ReadBytes(glucosilada.ContentLength);
                        bytesGLUCO = bytes;
                    }
                }
            }

            byte[] bytesDEC = null;
            if (declaracion != null)
            {
                if (declaracion != null && declaracion.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(declaracion.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(declaracion.InputStream))
                    {
                        bytes = br.ReadBytes(declaracion.ContentLength);
                        bytesDEC = bytes;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var revisionFoto = (from i in db.FotoPacienteESP where i.idPacienteESP == id select i.idFoto).FirstOrDefault();
                var foto = db.FotoPacienteESP.Find(revisionFoto);
                FotoPacienteESP nuevaFoto = new FotoPacienteESP();

                if(revisionFoto != 0 && file != null)
                {
                    foto.FotoESP = bytes2;
                    db.Entry(foto).State = EntityState.Modified;
                }
                else
                {
                    if(file != null)
                    {
                        nuevaFoto.FotoESP = bytes2;
                        nuevaFoto.idPacienteESP = id;
                        db.FotoPacienteESP.Add(nuevaFoto);
                    }
                }

                var revisionDocumentos = (from i in db.DocumentosESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionCartaNO = (from i in db.CartaNoAccidentesESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionDeclaracion = (from i in db.DeclaracionSaludESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();
                var revisionGlucosilada = (from i in db.HemoglobinaGlucosiladaESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault();

                if (revisionDocumentos != null)
                {
                    var idDocumentos = (from i in db.DocumentosESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (documentos != null)
                    {
                        var DOCS = db.DocumentosESP.Find(idDocumentos);
                        DOCS.Documentos = bytesDOCS;
                        db.Entry(DOCS).State = EntityState.Modified;
                    }
                }
                else
                {
                    DocumentosESP docs = new DocumentosESP();
                    if (documentos != null)
                    {
                        docs.Documentos = bytesDOCS;
                        docs.idPacienteESP = id;
                        db.DocumentosESP.Add(docs);
                    }
                }

                if (revisionDeclaracion != null)
                {
                    var idDeclaracion = (from i in db.DeclaracionSaludESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (declaracion != null)
                    {
                        var DEC = db.DeclaracionSaludESP.Find(idDeclaracion);
                        DEC.DeclaracionSalud = bytesDEC;
                        db.Entry(DEC).State = EntityState.Modified;
                    }
                }
                else
                {
                    DeclaracionSaludESP dec = new DeclaracionSaludESP();
                    if (declaracion != null)
                    {
                        dec.DeclaracionSalud = bytesDEC;
                        dec.idPacienteESP = id;
                        db.DeclaracionSaludESP.Add(dec);
                    }
                }

                if (revisionCartaNO != null)
                {
                    var idCartaNo = (from i in db.CartaNoAccidentesESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (carta != null)
                    {
                        var CARTA = db.CartaNoAccidentesESP.Find(idCartaNo);
                        CARTA.CartaNoAccidentes = bytesCARTA;
                        db.Entry(CARTA).State = EntityState.Modified;
                    }
                }
                else
                {
                    CartaNoAccidentesESP car = new CartaNoAccidentesESP();
                    if (carta != null)
                    {
                        car.CartaNoAccidentes = bytesCARTA;
                        car.idPacienteESP = id;
                        db.CartaNoAccidentesESP.Add(car);
                    }
                }

                if (revisionGlucosilada != null)
                {
                    var idGluco = (from i in db.HemoglobinaGlucosiladaESP where i.idPacienteESP == id select i.idDocumentacionESP).FirstOrDefault();
                    if (glucosilada != null)
                    {
                        var GLUCOSILADA = db.HemoglobinaGlucosiladaESP.Find(idGluco);
                        GLUCOSILADA.HemoglobinaGlucosilada = bytesGLUCO;
                        db.Entry(GLUCOSILADA).State = EntityState.Modified;
                    }
                }
                else
                {
                    HemoglobinaGlucosiladaESP hem = new HemoglobinaGlucosiladaESP();
                    if (glucosilada != null)
                    {
                        hem.HemoglobinaGlucosilada = bytesGLUCO;
                        hem.idPacienteESP = id;
                        db.HemoglobinaGlucosiladaESP.Add(hem);
                    }
                }

                db.Entry(revisionPacienteESP).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Citas");
        }

    }
}