using EsteroidesToDo.Application.DTOs;
using EsteroidesToDo.Application.Services.EmpresaServices;
using EsteroidesToDo.Application.ViewModels.EmpresaViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EsteroidesToDo.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly CrearEmpresaService _crearEmpresaService;

        public EmpresaController(CrearEmpresaService crearEmpresaService)
        {
            _crearEmpresaService = crearEmpresaService;
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CrearEmpresa(EmpresaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            //por si falla el claim:
            //out int idDuenio es una salida por referencia para sacar una variable desde un metodo(TryParse)
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idDuenio))
                return Unauthorized("Usuario no autenticado");

            var dto = new EmpresaDto
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                idDuenio = idDuenio
            };

            try
            {
                await _crearEmpresaService.CrearYAsignarEmpresa(dto);
                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

    }
}
