using System.ComponentModel.DataAnnotations;

namespace eclipse.Aplication;
public class User
{
    [Key]
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string? Password { get; set; }
    public required string? token { get; set; }
    public required Rol? Rol { get; set; }
}