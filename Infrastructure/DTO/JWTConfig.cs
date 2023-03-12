namespace Infrastructure.DTO;

public class JWTConfig
{
    public string issuer { get; set; }
    public string audience { get; set; }
    public byte[] key { get; set; }

    public JWTConfig(string issuer, string audience, byte[] key)
    {
        this.issuer = issuer;
        this.audience = audience;
        this.key = key;
    }
}