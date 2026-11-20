using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Farmaco.Modelos;

namespace Farmaco.Datos;

public class AppDbContext(DbContextOptions<AppDbContext> opciones)
    : IdentityDbContext<IdentityUser>(opciones)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Proovedor> Proveedores => Set<Proovedor>();
    public DbSet<Ventas> Ventas => Set<Ventas>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Obligatorio y siempre primero

        modelBuilder.Entity<Cliente>(entidad =>
        {
            entidad.ToTable("Clientes");
            entidad.HasKey(c => c.Dni);

            entidad.Property(c => c.Dni).HasMaxLength(8);
            entidad.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
            entidad.Property(c => c.Apellido).HasMaxLength(100).IsRequired();
            entidad.Property(c => c.Correo).HasMaxLength(150).IsRequired();
            entidad.Property(c => c.Telefono).HasMaxLength(15).IsRequired();
            entidad.Property(c => c.PuntosConseguidos).IsRequired();

            entidad.HasData(
                new Cliente("72451234", "Ana", "Torres", "ana@correo.com", "987654321", 120),
                new Cliente("70112233", "Luis", "Ramos", "luis@correo.com", "912345678", 50),
                new Cliente("75998877", "Sofía", "Paredes", "sofia@correo.com", "998877665", 200),
                new Cliente("71234567", "Marco", "Díaz", "marco@correo.com", "955443322", 10)
            );
        });

        modelBuilder.Entity<Empleado>(entidad =>
        {
            entidad.ToTable("Empleados");
            entidad.HasKey(e => e.Dni);

            entidad.Property(e => e.Dni).HasMaxLength(8);
            entidad.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entidad.Property(e => e.Apellido).HasMaxLength(100).IsRequired();
            entidad.Property(e => e.Correo).HasMaxLength(150).IsRequired();
            entidad.Property(e => e.Telefono).HasMaxLength(15).IsRequired();
            entidad.Property(e => e.Cargo).HasMaxLength(20).IsRequired();

            entidad.HasData(
                new Empleado("45123698", "Carlos", "Mendoza", "carlos@farmaco.com", "987111222", "ADMIN"),
                new Empleado("48963214", "Elena", "Gómez", "elena@farmaco.com", "987333444", "FARMACEUTICO"),
                new Empleado("73216549", "Jorge", "Vargas", "jorge@farmaco.com", "987555666", "CAJERO"),
                new Empleado("76549871", "Lucía", "Castro", "lucia@farmaco.com", "987777888", "ALMACENERO")
            );
        });

        modelBuilder.Entity<Producto>(entidad =>
        {
            entidad.ToTable("Productos");
            entidad.HasKey(p => p.Nombre);

            entidad.Property(p => p.Nombre).HasMaxLength(59).IsRequired();
            entidad.Property(p => p.Precio).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(p => p.Stock).IsRequired();

            entidad.HasData(
                new Producto("Paracetamol", 2.50m, 100),
                new Producto("Ibuprofeno", 3.00m, 80),
                new Producto("Amoxicilina", 8.50m, 40),
                new Producto("Iblino", 15.00m, 4)
            );
        });

        modelBuilder.Entity<Proovedor>(entidad =>
        {
            entidad.ToTable("Proveedores");
            entidad.HasKey(p => p.NombreProveedor);

            entidad.Property(p => p.NombreProveedor).HasMaxLength(60).IsRequired();
            entidad.Property(p => p.TelefonoProveedor).HasMaxLength(9).IsRequired();
            entidad.Property(p => p.DireccionProveedor).HasMaxLength(200).IsRequired();

            entidad.HasData(
                new Proovedor("Laboratorios Portugal", "987654321", "Av. Industrial 123"),
                new Proovedor("Química Suiza", "912345678", "Av. República de Panamá 456"),
                new Proovedor("Farmindustria", "998877665", "Av. Los Frutales 789"),
                new Proovedor("Medifarma", "955443322", "Jr. Ecuador 321")
            );
        });

        modelBuilder.Entity<Ventas>(entidad =>
        {
            entidad.ToTable("Ventas");
            entidad.HasKey(v => v.IdVenta);

            entidad.Property(v => v.NombreCliente).HasMaxLength(100).IsRequired();
            entidad.Property(v => v.NombreProducto).HasMaxLength(50).IsRequired();
            entidad.Property(v => v.Cantidad).IsRequired();
            entidad.Property(v => v.Precio).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(v => v.MetodoPago).HasMaxLength(20).IsRequired();
            entidad.Property(v => v.Total).HasColumnType("decimal(10,2)").IsRequired();

            // 1 A N
            entidad.HasOne(v => v.Cliente)
                   .WithMany(c => c.Ventas)
                   .HasForeignKey(v => v.ClienteDni)
                   .OnDelete(DeleteBehavior.Cascade);

            entidad.HasData(
                new { IdVenta = 1, ClienteDni = "72451234", NombreCliente = "Ana Torres", NombreProducto = "Paracetamol 500mg", Cantidad = 2, Precio = 2.50m, MetodoPago = "YAPE_PLIN", Total = 5.00m, FechaVenta = new DateTime(2026, 11, 1, 10, 0, 0) },
                new { IdVenta = 2, ClienteDni = "70112233", NombreCliente = "Luis Ramos", NombreProducto = "Ibuprofeno 400mg", Cantidad = 3, Precio = 3.00m, MetodoPago = "EFECTIVO", Total = 9.00m, FechaVenta = new DateTime(2026, 11, 2, 11, 30, 0) },
                new { IdVenta = 3, ClienteDni = "75998877", NombreCliente = "Sofía Paredes", NombreProducto = "Jarabe Panadol", Cantidad = 1, Precio = 15.00m, MetodoPago = "TARJETA", Total = 15.00m, FechaVenta = new DateTime(2026, 11, 3, 14, 15, 0) },
                new { IdVenta = 4, ClienteDni = "71234567", NombreCliente = "Marco Díaz", NombreProducto = "Amoxicilina 500mg", Cantidad = 2, Precio = 8.50m, MetodoPago = "TRANSFERENCIA", Total = 17.00m, FechaVenta = new DateTime(2026, 11, 4, 16, 45, 0) }
            );
        });
    }
}