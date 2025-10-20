using histopat_back.Data;
using histopat_back.Models.Module;
using histopat_back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using histopat_back.Services.Local;
using histopat_back.Services.Remote;

namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModuleController : ControllerBase
{
    private readonly HistopatDbContext _context;
    private readonly IImageStorageService _storage;
    private readonly IRemoteImageStorageService _remoteStorage;


    public ModuleController(HistopatDbContext context, IImageStorageService storage, IRemoteImageStorageService remoteStorage)
    {
        _context = context;
        _storage = storage;
        _remoteStorage = remoteStorage;
    }

    // GET: api/Module
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModuleModel>>> GetModules()
    {
        return await _context.Modules
            .Include(m => m.Topics) // inclui tópicos relacionados
            .ToListAsync();
    }

    // GET: api/Module/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ModuleModel>> GetModule(int id)
    {
        var module = await _context.Modules
            .Include(m => m.Topics)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (module == null)
            return NotFound();

        return module;
    }

    // POST: api/Module
    [HttpPost]
    public async Task<ActionResult<ModuleModel>> CreateModule(ModuleModel module)
    {
        module.CreatedAt = DateTime.UtcNow;

        _context.Modules.Add(module);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetModule), new { id = module.Id }, module);
    }

    // PUT: api/Module/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModule(int id, ModuleModel updatedModule)
    {
        if (id != updatedModule.Id)
            return BadRequest();

        var module = await _context.Modules.FindAsync(id);
        if (module == null)
            return NotFound();

        // Atualiza campos
        module.Title = updatedModule.Title;
        module.Active = updatedModule.Active;
        module.LastModified = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Module/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModule(int id)
    {
        var module = await _context.Modules.FindAsync(id);
        if (module == null)
            return NotFound();

        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var result = await _storage.SaveImageAsync(stream, file.FileName);
        return Ok(new { Path = result });
    }

    [HttpPost("upload/remote/{fileName}")]
    public async Task<IActionResult> UploadRemote(IFormFile file, string fileName)
    {
        await using var stream = file.OpenReadStream();

        var result = await _remoteStorage.SaveImageAsync(stream, fileName);

        return Ok(new { Path = result });
    }

    [HttpGet("download/{fileName}")]    public async Task<IActionResult> Download(string fileName)
    {
        try
        {
            var (stream, contentType) = await _storage.DownloadAsync(fileName);
            return File(stream, contentType, fileName);
        }
        catch (FileNotFoundException)
        {
            return NotFound("Imagem não encontrada.");
        }
    }
}
