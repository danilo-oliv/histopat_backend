using histopat_back.Context;
using histopat_back.Dominio.Models.Module;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly HistopatDbContext _context;
        private readonly IImageStorageService _imageStorageService;

        public ModuleController(HistopatDbContext context, IImageStorageService imageStorageService)
        {
            _context = context;
            _imageStorageService = imageStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var modules = await _context.Modules
                .AsNoTracking()
                .ToListAsync();

            var histories = await _context.Set<ModuleHistory>()
                .AsNoTracking()
                .ToListAsync();

            var result = modules.Select(m => new ModuleGet
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ImageUrl = m.ImageUrl,
                Active = m.Active,
                CreatedAt = m.CreatedAt,
                LastModified = m.LastModified,
                History = histories
                    .Where(h => h.IdModule == m.Id)
                    .OrderByDescending(h => h.ChangedAt)
                    .Select(h => new ModuleHistoryGet
                    {
                        Id = h.Id,
                        Action = h.Action,
                        ChangedAt = h.ChangedAt,
                        IdUser = h.IdUser
                    })
                    .ToList()
            }).ToList();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var module = await _context.Modules
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (module == null)
                return NotFound($"Módulo com ID {id} não encontrado.");

            var history = await _context.Set<ModuleHistory>()
                .Where(h => h.IdModule == id)
                .OrderByDescending(h => h.ChangedAt)
                .Select(h => new ModuleHistoryGet
                {
                    Id = h.Id,
                    Action = h.Action,
                    ChangedAt = h.ChangedAt,
                    IdUser = h.IdUser
                })
                .ToListAsync();

            var result = new ModuleGet
            {
                Id = module.Id,
                Title = module.Title,
                Description = module.Description,
                ImageUrl = module.ImageUrl,
                Active = module.Active,
                CreatedAt = module.CreatedAt,
                LastModified = module.LastModified,
                History = history
            };

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ModulePost modulePost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var module = new Module
            {
                Title = modulePost.Title,
                Description = modulePost.Description,
                ImageUrl = modulePost.ImageUrl,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            _context.Modules.Add(module);
            await _context.SaveChangesAsync();

            var history = new ModuleHistory
            {
                IdModule = module.Id,
                ChangedAt = DateTime.UtcNow,
                IdUser = GetUserId(),
                Action = "Created"
            };

            _context.Set<ModuleHistory>().Add(history);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Módulo criado com sucesso!", module.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ModuleEdit moduleEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var module = await _context.Modules.FindAsync(id);
            if (module == null)
                return NotFound("Módulo não encontrado.");

            module.Title = moduleEdit.Title ?? module.Title;
            module.Description = moduleEdit.Description ?? module.Description;
            module.ImageUrl = moduleEdit.ImageUrl ?? module.ImageUrl;
            module.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var history = new ModuleHistory
            {
                IdModule = module.Id,
                ChangedAt = DateTime.UtcNow,
                IdUser = GetUserId(),
                Action = "Updated"
            };

            _context.Set<ModuleHistory>().Add(history);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Módulo atualizado com sucesso!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module == null)
                return NotFound("Módulo não encontrado.");

            module.Active = false;
            module.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var history = new ModuleHistory
            {
                IdModule = module.Id,
                ChangedAt = DateTime.UtcNow,
                IdUser = GetUserId(),
                Action = "Deactivated"
            };

            _context.Set<ModuleHistory>().Add(history);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Módulo desativado com sucesso!" });
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            using var stream = file.OpenReadStream();
            var imagePath = await _imageStorageService.SaveImageAsync(stream, file.FileName);
            return Ok(new { path = imagePath });
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadImage(string fileName)
        {
            try
            {
                var (stream, contentType) = await _imageStorageService.DownloadAsync(fileName);
                return File(stream, contentType, fileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound("Imagem não encontrada.");
            }
        }

        private int GetUserId()
        {
            // Ajustar dps

            return 1;
        }
    }
}
