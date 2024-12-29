namespace eclipse.Aplication;
public class TicketUpdate
{
    public required int id { get; set; }
    public required string aeroline { get; set; }
    public required string from { get; set; }
    public required string to { get; set; }
    public required List<string> date { get; set; }
    public required List<PricesModel> price { get; set; }

}