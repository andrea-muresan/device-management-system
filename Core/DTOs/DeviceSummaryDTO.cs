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
    public string Manufacturer { get; set; } = string.Empty;

    [Required]
    public string Processor { get; set; } = string.Empty;

    [Required]
    public int RAM { get; set; }

    public string UserName {get;set;} = string.Empty;
}
