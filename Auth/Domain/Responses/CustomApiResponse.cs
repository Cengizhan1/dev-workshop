namespace Domain.Responses;

public class CustomApiResponse
{
    public int HttpStatusCode { get; set; }
    public bool IsSuccessful { get; set; } = false;
    public string ReturnCode { get; set; }
    public string Message { get; set; }
    public List<string> ErrorMessageList { get; set; } = new List<string> { };
    public string StringResponse { get; set; }
    public string ResponseType { get; set; }
    public string FileName { get; set; }
    public byte[] ZByteResponse { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
}
