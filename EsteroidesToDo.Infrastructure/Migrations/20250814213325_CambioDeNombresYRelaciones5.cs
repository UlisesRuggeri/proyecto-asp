using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsteroidesToDo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CambioDeNombresYRelaciones5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioVacantes_Usuarios_UsuarioId",
                table: "UsuarioVacantes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioVacantes_Vacantes_VacanteId",
                table: "UsuarioVacantes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioVacantes_Vacantes_VacanteId1",
                table: "UsuarioVacantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioVacantes",
                table: "UsuarioVacantes");

            migrationBuilder.RenameTable(
                name: "UsuarioVacantes",
                newName: "Postulantes");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioVacantes_VacanteId1",
                table: "Postulantes",
                newName: "IX_Postulantes_VacanteId1");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioVacantes_VacanteId",
                table: "Postulantes",
                newName: "IX_Postulantes_VacanteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Postulantes",
                table: "Postulantes",
                columns: new[] { "UsuarioId", "VacanteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Postulantes_Usuarios_UsuarioId",
                table: "Postulantes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Postulantes_Vacantes_VacanteId",
                table: "Postulantes",
                column: "VacanteId",
                principalTable: "Vacantes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Postulantes_Vacantes_VacanteId1",
                table: "Postulantes",
                column: "VacanteId1",
                principalTable: "Vacantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postulantes_Usuarios_UsuarioId",
                table: "Postulantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Postulantes_Vacantes_VacanteId",
                table: "Postulantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Postulantes_Vacantes_VacanteId1",
                table: "Postulantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Postulantes",
                table: "Postulantes");

            migrationBuilder.RenameTable(
                name: "Postulantes",
                newName: "UsuarioVacantes");

            migrationBuilder.RenameIndex(
                name: "IX_Postulantes_VacanteId1",
                table: "UsuarioVacantes",
                newName: "IX_UsuarioVacantes_VacanteId1");

            migrationBuilder.RenameIndex(
                name: "IX_Postulantes_VacanteId",
                table: "UsuarioVacantes",
                newName: "IX_UsuarioVacantes_VacanteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioVacantes",
                table: "UsuarioVacantes",
                columns: new[] { "UsuarioId", "VacanteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioVacantes_Usuarios_UsuarioId",
                table: "UsuarioVacantes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioVacantes_Vacantes_VacanteId",
                table: "UsuarioVacantes",
                column: "VacanteId",
                principalTable: "Vacantes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioVacantes_Vacantes_VacanteId1",
                table: "UsuarioVacantes",
                column: "VacanteId1",
                principalTable: "Vacantes",
                principalColumn: "Id");
        }
    }
}
