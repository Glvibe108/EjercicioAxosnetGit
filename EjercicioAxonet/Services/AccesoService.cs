using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Configs;
using EjercicioAxonet.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EjercicioAxonet.Services
{
    public class AccesoService
    {
        private readonly EjercicioAxonet_APIConfig _ejercicioAxonet;
        private readonly JwtConfig _jwtConfig;

        public AccesoService(IOptions<EjercicioAxonet_APIConfig> ejercicioAxonet, IOptions<JwtConfig> jwtConfig)
        {
            _ejercicioAxonet = ejercicioAxonet?.Value;
            _jwtConfig = jwtConfig?.Value;
        }

        public async Task<AccesoUsuarioDTO> Login(string correo, string contrasenia)
        {
            HttpResponseMessage respuesta;

            try
            {
                if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasenia))
                    throw new Exception("¡Ops! al parecer faltó ingresar el correo o la contraseña.");

                CredencialesUsuarioDTO credenciales = new CredencialesUsuarioDTO { Email = correo, Password = contrasenia };

                using (var httpCliente = new HttpClient())
                {
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.PostAsJsonAsync("Cuentas/Login", credenciales);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();
                        var AccesoUsuario = JsonConvert.DeserializeObject<AccesoUsuarioDTO>(cuerpoRespuesta);
                        AccesoUsuario.ClaimsPrincipal = JwtHelper.GetPrincipal(AccesoUsuario.AccessToken.Token, _jwtConfig.LlaveJwt);

                        return AccesoUsuario;
                    }
                    else
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Registro(string correo, string contrasenia)
        {
            HttpResponseMessage respuesta;

            try
            {
                if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasenia))

                    throw new Exception("¡Ops! al parecer faltó ingresar el correo o la contraseña.");
                CredencialesUsuarioDTO credenciales = new CredencialesUsuarioDTO { Email = correo, Password = contrasenia };

                using (var httpCliente = new HttpClient())
                {
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.PostAsJsonAsync("Cuentas/Resgitrar", credenciales);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string responseBody = await respuesta.Content.ReadAsStringAsync();
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
