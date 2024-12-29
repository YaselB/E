using eclise.Aplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eclipse.Aplication;
[ApiController]
[Route("api/[controller]")]
public class NavbarController : ControllerBase{
    private readonly DBContext context;
    public NavbarController(DBContext dbcontext)
    {
        context = dbcontext;
    }
    [HttpGet("GetNavbar")]
    public async Task<IActionResult> GetNavbar(){
        var navbar = await context.Navbars.FirstOrDefaultAsync();
        return Ok(navbar);
    }
    [HttpPatch("ChangeNavbar")]
    public async Task<IActionResult> ChangeNavbar([FromBody] NavbarModel navbarModel){
        var navbar = await context.Navbars.FirstOrDefaultAsync(option => option.IDNavbar == navbarModel.IDNavbar);
        if (navbar == null){
            return NotFound("Navbar do not registered");
        }
        navbar.Text = navbarModel.Text;
        context.Entry(navbar).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return Ok(new { message = "Navbar changed" });
    }
}