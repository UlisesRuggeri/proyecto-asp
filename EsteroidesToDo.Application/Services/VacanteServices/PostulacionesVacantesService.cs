

using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Models;

namespace EsteroidesToDo.Application.Services.VacanteServices
{
    public class PostulacionesVacantesService
    {
        private readonly IVacanteRepository _repo;
        private readonly IUsuarioRepository _usuarioRepository;

        public PostulacionesVacantesService(IVacanteRepository repo, IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _repo = repo;
        }

        public async Task<OperationResult<List<Vacante>>> ObtenerTodasLasVacantesDeUnaEmpresa(int usuarioId)
        {
            if (!await _repo.PuedeCrearVacanteAsync(usuarioId))
            {
                return OperationResult<List<Vacante>>.Failure("Usuario no tiene permiso para crear vacante");
            }

            var vacantes = await _repo.ObtenerPorEmpresaAsync(usuarioId);
            return OperationResult<List<Vacante>>.Success(vacantes);
        }

        public async Task<OperationResult<bool>> CambiarEstadoVacante(int vacanteId,string nuevoEstado)
        {
            var estadosPermitidos = new[] { "Activa", "Cerrada" };
            if (!estadosPermitidos.Contains(nuevoEstado))
                return OperationResult<bool>.Failure("Estado no permitido.");

            await _repo.ActualizarEstadoAsync(vacanteId, nuevoEstado);

            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<bool>> AceptarPostulado(int vacanteId, int usuarioId)
        {
            if (vacanteId == null) return OperationResult<bool>.Failure("Vacante no encontrada");
            if (usuarioId == null) return OperationResult<bool>.Failure("Usuario no encontrado");
            if(await _usuarioRepository.ObtenerEmpresaDelUsuarioAsync(usuarioId) != null) return OperationResult<bool>.Failure("Usuario ya pertenece a una empresa");

            await _repo.MarcarPostuladoComoAceptadoAsync(vacanteId, usuarioId);
            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<bool>> RechazarPostulado(int vacanteId, int usuarioId)
        {
            if (vacanteId == null) return OperationResult<bool>.Failure("Vacante no encontrada");
            if (usuarioId == null) return OperationResult<bool>.Failure("Usuario no encontrado");

            await _repo.MarcarPostuladoComoRechazadoAsync(vacanteId, usuarioId);
            return OperationResult<bool>.Success(true);
        }

    }
}
