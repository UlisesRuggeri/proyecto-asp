using EsteroidesToDo.Application.Interfaces.Cache;
using Microsoft.Extensions.Caching.Memory;


namespace EsteroidesToDo.Infrastructure.CacheManager
{

    public class UsuarioInfoCacheService : IClearCacheService
    {
        private readonly IMemoryCache _cache;
        public UsuarioInfoCacheService(IMemoryCache cache) => _cache = cache;

        public void ClearUserCache(string email) => _cache.Remove(email);
    }

}
