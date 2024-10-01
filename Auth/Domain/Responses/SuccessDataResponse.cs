using System.Net;

namespace Domain.Responses;

public class SuccessDataResponse : CustomApiResponse
{
    public SuccessDataResponse()
    {
        IsSuccessful = true;
    }

    public SuccessDataResponse(HttpStatusCode httpStatusCode)
    {
        HttpStatusCode = (int)httpStatusCode;
        IsSuccessful = true;
    }
}
