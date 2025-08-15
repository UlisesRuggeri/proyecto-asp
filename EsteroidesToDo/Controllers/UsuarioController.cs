using EsteroidesToDo.Application.DTOs.UsuarioDtos;
using EsteroidesToDo.Application.Interfaces.Cache;
using EsteroidesToDo.Application.Services.AutenticacionServices;
using EsteroidesToDo.Application.Services.UserServices;
using EsteroidesToDo.Application.ViewModels.UsuarioViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EsteroidesToDo.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication, registration, and profile information.
    /// Uses the Result Pattern for cleaner error handling between services and controller.
    /// </summary>
    public class UsuarioController : Controller
    {
        private readonly IClearCacheService _clearCacheService;
        private readonly AuthService _authService;
        private readonly LoginService _loginService;
        private readonly RegisterService _registerService;
        private readonly UsuarioInfoService _usuarioInfoService;

        public UsuarioController(
            LoginService loginService,
            RegisterService registerService,
            UsuarioInfoService usuarioInfoService,
            AuthService authService,
            IClearCacheService clearCacheService)
        {
            _clearCacheService = clearCacheService;
            _authService = authService;
            _loginService = loginService;
            _registerService = registerService;
            _usuarioInfoService = usuarioInfoService;
        }

        // ---------------------------
        // Helper Methods
        // ---------------------------
        private string? GetUserEmail() => User.FindFirstValue(ClaimTypes.Email);

        
        // ---------------------------
        // User Info
        // ---------------------------
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Informacion(string? returnUrl = null)
        {
            var email = GetUserEmail();
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var viewModel = await _usuarioInfoService.ObtenerUsuarioInfo(email);
            ViewData["ReturnUrl"] = returnUrl;
            return View(viewModel.Value);
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

            // Service returns OperationResult<Usuario>
            var result = await _loginService.VerificarLogin(model.Email.Trim(), model.Password.Trim());

            if (!result.IsSuccess)
                return Unauthorized(new { error = result.Error });

            var principal = _authService.CreateClaimsPrincipal(result.Value);
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

            // Service returns OperationResult<bool>
            var result = await _registerService.RegistrarUsuario(new RegisterDto
            {
                Nombre = model.Nombre.Trim(),
                Email = model.Email.Trim(),
                Password = model.Password
            });

            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var email = GetUserEmail();
            await HttpContext.SignOutAsync("CookieAuth");

            _clearCacheService.ClearUserCache(email);
            return RedirectToAction(nameof(Register));
        }
    }
}
