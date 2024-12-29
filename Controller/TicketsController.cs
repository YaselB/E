using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eclipse.Aplication;
[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly DBContext context;
    public TicketsController(DBContext dbcontext)
    {
        context = dbcontext;
    }
    [HttpPost("CreateTicket")]
    public async Task<IActionResult> CreateTicket([FromBody] TicketModel ticketModel)
    {
        List<Dates> dates = new List<Dates>();
        List<Prices> prices = new List<Prices>();
        Ticket ticket = new Ticket
        {
            nameAirline = ticketModel.nameAirline,
            origin = ticketModel.origin,
            destination = ticketModel.destination,
            prices = null,
            dates = null,
        };
        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();
        foreach (var i in ticketModel.selectedDates)
        {
            Dates date = new Dates
            {
                date = i,
                ticket = null,
                IDTickets = ticket.IDTicket
            };
            dates.Add(date);
        }
        foreach (var i in ticketModel.prices)
        {
            Prices prices1 = new Prices
            {
                price = i.value,
                currency = i.currency,
                ticket = null,
                IDTickets = ticket.IDTicket
            };
            prices.Add(prices1);
        }
        context.Dates.AddRange(dates);
        context.Prices.AddRange(prices);
        await context.SaveChangesAsync();
        return Ok(new { message = "Ticket created" });
    }
    [HttpGet("GetTicket")]
    public async Task<IActionResult> GetTicket()
    {
        var tickets = await context.Tickets.Include(t => t.dates).Include(t => t.prices).ToListAsync();
        List<TicketUpdate> ticketModels = new List<TicketUpdate>();
        foreach (var i in tickets)
        {
            if (i.dates != null && i.prices != null)
            {
                TicketUpdate ticketModel = new TicketUpdate
                {
                    id = i.IDTicket,
                    aeroline = i.nameAirline,
                    from = i.origin,
                    to = i.destination,
                    date = i.dates.Select(d => d.date).ToList(),
                    price = i.prices.Select(p => new PricesModel
                    {
                        currency = p.currency,
                        value = p.price
                    }).ToList()
                };
                ticketModels.Add(ticketModel);
            }
        }
        return new JsonResult(new { ticketModels }) 
    {
        StatusCode = StatusCodes.Status200OK,
        ContentType = "application/json"
    };
    }
    [HttpPatch("ChangeTicket")]
    public async Task<IActionResult> ChangeTicket(TicketUpdate ticketUpdate)
    {
        var ticket = await context.Tickets.Include(t => t.dates).Include(t => t.prices).FirstOrDefaultAsync(option => option.IDTicket == ticketUpdate.id);
        if (ticket == null)
        {
            return NotFound("Ticket do not registered");
        }
        ticket.nameAirline = ticketUpdate.aeroline;
        ticket.origin = ticketUpdate.from;
        ticket.destination = ticketUpdate.to;


        if (ticket.dates != null)
        {
            var datestoRemove = ticket.dates.Where(d => !ticketUpdate.date.Contains(d.date)).ToList();
            foreach (var dates in datestoRemove)
            {
                ticket.dates.Remove(dates);
            }
            foreach (var i in ticketUpdate.date)
            {
                var date = ticket.dates.FirstOrDefault(option => option.date == i);
                if (date == null)
                {
                    Dates dates = new Dates
                    {
                        date = i,
                        ticket = null,
                        IDTickets = ticket.IDTicket
                    };
                    ticket.dates.Add(dates);
                }
            }
        }

        if (ticket.prices != null)
        {
            var pricetoRemove = ticket.prices.Where(d => !ticketUpdate.date.Contains(d.currency)).ToList();
            foreach (var prices1 in pricetoRemove)
            {
                ticket.prices.Remove(prices1);
            }
            foreach (var i in ticketUpdate.price)
            {

                var price = ticket.prices.FirstOrDefault(option => option.currency == i.currency);
                if (price == null)
                {
                    Prices prices = new Prices
                    {
                        currency = i.currency,
                        price = i.value,
                        ticket = null,
                        IDTickets = ticket.IDTicket
                    };
                    ticket.prices.Add(prices);
                }
            }
        }
        await context.SaveChangesAsync();
        return Ok(new { message = "Ticket changed" });
    }

    [HttpDelete("DeleteTicket/{id}")]
    public async Task<IActionResult> DeleteTicket( int id)
    {
       var ticket = await context.Tickets.Include(t => t.dates).Include(t=> t.prices).FirstOrDefaultAsync(option => option.IDTicket == id);
       if(ticket == null){
           return NotFound(new { message = "Ticket do not registered" });
       }
       if(ticket.dates != null){
            ticket.dates.Clear();
       }
       if(ticket.prices != null){
           ticket.prices.Clear();
       }
       context.Tickets.Remove(ticket);
       await context.SaveChangesAsync();
       return Ok(new { message = "Ticket deleted" });
    }
}

