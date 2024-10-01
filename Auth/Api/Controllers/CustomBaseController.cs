using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    public CustomBaseController()
    {

    }

    protected virtual IActionResult ApiResponse()
    {
        return Ok(new SuccessDataResponse());
    }

    protected virtual IActionResult ApiResponse(HttpStatusCode httpStatusCode)
    {
        return SendResponse(new SuccessDataResponse(httpStatusCode));
    }
    protected virtual IActionResult ApiResponse<T>(CustomDataResponse<T> result)
    {
        return SendResponse(result);
    }

    protected virtual IActionResult ApiResponse(CustomApiResponse result)
    {
        return SendResponse(result);
    }

    protected virtual FileStreamResult ApiResponse(byte[] result, string contentType)
    {
        return new FileStreamResult(new MemoryStream(result), contentType);
    }

    private IActionResult SendResponse(dynamic result)
    {
        if (result.IsSuccessful)
        {
            switch (result.HttpStatusCode)
            {
                case (int)HttpStatusCode.OK:
                    return Ok(result);
                case (int)HttpStatusCode.Created:
                    return Created("", result);
                case (int)HttpStatusCode.Accepted:
                    return Accepted(result);
                case (int)HttpStatusCode.NoContent:
                    return NoContent();
            }
        }
        else
        {
            switch (result.HttpStatusCode)
            {
                case (int)HttpStatusCode.BadRequest:
                    return BadRequest(new ErrorDataResponse(result.ErrorMessageList, HttpStatusCode.BadRequest));
                case (int)HttpStatusCode.Unauthorized:
                    return Unauthorized(new { Message = "Unauthorized request" });
                case (int)HttpStatusCode.Forbidden:
                    return Forbid();
                case (int)HttpStatusCode.NotFound:
                    return NotFound();
                default:
                    return BadRequest();
            }
        }

        return BadRequest();
    }
}