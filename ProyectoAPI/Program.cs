using Microsoft.EntityFrameworkCore;
using Proyecto_Backend.Services;
using ProyectoAPI.Models;
using ProyectoAPI.Services; // Asegúrate de importar el namespace correcto

var builder = WebApplication.CreateBuilder(args);

// Configurar conexión a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios para controladores
builder.Services.AddControllers();

// Registrar servicios
builder.Services.AddScoped<AuthService>(); // 🔹 Se registra el AuthService
builder.Services.AddScoped<NegocioService>();
builder.Services.AddScoped<NegocioService>();
builder.Services.AddScoped<ServicioService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ValoracionService>();
builder.Services.AddScoped<CotizacionService>();

builder.Services.AddScoped<EstadisticaService>();



// Configurar CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("https://localhost:44312", "http://localhost:5279")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el middleware de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();
