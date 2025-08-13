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

        /// <summary>
        /// Initializes a new instance of the EmpresaController.
        /// </summary>
        /// <param name="crearEmpresaService">Service responsible for creating and assigning companies.</param>
        public EmpresaController(CrearEmpresaService crearEmpresaService)
        {
            _crearEmpresaService = crearEmpresaService;
        }

        /// <summary>
        /// Displays the Create Company form (only for authenticated users).
        /// </summary>
        [Authorize]
        [HttpGet]
        public IActionResult CrearEmpresa()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new company and assigns it to the logged-in user.
        /// </summary>
        /// <param name="model">Data entered by the user in the form.</param>
        /// <returns>Redirects to Home if successful, or returns an error response.</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CrearEmpresa(EmpresaViewModel model)
        {
            // Validate the received model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Retrieve the user's ID from authentication claims
            // "out int idDuenio" is an output parameter that gets the parsed value from TryParse
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idDuenio))
                return Unauthorized("User not authenticated");

            // Map ViewModel data to a Data Transfer Object (DTO)
            var dto = new EmpresaDto
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                idDuenio = idDuenio
            };

            // Attempt to create and assign the company
            var result = await _crearEmpresaService.CrearYAsignarEmpresa(dto);

            // If the operation was successful, redirect to the Home page
            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            // If failed, return a BadRequest with the error message
            return BadRequest(result.Error);
        }
    }
}
