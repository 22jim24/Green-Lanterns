using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Farmaco.Datos;
using Farmaco.Dtos;
using Farmaco.Middlewares;
using Farmaco.Repositorios;
using Farmaco.Servicios;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Registrar servicios y personalizar errores de validación de DTOs
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opciones =>
    {
        opciones.InvalidModelStateResponseFactory = contexto =>
        {
            List<string> errores = contexto.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            ErrorResponse respuesta = new(400, string.Join(" | ", errores));
            return new BadRequestObjectResult(respuesta);
        };
    });

builder.Services.AddOpenApi();

// Base de datos
builder.Services.AddDbContext<AppDbContext>(opciones =>
    opciones.UseSqlite(builder.Configuration.GetConnectionString("Conexion")));

builder.Services.AddIdentityCore<IdentityUser>(opciones =>
{
    opciones.Password.RequiredLength = 6;
    opciones.Password.RequireNonAlphanumeric = false;
    opciones.Password.RequireUppercase = false;
    opciones.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<ServicioToken>();
builder.Services.AddScoped<ServicioAutenticacion>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Emisor"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audiencia"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Clave"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Inyección de dependencias (todos como Scoped al usar EF Core)
builder.Services.AddScoped<IReCliente, ReClienteEf>();
builder.Services.AddScoped<ServicioCliente>();

builder.Services.AddScoped<IReEmpleado, ReEmpleadoEf>();
builder.Services.AddScoped<ServicioEmpleados>();

builder.Services.AddScoped<IReProducto, ReProductoEf>();
builder.Services.AddScoped<ServicioProductos>();

builder.Services.AddScoped<IReProveedor, ReProveedorEf>();
builder.Services.AddScoped<ServicioProveedores>();

builder.Services.AddScoped<IReVenta, ReVentaEf>();
builder.Services.AddScoped<ServicioVentas>();

var app = builder.Build();

// Crear la base de datos y cargar datos iniciales (HasData) si no existe
using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext contexto =
    scope.ServiceProvider.GetRequiredService<AppDbContext>();
    contexto.Database.Migrate();
}

app.UseMiddleware<ManejoErroresMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();