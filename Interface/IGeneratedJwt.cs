namespace eclipse.Aplication;

public interface IGeneratedJwt{

public string GeneratedToken(string Email, string Password);
public bool VerifyToken(string token);
}