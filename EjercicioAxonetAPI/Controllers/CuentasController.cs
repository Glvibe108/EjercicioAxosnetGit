using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonetAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioAxonetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly CuentasService _cuentasService;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, CuentasService cuentasService)
        {
            _cuentasService = cuentasService;
        }

        [AllowAnonymous]
        [HttpPost("Resgitrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registar([FromBody] CredencialesUsuarioDTO credencialesUsuario)
        {
            var resultado = await _cuentasService.CrearUsuarioAsync(credencialesUsuario);
            switch (resultado.HttpStatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(resultado.Result);
                default:
                    return StatusCode((int)resultado.HttpStatusCode, new ResponseError { HttpStatusCode = resultado.HttpStatusCode, Message = resultado.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login([FromBody] CredencialesUsuarioDTO credencialesUsuario)
        {
            var resultado = await _cuentasService.LoginAsync(credencialesUsuario);
            switch (resultado.HttpStatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(resultado.Result);
                default:
                    return StatusCode((int)resultado.HttpStatusCode, new ResponseError { HttpStatusCode = resultado.HttpStatusCode, Message = resultado.Message });
            }
        }
    }
}
