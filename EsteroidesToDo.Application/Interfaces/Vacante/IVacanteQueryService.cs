using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Application.ViewModels;
using EsteroidesToDo.Domain.Pagination;

namespace EsteroidesToDo.Application.Interfaces.Vacante
{

    public interface IVacanteQueryService
    {
        Task<OperationResult<PagedResult<VacanteInfoDto>>> ObtenerVacantesPaginadas(string? vacanteSolicitada, int? userId, int page, int pageSize);
        Task<OperationResult<VacantesVistaViewModel>> ObtenerVistaVacantes(string? vacanteSolicitada, int? userId, int page, int pageSize);
    }
}
