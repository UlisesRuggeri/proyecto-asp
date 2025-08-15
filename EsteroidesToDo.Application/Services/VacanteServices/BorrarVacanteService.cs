
using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Domain.Interfaces;

namespace EsteroidesToDo.Application.Services.VacanteServices
{
    public class BorrarVacanteService
    {
        private readonly IVacanteRepository _repo;

        public BorrarVacanteService(IVacanteRepository repo)
        {
            _repo = repo;
        }

        public async Task<OperationResult<bool>> BorrarVacante(int VacanteId, int? UsuarioId)
        {
            if (UsuarioId == null) return OperationResult<bool>.Failure("UsuarioID == null");
            if (!await _repo.PuedeCrearVacanteAsync(UsuarioId)) return OperationResult<bool>.Failure("El usuario no puede crear vacante, por lo tanto no puede borrarla");

            await _repo.BorrarVacante(VacanteId);
            return OperationResult<bool>.Success(true);
        }
    }
}
