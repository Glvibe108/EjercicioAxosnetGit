using Common;
using EjercicioAxonet.Common.DTOs;
using EjercicioAxonetAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioAxonetAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RecibosController : ControllerBase
    {
        private RecibosService _recibosService;

        public RecibosController(RecibosService recibosService)
        {
            _recibosService = recibosService;
        }

        [HttpGet("ListadoRecibos")]
        public ActionResult GetAll()
        {
            var response = _recibosService.GetAll();
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpGet("BuscarRecibo/{id}")]
        public ActionResult GetOne(int id)
        {
            var response = _recibosService.GetOne(id);
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] ReciboDTO recibo)
        {
            var usuario = User.Identity.Name;
            var response = _recibosService.Add(recibo,usuario);
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
