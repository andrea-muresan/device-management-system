using System;
using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace API.DTOs;

public class DeviceDetailsDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Manufacturer { get; set; } = string.Empty;

    [Required]
    public DeviceType Type { get; set; }

    [Required]
    public string OS { get; set; } = string.Empty;
    
    [Required]
    public string OSVersion { get; set; } = string.Empty;
    
    [Required]
    public string Processor { get; set; } = string.Empty;
    
    [Range(1, 512, ErrorMessage = "RAM must be between 1 and 512 GB")]
    public int RAM { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;


    public string UserName {get;set;} = string.Empty;

    public string UserRole {get;set;} = string.Empty;

    public string UserLocation {get;set;} = string.Empty;

    public string UserEmail {get;set;} = string.Empty;
}
