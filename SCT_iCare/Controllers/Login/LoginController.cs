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
        private GMIEntities db = new GMIEntities();

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
                        case 2:
                            if(oUser.Nombre.ToString() == "Andrés Anduaga" || oUser.Nombre.ToString() == "Teresa Lozano")
                            {
                                ViewBag.Nombre = oUser.Nombre.ToString();
                                return Redirect("~/Admin/Dashboard");
                            }
                            else
                            {
                                ViewBag.Nombre = oUser.Nombre.ToString();
                                return Redirect("~/Admin/Index");
                            }
                            
                        case 1:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/CallCenter/Index");
                        case 3:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Recepcion/Index");
                        case 21:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Recepcion/Index");
                        case 9:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/EPIs/Captura");
                        case 6:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Conciliacion/Index");
                        case 7:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/EPIs/Captura");
                        case 8:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/CERTIFICADOes/Index");
                        case 5:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Usuarios/Index");
                        case 11:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/GestorVenta/Index");
                        case 12:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/ArchivoClinico/Recepcion");
                        case 13:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/ArchivoClinico/Recepcion");
                        case 17:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/ArchivoClinico/Recepcion");
                        case 10:
                            ViewBag.Nombre = oUser.Nombre.ToString();

                            var logGestor = new log_InicioGestor();
                            logGestor.InicioSesion = DateTime.Now;
                            logGestor.NombreUsuario = oUser.Nombre.ToString();
                            logGestor.idUsuario = oUser.idUsuario;

                            if (ModelState.IsValid)
                            {
                                db.log_InicioGestor.Add(logGestor);
                                db.SaveChanges();
                            }
                            int idLog = logGestor.idLogInicioGestor;
                            TempData["ID"] = idLog;
                            return Redirect("~/Gestoria/Index");
                        case 22:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Dictamenes/Citas");
                        case 23:
                            ViewBag.Nombre = oUser.Nombre.ToString();
                            return Redirect("~/Dictamenes/Captura");

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