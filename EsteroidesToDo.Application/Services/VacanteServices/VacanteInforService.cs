using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Application.Interfaces.Vacante;
using EsteroidesToDo.Application.ViewModels;
using EsteroidesToDo.Domain.Filters;
using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Domain.Pagination;

public class VacanteInfoService : IVacanteQueryService
{
    private readonly IVacanteRepository _vacanteRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public VacanteInfoService(IVacanteRepository vacanteRepository, IUsuarioRepository usuarioRepository)
    {
        _vacanteRepository = vacanteRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<OperationResult<PagedResult<VacanteInfoDto>>> ObtenerVacantesPaginadas(string? vacanteSolicitada,int? userId, int page, int pageSize)
    {
        var empresaDelUsuario = await _usuarioRepository.ObtenerEmpresaDelUsuarioAsync(userId);
        
        var filter = new VacanteFilter
        {
            SearchTerm = vacanteSolicitada
        };

        var pagedVacantes = await _vacanteRepository.GetVacantesAsync(filter, page, pageSize);

        var dtoItems = new List<VacanteInfoDto>();

        foreach (var v in pagedVacantes.Items)
        {
            bool puedePostular = empresaDelUsuario == null && !await _vacanteRepository.UsuarioPuedePostular(v.Id, userId);

            dtoItems.Add(new VacanteInfoDto
            {
                VacanteId = v.Id,
                Titulo = v.Titulo,
                Descripcion = v.Descripcion,
                Estado = v.Estado,
                EmpresaNombre = v.Empresa.Nombre,
                PuedePostular = puedePostular,
                FechaCreacion = v.FechaCreacion
            });
        }

        var result = new PagedResult<VacanteInfoDto>
        {
            Items = dtoItems,
            TotalCount = pagedVacantes.TotalCount,
            PageNumber = pagedVacantes.PageNumber,
            PageSize = pagedVacantes.PageSize
        };

        return OperationResult<PagedResult<VacanteInfoDto>>.Success(result);
    }

    public async Task<OperationResult<VacantesVistaViewModel>> ObtenerVistaVacantes(string? vacanteSolicitada,int? userId, int page , int pageSize )
    {
        var esDuenio = await _vacanteRepository.PuedeCrearVacanteAsync(userId);
        var vacantesPaginadas = await ObtenerVacantesPaginadas(vacanteSolicitada, userId, page, pageSize);

        var VacantesVistaVM = new VacantesVistaViewModel
        {
            EsDuenio = esDuenio,
            Vacantes = vacantesPaginadas.Value.Items,
            PageNumber = vacantesPaginadas.Value.PageNumber,
            PageSize = vacantesPaginadas.Value.PageSize,
            TotalCount = vacantesPaginadas.Value.TotalCount
        };

        return OperationResult<VacantesVistaViewModel>.Success(VacantesVistaVM);
    }
        
}
    