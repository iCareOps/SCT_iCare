using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCT_iCare.Models
{
    public class SubirPDF
    {
        public String confirmacion { get; set; }
        public Exception error { get; set; }
        public void SubirArchivo(string rute, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(rute);
                this.confirmacion = "Archivo guardado";
            }
            catch(Exception ex)
            {
                this.error = ex;
            }
        }
    }
}