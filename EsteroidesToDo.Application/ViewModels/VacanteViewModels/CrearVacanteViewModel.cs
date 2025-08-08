using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace EsteroidesToDo.Application.ViewModels.VacanteViewModel { 
public class CrearVacanteViewModel
{
        [Required, NotNull]
        [MinLength(3, ErrorMessage = "tenes q poner una mas grande")]

        public string Titulo { get; set; }

        [Required, NotNull]
        public string Descripcion { get; set; }

    }
}