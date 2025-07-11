using EsteroidesToDo.Data;
using EsteroidesToDo.Services;
using EsteroidesToDo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteroidesToDo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly EsteroidesToDoDbContext _context;
        private readonly LoginService _loginService;
        private readonly RegisterService _registerService;

        public UsuarioController(EsteroidesToDoDbContext context, LoginService loginService, RegisterService registerService)
        {
            _context = context;
            _loginService = loginService;
            _registerService = registerService;
            
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Devuelve la vista Login.cshtml
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Devuelve la vista Login.cshtml
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)return View(model);

            var usuario = await _loginService.VerificarLogin(model.Email, model.Password);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Credenciales inválidas");
                return View(model);
            }

            var tokenGenerado = _loginService.GenerarToken(usuario);
            Response.Cookies.Append("jwtToken", tokenGenerado, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
            return RedirectToAction("Index", "Home");


        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            bool existe = await _context.Usuarios.AnyAsync(u => u.Email == model.Email);
            if (existe) throw new Exception("Este Correo ya esta registrado");

            await _registerService.RegistrarUsuario(model.Nombre, model.Email, model.ContraseniaHash);

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
