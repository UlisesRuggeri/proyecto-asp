using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Application.Services.VacanteServices;
using EsteroidesToDo.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace EsteroidesToDo.Controllers
{
    public class VacanteController : Controller
    {
        private readonly CrearVacanteService _crearVacanteService;
        private readonly VacanteInfoService _vacanteInfoService;
        private readonly PostulacionesVacantesService _postulacionesVacantesService;


        public VacanteController(VacanteInfoService vacanteInfo, CrearVacanteService crearVacanteService, PostulacionesVacantesService postulacionesVacantesService)
        {
            _postulacionesVacantesService = postulacionesVacantesService;
            _crearVacanteService = crearVacanteService;
            _vacanteInfoService = vacanteInfo;
        }

        [Authorize]
        [HttpGet]
        public IActionResult CrearVacante()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Vacantes()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var viewModel = await _vacanteInfoService.ObtenerVistaVacantes(userId);

            return View("Vacantes", viewModel);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CrearVacante(CrearVacanteViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                return Unauthorized();

            try
            {
                var dto = new VacanteDto
                {
                    Titulo = model.Titulo,
                    Descripcion = model.Descripcion,
                    UsuarioId = userId
                };

                await _crearVacanteService.CrearVacante(dto);
                return RedirectToAction("Vacantes");
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Vacantes");
            }
        }

        /**********************************************************************************/


        // 1. GET para mostrar la lista de postulados de una vacante
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListaPostulados()
        {
            var EmpresaId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            if (EmpresaId == null)
            {
                return BadRequest();
            }
            var postulados = await _postulacionesVacantesService.ObtenerTodasLasVacantesDeUnaEmpresa(int.Parse(EmpresaId));
            return View("MisVacantes", postulados);
        }

        // 2.  para aceptar un postulado, se lo agrega a la empresa y recibe una notificacion, (se elimina de la tabla UsuarioVacantes)
        [Authorize]
        public async Task<IActionResult> AceptarPostulado(int usuarioId, int vacanteId)
        {
            return RedirectToAction("ListaPostulados", new { vacanteId });
        }

        // 3. para ignorar/rechazar un postulado(se elimina de la tabla UsuarioVacantes)
        [Authorize]
        public async Task<IActionResult> RechazarPostulado(int usuarioId, int vacanteId)
        {
            return RedirectToAction("ListaPostulados", new { vacanteId });
        }

        // 4. cambia el estado de la vacante/elimina la vacante
        [Authorize]
        public async Task<IActionResult> cambiarEstadoVacante(int usuarioId, int vacanteId)
        {
            
            return RedirectToAction("ListaPostulados", new { vacanteId });
        }

    }
}
