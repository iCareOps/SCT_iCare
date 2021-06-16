using SCT_iCare.Controllers.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        private Usuarios oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUsuario = (Usuarios)HttpContext.Current.Session["User"];
                if(oUsuario == null)
                {
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Login/Inicio", false);
                    }
                }
                //else
                //{
                //    if(filterContext.Controller is LoginController == true)
                //    {
                //        filterContext.HttpContext.Response.Redirect("~/Login/Inicio");
                //    }
                //}
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Login/Inicio");
            }
            
        }
    }
}