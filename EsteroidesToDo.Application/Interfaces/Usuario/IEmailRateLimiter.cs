

namespace EsteroidesToDo.Application.Interfaces.Usuario;

public interface IEmailRateLimiter
{
    Task<bool> IsAllowedAsync(string email);
    Task<int> GetCountAsync(string email);
}
