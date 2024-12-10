using Application.Interfaces;
using Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class UserController : CustomBaseController
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        this._service = service;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Show()
    {
        if (User.Identity?.Name == null) return BadRequest("User not authenticated");

        var username = User.Identity.Name;

        return ApiResponse(await _service.GetAsync(username));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
    {
        _service.Update(request);

        return ApiResponse(_service.Update(request));
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        if (User.Identity?.Name == null) return BadRequest("User not authenticated");

        var username = User.Identity.Name;

        return ApiResponse(await _service.DeleteAsync(username));
    }
}


