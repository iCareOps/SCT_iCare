using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Contabilidad
{
    public class SkeedaPKController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        public static void GetApiKey()
        {
            conekta.Api.apiKey = ConfigurationManager.AppSettings["conekta"];
            conekta.Api.version = "2.0.0";
            conekta.Api.locale = "es";
        }

        // GET: SkeedaPKs
        public ActionResult Index()
        {
            GetApiKey();

            return View(db.SkeedaPK.ToList());
        }

        // GET: SkeedaPKs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkeedaPK skeedaPK = db.SkeedaPK.Find(id);
            if (skeedaPK == null)
            {
                return HttpNotFound();
            }
            return View(skeedaPK);
        }

        // GET: SkeedaPKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkeedaPKs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSkeeda,Scheduled_start,End,Duration__minutes_,Spaces_count,Type,Title,Price,Payment_status,Holder_first_name,Holder_last_name,Holder_organization,Holder_telephone,Holder_email,Holder_tags,Created,Gateway_charge_reference,Spaces,Mail__Custom_field_1_,Tel__Custom_field_2_,TipoLicenc__Custom_field_3_,TipoTramit__Custom_field_4_,TipoPago__Custom_field_5_,RefPagoOxx__Custom_field_6_,Ord_Num__Custom_field_7_,FecNac__Custom_field_8_,Estado__Custom_field_9_,Ciudad__Custom_field_10_,Visión__Custom_field_11_,Tiroides__Custom_field_12_,Diabetes__Custom_field_13_,Hipertens__Custom_field_14_,Audicion__Custom_field_15_,EnfCronica__Custom_field_16_,Embarazo__Custom_field_17_,Referido__Custom_field_18_,Otro___Custom_field_19_,CheckCita__Custom_field_20_,DrVeracruz__Custom_field_21_,DrSatelite__Custom_field_22_,DrLindavis__Custom_field_23_")] SkeedaPK skeedaPK)
        {
            if (ModelState.IsValid)
            {
                db.SkeedaPK.Add(skeedaPK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skeedaPK);
        }

        // GET: SkeedaPKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkeedaPK skeedaPK = db.SkeedaPK.Find(id);
            if (skeedaPK == null)
            {
                return HttpNotFound();
            }
            return View(skeedaPK);
        }

        // POST: SkeedaPKs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSkeeda,Scheduled_start,End,Duration__minutes_,Spaces_count,Type,Title,Price,Payment_status,Holder_first_name,Holder_last_name,Holder_organization,Holder_telephone,Holder_email,Holder_tags,Created,Gateway_charge_reference,Spaces,Mail__Custom_field_1_,Tel__Custom_field_2_,TipoLicenc__Custom_field_3_,TipoTramit__Custom_field_4_,TipoPago__Custom_field_5_,RefPagoOxx__Custom_field_6_,Ord_Num__Custom_field_7_,FecNac__Custom_field_8_,Estado__Custom_field_9_,Ciudad__Custom_field_10_,Visión__Custom_field_11_,Tiroides__Custom_field_12_,Diabetes__Custom_field_13_,Hipertens__Custom_field_14_,Audicion__Custom_field_15_,EnfCronica__Custom_field_16_,Embarazo__Custom_field_17_,Referido__Custom_field_18_,Otro___Custom_field_19_,CheckCita__Custom_field_20_,DrVeracruz__Custom_field_21_,DrSatelite__Custom_field_22_,DrLindavis__Custom_field_23_")] SkeedaPK skeedaPK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skeedaPK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skeedaPK);
        }

        // GET: SkeedaPKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkeedaPK skeedaPK = db.SkeedaPK.Find(id);
            if (skeedaPK == null)
            {
                return HttpNotFound();
            }
            return View(skeedaPK);
        }

        // POST: SkeedaPKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SkeedaPK skeedaPK = db.SkeedaPK.Find(id);
            db.SkeedaPK.Remove(skeedaPK);
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
    }
}
