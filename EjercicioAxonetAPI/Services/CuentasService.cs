using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonetAPI.Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace EjercicioAxonetAPI.Services
{
    public class CuentasService
    {
        private readonly JwtManager _jwtManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CuentasService(JwtManager jwtManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _jwtManager = jwtManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<Response<AccesoUsuarioDTO>> CrearUsuarioAsync(CredencialesUsuarioDTO credencialesUsuario)
        {
            try
            {
                IdentityUser usuario = new IdentityUser { UserName = credencialesUsuario.Email, Email = credencialesUsuario.Email };
                IdentityUser existeUsuario = await _userManager.FindByEmailAsync(credencialesUsuario.Email);
                if (existeUsuario != null)
                    throw new Exception("El correo ya esta en uso");

                var resultado = await _userManager.CreateAsync(usuario, credencialesUsuario.Password);

                if (resultado.Succeeded)
                {
                    AccesoUsuarioDTO usuarioPermitido = new AccesoUsuarioDTO { Succeeded = true };
                    return new Response<AccesoUsuarioDTO> { Result = usuarioPermitido, HttpStatusCode = System.Net.HttpStatusCode.OK };
                }
                else
                {
                    throw new Exception(resultado.Errors.Select(x => x.Description).ToString());
                }
            }
            catch (Exception ex)
            {
                return new Response<AccesoUsuarioDTO> { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }

        public async Task<Response<AccesoUsuarioDTO>> LoginAsync(CredencialesUsuarioDTO credencialesUsuario)
        {
            try
            {
                IdentityUser existeUsuario = await _userManager.FindByEmailAsync(credencialesUsuario.Email);

                if (existeUsuario == null)
                    throw new Exception("El correo no existe");

                var resultado = await _signInManager.PasswordSignInAsync(credencialesUsuario.Email, credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    var token = ObtenerToken(credencialesUsuario);
                    return new Response<AccesoUsuarioDTO> { Result = new AccesoUsuarioDTO { AccessToken = token, Succeeded = true }, HttpStatusCode = System.Net.HttpStatusCode.OK };
                }
                else
                {
                    throw new Exception("Credenciales incorrectas");
                }
            }
            catch (Exception ex)
            {
                return new Response<AccesoUsuarioDTO> { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }

        public RespuestaAutenticacion ObtenerToken(CredencialesUsuarioDTO credencialesUsuario)
        {
            return _jwtManager.ConstruirToken(credencialesUsuario);
        }
    }
}
