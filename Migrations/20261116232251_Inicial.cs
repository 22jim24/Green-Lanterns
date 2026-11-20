using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Farmacia.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Dni = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    PuntosConseguidos = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Dni);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Dni = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Dni);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 59, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Nombre);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    NombreProveedor = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    TelefonoProveedor = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    DireccionProveedor = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.NombreProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    IdVenta = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreCliente = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NombreProducto = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.IdVenta);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Dni", "Apellido", "Correo", "Nombre", "PuntosConseguidos", "Telefono" },
                values: new object[,]
                {
                    { "70112233", "Ramos", "luis@correo.com", "Luis", 50, "912345678" },
                    { "71234567", "Díaz", "marco@correo.com", "Marco", 10, "955443322" },
                    { "72451234", "Torres", "ana@correo.com", "Ana", 120, "987654321" },
                    { "75998877", "Paredes", "sofia@correo.com", "Sofía", 200, "998877665" }
                });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "Dni", "Apellido", "Cargo", "Correo", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { "45123698", "Mendoza", "ADMIN", "carlos@farmaco.com", "Carlos", "987111222" },
                    { "48963214", "Gómez", "FARMACEUTICO", "elena@farmaco.com", "Elena", "987333444" },
                    { "73216549", "Vargas", "CAJERO", "jorge@farmaco.com", "Jorge", "987555666" },
                    { "76549871", "Castro", "ALMACENERO", "lucia@farmaco.com", "Lucía", "987777888" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Nombre", "Precio", "Stock" },
                values: new object[,]
                {
                    { "Amoxicilina", 8.50m, 40 },
                    { "Iblino", 15.00m, 4 },
                    { "Ibuprofeno", 3.00m, 80 },
                    { "Paracetamol", 2.50m, 100 }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "NombreProveedor", "DireccionProveedor", "TelefonoProveedor" },
                values: new object[,]
                {
                    { "Farmindustria", "Av. Los Frutales 789", "998877665" },
                    { "Laboratorios Portugal", "Av. Industrial 123", "987654321" },
                    { "Medifarma", "Jr. Ecuador 321", "955443322" },
                    { "Química Suiza", "Av. República de Panamá 456", "912345678" }
                });

            migrationBuilder.InsertData(
                table: "Ventas",
                columns: new[] { "IdVenta", "Cantidad", "MetodoPago", "NombreCliente", "NombreProducto", "Precio", "Total" },
                values: new object[,]
                {
                    { 1, 2, "YAPE_PLIN", "Ana Torres", "Paracetamol 500mg", 2.50m, 5.00m },
                    { 2, 3, "EFECTIVO", "Luis Ramos", "Ibuprofeno 400mg", 3.00m, 9.00m },
                    { 3, 1, "TARJETA", "Sofía Paredes", "Jarabe Panadol", 15.00m, 15.00m },
                    { 4, 2, "TRANSFERENCIA", "Marco Díaz", "Amoxicilina 500mg", 8.50m, 17.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Ventas");
        }
    }
}
