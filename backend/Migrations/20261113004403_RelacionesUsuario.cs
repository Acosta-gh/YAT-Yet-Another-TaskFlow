using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow_API.Migrations
{
    /// <inheritdoc />
    public partial class RelacionesUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Tareas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Proyectos",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_UsuarioId",
                table: "Proyectos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_AspNetUsers_UsuarioId",
                table: "Proyectos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_AspNetUsers_UsuarioId",
                table: "Tareas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_AspNetUsers_UsuarioId",
                table: "Proyectos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_AspNetUsers_UsuarioId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_UsuarioId",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Proyectos");
        }
    }
}
