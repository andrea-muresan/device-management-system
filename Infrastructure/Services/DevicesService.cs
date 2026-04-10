
using API.DTOs;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using AutoMapper;

namespace Infrastructure.Services;

public class DevicesService(IDevicesRepository deviceRepo, IMapper mapper) : IDevicesService
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

    public async Task<Device> CreateDeviceAsync(CreateDeviceDTO dto)
    {
        var device = mapper.Map<Device>(dto);
        
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

    public async Task<bool> UpdateDeviceAsync(UpdateDeviceDTO dto)
    {
        var existingDevice = await deviceRepo.GetDeviceByIdAsync(dto.Id);

        if (existingDevice == null) return false;

        mapper.Map(dto, existingDevice);
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
        var device = await deviceRepo.GetDeviceByIdAsync(idDevice);

        return mapper.Map<DeviceDetailsDTO>(device);
    }

    public async Task<IReadOnlyList<DeviceSummaryDTO>> GetDevicesSummaryDTOAsync()
    {
        var devices = await deviceRepo.GetDevicesAsync(); 

        return mapper.Map<IReadOnlyList<DeviceSummaryDTO>>(devices);
    }
}
