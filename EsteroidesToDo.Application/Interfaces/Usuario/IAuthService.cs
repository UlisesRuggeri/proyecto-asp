using EsteroidesToDo.Application.DTOs.UsuarioDtos;
using System.Security.Claims;


namespace EsteroidesToDo.Application.Interfaces.Usuario
{
    public interface IAuthService
    {
        ClaimsPrincipal CreateClaimsPrincipal(LoginDto usuario);
    }

}
