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
            return await _vacanteRepo.UsuarioPuedeCrearVacante(usuarioId);
        }

        public async Task CrearVacante(VacanteDto dto)
        {

            var empresaId = await _usuarioRepo.UsuarioYaTieneEmpresa(dto.UsuarioId);
            if (empresaId == null)
                throw new InvalidOperationException("No tenés empresa, no podés crear vacantes.");

            if (!await UsuarioPuedeCrearVacante(dto.UsuarioId))
                throw new InvalidOperationException("No estás autorizado para crear vacantes.");



            var nuevaVacante = new Vacante
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                UsuarioId = dto.UsuarioId,
                EmpresaId = (int)empresaId,
                Estado = "Activa"
            };

            await _vacanteRepo.CrearVacante(nuevaVacante);
        }
    }
}
