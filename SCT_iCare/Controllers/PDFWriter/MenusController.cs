using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using iText.Kernel.Pdf;
//using iText.Kernel.Geom;
//using iTextSharp.text;
//using iText.Layout;
using SCT_iCare;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SCT_iCare.Controllers.PDFWriter
{
    public class MenusController : Controller
    {
        private GMIEntities db = new GMIEntities();

        // GET: Menus
        public ActionResult Index()
        {
            return View(db.Menu.ToList());
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMenu,Nombre,Controlador,Action")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMenu,Nombre,Controlador,Action")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menu.Find(id);
            db.Menu.Remove(menu);
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

        public ActionResult DescargarPDF(int? id)
        {
            Menu menu = db.Menu.Find(id);

            //MemoryStream ms = new MemoryStream();

            //PdfWriter pw = new PdfWriter(ms);
            //PdfDocument pdfDocument = new PdfDocument(pw);
            //iTextSharp.text.Document doc = new iText.Layout.Document(pdfDocument, iText.Kernel.Geom.PageSize.LETTER);

            MemoryStream ms = new MemoryStream();
            Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
            PdfWriter pw = PdfWriter.GetInstance(doc, ms);

            doc.Open();

            doc.AddAuthor("dkdkd");
            doc.AddTitle("djdjdjd");

            iTextSharp.text.Font cualquierCosa = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            Chunk c1 = new Chunk("N° de estudio:\n" + "\n");
            Chunk c2 = new Chunk("Paciente:\n" + id+  "\n");
            Chunk c3 = new Chunk("Fecha de Nacimiento:\n" + "\n");

            doc.Add(c1);
            doc.Add(c2);
            doc.Add(c3);

            doc.Close();
            pw.Close();

            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }
    }
}
