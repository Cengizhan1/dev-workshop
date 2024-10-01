using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    public Task<CustomDataResponse<LoginResponse>> Login(LoginRequest request)
    {
        // TODO: LoginRequest için validaasyon eklenecek

        throw new NotImplementedException();

    }
}
