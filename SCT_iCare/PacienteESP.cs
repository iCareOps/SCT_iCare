//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCT_iCare
{
    using System;
    using System.Collections.Generic;
    
    public partial class PacienteESP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PacienteESP()
        {
            this.FotoPacienteESP = new HashSet<FotoPacienteESP>();
            this.CartaNoAccidentesESP = new HashSet<CartaNoAccidentesESP>();
            this.DeclaracionSaludESP = new HashSet<DeclaracionSaludESP>();
            this.DocumentosESP = new HashSet<DocumentosESP>();
            this.HemoglobinaGlucosiladaESP = new HashSet<HemoglobinaGlucosiladaESP>();
            this.DictamenESP = new HashSet<DictamenESP>();
        }
    
        public int idPacienteESP { get; set; }
        public string Nombre { get; set; }
        public string CURP { get; set; }
        public string NoExpediente { get; set; }
        public string TipoTramite { get; set; }
        public string TipoLicencia { get; set; }
        public string Sucursal { get; set; }
        public string Doctor { get; set; }
        public string Usuario { get; set; }
        public string Asistencia { get; set; }
        public string CancelaComentario { get; set; }
        public string Entregado { get; set; }
        public string ReferidoPor { get; set; }
        public Nullable<System.DateTime> FechaCita { get; set; }
        public string Estatura { get; set; }
        public string Metra { get; set; }
        public string EstatusCaptura { get; set; }
        public string Capturista { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FotoPacienteESP> FotoPacienteESP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartaNoAccidentesESP> CartaNoAccidentesESP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeclaracionSaludESP> DeclaracionSaludESP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentosESP> DocumentosESP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HemoglobinaGlucosiladaESP> HemoglobinaGlucosiladaESP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DictamenESP> DictamenESP { get; set; }
    }
}
