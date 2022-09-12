using EjercicioAxonet.Common.DTOs;
using EjercicioAxonet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EjercicioAxonet.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ProveedoresService _serviceProveedores;

        public ProveedoresController(ProveedoresService serviceProveedores)
        {
            _serviceProveedores = serviceProveedores;
        }

        [HttpGet]
        public async Task<IActionResult> Proveedores()
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _serviceProveedores.ListadoProveedores(token);
            return View(data);
        }

        [HttpGet]
        public async Task<JsonResult> DatosProveedor(int id)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _serviceProveedores.BuscarProveedor(token, id);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> NuevoProveedor(ProveedorDTO proveedor)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _serviceProveedores.NuevoProveedor(token, proveedor);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> EditarProveedor(ProveedorDTO proveedor)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _serviceProveedores.EditarProveedor(token, proveedor);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarProveedor(int id)
        {
            string token = HttpContext.Request.Cookies["token"].ToString();
            var data = await _serviceProveedores.EliminarProveedor(token, id);
            return Json(data);
        }
    }
}
