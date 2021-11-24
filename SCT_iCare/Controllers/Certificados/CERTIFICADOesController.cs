using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iText.Barcodes;
using iText.Kernel.Pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SCT_iCare;
using PdfFileWriter;
using PagedList;

namespace SCT_iCare.Controllers.Certificados
{
    public class CERTIFICADOesController : Controller
    {
        private GMIEntities db = new GMIEntities();

        // GET: CERTIFICADOes
        public ActionResult Index(/*int? pageSize, int? page*/)
        {
            //pageSize = (pageSize ?? 10);
            //page = (page ?? 1);
            //ViewBag.PageSize = pageSize;

            return View(db.CERTIFICADO.ToList());
            //return View(db.CERTIFICADO.OrderByDescending(i => i.idCertificado).ToPagedList(page.Value, pageSize.Value).);
        }

        // GET: CERTIFICADOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
            CERTIFICADO cERTIFICADO = db.CERTIFICADO.Find(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICADO);
        }

        // GET: CERTIFICADOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CERTIFICADOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string usuario, string nombre, string paterno, string materno, DateTime nacimiento, DateTime toma, string resultado, string sexo, string pasaporte/*[Bind(Include = "idCertificado,Nombre,Pasaporte,Respuesta,FechaNacimiento,FechaToma,Sexo")] CERTIFICADO cERTIFICADO*/)
        {
            CERTIFICADO cer = new CERTIFICADO();

            cer.Nombre = nombre + " " + paterno + " " + materno;
            cer.Sexo = sexo;
            cer.FechaNacimiento = nacimiento;
            cer.FechaToma = toma;
            cer.Respuesta = resultado;
            cer.Pasaporte = pasaporte;
            cer.Usuario = usuario;

            if (ModelState.IsValid)
            {
                db.CERTIFICADO.Add(cer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cer);
        }

        // GET: CERTIFICADOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICADO cERTIFICADO = db.CERTIFICADO.Find(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICADO);
        }

