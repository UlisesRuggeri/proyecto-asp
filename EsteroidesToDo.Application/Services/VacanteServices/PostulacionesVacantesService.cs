

using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.DTOs.VacanteDtos;
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

        public async Task<OperationResult<bool>> CrearPostulacion(PostulanteDto dto)
        {
            if (dto.VacanteId == null) return OperationResult<bool>.Failure("Id Vacante null");
            if (dto.UsuarioId == null) return OperationResult<bool>.Failure("Id Usuario null");
            if (await _usuarioRepository.ObtenerEmpresaDelUsuarioAsync(dto.UsuarioId) != null) return OperationResult<bool>.Failure("El usuario ya pertenece a una empresa");
            var result = new Postulante 
            {
                VacanteId = dto.VacanteId,
                UsuarioId = dto.UsuarioId,
                PropuestaTexto = dto.PropuestaTexto,
                Estado = dto.Estado
            };
            await _repo.CrearPostulacion(result);
            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<bool>> UsuarioPuedePostular(int VacanteId,int? UsuarioId)
        {

            //verifica que existe un registro con las mismas "VacanteId, UsuarioId". por lo tanto si existe un registro no puede postular 
            return !await _repo.UsuarioPuedePostular(VacanteId, UsuarioId)?OperationResult<bool>.Success(true) : OperationResult<bool>.Failure("");
        }
    }
}
