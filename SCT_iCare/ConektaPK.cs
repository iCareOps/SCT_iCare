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
    
    public partial class ConektaPK
    {
        public int idConekta { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<System.DateTime> fecha_pago { get; set; }
        public string id_cargo { get; set; }
        public string tipo_transaccion { get; set; }
        public string metodo_pago { get; set; }
        public string estatus { get; set; }
        public string mensaje_error { get; set; }
        public string codigo_falla { get; set; }
        public string codigo_antifraude { get; set; }
        public Nullable<double> monto_cargo { get; set; }
        public Nullable<double> comision { get; set; }
        public Nullable<double> monto_deposito { get; set; }
        public string moneda { get; set; }
        public string cantidad_original { get; set; }
        public string moneda_original { get; set; }
        public Nullable<double> tipo_cambio { get; set; }
        public string id_referencia { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string ip { get; set; }
        public string nombre_tarjetahabiente { get; set; }
        public string numero_tarjeta { get; set; }
        public string banco { get; set; }
        public string marca_tarjeta { get; set; }
        public string pais_tarjeta { get; set; }
        public string tipo { get; set; }
        public string tipo_tarjeta { get; set; }
        public string pago_efectivo { get; set; }
        public string sucursal { get; set; }
        public string ip_ciudad { get; set; }
        public string id_deposito { get; set; }
        public Nullable<double> referencia_deposito { get; set; }
        public string fecha_deposito { get; set; }
        public string meses_sin_intereses { get; set; }
        public string id_transaccion { get; set; }
        public string id_recibo { get; set; }
        public string livemode { get; set; }
        public string checkout_id { get; set; }
        public string decision_antifraude { get; set; }
        public string decision_bancaria { get; set; }
        public string reglas_antifraude_disparadas { get; set; }
        public string direccion_de_envio { get; set; }
        public string id_cliente { get; set; }
        public string id_subscripcion { get; set; }
        public string id_orden { get; set; }
        public string fecha_contracargo { get; set; }
        public string fecha_devolucion { get; set; }
        public Nullable<double> auth_code { get; set; }
        public Nullable<double> referencia_oxxo { get; set; }
        public string canal { get; set; }
        public string checkout_request_id { get; set; }
        public string checkout_request_type { get; set; }
        public string checkout_request_name { get; set; }
        public string customer_custom_reference { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
    }
}
