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

namespace EjercicioAxonet.Services
{
    public class RecibosService
    {
        private readonly EjercicioAxonet_APIConfig _ejercicioAxonet;
        public RecibosService(IOptions<EjercicioAxonet_APIConfig> ejercicioAxonet)
        {
            _ejercicioAxonet = ejercicioAxonet?.Value;
        }

        public async Task<Response<List<ReciboDTO>>> ListadoRecibos(string token)
        {
            HttpResponseMessage respuesta;
            Response<List<ReciboDTO>> data = new Response<List<ReciboDTO>>();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.GetAsync("Recibos/ListadoRecibos");
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<List<ReciboDTO>>>(cuerpoRespuesta);

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

        public async Task<Response<ReciboDTO>> BuscarRecibo(string token, int reciboId)
        {
            HttpResponseMessage respuesta;
            Response<ReciboDTO> data = new Response<ReciboDTO>();

            try
            {
                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.GetAsync("Recibos/BuscarRecibo/" + reciboId);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<ReciboDTO>>(cuerpoRespuesta);
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

        public async Task<Response<ReciboDTO>> NuevoRecibo(string token, ReciboDTO recibo)
        {
            HttpResponseMessage respuesta;
            Response<ReciboDTO> data = new Response<ReciboDTO>();

            try
            {
                if (recibo.ProveedorIdAdd ==null || recibo.MonedaIdAdd==null || !recibo.FechaReciboAdd.HasValue)
                    throw new Exception("Los campos fecha de recibo, proveedor o moneda vienen sin información");

                using (var httpCliente = new HttpClient())
                {
                    httpCliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    httpCliente.BaseAddress = new Uri(_ejercicioAxonet.URI);
                    httpCliente.DefaultRequestHeaders.Accept.Clear();
                    httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    respuesta = await httpCliente.PostAsJsonAsync("Recibos", recibo);
                    string cuerpoRespuesta = await respuesta.Content.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                        data = JsonConvert.DeserializeObject<Response<ReciboDTO>>(cuerpoRespuesta);
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

    }
}
