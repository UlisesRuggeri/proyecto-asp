
using EsteroidesToDo.Application.Common;
using EsteroidesToDo.Application.Interfaces.Decorators;
using EsteroidesToDo.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EsteroidesToDo.Infrastructure.CacheDecorators
{
    public class UsuarioInfoCacheDecorator : IUsuarioInfoService
    {
        private readonly IUsuarioInfoService _inner;
        private readonly IMemoryCache _cache;

        public UsuarioInfoCacheDecorator(IUsuarioInfoService inner, IMemoryCache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<OperationResult<UsuarioInfoViewModel>> ObtenerUsuarioInfo(string email)
        {
            if (_cache.TryGetValue<OperationResult<UsuarioInfoViewModel>>(email, out var cached))
                return cached;

            var result = await _inner.ObtenerUsuarioInfo(email);
            if (result.IsSuccess)
            {
                _cache.Set(email, result);
            }

            return result;
        }
    }
}
/*
 builder.Services.AddMemoryCache();

// Servicio real
builder.Services.AddScoped<IUsuarioInfoService, UsuarioInfoService>();

// Decorador de caché
builder.Services.Decorate<IUsuarioInfoService, UsuarioInfoCacheDecorator>();

 */