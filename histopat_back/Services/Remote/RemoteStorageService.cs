using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using histopat_back.Configurations;
using histopat_back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace histopat_back.Services.Remote;

public class RemoteStorageService : IImageStorageService
{
    private readonly Cloudinary _cloudinary;

    public RemoteStorageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> SaveImageAsync(Stream file, string fileName)
    {
        try
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Nenhum arquivo foi recebido ou o arquivo está vazio.", nameof(file));

            ImageUploadParams uploadParams;


            uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, file),

                Transformation = new Transformation()
                    .Height(500)
                    .Width(500)
                    .Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Falha ao fazer upload da imagem para o Cloudinary: {uploadResult.Error.Message}");
            }

            var imageUrl = uploadResult.SecureUrl.ToString();

            return imageUrl;
        }
        catch (Exception ex) {
            throw new Exception("Erro so serviço de upload de imagem");
        }
    }

    public async Task<(Stream Stream, string ContentType)> DownloadAsync(string fileName)
    {
        byte[] fakeData = System.Text.Encoding.UTF8.GetBytes("Conteúdo de teste do arquivo");

        var stream = new MemoryStream(fakeData);

        return (stream, "text/plain");
    }
}