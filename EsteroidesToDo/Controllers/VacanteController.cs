using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Application.Services.VacanteServices;
using EsteroidesToDo.Application.ViewModels.CrearVacanteViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace EsteroidesToDo.Controllers
{
    public class VacanteController : Controller
    {
        private readonly CrearVacanteService _crearVacanteService;
        private readonly VacanteInfoService _vacanteInfoService;



        public VacanteController( VacanteInfoService vacanteInfo, CrearVacanteService crearVacanteService)
        {
            _crearVacanteService = crearVacanteService;
            _vacanteInfoService = vacanteInfo;
        }
        
        [Authorize]
        public async Task<IActionResult> Vacantes()
        {
            if(!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
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



    }
}
