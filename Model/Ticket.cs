using System.ComponentModel.DataAnnotations;

namespace eclipse.Aplication;
public class Ticket
{
    [Key]
    public int IDTicket { get; set; }
    public required string nameAirline { get; set; }
    public required string origin { get; set; }
    public required string destination { get; set; }
    public required ICollection<Prices>? prices { get; set; }
    public required ICollection<Dates>? dates { get; set; }
    
}