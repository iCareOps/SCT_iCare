﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SCTiCareEntities1 : DbContext
    {
        public SCTiCareEntities1()
            : base("name=SCTiCareEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolMenu> RolMenu { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Consultorios> Consultorios { get; set; }
        public virtual DbSet<Orden> Orden { get; set; }
        public virtual DbSet<OrdenConekta> OrdenConekta { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Doctores> Doctores { get; set; }
        public virtual DbSet<Sucursales> Sucursales { get; set; }
        public virtual DbSet<SkeedaPK> SkeedaPK { get; set; }
        public virtual DbSet<ConektaPK> ConektaPK { get; set; }
        public virtual DbSet<Recepcionista> Recepcionista { get; set; }
        public virtual DbSet<Dictamen> Dictamen { get; set; }
        public virtual DbSet<Expediente> Expediente { get; set; }
        public virtual DbSet<Archivo> Archivo { get; set; }
        public virtual DbSet<EPI> EPI { get; set; }
    }
}
