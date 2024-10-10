using System.Text.Json.Serialization;

namespace Domain.Responses;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpireTime { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonPropertyName("refresh_expires_in")]
    public int RefreshTokenExpireTime { get; set; }
}


