using System.Net;

namespace Domain.Responses;

public class ErrorDataResponse : CustomApiResponse
{
    public ErrorDataResponse(List<string> errorMessages, HttpStatusCode httpStatusCode)
    {
        IsSuccessful = false;
        ErrorMessageList = errorMessages;
        HttpStatusCode = (int)httpStatusCode;
    }
}
