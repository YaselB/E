using System.ComponentModel.DataAnnotations;

namespace eclipse.Aplication;
public class Navbar
{
    [Key]
    public int IDNavbar { get; set; }
    public required string Text { get; set; }
}