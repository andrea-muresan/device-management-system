using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController: ControllerBase
{
    private readonly DeviceDbContext context;

    public DevicesController(DeviceDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Device>>> getAll()
    {
        var devices = await context.Devices.Include(d => d.User).ToListAsync();
        return Ok(devices);
    }

    [HttpGet("{id:int}")]  // api/products/2
    public async Task<IActionResult> GetById(int id)
    {
        var device = await context.Devices.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
        if (device == null) return NotFound();
        return Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Device device)
    {
        context.Devices.Add(device);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = device.Id }, device);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Device updatedDevice)
    {
        var device = await context.Devices.FindAsync(id);
        if (device == null) return NotFound();

        device.Name = updatedDevice.Name;
        device.Manufacturer = updatedDevice.Manufacturer;
        device.Type = updatedDevice.Type;
        device.OS = updatedDevice.OS;
        device.OSVersion = updatedDevice.OSVersion;
        device.Processor = updatedDevice.Processor;
        device.RAM = updatedDevice.RAM;
        device.Description = updatedDevice.Description;
        device.UserId = updatedDevice.UserId;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var device = await context.Devices.FindAsync(id);
        if (device == null) return NotFound();

        context.Devices.Remove(device);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
