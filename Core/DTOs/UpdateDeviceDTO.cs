using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace Core.DTOs;

public class UpdateDeviceDTO
{
    [Required]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Manufacturer { get; set; } = string.Empty;

    [Required()]
    public DeviceType Type { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string OS { get; set; } = string.Empty;
    
    [Required(AllowEmptyStrings = false)]
    public string OSVersion { get; set; } = string.Empty;
    
    [Required(AllowEmptyStrings = false)]
    public string Processor { get; set; } = string.Empty;
    
    [Range(1, 512, ErrorMessage = "RAM must be between 1 and 512 GB")]
    public int RAM { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public string Description { get; set; } = string.Empty;

    public int? UserId { get; set; }
}
