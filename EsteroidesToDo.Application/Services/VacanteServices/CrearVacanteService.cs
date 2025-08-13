using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Models;

namespace EsteroidesToDo.Application.Services.VacanteServices
{
    public class CrearVacanteService
    {
        private readonly IVacanteRepository _vacanteRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        public CrearVacanteService(IVacanteRepository vacanteRepo, IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _vacanteRepo = vacanteRepo;
        }

        public async Task<bool> UsuarioPuedeCrearVacante(int usuarioId)
        {
            return await _vacanteRepo.PuedeCrearVacanteAsync(usuarioId);
        }

        public async Task<OperationResult<bool>> CrearVacante(VacanteDto dto)
        {

            var empresaId = await _usuarioRepo.ObtenerEmpresaDelUsuarioAsync(dto.UsuarioId);
            if (empresaId == null)
                return OperationResult<bool>.Failure("No tenés empresa, no podés crear vacantes.");

            if (!await UsuarioPuedeCrearVacante(dto.UsuarioId))
                return OperationResult<bool>.Failure("No estás autorizado para crear vacantes.");



            var nuevaVacante = new Vacante
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                EmpresaId = (int)empresaId,
                Estado = "Activa"
            };

            await _vacanteRepo.AgregarVacanteAsync(nuevaVacante);
            return OperationResult<bool>.Success(true);
        }
    }
}
