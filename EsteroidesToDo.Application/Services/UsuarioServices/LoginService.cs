using EsteroidesToDo.Application.DTOs.UsuarioDtos;
using EsteroidesToDo.Domain.Interfaces;

namespace EsteroidesToDo.Application.Services.UserServices
{
    public class LoginService
    {
        private readonly IUsuarioRepository _repo;


        public LoginService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<LoginDto> VerificarLogin(string email, string password)
        {
            var usuario = await _repo.ObtenerPorEmailAsync(email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(password, usuario.ContraseniaHash))
                return null;



            return new LoginDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email
            };
        }


    }
}

