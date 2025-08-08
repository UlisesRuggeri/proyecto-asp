namespace EsteroidesToDo.Application.DTOs.UsuarioDtos
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
