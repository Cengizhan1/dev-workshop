using Application.Interfaces.Services;
using BlogDomain.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : CustomBaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ApiResponse(await _blogService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ApiResponse(await _blogService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogCreateRequest request)
        {
            return ApiResponse(await _blogService.CreateAsync(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BlogUpdateRequest request)
        {
            return ApiResponse(await _blogService.UpdateAsync(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ApiResponse(await _blogService.DeleteAsync(id));
        }

    }
}
