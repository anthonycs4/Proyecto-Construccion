using Microsoft.EntityFrameworkCore;
using Proyecto_Backend.Services;
using ProyectoAPI.Models;
using ProyectoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔥 Puerto explícito para Docker/Azure

// Conexión a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// 1. Agregar política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
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
builder.Services.AddScoped<FeedbackService>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger habilitado también en Azure
app.UseSwagger();
app.UseSwaggerUI();


// 2. Usar CORS
app.UseCors("PermitirTodo");
app.UseAuthorization();
app.MapControllers();

app.Run();
