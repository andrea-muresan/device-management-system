using System;
using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.DTOs;

public class DeviceSummaryDTO
{
    [Required]
    public int Id { get; init; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DeviceType Type { get; set; }

    [Required]
    public string OS { get; set; } = string.Empty;

    public string UserName {get;set;} = string.Empty;

    public string UserLocation {get;set;} = string.Empty;
}
