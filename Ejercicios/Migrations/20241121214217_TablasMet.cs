using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ejercicios.Migrations
{
    /// <inheritdoc />
    public partial class TablasMet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Met",
                table: "TipoEjercicios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Met",
                table: "TipoEjercicios");
        }
    }
}
