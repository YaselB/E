using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eclipse.Aplication;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DBContext context;
    public readonly UserService userService ;
    public readonly ISendEmail sendEmail;
    public readonly IGeneratedJwt generatedJwt;

    public UserController(DBContext dbcontext , UserService _userService, ISendEmail _sendEmail , IGeneratedJwt _generatedJwt)
    {
        context = dbcontext;
        userService = _userService;
        sendEmail = _sendEmail;
        generatedJwt = _generatedJwt;
    }
    [HttpPost("CrearUsuario")]
    public async Task<IActionResult> CrearUsuario(Usermodel usermodel)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == usermodel.Email);
        if (user != null)
        {
            return NotFound(new { message = "El usuario ya existe" });
        }
        if (usermodel.Password != null)
        {
            
            Console.WriteLine("Validando contraseña");
            // Validaciones específicas para mensajes claros
            if (usermodel.Password.Length < 6)
            {
                return BadRequest("The password must have 6 or more characters");
            }
            if (!Regex.IsMatch(usermodel.Password, "(?=.*[A-Z])"))
            {
                return BadRequest("The password must have at least one uppercase letter");
            }
            if (!Regex.IsMatch(usermodel.Password, "(?=.*[a-z])"))
            {
                return BadRequest("The password must have at least one lowercase letter");
            }
            if (!Regex.IsMatch(usermodel.Password, "(?=.*\\d)"))
            {
                return BadRequest("The password must have at least one number");
            }
            if (!Regex.IsMatch(usermodel.Password, "(?=.*[^\\da-zA-Z])"))
            {
                return BadRequest("The password must contain at least one special character");
            }
            User user1 = new User
            {
                Email = usermodel.Email,
                Password = userService.GeneratePassword(usermodel.Password),
                token = null,
                Rol = null,
            };
            context.Users.Add(user1);
            await context.SaveChangesAsync();
            var user2 = await context.Users.FirstOrDefaultAsync(option => option.Email == usermodel.Email);
            if(user2 == null){
                return NotFound("User do not registered");
            }
            Rol rol = new Rol{
                UserEmail = user2.Email,
                UserID = user2.Id,
                UserRol = "user",
                Username = null
            };
            await context.Rols.AddAsync(rol);
            await context.SaveChangesAsync();
            Console.WriteLine("Usuario creado");
        }
        else{
            User user1 = new User{
                Email = usermodel.Email,
                Password = usermodel.Password,
                token = null,
                Rol = null
            };
            context.Users.Add(user1);
            await context.SaveChangesAsync();
            var user2 = await context.Users.FirstOrDefaultAsync(option => option.Email == usermodel.Email);
            if(user2 == null){
                return NotFound("User do not registered");
            }
            Rol rol = new Rol{
                UserEmail = user2.Email,
                UserID = user2.Id,
                UserRol = "user",
                Username = null
            };
            await context.Rols.AddAsync(rol);
            await context.SaveChangesAsync();
            Console.WriteLine("Usuario creado sin contraseña");
        }
        return Ok(new { message = "Usuario creado" });
    
    }
    [HttpPost("login")]
    public async Task<IActionResult> UserLogin([FromBody] NewPassword newPassword)
    {
        var user = await context.Users.FirstOrDefaultAsync(option => option.Email == newPassword.Email);
        if (user == null)
        {
            return NotFound("User do not registered");
        }
        if (newPassword.Password != null && user.Password != null &&!userService.verifyPassword(newPassword.Password, user.Password))
        {
            return BadRequest("Wrong Password");
        }
        var role = await context.Rols.FirstOrDefaultAsync(option => option.UserID == user.Id);
        if (role == null)
        {
            return NotFound("User haven't role");
        }
        user.token = generatedJwt.GeneratedToken(user.Email, user.Password ?? "");
        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return Ok(new { role.UserRol, user.token});
    }
        [HttpPost("ChangePassword")]
    public async Task<IActionResult> GetTokenUser([FromBody] SendTokenUser sendTokenUser)
    {
        var find = await context.Users.FirstOrDefaultAsync(option => option.Email == sendTokenUser.Email);
        if (find == null)
        {
            return NotFound("User do not registered");
        }
        find.token = sendEmail.SendEmailTokenAsync(find.Email);
        context.Entry(find).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return Ok("Please check your email");
    }

    [HttpGet("ConfirmChangeToken/{Token}")]
    public async Task<IActionResult> ConfirmChangeToken( string Token)
    {
        var userRegister = await context.Users.FirstOrDefaultAsync(option => option.token == Token);
        if (userRegister == null)
        {
            return NotFound("Please the user do not exist");
        }
        return Ok(userRegister.Email);
    }

    [HttpPatch("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] NewPassword newPassword)
    {
        var find = await context.Users.FirstOrDefaultAsync(option => option.Email == newPassword.Email);
        if (find == null)
        {
            return NotFound("Please this email do not be registered");
        }
        if(newPassword.Password != null){
        find.Password = userService.GeneratePassword(newPassword.Password);
        context.Entry(find).State = EntityState.Modified;
        await context.SaveChangesAsync();
        }
        return Ok("The password change was succesfully");
    }
}
        
            
    

        
    

