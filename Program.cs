using eclipse.Aplication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eclipse", Version = "v1" });
});
builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql("Host=junction.proxy.rlwy.net;Port=56618;Database=railway;Username=postgres;Password=mcpvXHQSsHJMGaOxNcHjQhqtjkfKZzat;SslMode=Require"));
builder.Services.AddScoped<ISendEmail, SendEmail>();
builder.Services.AddScoped<IGeneratedJwt, GeneratedJwt>();
builder.Services.AddScoped<UserService>();
//
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("https://eclipse-frontend-production.up.railway.app")
              .AllowAnyHeader()
              .AllowAnyMethod(); 
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("super secret key for new project with the 512 bytes patreon , thanks for user my app"))
    };
});
var app = builder.Build();
 
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();
    dbContext.Database.Migrate();
}
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eclipse V1");
    });
}
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.MapControllers();



app.Run();


