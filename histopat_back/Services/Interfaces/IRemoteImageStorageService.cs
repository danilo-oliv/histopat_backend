using System;

namespace histopat_back.Services.Interfaces;

public interface IRemoteImageStorageService
{
    Task<string> SaveImageAsync(Stream fileStream, string fileName);
    Task<(Stream Stream, string ContentType)> DownloadAsync(string fileName);
}