        // POST: CERTIFICADOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCertificado,Nombre,Pasaporte,Respuesta,FechaNacimiento,FechaToma,Sexo")] CERTIFICADO cERTIFICADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cERTIFICADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cERTIFICADO);
        }

        // GET: CERTIFICADOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICADO cERTIFICADO = db.CERTIFICADO.Find(id);
            if (cERTIFICADO == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICADO);
        }

        // POST: CERTIFICADOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CERTIFICADO cERTIFICADO = db.CERTIFICADO.Find(id);
            db.CERTIFICADO.Remove(cERTIFICADO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

            var img1 = requiredPath + ConfigurationManager.AppSettings["img1"];
            var img2P = requiredPath + ConfigurationManager.AppSettings["img2P"];
            var img2N = requiredPath + ConfigurationManager.AppSettings["img2N"];
            var img3 = requiredPath + ConfigurationManager.AppSettings["img3"];
            var img4 = requiredPath + ConfigurationManager.AppSettings["img4"];
            var imgi1 = requiredPath + ConfigurationManager.AppSettings["imgi1"];
            var imgi2P = requiredPath + ConfigurationManager.AppSettings["imgi2P"];
            var imgi2N = requiredPath + ConfigurationManager.AppSettings["imgi2N"];
            var imgi3 = requiredPath + ConfigurationManager.AppSettings["imgi3"];
            var imgi4 = requiredPath + ConfigurationManager.AppSettings["imgi4"];


            var cer = (from c in db.CERTIFICADO where c.idCertificado == id select c).FirstOrDefault();

            //List<GetPDFIng_Result> Lista = new List<GetPDFIng_Result>();
            //EntityLayer.Solicitudes.EntSolicitudTM soli = new EntityLayer.Solicitudes.EntSolicitudTM();


                /*----------------------------------PDFs Certificados----------------------------------*/

                if (true)
                {
                    System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
                    System.Console.WriteLine("Entra al llenado del pdf");

                    string nombre = cer.Nombre;
                    string fecha = Convert.ToDateTime(cer.FechaToma).ToString("dd-MM-yyyy");
                    string fechanac = Convert.ToDateTime(cer.FechaNacimiento).ToString("dd-MM-yyyy");
                    string res = cer.Respuesta;
                    string sexo = cer.Sexo;
                    string pasaporte = "";
                    string noEstudio = (cer.idCertificado + 1000).ToString();

                    if (cer.Pasaporte != null)
                    {
                        pasaporte = cer.Pasaporte;
                    }


                    //Comienza el armado del archivo
                    Document doc = new Document(PageSize.A4);
                    var mem = new MemoryStream();
                    iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, mem);
                    //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                    //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                    doc.Open();
                    System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                    System.Console.WriteLine("Se prepara para obtener las imagenes");

                    if (cer.Respuesta == "Negativo")
                    {
                        iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(img1);
                        iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(img2N);
                        iTextSharp.text.Image PNG3 = iTextSharp.text.Image.GetInstance(img3);
                        iTextSharp.text.Image PNG4 = iTextSharp.text.Image.GetInstance(img4);

                        System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                        System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                        PNG1.Alignment = Image.ALIGN_CENTER;
                        PNG2.Alignment = Image.ALIGN_CENTER;
                        PNG3.Alignment = Image.ALIGN_CENTER;
                        PNG4.Alignment = Image.ALIGN_CENTER;
                        PNG1.ScaleAbsolute(650, 120);
                        PNG2.ScaleAbsolute(600, 250);
                        PNG3.ScaleAbsolute(600, 100);
                        var color = new BaseColor(128, 128, 128);
                        var resultado = new BaseColor(0, 0, 255);
                        string coloro = "";
                        iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                        iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                        var font = FontFactory.GetFont(coloro, 11, Font.NORMAL, color);
                        var fontr = FontFactory.GetFont(coloro, 11, Font.NORMAL, resultado);
                        Chunk c1 = new Chunk("N° de estudio:" + noEstudio + "                                            " + "Fecha de Toma:" + fecha + "\n", font);
                        Chunk c2 = new Chunk("Paciente:" + nombre + "\n", font);
                        Chunk c3 = new Chunk("Fecha de Nacimiento:" + fechanac + "\n", font);
                        Chunk c4 = new Chunk("Sexo:" + sexo + "                                                          " + "Número de Pasaporte:" + pasaporte + "\n", font);
                        Chunk c5 = new Chunk("Médico: Dra. Daniela Carolina Navarro Fabila", font);
                        Chunk cr1 = new Chunk("RESULTADO:  ", font);
                        Chunk cr2 = new Chunk("Ag SARS COV-2 " + res + "", fontr); //Resultado en azul

                        System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                        System.Console.WriteLine("Se prepara para generar el QR");

                        iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                        Image codeQRImage = barcodeQRCode.GetImage();
                        codeQRImage.ScaleAbsolute(150, 150);
                        codeQRImage.Alignment = Image.ALIGN_LEFT;
                        doc.Add(PNG1);
                        p.Add(c1);
                        p.Add(c2);
                        p.Add(c3);
                        p.Add(c4);
                        p.Add(c5);
                        pr.Add(cr1);
                        pr.Add(cr2);
                        doc.Add(p);
                        doc.Add(PNG2);
                        doc.Add(pr);
                        doc.Add(PNG3);
                        PdfPTable table = new PdfPTable(3);
                        table.DefaultCell.Border = Rectangle.NO_BORDER;
                        table.WidthPercentage = 75f;
                        table.AddCell(codeQRImage);
                        table.AddCell(PNG4);
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
                        //HttpContext.Response.Write(mem);
                    Response.BinaryWrite(bytes2);
                    //Response.Flush();
                    Response.End();

                    //return RedirectToAction("Index", "CERTIFICADOes");
                    return File(bytes2, "application/pdf");


                    //var bytesBinary = ePI.Dictamen;
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
                    //Response.BinaryWrite(bytesBinary);
                    //Response.End();

                    //    Response.ContentType = "application/pdf";
                    //    Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
                    //    Response.BinaryWrite(bytesBinary);
                    //    //Response.End();
                    //HttpContext.ApplicationInstance.CompleteRequest();

                    //return RedirectToAction("capturaSucursal", "EPIs", new { sucursal = ePI.Sucursal.ToString() });

                    //var bytesBinary = ePI.Dictamen;
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
                    //Response.BinaryWrite(bytesBinary);
                    //Response.End();

                }
                else
                {
                    iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(img1);
                    iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(img2P);
                    iTextSharp.text.Image PNG3 = iTextSharp.text.Image.GetInstance(img3);
                    iTextSharp.text.Image PNG4 = iTextSharp.text.Image.GetInstance(img4);

                    System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                    System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                    PNG1.Alignment = Image.ALIGN_CENTER;
                    PNG2.Alignment = Image.ALIGN_CENTER;
                    PNG3.Alignment = Image.ALIGN_CENTER;
                    PNG4.Alignment = Image.ALIGN_CENTER;
                    PNG1.ScaleAbsolute(650, 120);
                    PNG2.ScaleAbsolute(600, 250);
                    PNG3.ScaleAbsolute(600, 100);
                    var color = new BaseColor(128, 128, 128);
                    var resultado = new BaseColor(0, 0, 255);
                    string coloro = "";
                    iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                    iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                    var font = FontFactory.GetFont(coloro, 11, Font.NORMAL, color);
                    var fontr = FontFactory.GetFont(coloro, 11, Font.NORMAL, resultado);
                    Chunk c1 = new Chunk("N° de estudio:" + noEstudio + "                                            " + "Fecha de Toma:" + fecha + "\n", font);
                    Chunk c2 = new Chunk("Paciente:" + nombre + "\n", font);
                    Chunk c3 = new Chunk("Fecha de Nacimiento:" + fechanac + "\n", font);
                    Chunk c4 = new Chunk("Sexo:" + sexo + "                                                          " + "Número de Pasaporte:" + pasaporte + "\n", font);
                    Chunk c5 = new Chunk("Médico: Dra. Daniela Carolina Navarro Fabila", font);
                    Chunk cr1 = new Chunk("RESULTADO:  ", font);
                    Chunk cr2 = new Chunk("Ag SARS COV-2 " + res + "", fontr); //Resultado en azul

                    System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                    System.Console.WriteLine("Se prepara para generar el QR");

                    iTextSharp.text.pdf.BarcodeQRCode barcodeQRCode = new iTextSharp.text.pdf.BarcodeQRCode("resultados.medicinagmi.mx/Resultados/Resultado?idSol=" + noEstudio, 1000, 1000, null);
                    Image codeQRImage = barcodeQRCode.GetImage();
                    codeQRImage.ScaleAbsolute(150, 150);
                    codeQRImage.Alignment = Image.ALIGN_LEFT;
                    doc.Add(PNG1);
                    p.Add(c1);
                    p.Add(c2);
                    p.Add(c3);
                    p.Add(c4);
                    p.Add(c5);
                    pr.Add(cr1);
                    pr.Add(cr2);
                    doc.Add(p);
                    doc.Add(PNG2);
                    doc.Add(pr);
                    doc.Add(PNG3);
                    PdfPTable table = new PdfPTable(3);
                    table.DefaultCell.Border = Rectangle.NO_BORDER;
                    table.WidthPercentage = 75f;
                    table.AddCell(codeQRImage);
                    table.AddCell(PNG4);
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

                    //if (file != null && file.ContentLength > 0)
                    //{
                    //    var fileName = Path.GetFileName(file.FileName);

                    //    byte[] bytes;
                    //    using (BinaryReader br = new BinaryReader(file.InputStream))
                    //    {
                    //        bytes = br.ReadBytes(file.ContentLength);

                    //        bytes2 = bytes;
                    //    }
                    //}

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + nombre + ".pdf");
                    //HttpContext.Response.Write(mem);
                    Response.BinaryWrite(bytes2);
                    //Response.Flush();
                    Response.End();

                    //return RedirectToAction("Index", "CERTIFICADOes");
                    return File(bytes2, "application/pdf");

                    //var bytesBinary = ePI.Dictamen;
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=" + ePI.NombrePaciente + ".pdf");
                    //Response.BinaryWrite(bytesBinary);
                    //Response.End();
                }



                /*----------------------------------PDFs Certificados----------------------------------*/


                //Lista = new clsFachada().listPdfPruebaRapida(id);

                //System.Diagnostics.Debug.WriteLine("Se obtiene la lista para el llenado del pdf");
                //System.Console.WriteLine("Se obtiene la lista para el llenado del pdf");

                ////string[] array = new string[Lista.Count];

                //int localid = 0;
                //for (int i = 0; i < Lista.Count; i++)
                //{
                //    if (localid != Lista[i].idSolicitudSCE)
                //    {
                //        for (int o = 0; o < Lista.Count; o++)
                //        {
                //            array[o] = Convert.ToString(Lista[o].Respuesta);
                //        }

                //        bool english = false;
                //        if (Lista.Count != 5 && english == false) //Positivo en español
                //        {
                //            System.Diagnostics.Debug.WriteLine("Entra al llenado del pdf");
                //            System.Console.WriteLine("Entra al llenado del pdf");

                //            string nombre = cer.Nombre;
                //            string fecha = cer.FechaToma.ToString();
                //            string fechanac = cer.FechaNacimiento.ToString();
                //            string res = cer.Respuesta;
                //            string sexo = cer.Sexo;
                //            string pasaporte = "";

                //            if(cer.Pasaporte != null)
                //            {
                //                pasaporte = cer.Pasaporte;
                //            }


                //            //Comienza el armado del archivo
                //            Document doc = new Document(PageSize.A4);
                //            var mem = new MemoryStream();
                //            PdfWriter wri = PdfWriter.GetInstance(doc, mem);
                //            //PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Response.OutputStream);
                //            //PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\punks\Documentos\" + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                //            doc.Open();
                //            System.Diagnostics.Debug.WriteLine("Se prepara para obtener las imagenes");
                //            System.Console.WriteLine("Se prepara para obtener las imagenes");

                //            if (res == "Positivo")
                //            {
                //                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(img1);
                //                iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(img2P);
                //                iTextSharp.text.Image PNG3 = iTextSharp.text.Image.GetInstance(img3);
                //                iTextSharp.text.Image PNG4 = iTextSharp.text.Image.GetInstance(img4);

                //                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                //                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                //                PNG1.Alignment = Image.ALIGN_CENTER;
                //                PNG2.Alignment = Image.ALIGN_CENTER;
                //                PNG3.Alignment = Image.ALIGN_CENTER;
                //                PNG4.Alignment = Image.ALIGN_CENTER;
                //                PNG1.ScaleAbsolute(650, 120);
                //                PNG2.ScaleAbsolute(600, 250);
                //                PNG3.ScaleAbsolute(600, 100);
                //                var color = new BaseColor(128, 128, 128);
                //                var resultado = new BaseColor(0, 0, 255);
                //                string coloro = "";
                //                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                //                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                //                var font = FontFactory.GetFont(coloro, 11, Font.NORMAL, color);
                //                var fontr = FontFactory.GetFont(coloro, 11, Font.NORMAL, resultado);
                //                Chunk c1 = new Chunk("N° de estudio:" + Lista[i].idSolicitudSCE + "                                        " + "Fecha de Toma:" + fecha + "\n", font);
                //                Chunk c2 = new Chunk("Paciente:" + nombre + "\n", font);
                //                Chunk c3 = new Chunk("Fecha de Nacimiento:" + fechanac + "                 " + "Fecha de Reporte:" + fechaReporte + "\n", font);
                //                Chunk c4 = new Chunk("Sexo:" + array[3] + "                                                          " + "Número de Pasaporte:" + pasaporte + "\n", font);
                //                Chunk c5 = new Chunk("Médico: Dra. Daniela Carolina Navarro Fabila", font);
                //                Chunk cr1 = new Chunk("RESULTADO:  ", font);
                //                Chunk cr2 = new Chunk("Ag SARS COV-2 " + Lista[i].Resultado + "", fontr); //Resultado en azul

                //                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                //                System.Console.WriteLine("Se prepara para generar el QR");

                //                BarcodeQRCode barcodeQRCode = new BarcodeQRCode("icare.com.mx/Client?idSol=" + Lista[i].idSolicitudes, 1000, 1000, null);
                //                Image codeQRImage = barcodeQRCode.GetImage();
                //                codeQRImage.ScaleAbsolute(150, 150);
                //                codeQRImage.Alignment = Image.ALIGN_LEFT;
                //                doc.Add(PNG1);
                //                p.Add(c1);
                //                p.Add(c2);
                //                p.Add(c3);
                //                p.Add(c4);
                //                p.Add(c5);
                //                pr.Add(cr1);
                //                pr.Add(cr2);
                //                doc.Add(p);
                //                doc.Add(PNG2);
                //                doc.Add(pr);
                //                doc.Add(PNG3);
                //                PdfPTable table = new PdfPTable(3);
                //                table.DefaultCell.Border = Rectangle.NO_BORDER;
                //                table.WidthPercentage = 75f;
                //                table.AddCell(codeQRImage);
                //                table.AddCell(PNG4);
                //                table.AddCell("");

                //                System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
                //                System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

                //                doc.Add(table);
                //                doc.Close();
                //                wri.Close();


                //                var pdf = mem.ToArray();
                //                string file = Convert.ToBase64String(pdf);

                //                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
                //                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

                //                mem.Close();

                //                //Termina de unir qr y pdf principal
                //                string fileName = "resultado_" + Lista[i].idSolicitudSCE + ".pdf";
                //                string correo = Lista[i].CorreoElectronico;
                //                Console.WriteLine("Corre" + correo);
                //                Console.WriteLine("Se envia Correo");
                //                if (email != null)
                //                {
                //                    email = "certificadosicare@gmail.com"; //Se hardcorea
                //                    System.Diagnostics.Debug.WriteLine("Se envia el documento a " + email);
                //                    System.Console.WriteLine("Se envia el documento a " + email);

                //                    Response.ContentType = "application/pdf";

                //                    sendMailAsync2(email, nombre, file, fileName).Wait();
                //                }
                //                else
                //                {
                //                    correo = "certificadosicare@gmail.com"; //Se hardcorea
                //                    System.Diagnostics.Debug.WriteLine("Se envia el documento al correo default");
                //                    System.Console.WriteLine("Se envia el documento al correo default");

                //                    Response.ContentType = "application/pdf";
                //                    Response.AddHeader("content-dispotition", "attachment;filename=Certificado-" + Lista[i].idSolicitudSCE + ".pdf");
                //                    HttpContext.Response.Write(doc);
                //                    Response.Flush();
                //                    Response.End();

                //                    sendMailAsync2(correo, nombre, file, fileName).Wait();
                //                }
                //            }
                //            else
                //            {
                //                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(img1);
                //                iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(img2N);
                //                iTextSharp.text.Image PNG3 = iTextSharp.text.Image.GetInstance(img3);
                //                iTextSharp.text.Image PNG4 = iTextSharp.text.Image.GetInstance(img4);

                //                System.Diagnostics.Debug.WriteLine("Se obtienen las imagenes" + PNG1.Url);
                //                System.Console.WriteLine("Se obtienen las imagenes" + PNG1.Url);

                //                PNG1.Alignment = Image.ALIGN_CENTER;
                //                PNG2.Alignment = Image.ALIGN_CENTER;
                //                PNG3.Alignment = Image.ALIGN_CENTER;
                //                PNG4.Alignment = Image.ALIGN_CENTER;
                //                PNG1.ScaleAbsolute(650, 120);
                //                PNG2.ScaleAbsolute(600, 250);
                //                PNG3.ScaleAbsolute(600, 100);
                //                var color = new BaseColor(128, 128, 128);
                //                var resultado = new BaseColor(0, 0, 255);
                //                string coloro = "";
                //                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                //                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                //                var font = FontFactory.GetFont(coloro, 11, Font.NORMAL, color);
                //                var fontr = FontFactory.GetFont(coloro, 11, Font.NORMAL, resultado);
                //                Chunk c1 = new Chunk("N° de estudio:" + Lista[i].idSolicitudSCE /*2068 SolicitudCSE*/ + "                                        " + "Fecha de Toma:" + fecha + "\n", font);
                //                Chunk c2 = new Chunk("Paciente:" + nombre + "\n", font);
                //                Chunk c3 = new Chunk("Fecha de Nacimiento:" + fechanac + "                 " + "Fecha de Reporte:" + fechaReporte + "\n", font);
                //                Chunk c4 = new Chunk("Sexo:" + array[3] + "                                                          " + "Número de Pasaporte:" + pasaporte + "\n", font);
                //                Chunk c5 = new Chunk("Médico: Dr. Daniela Carolina Navarro Fabila", font);
                //                Chunk cr1 = new Chunk("RESULTADO:  ", font);
                //                Chunk cr2 = new Chunk("Ag SARS COV-2 " + Lista[i].Resultado + "", fontr); //Resultado en azul

                //                System.Diagnostics.Debug.WriteLine("Se prepara para generar el QR");
                //                System.Console.WriteLine("Se prepara para generar el QR");

                //                iText.Barcodes.BarcodeQRCode barcodeQRCode = new BarcodeQRCode("icare.com.mx/Client?idSol=" + Lista[i].idSolicitudes  /*1432 Aquí va el id de la solicitud*/, 1000, 1000, null);
                //                Image codeQRImage = barcodeQRCode.GetImage();
                //                codeQRImage.ScaleAbsolute(150, 150);
                //                codeQRImage.Alignment = Image.ALIGN_LEFT;
                //                doc.Add(PNG1);
                //                p.Add(c1);
                //                p.Add(c2);
                //                p.Add(c3);
                //                p.Add(c4);
                //                p.Add(c5);
                //                pr.Add(cr1);
                //                pr.Add(cr2);
                //                doc.Add(p);
                //                doc.Add(PNG2);
                //                doc.Add(pr);
                //                doc.Add(PNG3);
                //                PdfPTable table = new PdfPTable(3);
                //                table.DefaultCell.Border = Rectangle.NO_BORDER;
                //                table.WidthPercentage = 75f;
                //                table.AddCell(codeQRImage);
                //                table.AddCell(PNG4);
                //                table.AddCell("");

                //                System.Diagnostics.Debug.WriteLine("Se TERMINA EL DOCUMENTO");
                //                System.Console.WriteLine("Se TERMINA EL DOCUMENTO");

                //                doc.Add(table);
                //                doc.Close();
                //                wri.Close();

                //                var pdf = mem.ToArray();
                //                string file = Convert.ToBase64String(pdf);

                //                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");
                //                System.Diagnostics.Debug.WriteLine("Se convierte el documento a base 64");

                //                mem.Close();

                //                //Termina de unir qr y pdf principal
                //                string fileName = "resultado_" + Lista[i].idSolicitudSCE + ".pdf";
                //                string correo = "";//Lista[i].CorreoElectronico;
                //                Console.WriteLine("Corre" + correo);
                //                Console.WriteLine("Se envia Correo");
                //                if (email != null)
                //                {
                //                    email = "certificadosicare@gmail.com"; //Se hardcorea
                //                    System.Diagnostics.Debug.WriteLine("Se envia el documento a " + email);
                //                    System.Console.WriteLine("Se envia el documento a " + email);




                //                    sendMailAsync2(email, nombre, file, fileName).Wait();
                //                }
                //                else
                //                {
                //                    correo = "certificadosicare@gmail.com"; //Se hardcorea
                //                    System.Diagnostics.Debug.WriteLine("Se envia el documento al correo default");
                //                    System.Console.WriteLine("Se envia el documento al correo default");

                //                    sendMailAsync2(correo, nombre, file, fileName).Wait();
                //                }
                //            }
                //        }
                //        else
                //        {
                //            if (/*array[6] != null*/ english == true)
                //            {
                //                string nombre = "";
                //                string fecha = "";
                //                string fechanac = "";
                //                string res = "";
                //                nombre = array[1] + " " + array[2] + " " + array[0];
                //                string pasaporte = array[5];
                //                string fechaReporte = DateTime.Now.ToString();
                //                fecha = Convert.ToString(Lista[i].FechaAgenda).Substring(0, 10);
                //                fechanac = array[4];
                //                localid = Convert.ToInt32(Lista[i].idSolicitudSCE);
                //                res = Lista[i].Resultado;

                //                //Comiensa el armado del archivo
                //                Document doc = new Document(PageSize.A4);
                //                var mem = new MemoryStream();
                //                PdfWriter wri = PdfWriter.GetInstance(doc, mem);
                //                //PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(pathDestino + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                //                doc.Open();
                //                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(imgi1);
                //                iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(imgi2N);
                //                iTextSharp.text.Image PNG3 = iTextSharp.text.Image.GetInstance(imgi3);
                //                iTextSharp.text.Image PNG4 = iTextSharp.text.Image.GetInstance(imgi4);
                //                PNG1.Alignment = Image.ALIGN_CENTER;
                //                PNG2.Alignment = Image.ALIGN_CENTER;
                //                PNG3.Alignment = Image.ALIGN_CENTER;
                //                PNG4.Alignment = Image.ALIGN_CENTER;
                //                PNG1.ScaleAbsolute(650, 120);
                //                PNG2.ScaleAbsolute(600, 250);
                //                PNG3.ScaleAbsolute(600, 100);
                //                var color = new BaseColor(128, 128, 128);
                //                var resultado = new BaseColor(0, 0, 255);
                //                string coloro = "";
                //                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                //                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                //                var font = FontFactory.GetFont(coloro, 11, Font.NORMAL, color);
                //                var fontr = FontFactory.GetFont(coloro, 11, Font.NORMAL, resultado);
                //                Chunk c1 = new Chunk("TEST NUMBER:" + Lista[i].idSolicitudSCE + "                                            " + "TEST DATE:" + fecha + "\n", font);
                //                Chunk c2 = new Chunk("PATIENT:" + nombre + "\n", font);
                //                Chunk c3 = new Chunk("BIRTHDAY DATE:" + fechanac + "                 " + "REPORT DATE:" + fechaReporte + "\n", font);
                //                Chunk c4 = new Chunk("GENDER:" + array[3] + "                                                    " + "PASSPORT:" + pasaporte + "\n", font);
                //                Chunk c5 = new Chunk("DOCTOR: Dr. Daniela Carolina Navarro Fabila", font);
                //                Chunk cr1 = new Chunk("RESULT:  ", font);
                //                Chunk cr2 = new Chunk(Lista[i].Resultado + "", fontr);
                //                BarcodeQRCode barcodeQRCode = new BarcodeQRCode("icare.com.mx/Client?idSol=" + Lista[i].idSolicitudes, 1000, 1000, null);
                //                Image codeQRImage = barcodeQRCode.GetImage();
                //                codeQRImage.ScaleAbsolute(150, 150);
                //                codeQRImage.Alignment = Image.ALIGN_LEFT;
                //                doc.Add(PNG1);
                //                p.Add(c1);
                //                p.Add(c2);
                //                p.Add(c3);
                //                p.Add(c4);
                //                p.Add(c5);
                //                pr.Add(cr1);
                //                pr.Add(cr2);
                //                doc.Add(p);
                //                doc.Add(PNG2);
                //                doc.Add(pr);
                //                doc.Add(PNG3);
                //                PdfPTable table = new PdfPTable(3);
                //                table.DefaultCell.Border = Rectangle.NO_BORDER;
                //                table.WidthPercentage = 70f;
                //                table.AddCell(codeQRImage);
                //                table.AddCell(PNG4);
                //                table.AddCell("");

                //                doc.Add(table);
                //                doc.Close();
                //                wri.Close();

                //                var pdf = mem.ToArray();
                //                string file = Convert.ToBase64String(pdf);
                //                mem.Close();

                //                //Termina de unir qr y pdf principal
                //                string fileName = "resultado_" + Lista[i].idSolicitudSCE + ".pdf";
                //                string correo = "";// Lista[i].CorreoElectronico;
                //                Console.WriteLine("Corre" + correo);
                //                Console.WriteLine("Se envia Correo");
                //                if (email != null)
                //                {
                //                    email = "certificadosicare@gmail.com"; //Se hardcorea
                //                    sendMailAsync2(email, nombre, file, fileName).Wait();
                //                }
                //                else
                //                {
                //                    correo = "certificadosicare@gmail.com"; //Se hardcorea
                //                    sendMailAsync2(correo, nombre, file, fileName).Wait();
                //                }
                //            }
                //            else
                //            {

                //                string nombre = "";
                //                string fecha = "";
                //                string fechanac = "";
                //                string res = "";
                //                nombre = array[1] + " " + array[2] + " " + array[0];
                //                string pasaporte = array[6];
                //                fecha = Convert.ToString(Lista[i].FechaAgenda).Substring(0, 10);
                //                string fechaReporte = DateTime.Now.ToString();
                //                fechanac = array[4];
                //                localid = Convert.ToInt32(Lista[i].idSolicitudSCE);
                //                res = Lista[i].Resultado;

                //                //Comiensa el armado del archivo
                //                Document doc = new Document(PageSize.A4);
                //                var mem = new MemoryStream();
                //                PdfWriter wri = PdfWriter.GetInstance(doc, mem);
                //                //PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(pathDestino + "resultado_" + Lista[i].idSolicitudSCE + ".pdf", FileMode.Create));
                //                doc.Open();
                //                iTextSharp.text.Image PNG1 = iTextSharp.text.Image.GetInstance(img1);
                //                iTextSharp.text.Image PNG2 = iTextSharp.text.Image.GetInstance(img2P);
                //                iTextSharp.text.Image PNG3 = iTextSharp.text.Image.GetInstance(img3);
                //                iTextSharp.text.Image PNG4 = iTextSharp.text.Image.GetInstance(img4);
                //                PNG1.Alignment = Image.ALIGN_CENTER;
                //                PNG2.Alignment = Image.ALIGN_CENTER;
                //                PNG3.Alignment = Image.ALIGN_CENTER;
                //                PNG4.Alignment = Image.ALIGN_CENTER;
                //                PNG1.ScaleAbsolute(650, 120);
                //                PNG2.ScaleAbsolute(600, 250);
                //                PNG3.ScaleAbsolute(600, 100);
                //                var color = new BaseColor(128, 128, 128);
                //                var resultado = new BaseColor(0, 0, 255);
                //                string coloro = "";
                //                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                //                iTextSharp.text.Paragraph pr = new iTextSharp.text.Paragraph();
                //                var font = FontFactory.GetFont(coloro, 11, Font.NORMAL, color);
                //                var fontr = FontFactory.GetFont(coloro, 11, Font.NORMAL, resultado);
                //                Chunk c1 = new Chunk("N° de estudio:" + Lista[i].idSolicitudSCE + "                                        " + "Fecha de Toma:" + fecha + "\n", font);
                //                Chunk c2 = new Chunk("Paciente:" + nombre + "\n", font);
                //                Chunk c3 = new Chunk("Fecha de Nacimiento:" + fechanac + "                 " + "Fecha de Reporte:" + fechaReporte + "\n", font);
                //                Chunk c4 = new Chunk("Sexo:" + array[3] + "                                                          " + "Número de Pasaporte:" + pasaporte + "\n", font);
                //                Chunk c5 = new Chunk("Médico: Dr. Daniela Carolina Navarro Fabila", font);
                //                Chunk cr1 = new Chunk("RESULTADO:  ", font);
                //                Chunk cr2 = new Chunk(Lista[i].Resultado + "", fontr);
                //                BarcodeQRCode barcodeQRCode = new BarcodeQRCode("icare.com.mx/Client?idSol=" + Lista[i].idSolicitudes, 1000, 1000, null);
                //                Image codeQRImage = barcodeQRCode.GetImage();
                //                codeQRImage.ScaleAbsolute(150, 150);
                //                codeQRImage.Alignment = Image.ALIGN_LEFT;
                //                doc.Add(PNG1);
                //                p.Add(c1);
                //                p.Add(c2);
                //                p.Add(c3);
                //                p.Add(c4);
                //                pr.Add(cr1);
                //                pr.Add(cr2);
                //                doc.Add(p);
                //                doc.Add(PNG2);
                //                doc.Add(pr);
                //                doc.Add(PNG3);
                //                PdfPTable table = new PdfPTable(3);
                //                table.DefaultCell.Border = Rectangle.NO_BORDER;
                //                table.WidthPercentage = 75f;
                //                table.AddCell(codeQRImage);
                //                table.AddCell(PNG4);
                //                table.AddCell("");

                //                doc.Add(table);
                //                doc.Close();
                //                wri.Close();

                //                var pdf = mem.ToArray();
                //                string file = Convert.ToBase64String(pdf);
                //                mem.Close();

                //                //Termina de unir qr y pdf principal
                //                string fileName = "resultado_" + Lista[i].idSolicitudSCE + ".pdf";
                //                string correo = "";//Lista[i].CorreoElectronico;
                //                Console.WriteLine("Corre" + correo);
                //                Console.WriteLine("Se envia Correo");
                //                if (email != null)
                //                {
                //                    email = "certificadosicare@gmail.com"; //Se hardcorea
                //                    sendMailAsync2(email, nombre, file, fileName).Wait();
                //                }
                //                else
                //                {
                //                    correo = "certificadosicare@gmail.com"; //Se hardcorea
                //                    sendMailAsync2(correo, nombre, file, fileName).Wait();
                //                }
                //            }
                //        }
                //    }
                //}

            }

            return Redirect("Index");
        }
    }
}
