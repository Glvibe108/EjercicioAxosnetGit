using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EjercicioAxonet.Controllers
{
    public class MonedasController : Controller
    {
        private readonly MonedasService _monedasService;

        public MonedasController(MonedasService monedasService)
        {
            _monedasService = monedasService;
        }

        [HttpGet]
        public async Task<IActionResult> Monedas()
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _monedasService.ListadoMonedas(token);
            return View(data);
        }

        [HttpGet]
        public async Task<JsonResult> DatosMoneda(int id)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _monedasService.BuscarMoneda(token, id);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> NuevaMoneda(MonedaDTO monedaData)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _monedasService.NuevaMoneda(token, monedaData);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> EditarMoneda(MonedaDTO monedaData)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _monedasService.EditarMoneda(token, monedaData);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarMoneda(int id)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _monedasService.EliminarMoneda(token, id);
            return Json(data);
        }
    }
}
