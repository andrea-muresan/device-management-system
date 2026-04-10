
using API.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services;

public class DevicesService(IDevicesRepository deviceRepo, IUserRepository userRepo) : IDevicesService
{
    public async Task<Device> CreateDeviceAsync(Device device)
    {
        deviceRepo.AddDevice(device);
        
        if (await deviceRepo.SaveChangesAsync())
        {
            return device;
        }

        throw new Exception("Problem creating the device");
    }

    public async Task<IReadOnlyList<Device>> GetDevicesAsync()
    {
        return await deviceRepo.GetDevicesAsync();
    }

    public async Task<Device?> GetDeviceByIdAsync(int id)
    {
        return await deviceRepo.GetDeviceByIdAsync(id);
    }

    public async Task<bool> UpdateDeviceAsync(Device device)
    {
        if (!deviceRepo.DeviceExists(device.Id)) return false;

        deviceRepo.UpdateDevice(device);
        return await deviceRepo.SaveChangesAsync();
    }

    public async Task<bool> DeleteDeviceAsync(int id)
    {
        var device = await deviceRepo.GetDeviceByIdAsync(id);
        
        if (device == null) return false;

        deviceRepo.DeleteDevice(device);
        return await deviceRepo.SaveChangesAsync();
    }

    public async Task<DeviceDetailsDTO?> GetDeviceDetailsDTOAsync(int idDevice)
    {
        Device? device = await deviceRepo.GetDeviceByIdAsync(idDevice);

        if (device == null) return null;

        DeviceDetailsDTO dev = new DeviceDetailsDTO
        {
            Name = device.Name,
            Manufacturer = device.Manufacturer,
            Type = device.Type,
            OS = device.OS,
            Processor = device.Processor,
            RAM = device.RAM,
            Description = device.Description,
        };

        int? userId = device.UserId;
        if (userId == null) return dev;
        User? user = await userRepo.GetUserByIdAsync(userId.Value);

        if (user == null) return dev;

        dev.UserName = user.Name;
        dev.UserLocation = user.Location;
        dev.UserRole = user.Role;

        return dev;
    }
}
