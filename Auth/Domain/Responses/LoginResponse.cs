using Domain.Enums;

namespace Domain.Responses;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpireTime { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
    public TokenType TokenType { get; set; }
}


