//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CajaPopular
{
    using System;
    using System.Collections.Generic;
    
    public partial class balance
    {
        [Key, Column(Order=0)]
        public int id_solicitante { get; set; }
        [Key, Column(Order = 1)]
        public int factura { get; set; }
        [Key, Column(Order = 2)]
        public int num_documento { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public System.DateTime fecha_captura { get; set; }
        public System.DateTime fecha_vencimiento { get; set; }
        public System.DateTime fecha_pago { get; set; }
        public int capturista { get; set; }
        public bool borrado { get; set; }
        public bool cubierto { get; set; }
    
        public virtual solicitante solicitante { get; set; }
        public virtual usuario usuario { get; set; }
        public virtual persona persona { get; set; }

        public virtual List<persona> listaper { get; set; }
        public virtual List<solicitante> listasol { get; set; }
        public object PrimaryKey { get; set; }
    }
}
