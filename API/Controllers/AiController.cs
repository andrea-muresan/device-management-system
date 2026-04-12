using Core.DTOs;
using Core.Entities;
using Infrastructure.Auth;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AiDescriptionRequest
{
    public string Prompt { get; set; } = string.Empty;
}

[ApiController]
[Route("api/[controller]"), BasicAuthorization]
public class AiController(IAiService aiService) : ControllerBase
{
    [HttpPost("generate-description")]
    public async Task<IActionResult> GenerateDescription([FromBody] AiDescriptionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
        {
            return BadRequest("Prompt cannot be empty.");
        }
        try 
        {
            var response = await aiService.GetDeviceDescriptionAsync(request.Prompt);
            return Ok(new ChatResponse { response = response.Trim() });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }
}
