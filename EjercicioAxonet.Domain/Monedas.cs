using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioAxonet.Domain
{
    public class Monedas
    {
        [Key]
        public int MonedaId { get; set; }
        [Required]
        [StringLength(100)]
        public string Moneda { get; set; }
        [Required]
        [StringLength(3)]
        public string MonedaAbreviatura { get; set; }
        public bool Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioAlta { get; set; }
        public DateTime? FechaMod { get; set; }
        public string UsuarioMod { get; set; }
    }
}