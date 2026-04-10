namespace Core.Entities;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Role { get; set; }
    public required string Location { get; set; }
    public required string Email { get; set; }
}
