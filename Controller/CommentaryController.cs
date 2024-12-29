using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eclipse.Aplication;
[ApiController]
[Route("api/[controller]")]
public class CommentaryController : ControllerBase
{
    private readonly DBContext context;
    public CommentaryController(DBContext dbcontext)
    {
        context = dbcontext;
    }
    [HttpPost("CreateCommentary")]
    public async Task<IActionResult> CreateCommentary(CommentaryModel commentaryModel)
    {
        Comentary comentary = new Comentary
        {
            Comentarytext = commentaryModel.Comentarytext,
            Email = commentaryModel.Email,
            Stars = commentaryModel.Stars,
            Date =DateTime.UtcNow,
            Image = commentaryModel.Image
        };
        context.Comentarys.Add(comentary);
        await context.SaveChangesAsync();
        return Ok(new { message = "Comentary created" });
    }
    [HttpGet("GetComentary")]
    public async Task<IActionResult> GetComentary()
    {
        var totalComentary = context.Comentarys.Select(option => option.Stars).Average();
        var comentary = await context.Comentarys.OrderByDescending(c => c.Stars).ToListAsync();
        List<CommentaryBack> comentaryBack = new List<CommentaryBack>();
        foreach (var i in comentary)
        {
            CommentaryBack commentaryBack2 = new CommentaryBack{
                Comentarytext = i.Comentarytext,
                Email = i.Email,
                IDComentary = i.IDComentary,
                Stars = i.Stars,
                Image = i.Image,
                Date = i.Date.ToString("yyyy-MM-dd")
            };
            comentaryBack.Add(commentaryBack2);
            if(comentaryBack.Count >=20){
                break;
            }
        }

        return Ok(new { comentaryBack , totalComentary});
    }
}
