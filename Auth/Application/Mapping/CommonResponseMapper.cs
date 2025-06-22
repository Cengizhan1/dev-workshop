using Domain.Responses;
using System.Net;

namespace Application.Mapping;

public static class CommonResponseMapper
{

    public static CustomApiResponse ToCustomApiResponse(bool isSuccessful, HttpStatusCode httpStatusCode = HttpStatusCode.OK, List<string> errorMessageList = null)
    {
        errorMessageList ??= new List<string>();

        return new CustomApiResponse
        {
            HttpStatusCode = (int)httpStatusCode,
            IsSuccessful = isSuccessful,

            ErrorMessageList = errorMessageList,
        };

    }

    public static CustomDataResponse<T> ToCustomDataResponse<T>(this T request, bool isSuccessful, HttpStatusCode httpStatusCode = HttpStatusCode.OK,
        List<string> errorMessageList = null)
    {
        errorMessageList ??= new List<string>();

        return new CustomDataResponse<T>
        {
            Data = request,
            DataList = [],
            HttpStatusCode = (int)httpStatusCode,
            IsSuccessful = isSuccessful,
            ErrorMessageList = errorMessageList,
        };

    }

    public static CustomDataResponse<T> ToCustomDataListResponse<T>(this IEnumerable<T> requestDataList, bool isSuccessful, HttpStatusCode httpStatusCode = HttpStatusCode.OK,
List<string> errorMessageList = null)
    {
        errorMessageList ??= new List<string>();

        return new CustomDataResponse<T>
        {
            Data = default,
            DataList = requestDataList,
            HttpStatusCode = (int)httpStatusCode,
            IsSuccessful = isSuccessful,
            ErrorMessageList = errorMessageList,
        };
    }
}