using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsteroidesToDo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CambioDeNombresYRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioProyectoRoles",
                table: "UsuarioProyectoRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioProyectoRoles_UsuarioId",
                table: "UsuarioProyectoRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notificaciones",
                table: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_IdEmpresa",
                table: "Notificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversacionesTarea",
                table: "ConversacionesTarea");

            migrationBuilder.DropIndex(
                name: "IX_ConversacionesTarea_TareaId",
                table: "ConversacionesTarea");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsuarioProyectoRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ConversacionesTarea");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioProyectoRoles",
                table: "UsuarioProyectoRoles",
                columns: new[] { "UsuarioId", "ProyectoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notificaciones",
                table: "Notificaciones",
                columns: new[] { "IdEmpresa", "IdUsuario" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversacionesTarea",
                table: "ConversacionesTarea",
                columns: new[] { "TareaId", "UsuarioId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioProyectoRoles",
                table: "UsuarioProyectoRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notificaciones",
                table: "Notificaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversacionesTarea",
                table: "ConversacionesTarea");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsuarioProyectoRoles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Notificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ConversacionesTarea",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioProyectoRoles",
                table: "UsuarioProyectoRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notificaciones",
                table: "Notificaciones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversacionesTarea",
                table: "ConversacionesTarea",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioProyectoRoles_UsuarioId",
                table: "UsuarioProyectoRoles",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_IdEmpresa",
                table: "Notificaciones",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_ConversacionesTarea_TareaId",
                table: "ConversacionesTarea",
                column: "TareaId");
        }
    }
}
