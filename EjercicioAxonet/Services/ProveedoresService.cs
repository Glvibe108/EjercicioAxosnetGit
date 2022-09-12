using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Configs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EjercicioAxonet.Services
{
    public class ProveedoresService
    {
        private readonly EjercicioAxonet_APIConfig _ejercicioAxonet;
        public ProveedoresService(IOptions<EjercicioAxonet_APIConfig> ejercicioAxonet)
        {
            _ejercicioAxonet = ejercicioAxonet?.Value;
        }

        public async Task<Response<List<ProveedorDTO>>> ListadoProveedores(string token)
        {
            HttpResponseMessage respuesta;
            Response<List<ProveedorDTO>> data = new Response<List<ProveedorDTO>>();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.GetAsync("Proveedores/ListadoProveedores");
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<List<ProveedorDTO>>>(cuerpoRespuesta);

                    return data;
                }
            }
            catch (Exception ex)
            {
                data.Message = ex.Message;
                data.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                data.Code = -1;

                return data;
            }
        }

        public async Task<Response<ProveedorDTO>> BuscarProveedor(string token, int proveedorId)
        {
            HttpResponseMessage respuesta;
            Response<ProveedorDTO> data = new Response<ProveedorDTO>();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.GetAsync("Proveedores/BuscarProveedor/" + proveedorId);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<ProveedorDTO>>(cuerpoRespuesta);
                    else
                    {
                        data.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                        data.Code = -1;
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                data.Message = ex.Message;
                data.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                data.Code = -1;

                return data;
            }
        }

        public async Task<Response<ProveedorDTO>> NuevoProveedor(string token, ProveedorDTO proveedor)
        {
            HttpResponseMessage respuesta;
            Response<ProveedorDTO> data = new Response<ProveedorDTO>();

            try
            {
                if (string.IsNullOrEmpty(proveedor.NombreLargoProveedor) || string.IsNullOrEmpty(proveedor.NombreCortoProveedor))
                    throw new Exception("Alguno de los campos nombre corto y largo del proveedor viene sin información");

                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.PostAsJsonAsync("Proveedores", proveedor);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<ProveedorDTO>>(cuerpoRespuesta);
                    else
                    {
                        data.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                        data.Code = -1;
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                data.Message = ex.Message;
                data.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                data.Code = -1;

                return data;
            }
        }

        public async Task<Response<ProveedorDTO>> EditarProveedor(string token, ProveedorDTO proveedor)
        {
            HttpResponseMessage respuesta;
            Response<ProveedorDTO> data = new Response<ProveedorDTO>();

            try
            {
                if (string.IsNullOrEmpty(proveedor.NombreLargoProveedor) || string.IsNullOrEmpty(proveedor.NombreCortoProveedor))
                    throw new Exception("Alguno de los campos nombre corto y largo del proveedor viene sin información");

                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.PutAsJsonAsync("Proveedores/" + proveedor.ProveedorId, proveedor);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<ProveedorDTO>>(cuerpoRespuesta);
                    else
                    {
                        data.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                        data.Code = -1;
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                data.Message = ex.Message;
                data.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                data.Code = -1;

                return data;
            }
        }

        public async Task<Response<bool>> EliminarProveedor(string token, int proveedorId)
        {
            HttpResponseMessage respuesta;
            Response<bool> data = new Response<bool>();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.DeleteAsync("Proveedores/" + proveedorId);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<bool>>(cuerpoRespuesta);
                    else
                    {
                        data.HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
                        data.Result = false;
                        data.Code = -1;
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                data.Message = ex.Message;
                data.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                data.Code = -1;
                data.Result = false;

                return data;
            }
        }

        public async Task<List<SelectListItem>> SelectListProveedores(string token)
        {
            List<SelectListItem> selectListMonedas = new List<SelectListItem>();
            var listadoMonedas = await ListadoProveedores(token);

            if (listadoMonedas.HttpStatusCode != System.Net.HttpStatusCode.OK || listadoMonedas.Result == null)
                return selectListMonedas;

            listadoMonedas.Result.ForEach(x =>
            {
                selectListMonedas.Add(new SelectListItem { Text = x.NombreCortoProveedor, Value = x.ProveedorId.ToString() });
            });

            return selectListMonedas;
        }
    }
}
