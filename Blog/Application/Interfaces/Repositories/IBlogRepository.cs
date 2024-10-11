using BlogDomain.Entities;

namespace Application.Interfaces.Repositories;

public interface IBlogRepository
{

    public ValueTask Create(Blog blog);

    public void Update(Blog blog);

    public void Delete(Blog blog);

    public Task<Blog?> GetById(int id);

    public Task<IEnumerable<Blog>> GetAll();
}
