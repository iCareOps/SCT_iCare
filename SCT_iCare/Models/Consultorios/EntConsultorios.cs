using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCT_iCare.Models.Consultorios
{
    public class EntConsultorios
    {
        public int Id { get; set; }
        public string NombreDoctor { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Consultorio { get; set; }
        public int NoConsultorio { get; set; }
    }

    public class MejorSucursal
    {
        public string Sucursal { get; set; }
        public int EPIS { get; set; }
    }
}