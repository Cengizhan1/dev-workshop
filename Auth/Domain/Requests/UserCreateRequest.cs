namespace Domain.Requests;

public class UserCreateRequest
{
    public string Email { get; set; }
    public string PhoneCode { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
}