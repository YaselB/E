using System.ComponentModel.DataAnnotations;

namespace eclipse.Aplication;
public class Prices
{
    [Key]
    public int IDPrices { get; set; }
    public required string currency { get; set; }
    public required float price { get; set; }
    public required int IDTickets { get; set; }
    public required Ticket? ticket { get; set; }
}