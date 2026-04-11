using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class RegisterUserDTO
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Role { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Location { get; set; }

    [Required]
    [MaxLength(256)]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
}
