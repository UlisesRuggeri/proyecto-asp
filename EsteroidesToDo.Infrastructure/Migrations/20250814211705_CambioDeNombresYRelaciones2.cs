using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsteroidesToDo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CambioDeNombresYRelaciones2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
    name: "Postulante",
    newName: "UsuarioVacantes"
);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
