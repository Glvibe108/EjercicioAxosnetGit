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
    public class MonedasController : ControllerBase
    {
        private MonedasService _monedasService;

        public MonedasController(MonedasService monedasService)
        {
            _monedasService = monedasService;
        }

        [HttpGet("ListadoMonedas")]
        public ActionResult GetAll()
        {

            var response = _monedasService.GetAll();
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpGet("BuscarMoneda/{id}")]
        public ActionResult GetOne(int id)
        {
            var response = _monedasService.GetOne(id);
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] MonedaDTO moneda)
        {
            var usuario = User.Identity.Name;
            var response = _monedasService.Add(moneda, usuario);
            switch (response.HttpStatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                default:
                    return StatusCode((int)response.HttpStatusCode, new ResponseError { HttpStatusCode = response.HttpStatusCode, Message = response.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] MonedaDTO moneda)
        {
            var usuario = User.Identity.Name;
            var response = _monedasService.Put(id, moneda, usuario);
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
            var response = _monedasService.Delete(id, usuario);
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
