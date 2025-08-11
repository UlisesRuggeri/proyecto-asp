
namespace EsteroidesToDo.Application.ViewModels
{
    internal class VacanteViewModel
    {
        public int VacanteId { get; set; }
        public string Titulo { get; set; }
        public string Estado { get; set; }
        public List<PostuladoViewModel> Postulados { get; set; }
    }
}
