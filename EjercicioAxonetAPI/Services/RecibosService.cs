using AutoMapper;
using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Repository;
using System.Collections.Generic;
using System;
using EjercicioAxonet.Domain;
using System.Linq;

namespace EjercicioAxonetAPI.Services
{
    public class RecibosService
    {
        private readonly RecibosRepository _recibosRepository;
        private readonly MonedasService _monedasService;
        private readonly ProveedoresService _proveedoresService;
        private readonly IMapper _mapper;

        public RecibosService(RecibosRepository recibosRepository, IMapper mapper, MonedasService monedasService, ProveedoresService proveedoresService)
        {
            _recibosRepository = recibosRepository;
            _monedasService = monedasService;
            _proveedoresService = proveedoresService;
            _mapper = mapper;
        }

        public Response<IEnumerable<ReciboDTO>> GetAll()
        {
            List<ReciboDTO> dataDTO = new List<ReciboDTO>();
            try
            {
                var data = _recibosRepository.Find(x => x.Estatus == true);
                var dataMonedas = _monedasService.GetAll();
                var dataProveedores = _proveedoresService.GetAll();

                dataDTO = _mapper.Map<List<ReciboDTO>>(data);
                dataDTO.ForEach(x =>
                {
                    x.NombreProveedor = dataProveedores.Result.Where(y => y.ProveedorId == x.ProveedorId).FirstOrDefault().NombreCortoProveedor;
                    x.NombreMoneda = dataMonedas.Result.Where(y => y.MonedaId == x.MonedaId).FirstOrDefault().MonedaAbreviatura;
                });

                return new Response<IEnumerable<ReciboDTO>>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = dataDTO
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ReciboDTO>>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = dataDTO
                };
            }
        }

        public Response<ReciboDTO> GetOne(int id)
        {
            ReciboDTO dataDTO = new ReciboDTO();
            try
            {
                var dataMonedas = _monedasService.GetAll();
                var dataProveedores = _proveedoresService.GetAll();
                var data = _recibosRepository.GetById(id);
                if (data == null)
                    throw new Exception("No coincide el ID del recibo, favor de verificar");

                dataDTO = _mapper.Map<ReciboDTO>(data);
                dataDTO.FechaCortaRecibo = data.FechaRecibo.ToShortDateString();
                dataDTO.NombreProveedor = dataProveedores.Result.Where(x => x.ProveedorId == dataDTO.ProveedorId).FirstOrDefault().NombreCortoProveedor;
                dataDTO.NombreMoneda = dataMonedas.Result.Where(x => x.MonedaId == dataDTO.MonedaId).FirstOrDefault().Moneda;
                return new Response<ReciboDTO>
                {
                    HttpStatusCode = data == null ? System.Net.HttpStatusCode.NotFound : System.Net.HttpStatusCode.OK,
                    Result = dataDTO
                };
            }
            catch (Exception ex)
            {
                return new Response<ReciboDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = dataDTO
                };
            }
        }

        public Response<ReciboDTO> Add(ReciboDTO recibo, string usuario)
        {
            try
            {
                var data = _recibosRepository.Find(x => x.Estatus == true);
                var dataMonedas = _monedasService.GetAll();
                var dataProveedores = _proveedoresService.GetAll();
                var numRegistros = _recibosRepository.GetAll().Count();
                var entity = _mapper.Map<Recibos>(recibo);
                entity.FechaRecibo = recibo.FechaReciboAdd.Value;
                entity.MonedaId = recibo.MonedaIdAdd.Value;
                entity.ProveedorId = recibo.ProveedorIdAdd.Value;
                entity.Estatus = true;
                entity.UsuarioAlta = usuario;
                entity.FechaAlta = DateTime.Now;
                entity.FolioRecibo = "FOLIO-AXO-" + (numRegistros + 1);

                _recibosRepository.Add(entity);
                _recibosRepository.SaveChange();

                recibo.ReciboId = entity.ReciboId;
                recibo.FolioRecibo = entity.FolioRecibo;
                recibo.NombreProveedor= dataProveedores.Result.Where(x => x.ProveedorId == entity.ProveedorId).FirstOrDefault().NombreCortoProveedor;
                recibo.NombreMoneda = dataMonedas.Result.Where(x => x.MonedaId == entity.MonedaId).FirstOrDefault().Moneda;

                return new Response<ReciboDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = recibo
                };
            }
            catch (Exception ex)
            {
                return new Response<ReciboDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = null
                };
            }
        }
    }
}
