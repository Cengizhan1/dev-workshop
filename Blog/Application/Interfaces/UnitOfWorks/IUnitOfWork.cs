namespace Application.Interfaces.UnitOfWorks;

public interface IUnitOfWork
{
    Task CommitAsync();
    void Commit();
}
