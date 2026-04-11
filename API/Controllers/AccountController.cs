using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IUserRepository userRepo): ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO userData)
    {
        var existingUser = await userRepo.GetUserByEmailAsync(userData.Email);
        if (existingUser != null) return Conflict("User already exists.");

        var user = new User
        {
            Email = userData.Email,
            Name = userData.Name,
            Role = userData.Role,
            Location = userData.Location,
            
            Password = BCrypt.Net.BCrypt.HashPassword(userData.Password)
        };

        var success = await userRepo.CreateUserAsync(user);

        if (success) return Ok(new { message = "Registration successful" });
        
        return StatusCode(500, "An error occurred while saving the user.");
    }

    [HttpGet("login")]
    public async Task<ActionResult<User>> Login()
    {
        var email = User.Identity?.Name;

        if (string.IsNullOrEmpty(email)) return Unauthorized();

        var user = await userRepo.GetUserByEmailAsync(email);

        if (user == null) return Unauthorized();

        return Ok(user);
    }
}
