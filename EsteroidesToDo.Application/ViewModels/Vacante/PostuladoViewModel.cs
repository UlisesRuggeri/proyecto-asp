
namespace EsteroidesToDo.Application.ViewModels
{
    public class PostuladoViewModel
    {

        //usuarioId y vacanteId es una llave compuesta
        public int UsuarioId { get; set; }
        public int VacanteId { get; set; }
        public string PropuestaTexto { get; set; }
        public string Estado { get; set; }
    }
}
