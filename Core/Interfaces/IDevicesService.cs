using API.DTOs;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

public interface IDevicesService
{
    Task<DeviceDetailsDTO?> GetDeviceDetailsDTOAsync(int idDevice);
    Task<IReadOnlyList<DeviceSummaryDTO>> GetDevicesSummaryDTOAsync();
    Task<IReadOnlyList<Device>> GetDevicesAsync();
    Task<Device?> GetDeviceByIdAsync(int id);
    Task<Device> CreateDeviceAsync(Device device);
    Task<Device> CreateDeviceAsync(CreateDeviceDTO device);
    Task<bool> UpdateDeviceAsync(UpdateDeviceDTO device);
    Task<bool> DeleteDeviceAsync(int id);
    Task<bool> AssignDeviceToEmail(int id, string email);
    Task<bool> UnassignDeviceFromEmail(int id, string email);
    List<DeviceSummaryDTO> SearchDevices(string query, List<DeviceSummaryDTO> allDevices);
}
