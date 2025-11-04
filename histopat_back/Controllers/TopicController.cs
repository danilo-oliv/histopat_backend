
using histopat_back.Context;
using histopat_back.Dominio.Models.Topic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicController : ControllerBase
{
    private readonly HistopatDbContext _context;

    public TopicController(HistopatDbContext context)
    {
        _context = context;
    }

    // GET: api/Topic
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
    {
        return await _context.Topics
            .Include(t => t.SubTopics) // inclui sub-tópicos relacionados
            .ToListAsync();
    }

    // GET: api/Topic/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Topic>> GetTopic(int id)
    {
        var topic = await _context.Topics
            .Include(t => t.SubTopics)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (topic == null)
            return NotFound();

        return topic;
    }

    // POST: api/Topic
    [HttpPost]
    public async Task<ActionResult<Topic>> CreateTopic(Topic topic)
    {
        topic.CreatedAt = DateTime.UtcNow;

        _context.Topics.Add(topic);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, topic);
    }

    // PUT: api/Topic/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTopic(int id, Topic updatedTopic)
    {
        if (id != updatedTopic.Id)
            return BadRequest();

        var topic = await _context.Topics.FindAsync(id);
        if (topic == null)
            return NotFound();

        topic.Title = updatedTopic.Title;
        topic.Active = updatedTopic.Active;
        topic.LastModified = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Topic/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTopic(int id)
    {
        var topic = await _context.Topics.FindAsync(id);
        if (topic == null)
            return NotFound();

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/Topic/module/2 (retorna todos os tópicos de um módulo)
    [HttpGet("module/{moduleId}")]
    public async Task<ActionResult<IEnumerable<Topic>>> GetTopicsByModule(int moduleId)
    {
        var topics = await _context.Topics
            .Where(t => t.IdModule == moduleId)
            .Include(t => t.SubTopics)
            .ToListAsync();

        return topics;
    }
}
