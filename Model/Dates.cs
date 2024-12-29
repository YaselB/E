using System.ComponentModel.DataAnnotations;

namespace eclipse.Aplication;
public class Dates{
    [Key]
    public int IDDates { get; set; }
    public required string date { get ; set ;}
    public required int IDTickets { get; set; }
    public required Ticket? ticket { get; set; }
}