namespace DesignPatternsDemo;

public sealed class InMemoryUnitOfWork : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Unit of Work committed.");
        return Task.CompletedTask;
    }
}
