using Drupal.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Drupal.Infrastructure.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("create")]
    public async Task<IActionResult> CreateUser()
    {
        var userId = await userService.CreateUser();
        return Ok(userId);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await userService.GetById(id);
        return Ok(user);
    }
}
