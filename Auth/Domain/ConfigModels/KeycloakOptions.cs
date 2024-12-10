namespace Domain.ConfigModels;

public class KeycloakOptions
{
    public string Authority { get; set; }
    public string KeyCloakBaseUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Audience { get; set; }
    public EndpointsOptions Endpoints { get; set; }
}

public class EndpointsOptions
{
    public string Token { get; set; }
    public string Login { get; set; }
    public string Logout { get; set; }
    public string Register { get; set; }
}

