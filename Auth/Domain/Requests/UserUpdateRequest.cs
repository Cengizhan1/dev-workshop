namespace Domain.Requests;

public class UserUpdateRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneCode { get; set; }

    public string Phone { get; set; }
}