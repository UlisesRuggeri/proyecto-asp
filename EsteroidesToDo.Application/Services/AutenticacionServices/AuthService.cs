using EsteroidesToDo.Application.DTOs.UsuarioDtos;
using EsteroidesToDo.Application.Interfaces.Usuario;
using System.Security.Claims;


namespace EsteroidesToDo.Application.Services.AutenticacionServices
{
    public class AuthService : IAuthService
    {
        public ClaimsPrincipal CreateClaimsPrincipal(LoginDto usuario)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            return new ClaimsPrincipal(identity);
        }
    }

}
