using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farmacia.Migrations
{
    /// <inheritdoc />
    public partial class RelacionClienteVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdVenta",
                table: "Ventas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteDni",
                table: "Ventas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "IdVenta",
                keyValue: 1,
                column: "ClienteDni",
                value: "72451234");

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "IdVenta",
                keyValue: 2,
                column: "ClienteDni",
                value: "70112233");

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "IdVenta",
                keyValue: 3,
                column: "ClienteDni",
                value: "75998877");

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "IdVenta",
                keyValue: 4,
                column: "ClienteDni",
                value: "71234567");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteDni",
                table: "Ventas",
                column: "ClienteDni");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Clientes_ClienteDni",
                table: "Ventas",
                column: "ClienteDni",
                principalTable: "Clientes",
                principalColumn: "Dni",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Clientes_ClienteDni",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_ClienteDni",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "ClienteDni",
                table: "Ventas");

            migrationBuilder.AlterColumn<int>(
                name: "IdVenta",
                table: "Ventas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
