using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Configs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EjercicioAxonet.Services
{
    public class MonedasService
    {
        private readonly EjercicioAxonet_APIConfig _ejercicioAxonet;

        public MonedasService(IOptions<EjercicioAxonet_APIConfig> ejercicioAxonet)
        {
            _ejercicioAxonet = ejercicioAxonet?.Value;
        }

        public async Task<Response<List<MonedaDTO>>> ListadoMonedas(string token)
        {
            HttpResponseMessage respuesta;
            Response<List<MonedaDTO>> data = new Response<List<MonedaDTO>>();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.GetAsync("Monedas/ListadoMonedas");
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<List<MonedaDTO>>>(cuerpoRespuesta);

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

        public async Task<Response<MonedaDTO>> BuscarMoneda(string token, int monedaId)
        {
            HttpResponseMessage respuesta;
            Response<MonedaDTO> data = new Response<MonedaDTO>();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.GetAsync("Monedas/BuscarMoneda/" + monedaId);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<MonedaDTO>>(cuerpoRespuesta);
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

        public async Task<Response<MonedaDTO>> NuevaMoneda(string token, MonedaDTO moneda)
        {
            HttpResponseMessage respuesta;
            Response<MonedaDTO> data = new Response<MonedaDTO>();

            try
            {
                if (string.IsNullOrEmpty(moneda.Moneda) || string.IsNullOrEmpty(moneda.MonedaAbreviatura))
                    throw new Exception("Alguno de los campos moneda y abreviatura moneda viene sin información");

                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.PostAsJsonAsync("Monedas", moneda);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<MonedaDTO>>(cuerpoRespuesta);
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

        public async Task<Response<MonedaDTO>> EditarMoneda(string token, MonedaDTO moneda)
        {
            HttpResponseMessage respuesta;
            Response<MonedaDTO> data = new Response<MonedaDTO>();

            try
            {
                if (string.IsNullOrEmpty(moneda.Moneda) || string.IsNullOrEmpty(moneda.MonedaAbreviatura))
                    throw new Exception("Alguno de los campos moneda y abreviatura moneda viene sin información");

                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.PutAsJsonAsync("Monedas/" + moneda.MonedaId, moneda);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<MonedaDTO>>(cuerpoRespuesta);
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

        public async Task<Response<bool>> EliminarMoneda(string token, int monedaId)
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
                    respuesta = await httpCliente.DeleteAsync("Monedas/" + monedaId);
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

        public async Task<List<SelectListItem>> SelectListMonedas(string token)
        {
            List<SelectListItem> selectListMonedas = new List<SelectListItem>();
            var listadoMonedas = await ListadoMonedas(token);

            if (listadoMonedas.HttpStatusCode != System.Net.HttpStatusCode.OK || listadoMonedas.Result == null)
                return selectListMonedas;

            listadoMonedas.Result.ForEach(x =>
            {
                selectListMonedas.Add(new SelectListItem { Text = x.MonedaAbreviatura, Value = x.MonedaId.ToString() });
            });

            return selectListMonedas;
        }
    }
}
