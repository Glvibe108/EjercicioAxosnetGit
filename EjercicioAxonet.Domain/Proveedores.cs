using System;
using System.ComponentModel.DataAnnotations;

namespace EjercicioAxonet.Domain
{
    public class Proveedores
    {
        [Key]
        public int ProveedorId { get; set; }
        [StringLength(250)]
        public string NombreCortoProveedor { get; set; }
        [StringLength(1000)]
        public string NombreLargoProveedor { get; set; }
        [StringLength(200)]
        public string CorreoProveedor { get; set; }
        [StringLength(13)]
        public string TelefonoProveedor { get; set; }
        [StringLength(4000)]
        public string DireccionProveedor { get; set; }
        public bool Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioAlta { get; set; }
        public DateTime? FechaMod { get; set; }
        public string UsuarioMod { get; set; }
    }
}

