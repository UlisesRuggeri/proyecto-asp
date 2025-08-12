
using EsteroidesToDo.Application.DTOs.UsuarioDtos;
using System.Security.Claims;


namespace EsteroidesToDo.Application.Interfaces
{
    public interface IAuthService
    {
        ClaimsPrincipal CreateClaimsPrincipal(LoginDto usuario);
    }

}
