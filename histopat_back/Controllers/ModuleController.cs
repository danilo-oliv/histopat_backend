

using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Module;
using Microsoft.AspNetCore.Mvc;
namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModuleController : ControllerBase
{
    private IModuleService _moduleService;
    public ModuleController(IModuleService moduleService)
    {
        this._moduleService = moduleService;
    }

    // GET: api/Module
    [HttpGet]
    public async Task<ActionResult> FindAllModules()
    {
        var modules = await _moduleService.FindAllModules();

        return Ok(modules);
    }

    // GET: api/Module/5
    [HttpGet("{moduleId}")]
    public async Task<ActionResult> FindById(int moduleId)
    {
        var module = await _moduleService.FindById(moduleId);

        return Ok(module);
    }

    // POST: api/Module
    [HttpPost]
    public async Task<ActionResult> SaveModule(ModulePost modulePost)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _moduleService.SaveModule(modulePost);

        return Created();
    }

    // PUT: api/Module/5
    [HttpPut("{moduleId}")]
    public async Task<IActionResult> EditModule(ModuleEdit moduleEdit, int moduleId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _moduleService.EditModule(moduleEdit, moduleId);

        return NoContent();
    }

    // DELETE: api/Module/5
    [HttpDelete("{moduleId}")]
    public async Task<IActionResult> DeleteModule(int moduleId)
    {
        await _moduleService.DeleteModule(moduleId);

        return NoContent();
    }
}
