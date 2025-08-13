using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Domain.Interfaces;

namespace EsteroidesToDo.Application.Services.UserServices
{
    public class UsuarioInfoService
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
            if (usuario == null) return OperationResult<UsuarioInfoViewModel>.Failure("Usuario no encontrado");

            var empresa = await _empresaRepository.ObtenerPorIdDuenioAsync(usuario.Id);

            var UsuarioInfoVM = new UsuarioInfoViewModel
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                PerteneceAEmpresa = empresa != null,
                NombreEmpresa = empresa?.Nombre
            };

            return OperationResult<UsuarioInfoViewModel>.Success(UsuarioInfoVM);
        }
    }
}
