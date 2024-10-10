using Application.Interfaces;
using BlogDomain.Requests;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : CustomBaseController
{
    private readonly IBlogService _service;
    public BlogController(IBlogService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BlogCreateRequest request)
    {
        return ApiResponse(await _service.Create(request));
    }


}
