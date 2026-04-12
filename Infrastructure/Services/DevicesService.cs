using API.DTOs;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using AutoMapper;

namespace Infrastructure.Services;

public class DevicesService(IDevicesRepository deviceRepo, IUserRepository userRepo, IMapper mapper) : IDevicesService
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

    public async Task<bool> AssignDeviceToEmail(int id, string email)
    {
        var device = await deviceRepo.GetDeviceByIdAsync(id);
        var user = await userRepo.GetUserByEmailAsync(email);

        if (device == null || user == null) return false;

        if (device.UserId != null) return false;

        device.UserId = user.Id;

        return await deviceRepo.SaveChangesAsync();
    }

    public async Task<bool> UnassignDeviceFromEmail(int id, string email)
    {
        var device = await deviceRepo.GetDeviceByIdAsync(id);
        var user = await userRepo.GetUserByEmailAsync(email);

        if (device == null || user == null) return false;

        if (device.UserId == null) return false;

        if (device.Id != id || user.Email != email) return false;

        device.UserId = null;

        return await deviceRepo.SaveChangesAsync();
    }

    public List<DeviceSummaryDTO> SearchDevices(string query, List<DeviceSummaryDTO> allDevices)
    {
        if (string.IsNullOrWhiteSpace(query)) return allDevices;

        var cleanQuery = new string(query.Where(c => !char.IsPunctuation(c)).ToArray());
        var tokens = cleanQuery.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return allDevices
            .Select(device => new
            {
                Device = device,
                Score = CalculateRelevanceScore(device, tokens)
            })
            .Where(x => x.Score > 0)
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.Device.Name)
            .Select(x => x.Device)
            .ToList();
    }

    private int CalculateRelevanceScore(DeviceSummaryDTO device, string[] tokens)
    {
        int score = 0;

        foreach (var token in tokens)
        {
            // name first - 10 puncte
            if (device.Name.ToLower().Contains(token)) score += 10;

            // manufactureur - 5
            if (device.Manufacturer.ToLower().Contains(token)) score += 5;

            // processor 3
            if (device.Processor.ToLower().Contains(token)) score += 3;

            // ram 1
            if (device.RAM.ToString().Contains(token)) score += 1;
        }

        return score;
    }
}
