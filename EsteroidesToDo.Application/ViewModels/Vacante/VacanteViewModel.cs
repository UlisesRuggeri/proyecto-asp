
using EsteroidesToDo.Application.Common;

namespace EsteroidesToDo.Application.ViewModels
{
    public class VacanteViewModel
    {
        public int VacanteId { get; set; }
        public string Titulo { get; set; }
        public string Estado { get; set; }
        public List<PostuladoViewModel> Postulados { get; set; }
    }
}
