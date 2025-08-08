using EsteroidesToDo.Application.Services.EmpresaServices;
using EsteroidesToDo.Application.Services.UserServices;
using EsteroidesToDo.Application.Services.VacanteServices;
using Microsoft.Extensions.DependencyInjection;

namespace EsteroidesToDo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<LoginService>();
            services.AddScoped<CrearEmpresaService>();
            //services.AddScoped<EstadoUsuarioService>();
            services.AddScoped<CrearVacanteService>();
            services.AddScoped<RegisterService>();
            services.AddScoped<VacanteInfoService>();
            services.AddScoped<UsuarioInfoService>();

            return services;
        }
    }
}
