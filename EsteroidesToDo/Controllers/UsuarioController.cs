using EsteroidesToDo.Application.DTOs.UsuarioDtos;
using EsteroidesToDo.Application.Services.UserServices;
using EsteroidesToDo.Application.ViewModels.UsuarioViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EsteroidesToDo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly LoginService _loginService;
        private readonly RegisterService _registerService;
        private readonly UsuarioInfoService _usuarioInfoService;

        public UsuarioController(LoginService loginService, RegisterService registerService,UsuarioInfoService usuarioInfoService)
        {
            _usuarioInfoService = usuarioInfoService;
            _loginService = loginService;
            _registerService = registerService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Informacion(string? returnUrl = null)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var viewModel = await _usuarioInfoService.ObtenerUsuarioInfo(email);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var email = model.Email.Trim();
            var contrasenia = model.Password.Trim();

            var usuario = await _loginService.VerificarLogin(email, contrasenia);

            if (usuario == null)
                return Unauthorized(new { error = "Email o contraseña incorrectos." });


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errores = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                Console.WriteLine(errores);
                return BadRequest(ModelState);
            }

            await _registerService.RegistrarUsuario(new RegisterDto
            {
                Nombre = model.Nombre,
                Email = model.Email,
                Password = model.Password
            });

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
