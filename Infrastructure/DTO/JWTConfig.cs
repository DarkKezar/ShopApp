namespace Infrastructure.DTO;

public class JWTConfig
{
    public string issuer { get; set; }
    public string audience { get; set; }
    public byte[] key { get; set; }
}