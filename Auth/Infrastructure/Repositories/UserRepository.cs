using Domain.Entities;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<User> _userSet;

    public UserRepository(AppDbContext context)
    {
        _context = context;
        _userSet = context.Set<User>();
    }
    public async ValueTask CreateAsync(User user)
    {
        await _userSet.AddAsync(user);
    }

    public void Delete(User user)
    {
        _userSet.Remove(user);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userSet.FindAsync(id);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _userSet.FirstAsync(x => x.Username == username);
    }

    public void Update(User user)
    {
        _userSet.Update(user);
    }
}
