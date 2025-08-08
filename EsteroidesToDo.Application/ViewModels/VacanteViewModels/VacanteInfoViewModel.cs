using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace EsteroidesToDo.Application.ViewModels.VacanteViewModel
{
    public class VacanteInfoViewModel
    {
        [Required, NotNull]
        public string Titulo { get; set; }

        [Required, NotNull]
        public string Descripcion { get; set; }

        [Required, NotNull]
        public string Estado { get; set; }

        [Required, NotNull]
        public string EmpresaNombre { get; set; }

        [Required, NotNull]
        public DateTime FechaCreacion { get; set; }

    }
}