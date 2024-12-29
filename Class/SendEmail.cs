using System.Net.Mail;


namespace eclipse.Aplication;

public class SendEmail : ISendEmail
{
    public readonly IConfiguration _configuration;
    public SendEmail(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string SendEmailAsync(string email, string confirmacionLink)
    {
        string token = GenerateRandomToken(4);
        MailMessage mailMessage = new MailMessage("yaselbarrioscarrillo@gmail.com", email, "Yasel", confirmacionLink + token);
        mailMessage.IsBodyHtml = true;
        System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com");
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Port = Convert.ToInt32(_configuration.GetSection("Email:Port").Value);
        smtpClient.Credentials = new System.Net.NetworkCredential(_configuration.GetSection("Email:Username").Value, _configuration.GetSection("Email:Password").Value);
        smtpClient.Send(mailMessage);
        return token;
    }
    public string SendEmailTokenAsync(string email)
    {
       string token = GenerateRandomToken(6);
          string body = $@"
        <h1>Recuperación de contraseña</h1>
        <p>Tu token de recuperación es:</p>
        <h2>{token}</h2>
        <p>Si no solicitaste este correo, ignóralo.</p>
    ";

    MailMessage mailMessage = new MailMessage("yaselbarrioscarrillo@gmail.com", email)
    {
        Subject = "Token de recuperación",
        Body = body,
        IsBodyHtml = true // Permitir HTML en el mensaje
    };
        System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com");
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Port = Convert.ToInt32(_configuration.GetSection("Email:Port").Value);
        smtpClient.Credentials = new System.Net.NetworkCredential(_configuration.GetSection("Email:Username").Value, _configuration.GetSection("Email:Password").Value);
        smtpClient.Send(mailMessage);
        return token;
    }
    public string GenerateRandomToken(int length)
    {
        const string chars = "0123456789";
        var random = new Random();
        var token = new char[length];

        for (int i = 0; i < length; i++)
        {
            token[i] = chars[random.Next(chars.Length)];
        }

        return new string(token);
    }
    
    
}