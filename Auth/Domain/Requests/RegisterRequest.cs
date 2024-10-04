namespace Domain.Requests;

public class RegisterRequest
{
    public string Email { get; set; }
    public string PhoneCode { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}