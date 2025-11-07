using histopat_back.Context;
using histopat_back.Dominio.Models.Topic;
using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Topic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly HistopatDbContext _context;
        private readonly IImageStorageService _imageStorageService;

        public TopicController(HistopatDbContext context, IImageStorageService imageStorageService)
        {
            _context = context;
            _imageStorageService = imageStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var topics = await _context.Topics
                .AsNoTracking()
                .ToListAsync();

            var histories = await _context.Set<TopicHistory>()
                .AsNoTracking()
                .ToListAsync();

            var result = topics.Select(t => new TopicGet
            {
                Id = t.Id,
                IdModule = t.IdModule,
                Title = t.Title,
                Active = t.Active,
                CreatedAt = t.CreatedAt,
                LastModified = t.LastModified,
                History = histories
                    .Where(h => h.IdTopic == t.Id)
                    .OrderByDescending(h => h.ChangedAt)
                    .Select(h => new TopicHistoryGet
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
            var topic = await _context.Topics
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (topic == null)
                return NotFound($"Tópico com ID {id} não encontrado.");

            var history = await _context.Set<TopicHistory>()
                .Where(h => h.IdTopic == id)
                .OrderByDescending(h => h.ChangedAt)
                .Select(h => new TopicHistoryGet
                {
                    Id = h.Id,
                    Action = h.Action,
                    ChangedAt = h.ChangedAt,
                    IdUser = h.IdUser
                })
                .ToListAsync();

            var result = new TopicGet
            {
                Id = topic.Id,
                IdModule = topic.IdModule,
                Title = topic.Title,
                Active = topic.Active,
                CreatedAt = topic.CreatedAt,
                LastModified = topic.LastModified,
                History = history
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TopicPost topicPost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var moduleExists = await _context.Modules.AnyAsync(m => m.Id == topicPost.IdModule);
            if (!moduleExists)
                return NotFound($"Módulo com ID {topicPost.IdModule} não encontrado.");

            var topic = new Topic
            {
                Title = topicPost.Title,
                IdModule = topicPost.IdModule,
                Active = true,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            var history = new TopicHistory
            {
                IdTopic = topic.Id,
                ChangedAt = DateTime.UtcNow,
                IdUser = GetUserId(),
                Action = "Created"
            };

            _context.Set<TopicHistory>().Add(history);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Tópico criado com sucesso!", topic.Id });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TopicEdit topicEdit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
                return NotFound("Tópico não encontrado.");

            topic.Title = topicEdit.Title ?? topic.Title;
            topic.Active = topicEdit.Active ?? topic.Active;
            topic.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var history = new TopicHistory
            {
                IdTopic = topic.Id,
                ChangedAt = DateTime.UtcNow,
                IdUser = GetUserId(),
                Action = "Updated"
            };

            _context.Set<TopicHistory>().Add(history);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Tópico atualizado com sucesso!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
                return NotFound("Tópico não encontrado.");

            topic.Active = false;
            topic.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var history = new TopicHistory
            {
                IdTopic = topic.Id,
                ChangedAt = DateTime.UtcNow,
                IdUser = GetUserId(),
                Action = "Deactivated"
            };

            _context.Set<TopicHistory>().Add(history);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Tópico desativado com sucesso!" });
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
            // temporário
            return 1;
        }
    }
}
