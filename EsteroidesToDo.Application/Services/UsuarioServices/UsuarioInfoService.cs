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

        public async Task<UsuarioInfoViewModel?> ObtenerUsuarioInfo(string email)
        {
            var usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);
            if (usuario == null) return null;

            var empresa = await _empresaRepository.ObtenerPorIdDuenioAsync(usuario.Id);

            return new UsuarioInfoViewModel
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                PerteneceAEmpresa = empresa != null,
                NombreEmpresa = empresa?.Nombre
            };
        }
    }
}
