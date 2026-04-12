using API.DTOs;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController(IDevicesService srv): ControllerBase
{
    

    [HttpGet, BasicAuthorization]
    public async Task<ActionResult<IReadOnlyList<Device>>> getAll()
    {
       return Ok(await srv.GetDevicesAsync());
    }

    [HttpGet("{id:int}"), BasicAuthorization]  // api/products/2
    public async Task<IActionResult> GetById(int id)
    {
        var device = await srv.GetDeviceByIdAsync(id);
        if (device == null) return NotFound();
        return Ok(device);
    }

    [HttpPost, BasicAuthorization]
    public async Task<IActionResult> Create([FromBody] CreateDeviceDTO device)
    {

        var createdDevice = await srv.CreateDeviceAsync(device);

        return CreatedAtAction(nameof(GetById), new { id = createdDevice.Id }, createdDevice);
    }

    [HttpPut("{id:int}"), BasicAuthorization]
    public async Task<IActionResult> Update(int id, UpdateDeviceDTO device)
    {
        if (id != device.Id) return BadRequest("ID mismatch");

        var updated = await srv.UpdateDeviceAsync(device);

        if (!updated) return BadRequest("Problem updating the device");
        
        return NoContent();
    }

    [HttpDelete("{id:int}"), BasicAuthorization]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await srv.DeleteDeviceAsync(id);

        if (!deleted) return BadRequest("Problem deleting the device");

        return NoContent();
    }

    [HttpGet("details/{id:int}"), BasicAuthorization]
    public async Task<IActionResult> GetDeviceDetails(int id)
    {
        var device = await srv.GetDeviceDetailsDTOAsync(id);
        if (device == null) return NotFound();
        return Ok(device);
    }

    [HttpGet("summary"), BasicAuthorization]
    public async Task<ActionResult<IReadOnlyList<DeviceSummaryDTO>>> GetDevicesSummary(int id)
    {
        return Ok(await srv.GetDevicesSummaryDTOAsync());
    }

    [HttpPut("{id}/assign"), BasicAuthorization]
    public async Task<IActionResult> AssignToMe(int id)
    {
        var userEmail = User.Identity?.Name; 

        if (string.IsNullOrEmpty(userEmail)) return Unauthorized();

        var success = await srv.AssignDeviceToEmail(id, userEmail);
        if (!success) return NotFound();
        

        return NoContent();
    }

    [HttpPut("{id}/unassign"), BasicAuthorization]
    public async Task<IActionResult> Unassign(int id)
    {
        var userEmail = User.Identity?.Name;

        if (string.IsNullOrEmpty(userEmail)) return Unauthorized();
        
        var success = await srv.UnassignDeviceFromEmail(id, userEmail);
        if (!success) return NotFound();
        
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        var allDevices = await srv.GetDevicesSummaryDTOAsync();
        var filteredResults = srv.SearchDevices(q, allDevices.ToList());
        
        return Ok(filteredResults);
    }
}
