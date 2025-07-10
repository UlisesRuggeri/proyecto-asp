using EsteroidesToDo.Data;
using Microsoft.EntityFrameworkCore;

namespace EsteroidesToDo.Services
{
    public class RegisterService
    {
        private readonly EsteroidesToDoDbContext _context;

        public RegisterService(EsteroidesToDoDbContext context) {
            _context = context;
        }

       public async Task RegistrarUsuario(string nombre, string email, string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);

            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                ContraseniaHash = hash
            };
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
        }

    }
}

/*registro:

1 el usuario ingresa los datos nombre,email y contrasenia 

2 esos datos se envian al (ViewModels y luego al) controlador (por el verbo post) 

3 el controlador valida los datos, y se los manda limpios al servicio 

4 el servicio verifica si el email existe o no en la db para que no se repitan

5 se hashea la contrasenia y se crea un nuevo objeto user con esos datos si todo es ok
*/