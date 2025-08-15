using EsteroidesToDo.Models;

namespace EsteroidesToDo.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<Empresa?> ObtenerPorIdDuenioAsync(int idDuenio);
        Task CrearEmpresa(Empresa empresa);
        Task<string> ObtenerEmpresaPorIdUsuario(int id);
    }
}
