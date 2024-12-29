using System.ComponentModel.DataAnnotations;

namespace eclipse.Aplication;
public class Comentary
{
    [Key]
    public int IDComentary { get; set; }
    public required string Comentarytext { get; set; }
    public required string Email { get; set; }
    public required int Stars { get; set; }
    public required string Image { get ; set ; }
    public required DateTime Date { get ; set ;}
}