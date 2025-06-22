using Domain.Entities;

namespace Domain.Responses;

public interface IUserRepository
{
    public ValueTask CreateAsync(User user);

    public void Update(User user);
    public void Delete(User user);

    public Task<User?> GetByIdAsync(Guid id);
    public Task<User> GetByUsernameAsync(string username);

}
