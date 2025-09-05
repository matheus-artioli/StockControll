using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockControlAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCamposParaDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstoqueMinimo",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitario",
                table: "Movimentos",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstoqueMinimo",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PrecoUnitario",
                table: "Movimentos");
        }
    }
}
