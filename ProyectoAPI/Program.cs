using Microsoft.EntityFrameworkCore;
using Proyecto_Backend.Services;
using ProyectoAPI.Models;
using ProyectoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔥 Puerto explícito para Docker/Azure

// Conexión a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controladores
builder.Services.AddControllers();

// Servicios
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<NegocioService>();
builder.Services.AddScoped<ServicioService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ValoracionService>();
builder.Services.AddScoped<CotizacionService>();
builder.Services.AddScoped<EstadisticaService>();

// CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(
            "https://localhost:44312",
            "http://localhost:5279",
            "https://app-front-valverde-cano.azurewebsites.net"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger habilitado también en Azure
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();
