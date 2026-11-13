using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarColaboradores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "Colaboradores",
                table: "Tareas",
                type: "text[]",
                nullable: false,
                defaultValueSql: "'{}'");

            migrationBuilder.AddColumn<List<string>>(
                name: "Colaboradores",
                table: "Proyectos",
                type: "text[]",
                nullable: false,
                defaultValueSql: "'{}'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colaboradores",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Colaboradores",
                table: "Proyectos");
        }
    }
}
