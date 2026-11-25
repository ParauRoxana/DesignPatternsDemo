namespace DesignPatternsDemo;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
