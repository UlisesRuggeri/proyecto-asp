

using EsteroidesToDo.Application.Interfaces.Cache;
using EsteroidesToDo.Application.Interfaces.Decorators;
using EsteroidesToDo.Application.Services.UserServices;
using EsteroidesToDo.Infrastructure.CacheDecorators;
using EsteroidesToDo.Infrastructure.CacheManager;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace EsteroidesToDo.Infrastructure.DependencyInjection
{
    public static class CacheDecoratorsRegistration
    {
        public static void AddCacheDecorators(this IServiceCollection services)
        {
            services.AddScoped<UsuarioInfoService>();

            services.AddScoped<IUsuarioInfoService>(provider =>
            {
                var inner = provider.GetRequiredService<UsuarioInfoService>();
                var cache = provider.GetRequiredService<IMemoryCache>();
                return new UsuarioInfoCacheDecorator(inner, cache);
            });

            services.AddScoped<IClearCacheService, UsuarioInfoCacheService>();

        }
    }
}
