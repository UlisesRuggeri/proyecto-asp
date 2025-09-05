

using EsteroidesToDo.Application.Interfaces.Usuario;
using StackExchange.Redis;

namespace EsteroidesToDo.Infrastructure;

public class EmailRateLimiter : IEmailRateLimiter
{
    private readonly StackExchange.Redis.IDatabase _db;
    private readonly int _limit = 3;
    private readonly TimeSpan _window = TimeSpan.FromMinutes(30);

    public EmailRateLimiter(IConnectionMultiplexer mux)
    {
        _db = mux.GetDatabase();
    }

    public async Task<bool> IsAllowedAsync(string email)
    {
        var key = $"signup:{email.ToLower()}";
        var count = await _db.StringIncrementAsync(key);
        if (count == 1)
            await _db.KeyExpireAsync(key, _window);

        return count <= _limit;
    }

    public async Task<int> GetCountAsync(string email)
    {
        var val = await _db.StringGetAsync($"signup:{email.ToLower()}");
        return val.IsNullOrEmpty ? 0 : (int)val;
    }
}

