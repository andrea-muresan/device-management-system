using API.DTOs;
using Core.Entities;
using AutoMapper;
using Core.DTOs;

namespace Infrastructure.Data;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDeviceDTO, Device>();
        
        CreateMap<UpdateDeviceDTO, Device>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Device, DeviceSummaryDTO>();
        
        CreateMap<Device, DeviceDetailsDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.Name))
            .ForMember(dest => dest.UserLocation, opt => opt.MapFrom(src => src.User!.Location))
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.User!.Role))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User!.Email));

        CreateMap<Device, DeviceSummaryDTO>()
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.User!.Name));
    }
}
