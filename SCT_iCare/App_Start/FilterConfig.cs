using System.Web;
using System.Web.Mvc;

namespace SCT_iCare
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.VerificarSession());
        }
    }
}
