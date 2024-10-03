namespace Domain.Responses;

public class TokenResponse
{
    public string Token { get; set; }
    public DateTime ExpireTime { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}


