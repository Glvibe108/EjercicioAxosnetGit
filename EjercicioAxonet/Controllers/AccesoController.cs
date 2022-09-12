using EjercicioAxonet.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Net;

namespace EjercicioAxonet.Controllers.Acceso
{
    public class AccesoController : Controller
    {
        private readonly AccesoService _accesoService;

        public AccesoController(AccesoService accesoService)
        {
            _accesoService = accesoService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string usr, string pwd)
        {
            var data = await _accesoService.Login(usr, pwd);

            if (data != null)
            {
                await HttpContext.SignInAsync(data.ClaimsPrincipal);
                Response.Cookies.Append("token", data.AccessToken.Token, new CookieOptions { Expires = data.AccessToken.Expiracion });

                return RedirectToAction("Index", "Home");
            }
            else{
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult Registro()
        {          
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegistroUsuario(string usrRes, string pwdRes)
        {
            var data = await _accesoService.Registro(usrRes, pwdRes);

            if (data)
                return RedirectToAction("Login");
            else
                return View(false);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {        
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("token");
            return RedirectToAction("Login");
        }
    }
}
