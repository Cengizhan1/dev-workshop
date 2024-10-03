using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using FluentValidation;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IValidator<LoginRequest> _validator;
    public AuthService(IValidator<LoginRequest> validator)
    {
        _validator = validator;
    }

    public async Task<CustomDataResponse<LoginResponse>> Login(LoginRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return new CustomDataResponse<LoginResponse>(); // değiştirilecek
        }

        // TODO: Login işlemleri yapılacak
        var response = new LoginResponse
        {
            
        };

        return new CustomDataResponse<LoginResponse>(); // değiştirilecek
    }
}
