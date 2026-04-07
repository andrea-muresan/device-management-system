using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController(IDevicesRepository repo): ControllerBase
{
    

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Device>>> getAll()
    {
       return Ok(await repo.GetDevicesAsync());
    }

    [HttpGet("{id:int}")]  // api/products/2
    public async Task<IActionResult> GetById(int id)
    {
        var device = await repo.GetDeviceByIdAsync(id);
        if (device == null) return NotFound();
        return Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Device device)
    {
        repo.AddDevice(device);
        
        if (await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetById", new {id = device.Id}, device);
        }
        
        return BadRequest("Problem creating device");
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Device device)
    {
        if (device.Id != id || !DeviceExists(id))
            return BadRequest("Cannot update this device");

        repo.UpdateDevice(device);

        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the device");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var device = await repo.GetDeviceByIdAsync(id);
        if (device == null) return NotFound();

        repo.DeleteDevice(device);
        
        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the device");
    }

    private bool DeviceExists(int id)
    {
        return repo.DeviceExists(id);
    }
}
