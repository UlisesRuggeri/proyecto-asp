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
        private const int DEFAULT_PAGE_SIZE = 10;
        private const int MAX_PAGE_SIZE = 25;

        private readonly BorrarVacanteService _borrarVacanteService;
        private readonly CrearVacanteService _crearVacanteService;
        private readonly VacanteInfoService _vacanteInfoService;
        private readonly PostulacionesVacantesService _postulacionesVacantesService;

        public VacanteController(
            VacanteInfoService vacanteInfo,
            CrearVacanteService crearVacanteService,
            PostulacionesVacantesService postulacionesVacantesService,
            BorrarVacanteService borrarVacanteService)
        {
            _borrarVacanteService = borrarVacanteService;
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

        public async Task<IActionResult> Vacantes(
            [FromQuery] string? q,
            [FromQuery(Name = "page" )] int pageNumber = 1,
            [FromQuery(Name = "Size")] int pageSize = DEFAULT_PAGE_SIZE
            )
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? DEFAULT_PAGE_SIZE : Math.Min(pageSize, MAX_PAGE_SIZE);


            int? userId = GetUserId();
            var op = await _vacanteInfoService.ObtenerVistaVacantes(q, userId, pageNumber, pageSize);


            if (!op.IsSuccess)
                return Problem(op.Error ?? "No se pudieron obtener las vacantes.");


            ViewData["q"] = q; 
            return View(op.Value);
        }

        

        [HttpPost]
        public async Task<IActionResult> CrearVacante(CrearVacanteViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            if (userId == null) return Unauthorized();

          
           var dto = new VacanteDto
           {
              Titulo = model.Titulo,
              Descripcion = model.Descripcion,
              UsuarioId = userId.Value
           };

            var result = await _crearVacanteService.CrearVacante(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return RedirectToAction(nameof(Vacantes));
            
         
        }

        // ---------------------------
        // Applicants Management
        // ---------------------------
        [HttpGet]
        public async Task<IActionResult> ListaPostulados()
        {
            var empresaId = GetUserId();
            if (empresaId == null) return BadRequest();

            var result = await _postulacionesVacantesService
                .ObtenerTodasLasVacantesDeUnaEmpresa(empresaId.Value);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            var vacantesVM = result.Value.Select(v => new VacanteViewModel
            {
                VacanteId = v.Id,
                Titulo = v.Titulo,
                Estado = v.Estado,
                Postulados = v.UsuarioVacantes
                    .Select(uv => new PostuladoViewModel
                    {
                        UsuarioId = uv.UsuarioId,
                        VacanteId = uv.VacanteId,
                        PropuestaTexto = uv.PropuestaTexto,
                        Estado = uv.Estado
                    }).ToList(),
            }).ToList();

            return View("MisVacantes", vacantesVM);

        }


        public async Task<IActionResult> AceptarPostulado(int vacanteId, int usuarioId)
        {
            var result = await _postulacionesVacantesService.AceptarPostulado(vacanteId, usuarioId);
            if (result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return RedirectToAction(nameof(ListaPostulados), new { vacanteId });
        }

        public async Task<IActionResult> RechazarPostulado(int vacanteId, int usuarioId)
        {
            var result = await _postulacionesVacantesService.RechazarPostulado(vacanteId, usuarioId);
            if (result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return RedirectToAction(nameof(ListaPostulados), new { vacanteId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Postular(int VacanteId)
        {
            var UsuarioId = GetUserId();
            var model = new PostuladoViewModel
            {
                UsuarioId = (int)UsuarioId,
                VacanteId = VacanteId
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Postular(PostuladoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = new PostulanteDto
            {
                UsuarioId = model.UsuarioId,
                VacanteId = model.VacanteId,
                PropuestaTexto = model.PropuestaTexto,
                Estado = "Activo"
            };

            var result = await _postulacionesVacantesService.CrearPostulacion(dto);
            if (!result.IsSuccess) { return BadRequest(result.Error); }
            return RedirectToAction(nameof(Vacantes));
        }

        [Authorize]
        public async Task<IActionResult> BorrarVacante(int VacanteId)
        {
            var UsuarioId = GetUserId();
            var result = await _borrarVacanteService.BorrarVacante(VacanteId, UsuarioId);
            if (!result.IsSuccess)  return BadRequest(result.Error); 

            return View("MisVacantes");
        }

    }
}
