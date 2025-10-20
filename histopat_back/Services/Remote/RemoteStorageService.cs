using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using histopat_back.Configurations.Cloudinary;
using histopat_back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace histopat_back.Services.Remote;

public class RemoteStorageService : IRemoteImageStorageService
{
    private readonly Cloudinary _cloudinary;
        
    public RemoteStorageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<string> SaveImageAsync(Stream file, string fileName)
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

            if(uploadResult.Error != null)
            {
                throw new Exception($"Falha ao fazer upload da imagem para o Cloudinary: {uploadResult.Error.Message}");
            }

            var imageUrl = uploadResult.SecureUrl.ToString();

            return imageUrl;
        
    }

    public async Task<(Stream Stream, string ContentType)> DownloadAsync(string fileName)
    {
        byte[] fakeData = System.Text.Encoding.UTF8.GetBytes("Conteúdo de teste do arquivo");

        // Cria um MemoryStream com os bytes
        var stream = new MemoryStream(fakeData);

        // Retorna a tupla com stream e tipo de conteúdo
        return (stream, "text/plain");
    }
}
