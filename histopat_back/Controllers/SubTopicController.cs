using histopat_back.Context;
using histopat_back.Dominio.Models.Subtopic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubTopicController : ControllerBase
{
    private readonly HistopatDbContext _context;

    public SubTopicController(HistopatDbContext context)
    {
        _context = context;
    }

    // GET: api/SubTopic
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubtopicModel>>> GetSubTopics()
    {
        return await _context.SubTopics.ToListAsync();
    }

    // GET: api/SubTopic/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SubtopicModel>> GetSubTopic(int id)
    {
        var subTopic = await _context.SubTopics
            .Include(s => s.Slides)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (subTopic == null)
            return NotFound();

        return subTopic;
    }

    // POST: api/SubTopic
    [HttpPost]
    public async Task<ActionResult<SubtopicModel>> CreateSubTopic(SubtopicModel subTopic)
    {
        subTopic.CreatedAt = DateTime.UtcNow;

        _context.SubTopics.Add(subTopic);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSubTopic), new { id = subTopic.Id }, subTopic);
    }

    // PUT: api/SubTopic/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubTopic(int id, SubtopicModel updatedSubTopic)
    {
        if (id != updatedSubTopic.Id)
            return BadRequest();

        var subTopic = await _context.SubTopics.FindAsync(id);
        if (subTopic == null)
            return NotFound();

        subTopic.Title = updatedSubTopic.Title;
        subTopic.Active = updatedSubTopic.Active;
        subTopic.LastModified = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/SubTopic/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubTopic(int id)
    {
        var subTopic = await _context.SubTopics.FindAsync(id);
        if (subTopic == null)
            return NotFound();

        _context.SubTopics.Remove(subTopic);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/SubTopic/topic/3 (retorna todos os sub-tópicos de um tópico)
    [HttpGet("topic/{topicId}")]
    public async Task<ActionResult<IEnumerable<SubtopicModel>>> GetSubTopicsByTopic(int topicId)
    {
        var subTopics = await _context.SubTopics
            .Where(s => s.IdTopic == topicId)
            .ToListAsync();

        return subTopics;
    }
}
