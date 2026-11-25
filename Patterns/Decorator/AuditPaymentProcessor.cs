namespace DesignPatternsDemo;

public sealed class AuditPaymentProcessor : IPaymentProcessor
{
    public Task ProcessAsync(Order order, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Payment audit recorded for order {order.Id}.");
        return Task.CompletedTask;
    }
}
