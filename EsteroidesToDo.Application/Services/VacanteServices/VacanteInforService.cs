using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.DTOs.VacanteDtos;
using EsteroidesToDo.Application.Interfaces;
using EsteroidesToDo.Application.ViewModels;
using EsteroidesToDo.Domain.Interfaces;

public class VacanteInfoService : IVacanteInfoService
{
    private readonly IVacanteRepository _repo;
    private readonly IUsuarioRepository _usuarioRepository;

    public VacanteInfoService(IVacanteRepository repo, IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
        _repo = repo;
    }
    //ObtenerEmpresaDelUsuarioAsync(int usuarioId)
    public async Task<OperationResult<List<VacanteInfoDto>>> ObtenerTodasLasVacantes(int userId)
    {
        var vacantes = await _repo.ObtenerTodasAsync();

        var result = new List<VacanteInfoDto>();

        foreach (var v in vacantes)
        {
            //2 casos posibles: 
            //1_ el usuario pertenece a una empresa por lo tanto no puede postular
            //2_El usuario no pertenece a ninguna empresa pero ya postulo a X vacante
            var puedePostular = !await _repo.UsuarioPuedePostular(v.Id, userId);

            var empresaDelUsuario = await _usuarioRepository.ObtenerEmpresaDelUsuarioAsync(userId);
            if (empresaDelUsuario != null) puedePostular = false;

            Console.WriteLine($"VacanteId: {v.Id}, UsuarioId: {userId}, PuedePostular: {puedePostular}");


            result.Add(new VacanteInfoDto
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



        return OperationResult<List<VacanteInfoDto>>.Success(result);
    }

    public async Task<OperationResult<VacantesVistaViewModel>> ObtenerVistaVacantes(int userId)
    {
        var esDuenio = await _repo.PuedeCrearVacanteAsync(userId);
        var vacantesResult = await ObtenerTodasLasVacantes(userId); 
        var VacantesVistaVM = new VacantesVistaViewModel
        {
            EsDuenio = esDuenio,
            Vacantes = vacantesResult.Value
        };

        return OperationResult<VacantesVistaViewModel>.Success(VacantesVistaVM);
    }


}
