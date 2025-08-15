using EsteroidesToDo.Application.Common;


namespace EsteroidesToDo.Application.Interfaces.Decorators
{
    public interface IUsuarioInfoService
    {
        Task<OperationResult<UsuarioInfoViewModel>> ObtenerUsuarioInfo(string email);
    }
}
