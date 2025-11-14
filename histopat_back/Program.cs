
using CloudinaryDotNet;
using histopat_back.Configurations;
using histopat_back.Context;
using histopat_back.Middlewares;
using histopat_back.Services.Interfaces;
using histopat_back.Services.Local;
using histopat_back.Services.Remote;
using histopat_back.Services.ServicesImpl;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
builder.Services.AddMapster();
builder.Services.AddScoped<ISubTopicService, SubTopicService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<ISlideService, SlideService>();
builder.Services.AddScoped<IModuleService, ModuleService>();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton(sp =>
{
    var cloudinarySettings = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;

    Account account = new Account(
        cloudinarySettings.CloudName,
        cloudinarySettings.ApiKey,
        cloudinarySettings.ApiSecret);

    return new Cloudinary(account);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IImageStorageService, RemoteStorageService>();

// Registra o DbContext no DI
builder.Services.AddDbContext<HistopatDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

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
