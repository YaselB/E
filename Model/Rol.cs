using System.ComponentModel.DataAnnotations;
using eclipse.Aplication;

public class Rol
{
    [Key]
    public int IDRol { get; set; }
    public required string UserRol { get; set; }
    public required string UserEmail { get; set; }
    public required User? Username { get; set; }
    public required int UserID { get; set; }

}