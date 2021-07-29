using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCT_iCare.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private Usuarios oUsuario;
        private GMIEntities db = new GMIEntities();
        private int idOperacion;

        public AuthorizeUser(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOperacion = "";
            String nombreModulo = "";
            try
            {
                oUsuario = (Usuarios)HttpContext.Current.Session["Usuario"];
                var lstMisOperaciones = from m in db.RolMenu where m.idRol == oUsuario.idRol && m.idRolMenu == idOperacion select m;

                if (lstMisOperaciones.ToList().Count() < 1)
                {
                    var oOperacion = db.Menu.Find(idOperacion);
                    int? idRolMenu = oOperacion.idMenu;
                    nombreOperacion = getNombreDeOperacion(idOperacion);
                    nombreModulo = getNombreDelModulo(idRolMenu);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion);
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion);
            }

            //base.OnAuthorization(filterContext);
        }

        public string getNombreDeOperacion(int idOperacion)
        {
            var ope = from op in db.Menu where op.idMenu == idOperacion select op.Nombre;

            String nombreOperacion;
            try
            {
                nombreOperacion = ope.First();
            }
            catch (Exception)
            {
                nombreOperacion = "";
            }

            return nombreOperacion;
        }

        public string getNombreDelModulo(int? idRolMenu)
        {
            var modulo = from m in db.Menu where m.idMenu == idRolMenu select m.Nombre;

            String nombreModulo;
            try
            {
                nombreModulo = modulo.First();
            }
            catch (Exception)
            {
                nombreModulo = "";
            }

            return nombreModulo;
        }

    }
}