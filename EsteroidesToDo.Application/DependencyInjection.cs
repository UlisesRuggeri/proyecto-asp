using EsteroidesToDo.Application.Services.AutenticacionServices;
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
            //usuario
            services.AddScoped<LoginService>();
            services.AddScoped<AuthService>();
            services.AddScoped<RegisterService>();
            services.AddScoped<UsuarioInfoService>();

            //empresa
            services.AddScoped<CrearEmpresaService>();
            
            //vacante
            services.AddScoped<CrearVacanteService>();
            services.AddScoped<VacanteInfoService>();
            services.AddScoped<PostulacionesVacantesService>();
            services.AddScoped<BorrarVacanteService>();

            return services;
        }
    }
}
