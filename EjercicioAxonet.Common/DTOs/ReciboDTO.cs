using EjercicioAxonet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioAxonet.Common.DTOs
{
    public class ReciboDTO : Recibos
    {
        public int? ProveedorIdAdd { get; set; }
        public int? MonedaIdAdd { get; set; }
        public DateTime? FechaReciboAdd { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreMoneda { get; set; }
        public string FechaCortaRecibo { get; set; }
    }
}
