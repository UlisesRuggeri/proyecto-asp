using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Application.Services.VacanteServices;
using EsteroidesToDo.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EsteroidesToDo.Controllers
{
    /// <summary>
    /// Controller for managing job vacancies and applicants.
    /// </summary>
    [Authorize]
    public class VacanteController : Controller
    {
        private readonly CrearVacanteService _crearVacanteService;
        private readonly VacanteInfoService _vacanteInfoService;
        private readonly PostulacionesVacantesService _postulacionesVacantesService;

        public VacanteController(
            VacanteInfoService vacanteInfo,
            CrearVacanteService crearVacanteService,
            PostulacionesVacantesService postulacionesVacantesService)
        {
            _vacanteInfoService = vacanteInfo;
            _crearVacanteService = crearVacanteService;
            _postulacionesVacantesService = postulacionesVacantesService;
        }

        // ---------------------------
        // Helper Method
        // ---------------------------
        private int? GetUserId()
        {
            return int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int id) ? id : (int?)null;
        }

        // ---------------------------
        // Vacancy Views
        // ---------------------------
        [HttpGet]
        public IActionResult CrearVacante() => View();

        public async Task<IActionResult> Vacantes()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var viewModel = await _vacanteInfoService.ObtenerVistaVacantes(userId.Value);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CrearVacante(CrearVacanteViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            try
            {
                var dto = new VacanteDto
                {
                    Titulo = model.Titulo,
                    Descripcion = model.Descripcion,
                    UsuarioId = userId.Value
                };

                await _crearVacanteService.CrearVacante(dto);
                return RedirectToAction(nameof(Vacantes));
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Vacantes));
            }
        }

        // ---------------------------
        // Applicants Management
        // ---------------------------
        [HttpGet]
        public async Task<IActionResult> ListaPostulados()
        {
            var empresaId = GetUserId();
            if (empresaId == null) return BadRequest();

            var postulados = await _postulacionesVacantesService
                .ObtenerTodasLasVacantesDeUnaEmpresa(empresaId.Value);

            return View("MisVacantes", postulados);
        }

        public async Task<IActionResult> AceptarPostulado(int vacanteId, int usuarioId)
        {
            await _postulacionesVacantesService.AceptarPostulado(vacanteId, usuarioId);
            return RedirectToAction(nameof(ListaPostulados), new { vacanteId });
        }

        public async Task<IActionResult> RechazarPostulado(int usuarioId, int vacanteId)
        {
            await _postulacionesVacantesService.RechazarPostulado(usuarioId, vacanteId);
            return RedirectToAction(nameof(ListaPostulados), new { vacanteId });
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstadoVacante(int vacanteId, string nuevoEstado)
        {
            await _postulacionesVacantesService.CambiarEstadoVacante(vacanteId, nuevoEstado);
            return RedirectToAction(nameof(Vacantes));
        }
    }
}
