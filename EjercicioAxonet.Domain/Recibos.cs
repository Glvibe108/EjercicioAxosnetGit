using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioAxonet.Domain
{
    public class Recibos
    {
        [Key]
        public int ReciboId { get; set; }
        [StringLength(13)]
        public string FolioRecibo { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MontoRecibo { get; set; }
        [Required]
        public DateTime FechaRecibo { get; set; }
        [StringLength(4000)]
        public string Comentario { get; set; }
        [Required]
        public int MonedaId { get; set; }
        [ForeignKey("MonedaId")]
        public virtual Monedas Moneda { get; set; }
        [Required]
        public int ProveedorId { get; set; }
        [ForeignKey("ProveedorId")]
        public virtual Proveedores Proveedor { get; set; }
        public bool Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioAlta { get; set; }
        public DateTime? FechaMod { get; set; }
        public string UsuarioMod { get; set; }
    }
}
