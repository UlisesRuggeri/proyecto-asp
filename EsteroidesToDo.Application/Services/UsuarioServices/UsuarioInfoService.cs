
using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.Interfaces.Decorators;
using EsteroidesToDo.Domain.Interfaces;

namespace EsteroidesToDo.Application.Services.UserServices
{
    public class UsuarioInfoService : IUsuarioInfoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public UsuarioInfoService(IUsuarioRepository usuarioRepository, IEmpresaRepository empresaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<OperationResult<UsuarioInfoViewModel>> ObtenerUsuarioInfo(string email)
        {
            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);
            if (usuario == null)
                return OperationResult<UsuarioInfoViewModel>.Failure("Email o usuario no encontrado");

            var empresa = await _empresaRepository.ObtenerEmpresaPorIdUsuario(usuario.Id);
            
            var UsuarioInfoVM = new UsuarioInfoViewModel
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                NombreEmpresa = empresa?.Nombre
            };

            return OperationResult<UsuarioInfoViewModel>.Success(UsuarioInfoVM);
        }
    }
}
