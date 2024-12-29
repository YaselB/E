namespace eclipse.Aplication;
public class TicketModel
{
    public required string nameAirline { get; set; }
    public required string origin { get; set; }
    public required string destination { get; set; }
    public required List<string> selectedDates { get; set; }
    public required List<PricesModel> prices { get; set; }
}