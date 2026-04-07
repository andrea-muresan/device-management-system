using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly DeviceDbContext context;

    public UsersController(DeviceDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await context.Users.Include(u => u.Devices).ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await context.Users.Include(u => u.Devices).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, User updatedUser)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();

        user.Name = updatedUser.Name;
        user.Role = updatedUser.Role;
        user.Location = updatedUser.Location;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return NotFound();

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
