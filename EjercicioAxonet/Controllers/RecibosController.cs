using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EjercicioAxonet.Controllers
{
    public class RecibosController : Controller
    {
        private readonly RecibosService _recibosService;
        private readonly MonedasService _monedasService;
        private readonly ProveedoresService _proveedoresService;

        public RecibosController(RecibosService serviceProveedores, MonedasService monedasService, ProveedoresService proveedoresService)
        {
            _recibosService = serviceProveedores;
            _monedasService = monedasService;
            _proveedoresService = proveedoresService;
        }
        [HttpGet]
        public async Task<IActionResult> Recibos()
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _recibosService.ListadoRecibos(token);
            return View(data);
        }

        [HttpGet]
        public async Task<JsonResult> _modalVerRecibos(int id)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _recibosService.BuscarRecibo(token, id);
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> ListadoMonedas()
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _monedasService.SelectListMonedas(token);
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> ListadoProveedores()
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _proveedoresService.SelectListProveedores(token);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> NuevoRecibo(ReciboDTO recibo)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _recibosService.NuevoRecibo(token, recibo);
            return Json(data);
        }
    }
}
