using histopat_back.Services.Interfaces;
using System;
using System.IO;

namespace histopat_back.Services.Local;

public class LocalStorageService : IImageStorageService
{
    private readonly string _basePath;

    public LocalStorageService(IWebHostEnvironment env)
    {
        _basePath = Path.Combine(env.ContentRootPath, "uploads");
    }

    public async Task<(Stream Stream, string ContentType)> DownloadAsync(string fileName)
    {
        var filePath = Path.Combine(_basePath, fileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Arquivo não encontrado.", fileName);

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var contentType = GetMimeType(filePath);

        return (stream, contentType);
    }

    public async Task<string> SaveImageAsync(Stream fileStream, string fileName)
    {
        Directory.CreateDirectory(_basePath);
        var filePath = Path.Combine(_basePath, fileName);
        using var output = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(output);
        return $"/uploads/{fileName}";
    }

    private string GetMimeType(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".webp" => "image/webp",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            _ => "application/octet-stream"
        };
    }


}
