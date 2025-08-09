using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Application.Interfaces;
using EsteroidesToDo.Application.ViewModels.VacanteVistaViewModel;
using EsteroidesToDo.Domain.Interfaces;

public class VacanteInfoService : IVacanteInfoService
{
    private readonly IVacanteRepository _repo;

    public VacanteInfoService(IVacanteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<VacanteInfoDto>> ObtenerTodasLasVacantes()
    {
        var vacantes = await _repo.ObtenerTodasLasVacantes();

        return vacantes.Select(v => new VacanteInfoDto
        {
            Titulo = v.Titulo,
            Descripcion = v.Descripcion,
            Estado = v.Estado,
            EmpresaNombre = v.Empresa.Nombre,
            FechaCreacion = v.FechaCreacion

        }).ToList();
    }

    public async Task<VacantesVistaViewModel> ObtenerVistaVacantes(int userId)
    {
        var esDuenio = await _repo.UsuarioPuedeCrearVacante(userId);
        var vacantes = await ObtenerTodasLasVacantes(); 
        return new VacantesVistaViewModel
        {
            EsDuenio = esDuenio,
            Vacantes = vacantes
        };
    }

}
