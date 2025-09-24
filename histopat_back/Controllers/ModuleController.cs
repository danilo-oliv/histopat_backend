using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using histopat_back.Data;
using histopat_back.Models;

namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModuleController : ControllerBase
{
    private readonly HistopatDbContext _context;

    public ModuleController(HistopatDbContext context)
    {
        _context = context;
    }

    // GET: api/Module
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Module>>> GetModules()
    {
        return await _context.Modules
            .Include(m => m.Topics) // inclui tópicos relacionados
            .ToListAsync();
    }

    // GET: api/Module/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Module>> GetModule(long id)
    {
        var module = await _context.Modules
            .Include(m => m.Topics)
            .FirstOrDefaultAsync(m => m.IdModule == id);

        if (module == null)
            return NotFound();

        return module;
    }

    // POST: api/Module
    [HttpPost]
    public async Task<ActionResult<Module>> CreateModule(Module module)
    {
        module.CreatedAt = DateTime.UtcNow;

        _context.Modules.Add(module);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetModule), new { id = module.IdModule }, module);
    }

    // PUT: api/Module/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModule(long id, Module updatedModule)
    {
        if (id != updatedModule.IdModule)
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
    public async Task<IActionResult> DeleteModule(long id)
    {
        var module = await _context.Modules.FindAsync(id);
        if (module == null)
            return NotFound();

        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
