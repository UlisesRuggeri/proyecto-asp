using EsteroidesToDo.Application.DTOs.UsuarioDtos;
using EsteroidesToDo.Domain.Interfaces;

namespace EsteroidesToDo.Application.Services.UserServices
{
    public class RegisterService
    {
        private readonly IUsuarioRepository _repo;

        public RegisterService(IUsuarioRepository repo) {
            _repo = repo;
        }

       public async Task RegistrarUsuario(RegisterDto dto)
        {

            if (await _repo.EmailExiste(dto.Email))
                throw new InvalidOperationException("El correo ya está registrado.");


            string hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                ContraseniaHash = hash
            };
            await _repo.Agregar(nuevoUsuario);
        }

    }
}

