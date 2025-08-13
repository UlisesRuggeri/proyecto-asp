using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Application.Interfaces;
using EsteroidesToDo.Application.ViewModels;
using EsteroidesToDo.Domain.Interfaces;

public class VacanteInfoService : IVacanteInfoService
{
    private readonly IVacanteRepository _repo;

    public VacanteInfoService(IVacanteRepository repo)
    {
        _repo = repo;
    }

    public async Task<OperationResult<List<VacanteInfoDto>>> ObtenerTodasLasVacantes()
    {
        var vacantes = await _repo.ObtenerTodasAsync();

        var result = vacantes.Select(v => new VacanteInfoDto
        {
            Titulo = v.Titulo,
            Descripcion = v.Descripcion,
            Estado = v.Estado,
            EmpresaNombre = v.Empresa.Nombre,
            FechaCreacion = v.FechaCreacion

        }).ToList();

        return OperationResult<List<VacanteInfoDto>>.Success(result);
    }

    public async Task<OperationResult<VacantesVistaViewModel>> ObtenerVistaVacantes(int userId)
    {
        var esDuenio = await _repo.PuedeCrearVacanteAsync(userId);
        var vacantesResult = await ObtenerTodasLasVacantes(); 
        var VacantesVistaVM = new VacantesVistaViewModel
        {
            EsDuenio = esDuenio,
            Vacantes = vacantesResult.Value
        };

        return OperationResult<VacantesVistaViewModel>.Success(VacantesVistaVM);
    }


}
