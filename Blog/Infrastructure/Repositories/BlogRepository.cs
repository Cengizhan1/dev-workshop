using Application.Interfaces.Repositories;
using BlogDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Blog> _blogSet;

    public BlogRepository(AppDbContext context)
    {
        _context = context;
        _blogSet = context.Set<Blog>();
    }
    public async ValueTask CreateAsync(Blog blog)
    {
        await _blogSet.AddAsync(blog);
    }

    public void Delete(Blog blog)
    {
        _blogSet.Remove(blog);
    }

    public async Task<IEnumerable<Blog>> GetAllAsync()
    {
        // TODO: filtre ve pagination eklenecek
        return await _blogSet.ToListAsync();
    }

    public async Task<Blog?> GetByIdAsync(int id)
    {
        return await _blogSet.FindAsync(id);
    }

    public void Update(Blog blog)
    {
        _blogSet.Update(blog);
    }
}
