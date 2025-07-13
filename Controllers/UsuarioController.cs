using EsteroidesToDo.Data;
using EsteroidesToDo.Services;
using EsteroidesToDo.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

            var claims = new List<Claim>
            {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim(ClaimTypes.Email, usuario.Email)
            };

        
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal, new AuthenticationProperties
            {
                IsPersistent = true, 
                ExpiresUtc = DateTime.UtcNow.AddHours(1)
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
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }

    }
}
