using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCT_iCare.Models
{
    public class Root
    {
        public class Roots
        {
            public int total { get; set; }
            public int totalNotFiltered { get; set; }
            public List<Paciente> rows { get; set; }
        }
    }
}