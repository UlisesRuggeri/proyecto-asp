using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EsteroidesToDo.Filters
{
    /// Filtro que impide el acceso a usuarios cuyo email no sea corporativo.
    
    public class SoloUsuariosConCorreoCorporativoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Buscamos el email en las claims del usuario autenticado
            var email = context.HttpContext.User?.Identity?.Name;

            // Verificamos si es nulo o si no termina con @tuempresa.com
            if (string.IsNullOrEmpty(email) || !email.EndsWith("@tuempresa.com", StringComparison.OrdinalIgnoreCase))
            {
                // Redirigimos al usuario a una página de acceso denegado
                context.Result = new RedirectToActionResult("AccesoDenegado", "Home", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
