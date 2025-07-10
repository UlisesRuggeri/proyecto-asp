using EsteroidesToDo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace EsteroidesToDo.Services
{
    public class LoginService
    {
        private readonly EsteroidesToDoDbContext _context;
        private readonly IConfiguration _config;

        public LoginService(EsteroidesToDoDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Usuario?> VerificarLogin(string email, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(password, usuario.ContraseniaHash))
                return null;

            return usuario;
        }


        /*un middleware para configurar los claims del jwt*/
        public string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

/*
ingreso: 

1 el usuario ingresa su email y su contrasenia 

2 los datos se envian al (ViewModel y luego al) controlador y se validan ahi mismo

3 le pasa los datos ya limpios al servicio para que busque si existe un usuario con las mismas credenciales

4 si las credenciales son exactamente iguales a la de un usuario registrado se inicia sesion y se guarda un token en el navegador del usuario
sino se envia un mensaje de que las credenciales son incorrectas
*/