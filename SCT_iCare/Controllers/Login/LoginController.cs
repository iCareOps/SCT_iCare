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

            //string msj = "El usuario o la contraseña son incorrectos";

            //try
            //{
            //    Usuarios oUsuario = new Usuarios();

            //    if (oUsuario != null)
            //    {
            //        Session["Usuario"] = oUsuario;
            //        switch (5)
            //        {
            //            case 5:
            //                return RedirectToAction("Index", "Usuarios");
            //            //case CatalogosUtil.EntCatRoles.SupervisorCallCenter:
            //            //    return Redirect("~/Supervisor/Index");
            //            //case CatalogosUtil.EntCatRoles.Enfermero:
            //            //    return Redirect("~/Enfermero/Index");
            //            //case CatalogosUtil.EntCatRoles.Helpdesk:
            //            //    return Redirect("~/HelpdeskSupervisor/Index");
            //            //case CatalogosUtil.EntCatRoles.Corporativo:
            //            //    return Redirect("~/Convenios/Index");
            //            //case CatalogosUtil.EntCatRoles.AdminAgentes:
            //            //    return Redirect("~/AdminAgentes/Index");
            //            //case CatalogosUtil.EntCatRoles.KioscoBack:
            //            //    return Redirect("~/KioscoBack/Index");
            //            //case CatalogosUtil.EntCatRoles.KioscoBackHD:
            //            //    return Redirect("~/KioscoBackHD/Index");

            //            default:
            //                return Redirect("~/AdminUsuarios/Index");
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.Error = msj;
            //        return View("~/Views/Pages/Authentication/LoginV2.cshtml");
            //    }

            //}
            //catch (System.Exception ex)
            //{
            //    ViewBag.Error = "Ocurrio un error al ";
            //    return View("~/Views/Pages/Authentication/LoginV2.cshtml");
            //}

            //var Email = form["email"];
            //var Password = form["email"];

            //Usuarios usuario = new Usuarios();
            //var rol = usuario.idRol;

            //if (Email != null)
            //{
            //    if (rol == 5)
            //    {
            //        return RedirectToAction("Index", "Usuarios");
            //    }

            //}
            //else
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            //return RedirectToAction("Login", "Login");

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
                        return Redirect("~/Recepcion/Index");
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