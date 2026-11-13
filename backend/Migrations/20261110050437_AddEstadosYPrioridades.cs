using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow_API.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadosYPrioridades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Tareas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prioridad",
                table: "Tareas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Proyectos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Prioridad",
                table: "Proyectos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Prioridad",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "Prioridad",
                table: "Proyectos");
        }
    }
}
