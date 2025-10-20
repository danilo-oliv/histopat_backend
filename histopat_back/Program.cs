using histopat_back.Data;
using histopat_back.Services.Interfaces;
using histopat_back.Services.Local;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using histopat_back.Configurations.Cloudinary;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using histopat_back.Services.Remote;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IImageStorageService, LocalStorageService>();
builder.Services.AddScoped<IRemoteImageStorageService, RemoteStorageService>();

// Registra o DbContext no DI
builder.Services.AddDbContext<HistopatDbContext>(options =>
    options.UseSqlServer(connectionString));

// Mapeia envs para CloudinarySettings
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));

builder.Services.AddSingleton(sp => 
{
    var cloudinarySettings = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;

    Account account = new Account(
        cloudinarySettings.CloudName,
        cloudinarySettings.ApiKey,
        cloudinarySettings.ApiSecret);

    return new Cloudinary(account);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
