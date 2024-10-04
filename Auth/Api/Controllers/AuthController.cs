using Application.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : CustomBaseController
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        this._service = service;
    } 


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return ApiResponse(await _service.Login(request));
    }
/*
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return ApiResponse(await _service.Register());
    }



    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return ApiResponse(await _service.Logout());
    }
*/
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        return ApiResponse(await _service.Refresh(request));
    }
    
}
