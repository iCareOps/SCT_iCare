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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {

            try
            {
                Pass = Encrypt.GetSHA256(Pass.Trim());
                //Usuarios usuario = new Usuarios();
                var oUser = (from d in db.Usuarios where d.Email==User && d.Password == Pass.Trim() select d).FirstOrDefault();

                if(oUser == null)
                {
                    ViewBag.Error = "Usuario o Contraseña inválida";
                    return View();
                }

                Session["User"] = oUser;

                switch (oUser.idRol)
                {
                    case 5:
                        ViewBag.Nombre = oUser.Nombre.ToString();
                        return Redirect("~/Consultorios/Index");
                    case 1:
                        ViewBag.Nombre = oUser.Nombre.ToString();
                        return Redirect("~/CallCenter/Index");
                    case 3:
                        ViewBag.Nombre = oUser.Nombre.ToString();
                        return Redirect("~/SolicitudesConekta/Index");
                    case 4:
                        ViewBag.Nombre = oUser.Nombre.ToString();
                        return Redirect("~/Contabilidad/Index");

                    default:
                        return Redirect("~/Login/Login");
                }

                //return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}