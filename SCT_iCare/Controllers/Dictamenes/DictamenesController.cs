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

        public ActionResult Captura()
        {
            return View();
        }


        public ActionResult Dashboard(DateTime? inicio, DateTime? final)
        {
            DateTime thisDate = new DateTime();
            DateTime tomorrowDate = new DateTime();

            DateTime start1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime finish1 = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);

            int nulos = 0;

            if (inicio != null || final != null)
            {
                nulos = 1;
            }

            if (inicio != null)
            {
                DateTime start = Convert.ToDateTime(inicio);
                int year = start.Year;
                int month = start.Month;
                int day = start.Day;

                inicio = new DateTime(year, month, day);
                thisDate = new DateTime(year, month, day);
            }
            if (final != null)
            {
                DateTime finish = Convert.ToDateTime(final).AddDays(1);
                int year = finish.Year;
                int month = finish.Month;
                int day = finish.Day;

                final = new DateTime(year, month, day);
                tomorrowDate = new DateTime(year, month, day);
            }

            inicio = (inicio ?? start1);
            final = (final ?? finish1);

            ViewBag.Inicio = inicio;
            ViewBag.Final = final;
            ViewBag.Estado = nulos;

            ViewBag.Parameter = "";

            return View();
        }

        [HttpPost]
        public ActionResult Create1(string nombre, string usuario, /*string sucursal, */string cantidad, string cantidadAereo, string referido)
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
                //paciente.Sucursal = sucursal;
                paciente.Solicita = usuario;
                paciente.ReferidoPor = referido.ToUpper();

                string TIPOLIC = null;
                if (cantidadA != 0)
                {
                    TIPOLIC = "AEREO";
                }
                else
                {
                    TIPOLIC = "AUTOTRANSPORTE";
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
                    //paciente.Sucursal = sucursal;
                    paciente.Solicita = usuario;
                    paciente.FechaCita = DateTime.Now;
                    paciente.ReferidoPor = referido.ToUpper();


                    if (n > cantidadN)
                    {
                        paciente.TipoLicencia = "AEREO";
                    }
                    else
                    {
                        paciente.TipoLicencia = "AUTOTRANSPORTE";
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


        public ActionResult CompletarDatos(int? id, string nombre, string estatura, string curp, string numero, /*string metra,*/ string genero,
            string doctor, /*string tipoL, */string tipoT, HttpPostedFileBase file,
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

            if(revisionPacienteESP.NoExpediente != null && revisionPacienteESP.TipoLicencia != null && revisionPacienteESP.TipoTramite != null && revisionPacienteESP.Doctor != null 
                && revisionPacienteESP.Estatura != null && revisionPacienteESP.Metra != null 
                && (from i in db.DictamenESP where i.idPacienteESP == id select i.idPacienteESP).FirstOrDefault() == null)
            {
                revisionPacienteESP.EstatusCaptura = "En Proceso";
            }

            if(nombre != "")
            {
                revisionPacienteESP.Nombre = nombre != "" ? nombre.ToUpper() : revisionPacienteESP.Nombre;
            }

            //revisionPacienteESP.Metra = metra == "on" ? revisionPacienteESP.Metra = "SI" : revisionPacienteESP.Metra = null;

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

            //if (tipoL != "")
            //{
            //    revisionPacienteESP.TipoLicencia = tipoL != "" ? tipoL : revisionPacienteESP.TipoLicencia;
            //}

            if (tipoT != "")
            {
                revisionPacienteESP.TipoTramite = tipoT != "" ? tipoT : revisionPacienteESP.TipoTramite;
            }

            if (genero != "")
            {
                revisionPacienteESP.Genero = genero != "" ? genero : revisionPacienteESP.Genero;
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

        public ActionResult Metra(int? id, string usuario)
        {
            var paciente = db.PacienteESP.Find(id);
            paciente.Metra = "SI";
            paciente.Usuario = usuario;

            if(ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Citas");
        }

        public ActionResult IniciarCaptura(int? id, string capturista)
        {
            var paciente = db.PacienteESP.Find(id);

            paciente.EstatusCaptura = "En Proceso";
            paciente.Capturista = capturista;

            if(ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Captura");
        }


        public ActionResult ReportarProblema(int? id, string comentario)
        {
            var paciente = db.PacienteESP.Find(id);

            paciente.EstatusCaptura = "Pausado";
            paciente.Problema = comentario;

            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Captura");
        }

        public ActionResult AbrirElectro()
        {
            var ekg = (from i in db.EKGRandom select i.idEKG).Count();

            Random ran = new Random();
            int numero = ran.Next(ekg);

            TempData["Electro"] = numero;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarElectro()
        {
            var ekg = (from i in db.EKGRandom select i.idEKG).Count();
            Random ran = new Random();
            int numero = ran.Next(ekg);

            var expediente = (from e in db.EKGRandom where e.idEKG == numero select e).FirstOrDefault();

            var bytesBinary = expediente.EKG;
            TempData["Electro"] = null;
            return File(bytesBinary, "application/pdf");
        }


        public ActionResult AbrirDocumentos(int? id)
        {
            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            TempData["Documentos"] = id;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarDocumentos(int? id)
        {
            var expediente = (from e in db.DocumentosESP where e.idPacienteESP == id select e).FirstOrDefault();

            var bytesBinary = expediente.Documentos;
            TempData["Documentos"] = null;
            return File(bytesBinary, "application/pdf");
        }

        public ActionResult AbrirNoAccidentes(int? id)
        {
            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            TempData["NoAccidentes"] = id;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarNoAccidentes(int? id)
        {
            var expediente = (from e in db.CartaNoAccidentesESP where e.idPacienteESP == id select e).FirstOrDefault();

            var bytesBinary = expediente.CartaNoAccidentes;
            TempData["NoAccidentes"] = null;
            return File(bytesBinary, "application/pdf");
        }


        public ActionResult AbrirDeclaracion(int? id)
        {
            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            TempData["Declaracion"] = id;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarDeclaracion(int? id)
        {
            var expediente = (from e in db.DeclaracionSaludESP where e.idPacienteESP == id select e).FirstOrDefault();

            var bytesBinary = expediente.DeclaracionSalud;
            TempData["Declaracion"] = null;
            return File(bytesBinary, "application/pdf");
        }


        public ActionResult AbrirGlucosilada(int? id)
        {
            var paciente = (from i in db.PacienteESP where i.idPacienteESP == id select i).FirstOrDefault();

            TempData["Glucosilada"] = id;
            return RedirectToAction("Captura");
        }

        public FileResult DescargarGlucosilada(int? id)
        {
            var expediente = (from e in db.HemoglobinaGlucosiladaESP where e.idPacienteESP == id select e).FirstOrDefault();

            var bytesBinary = expediente.HemoglobinaGlucosilada;
            TempData["Glucosilada"] = null;
            return File(bytesBinary, "application/pdf");
        }


        [HttpPost]
        public ActionResult Dictaminar(HttpPostedFileBase dictamen, int? id, string usuario)
        {

            byte[] bytes2 = null;
            if (dictamen != null)
            {
                if (dictamen != null && dictamen.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(dictamen.FileName);

                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(dictamen.InputStream))
                    {
                        bytes = br.ReadBytes(dictamen.ContentLength);
                        bytes2 = bytes;
                    }
                }
            }

            DictamenESP dic = new DictamenESP();

            dic.DictamenArchivo = bytes2;
            dic.idPacienteESP = id;

            PacienteESP paciente = db.PacienteESP.Find(id);
            paciente.EstatusCaptura = "Terminado";
            paciente.Capturista = usuario;

            if(ModelState.IsValid)
            {
                db.DictamenESP.Add(dic);
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Captura");
        }

        public ActionResult DescargarPDF(int? id)
        {
            PacienteESP paciente = db.PacienteESP.Find(id);

            var documento = (from d in db.DictamenESP where d.idPacienteESP == id  orderby d.idDictamenESP descending select d.DictamenArchivo).FirstOrDefault();

            var bytesBinary = documento;

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + paciente.Nombre + ".pdf");
            Response.BinaryWrite(bytesBinary);
            Response.End();

            return RedirectToAction("Citas");
        }

    }
}