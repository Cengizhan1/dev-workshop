using Application.Interfaces;
using Application.Mapping;
using Domain.ConfigModels;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Proxies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly KeycloakOptions _config;
    private readonly ILogger<AuthService> _logger;
    private readonly IUserService _userService;
    private readonly IKeycloakProxy _keycloakProxy;
    public AuthService(IOptions<KeycloakOptions> config, IUserService userService, ILogger<AuthService> logger, KeycloakProxy keycloakProxy)
    {
        _config = config.Value;
        _userService = userService;
        _logger = logger;
        _keycloakProxy = keycloakProxy;
    }
    public async Task<CustomDataResponse<LoginResponse>> Login(LoginRequest request)
    {
        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", request.Username),
            new KeyValuePair<string, string>("password", request.Password)
        });

        var loginResponse = await _keycloakProxy.LoginAsync(requestData);

        return loginResponse.ToCustomDataResponse(true);

    }

    public async Task<CustomApiResponse> Logout(LogoutRequest request)
    {
        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
            new KeyValuePair<string, string>("refresh_token", request.RefreshToken),
        });

        await _keycloakProxy.LogoutAsync(requestData);

        return CommonResponseMapper.ToCustomApiResponse(true);
    }

    public async Task<CustomApiResponse> Register(RegisterRequest request)
    {
        var requestData = new StringContent(JsonSerializer.Serialize(new
        {
            username = request.Username,
            email = request.Email,
            enabled = true,
            emailVerified = false,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = request.Password,
                    temporary = false
                }
            }
        }), System.Text.Encoding.UTF8, "application/json");

        await _keycloakProxy.RegisterAsync(requestData);

        UserCreateRequest userCreateRequest = new UserCreateRequest
        {
            Username = request.Username,
            Email = request.Email,
            Phone = request.Phone,
            PhoneCode = request.PhoneCode
        };

        await _userService.CreateAsync(userCreateRequest);

        return CommonResponseMapper.ToCustomApiResponse(true);
    }

    public async Task<CustomDataResponse<TokenResponse>> Refresh(RefreshRequest request)
    {
        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", request.RefreshToken)
        });

        var tokenResponse = await _keycloakProxy.RefreshAsync(requestData);

        return tokenResponse.ToCustomDataResponse(true);
    }
}
