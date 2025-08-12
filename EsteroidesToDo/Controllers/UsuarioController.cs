using EsteroidesToDo.Application.DTOs.UsuarioDtos;
using EsteroidesToDo.Application.Services.AutenticacionServices;
using EsteroidesToDo.Application.Services.UserServices;
using EsteroidesToDo.Application.ViewModels.UsuarioViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EsteroidesToDo.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication, registration, and profile information.
    /// </summary>
    public class UsuarioController : Controller
    {
        private readonly AuthService _authService;
        private readonly LoginService _loginService;
        private readonly RegisterService _registerService;
        private readonly UsuarioInfoService _usuarioInfoService;

        public UsuarioController(
            LoginService loginService,
            RegisterService registerService,
            UsuarioInfoService usuarioInfoService
            AuthService authService)
        {
            _authService = authService;
            _loginService = loginService;
            _registerService = registerService;
            _usuarioInfoService = usuarioInfoService;
        }

        // ---------------------------
        // Helper Methods
        // ---------------------------
        private string? GetUserEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }

        private int? GetUserId()
        {
            return int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int id) ? id : (int?)null;
        }

        // ---------------------------
        // User Info
        // ---------------------------
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Informacion(string? returnUrl = null)
        {
            var email = GetUserEmail();
            if (string.IsNullOrEmpty(email)) return Unauthorized();

            var viewModel = await _usuarioInfoService.ObtenerUsuarioInfo(email);
            ViewData["ReturnUrl"] = returnUrl;
            return View(viewModel);
        }

        // ---------------------------
        // Authentication Views
        // ---------------------------
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

        // ---------------------------
        // Authentication Actions
        // ---------------------------
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _loginService.VerificarLogin(model.Email.Trim(), model.Password.Trim());
            if (usuario == null)
                return Unauthorized(new { error = "Email or password incorrect." });

            var principal = _authService.CreateClaimsPrincipal(usuario);
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
                Nombre = model.Nombre.Trim(),
                Email = model.Email.Trim(),
                Password = model.Password
            });

            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
