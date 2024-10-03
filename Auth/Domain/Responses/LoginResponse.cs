using Domain.Enums;

namespace Domain.Responses;

public class LoginResponse : TokenResponse
{
    public TokenType TokenType { get; set; }
}


