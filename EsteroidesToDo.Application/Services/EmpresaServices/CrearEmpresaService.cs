using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.DTOs;
using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Models;

namespace EsteroidesToDo.Application.Services.EmpresaServices
{
    public class CrearEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public CrearEmpresaService(IEmpresaRepository EmpresaRepo, IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _empresaRepo = EmpresaRepo;
        }

        public async Task<OperationResult<bool>> CrearYAsignarEmpresa(EmpresaDto dto)
        {
            if (await _usuarioRepo.ObtenerEmpresaDelUsuarioAsync(dto.idDuenio) != null)
                return OperationResult<bool>.Failure("Usuario ya tiene empresa");
           
            var empresa = new Empresa
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                IdDuenio = dto.idDuenio,
            };
            
            await _empresaRepo.CrearEmpresa(empresa); 
            await _usuarioRepo.AgregarEmpresaId(dto.idDuenio, empresa.Id);

            return OperationResult<bool>.Success(true);
        }


    }
}

