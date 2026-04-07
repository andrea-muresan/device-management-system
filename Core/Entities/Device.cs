using Core.Enums;

namespace Core.Entities;

public class Device : BaseEntity
{
    public required string Name { get; set; }
    public required string Manufacturer { get; set; }
    public required DeviceType Type { get; set; }
    public required string OS { get; set; }
    public required string OSVersion { get; set; }
    public required string Processor { get; set; }
    public int RAM { get; set; }
    public required string Description { get; set; }

    public int? UserId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public User? User { get; set; }
}
