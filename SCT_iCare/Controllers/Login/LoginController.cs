using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SCT_iCare.Controllers.Login
{
    public class LoginController : Controller
    {
        private SCTiCareEntities1 db = new SCTiCareEntities1();

        // GET: Login
        public ActionResult Inicio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Inicio(string User, string Pass)
        {

            try
            {
                //Query para verificar que los contadores y fechas estén bien
                int hoy = DateTime.Now.Day;
                int anio = DateTime.Now.Year;

                var contadores = (from c in db.Sucursales select c);

                
                foreach (var item in contadores)
                {
                    if (hoy != Convert.ToDateTime(item.ContadorFecha).Day)
                    {
                        Sucursales sucursales = db.Sucursales.Find(item.idSucursal);
                        sucursales.Contador = 0;
                        sucursales.ContadorFecha = DateTime.Now;

                        if (ModelState.IsValid)
                        {
                            db.Entry(sucursales).State = EntityState.Modified;
                            
                        }
                    }
                }
                db.SaveChanges();

                //if(anio != Convert.ToDateTime(contadores.ContadorFecha).Year)
                //{
                //    Sucursales sucursales = new Sucursales();
                //    sucursales.Contador = 0;

                //    if (ModelState.IsValid)
                //    {
                //        db.Entry(sucursales).State = EntityState.Modified;
                //        db.SaveChanges();
                //    }
                //}


                Pass = Encrypt.GetSHA256(Pass.Trim());
                //Usuarios usuario = new Usuarios();
                var oUser = (from d in db.Usuarios where d.Email==User && d.Password == Pass.Trim() select d).FirstOrDefault();

                if(oUser == null)
                {
                    ViewBag.Error = "Usuario o Contraseña inválida";
                    return View();
                }
                else
                {
                    Session["User"] = oUser;

                    switch (oUser.idRol)
                    {
                        case 5:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Sucursales/Index");
                        case 1:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/CallCenter/Index");
                        case 3:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/EPIs/Index");
                        case 4:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Contabilidad/Index");
                        case 6:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/SkeedaPK/Index");
                        case 7:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/EPIs/Captura");
                        case 8:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/CERTIFICADOes/Index");

                        default:
                            //return Redirect("~/Login/Login");
                            return View();
                    }
                }

                

                

                //return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult Redireccionar()
        {
            return RedirectToAction("Inicio");
        }

    }
}