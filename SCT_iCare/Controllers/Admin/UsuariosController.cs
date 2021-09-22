using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCT_iCare;

namespace SCT_iCare.Controllers.Admin
{
    public class UsuariosController : Controller
    {
        private GMIEntities db = new GMIEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Roles);
            return View(usuarios.ToList());
        }

        public ActionResult Gestores()
        {
            var usuarios = db.Usuarios.Where(w => w.idRol == 10).Include(u => u.Roles);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,Nombre,Email,Password,idRol")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Password = Encrypt.GetSHA256(usuarios.Password);
                
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View(usuarios);
        }

        [HttpPost]
        public ActionResult CreateGestor(string email, string password, string nombre)
        {
            Usuarios usuarios = new Usuarios();

            usuarios.Nombre = nombre;
            usuarios.Email = email;
            usuarios.Password = Encrypt.GetSHA256(password);
            usuarios.idRol = 10;

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View("Index");
        }

        [HttpPost]
        public ActionResult CreateCallCenter(string email, string password, string nombre)
        {
            Usuarios usuarios = new Usuarios();

            usuarios.Nombre = nombre;
            usuarios.Email = email;
            usuarios.Password = Encrypt.GetSHA256(password);
            usuarios.idRol = 1;

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return View("Index", "CallCenter");
            }

            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View("Index", "CallCenter");
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,Nombre,Email,Password,idRol")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.Password = Encrypt.GetSHA256(usuarios.Password);
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRol = new SelectList(db.Roles, "idRol", "Nombre", usuarios.idRol);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarSucursal(int id, int sucursal)
        {
            var recepcion = (from r in db.Recepcionista where r.idUsuario == id select r).FirstOrDefault();
            var idSucursal = (from i in db.Sucursales where i.idSucursal == sucursal select i.idSucursal).FirstOrDefault();

            if(recepcion != null)
            {
                Recepcionista recepcionista = db.Recepcionista.Find(id);
                recepcionista.idUsuario = id;
                recepcionista.idSucursal = idSucursal;

                if (ModelState.IsValid)
                {
                    db.Entry(recepcionista).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Recepcionista recepcionista = new Recepcionista();
                    recepcionista.idUsuario = id;
                    recepcionista.idSucursal = idSucursal;

                    db.Recepcionista.Add(recepcionista);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

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


        /*-----------------------Sección de Carrusel Médico para Usuarios-----------------------------*/

        public ActionResult CarruselMedico()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AsignarModulo(int modulo, int sucursal, int doctor)
        {
            SCT_iCare.DoctorModulo dm = new SCT_iCare.DoctorModulo();

            dm.idUsuario = doctor;
            dm.idModulo = modulo;
            dm.idSucursal = sucursal;

            if (ModelState.IsValid)
            {
                db.DoctorModulo.Add(dm);
                db.SaveChanges();
            }

            return Redirect("CarruselMedico");
        }


    }
}
