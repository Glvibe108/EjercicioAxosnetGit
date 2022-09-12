using AutoMapper;
using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Domain;
using EjercicioAxonet.Repository;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EjercicioAxonetAPI.Services
{
    public class MonedasService
    {
        private readonly MonedasRepository _monedasRepository;
        private readonly IMapper _mapper;

        public MonedasService(MonedasRepository monedasRepository, IMapper mapper)
        {
            _monedasRepository = monedasRepository;
            _mapper = mapper;
        }

        public Response<IEnumerable<MonedaDTO>> GetAll()
        {
            List<MonedaDTO> dataDTO = new();
            try
            {
                var data = _monedasRepository.Find(x => x.Estatus == true);
                dataDTO = _mapper.Map<List<MonedaDTO>>(data);

                return new Response<IEnumerable<MonedaDTO>>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = dataDTO
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<MonedaDTO>>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = dataDTO
                };
            }
        }

        public Response<MonedaDTO> GetOne(int id)
        {
            MonedaDTO dataDTO = new();
            try
            {
                var data = _monedasRepository.GetById(id);
                if (data == null)
                    throw new Exception("No coincide el ID de la moneda, favor de verificar");

                dataDTO = _mapper.Map<MonedaDTO>(data);

                return new Response<MonedaDTO>
                {
                    HttpStatusCode = data == null ? System.Net.HttpStatusCode.NotFound : System.Net.HttpStatusCode.OK,
                    Result = dataDTO
                };
            }
            catch (Exception ex)
            {
                return new Response<MonedaDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = dataDTO
                };
            }
        }

        public Response<MonedaDTO> Add(MonedaDTO moneda, string usuario)
        {
            try
            {
                var entity = _mapper.Map<MonedaDTO>(moneda);
                entity.Estatus = true;
                entity.FechaAlta = DateTime.Now;
                entity.UsuarioAlta = usuario;

                _monedasRepository.Add(entity);
                _monedasRepository.SaveChange();

                moneda.MonedaId = entity.MonedaId;

                return new Response<MonedaDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = moneda
                };
            }
            catch (Exception ex)
            {
                return new Response<MonedaDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public Response<MonedaDTO> Put(int id, MonedaDTO moneda, string usuario)
        {
            try
            {
                if (id != moneda.MonedaId)
                    throw new Exception("No coincide el ID de la moneda, favor de verificar");

                var entity = _monedasRepository.GetById(id);
                if (entity == null)
                    throw new Exception("No se encontró la moneda");

                entity.Moneda = moneda.Moneda;
                entity.MonedaAbreviatura = moneda.MonedaAbreviatura;
                entity.FechaMod = DateTime.Now;
                entity.UsuarioMod = usuario;

                _monedasRepository.Update(entity);
                _monedasRepository.SaveChange();

                return new Response<MonedaDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = moneda
                };
            }
            catch (Exception ex)
            {
                return new Response<MonedaDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public Response<bool> Delete(int id,string usuario)
        {
            try
            {
                var entity = _monedasRepository.GetById(id);

                if (entity == null)
                    throw new Exception("No coincide el ID de la moneda, favor de verificar");

                entity.Estatus = false;
                entity.FechaMod=DateTime.Now;
                entity.UsuarioMod = usuario;

                _monedasRepository.Delete(entity);
                _monedasRepository.SaveChange();

                return new Response<bool>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = true
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = false
                };
            }
        }
    }
}
