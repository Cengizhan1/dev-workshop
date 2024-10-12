using BlogDomain.Entities;

namespace Application.Interfaces.Repositories;

public interface IBlogRepository
{

    public ValueTask CreateAsync(Blog blog);

    public void Update(Blog blog);

    public void Delete(Blog blog);

    public Task<Blog?> GetByIdAsync(int id);

    public Task<IEnumerable<Blog>> GetAllAsync();
}
