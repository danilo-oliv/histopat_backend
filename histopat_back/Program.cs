using histopat_back.Context;
using histopat_back.Mapster;
using histopat_back.Services.Interfaces;
using histopat_back.Services.Local;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MapsterMapper;
using histopat_back.Services.ServicesImpl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // URL do seu front-end
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Add services to the container.

builder.Services.AddControllers();

// --- 🔧 Configuração do Mapster ---
var config = TypeAdapterConfig.GlobalSettings;

// Se existir a classe de configuração, registre-a (opcional):
// histopat_back.Mapster.MapsterConfig.Register(config);

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();
// --- 🔧 fim da configuração ---

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IImageStorageService, LocalStorageService>();
builder.Services.AddScoped<IModuleService, ModuleService>();

// Registra o DbContext no DI
builder.Services.AddDbContext<HistopatDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
