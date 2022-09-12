using AutoMapper;
using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Domain;
using EjercicioAxonet.Repository;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;

namespace EjercicioAxonetAPI.Services
{
    public class ProveedoresService
    {
        private readonly ProveedoresRepository _proveedoresRepository;
        private readonly IMapper _mapper;

        public ProveedoresService(ProveedoresRepository proveedoresRepository, IMapper mapper)
        {
            _proveedoresRepository = proveedoresRepository;
            _mapper = mapper;
        }

        public Response<IEnumerable<ProveedorDTO>> GetAll()
        {
            List<ProveedorDTO> dataDTO = new List<ProveedorDTO>();
            try
            {
                var data = _proveedoresRepository.Find(x => x.Estatus == true);
                dataDTO = _mapper.Map<List<ProveedorDTO>>(data);

                return new Response<IEnumerable<ProveedorDTO>>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = dataDTO
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ProveedorDTO>>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = dataDTO
                };
            }
        }

        public Response<ProveedorDTO> GetOne(int id)
        {
            ProveedorDTO dataDTO = new ProveedorDTO();
            try
            {
                var data = _proveedoresRepository.GetById(id);
                if (id != data.ProveedorId)
                    throw new Exception("No coincide el ID del proveedor, favor de verificar");

                dataDTO = _mapper.Map<ProveedorDTO>(data);

                return new Response<ProveedorDTO>
                {
                    HttpStatusCode = data == null ? System.Net.HttpStatusCode.NotFound : System.Net.HttpStatusCode.OK,
                    Result = dataDTO
                };
            }
            catch (Exception ex)
            {
                return new Response<ProveedorDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = dataDTO
                };
            }
        }

        public Response<ProveedorDTO> Add(ProveedorDTO proveedor, string usuario)
        {
            try
            {
                var entity = _mapper.Map<Proveedores>(proveedor);
                entity.Estatus = true;
                entity.FechaAlta = DateTime.Now;
                entity.UsuarioAlta = usuario;

                _proveedoresRepository.Add(entity);
                _proveedoresRepository.SaveChange();

                proveedor.ProveedorId = entity.ProveedorId;

                return new Response<ProveedorDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = proveedor
                };
            }
            catch (Exception ex)
            {
                return new Response<ProveedorDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public Response<ProveedorDTO> Put(int id, ProveedorDTO proveedor, string usuario)
        {
            try
            {
                if (id != proveedor.ProveedorId)
                    throw new Exception("No coincide el ID del proveedor, favor de verificar");

                var entity = _proveedoresRepository.GetById(id);
                if (entity == null)
                    throw new Exception("No se encontró el proveedor");

                entity.NombreCortoProveedor = proveedor.NombreCortoProveedor;
                entity.NombreLargoProveedor = proveedor.NombreLargoProveedor;
                entity.TelefonoProveedor = proveedor.TelefonoProveedor;
                entity.CorreoProveedor = proveedor.CorreoProveedor;
                entity.DireccionProveedor = proveedor.DireccionProveedor;
                entity.FechaMod = DateTime.Now;
                entity.UsuarioMod = usuario;

                _proveedoresRepository.Update(entity);
                _proveedoresRepository.SaveChange();

                return new Response<ProveedorDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.OK,
                    Result = proveedor
                };
            }
            catch (Exception ex)
            {
                return new Response<ProveedorDTO>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public Response<bool> Delete(int id, string usuario)
        {
            try
            {
                var entity = _proveedoresRepository.GetById(id);

                if (entity == null)
                    throw new Exception("No coincide el ID del proveedor, favor de verificar");

                entity.Estatus = false;
                entity.FechaMod = DateTime.Now;
                entity.UsuarioMod = usuario;

                _proveedoresRepository.Delete(entity);
                _proveedoresRepository.SaveChange();

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
