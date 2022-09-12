using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioAxonet.Common.DTOs
{
    public class AccesoUsuarioDTO
    {
        public bool Succeeded { get; set; }
        public RespuestaAutenticacion AccessToken { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
