using histopat_back.Services.Interfaces;
using histopat_back.ViewModel.User;
using Microsoft.AspNetCore.Mvc;

namespace histopat_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult> FindAllUsers()
    {
        var users = await _userService.FindAllUsers();
        return Ok(users);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> FindById(int userId)
    {
        var user = await _userService.FindById(userId);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> SaveUser(UserPost userPost)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _userService.SaveUser(userPost);
        return Created();
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> EditUser(UserEdit userEdit, int userId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _userService.EditUser(userEdit, userId);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await _userService.DeleteUser(userId);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userService.Login(userLogin);
        return Ok(user);
    }
}
