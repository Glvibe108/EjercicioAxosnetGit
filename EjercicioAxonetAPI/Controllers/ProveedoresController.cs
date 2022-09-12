using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonetAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EjercicioAxonetAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private ProveedoresService _proveedoresService;

        public ProveedoresController(ProveedoresService proveedoresService)
        {
            _proveedoresService = proveedoresService;
        }

        [HttpGet("ListadoProveedores")]
        public ActionResult GetAll()
        {
            var response = _proveedoresService.GetAll();
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpGet("BuscarProveedor/{id}")]
        public ActionResult GetOne(int id)
        {
            var response = _proveedoresService.GetOne(id);
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] ProveedorDTO proveedor)
        {
            var usuario = User.Identity.Name;
            var response = _proveedoresService.Add(proveedor, usuario);
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProveedorDTO proveedor)
        {
            var usuario = User.Identity.Name;
            var response = _proveedoresService.Put(id, proveedor, usuario);
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var usuario = User.Identity.Name;
            var response = _proveedoresService.Delete(id, usuario);
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

    }
}
