using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.Role;
using Microsoft.AspNetCore.Mvc;

namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult> FindAllRoles()
    {
        var roles = await _roleService.FindAllRoles();
        return Ok(roles);
    }

    [HttpGet("{roleId}")]
    public async Task<ActionResult> FindById(byte roleId)
    {
        var role = await _roleService.FindById(roleId);
        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult> SaveRole(RolePost rolePost)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _roleService.SaveRole(rolePost);
        return Created();
    }

    [HttpPut("{roleId}")]
    public async Task<IActionResult> EditRole(RoleEdit roleEdit, byte roleId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _roleService.EditRole(roleEdit, roleId);
        return NoContent();
    }

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRole(byte roleId)
    {
        await _roleService.DeleteRole(roleId);
        return NoContent();
    }
}
