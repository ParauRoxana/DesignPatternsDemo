namespace DesignPatternsDemo;

public sealed class CardPaymentProcessor : IPaymentProcessor
{
    public Task ProcessAsync(Order order, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Card payment authorized for {order.Total:C}.");
        order.MarkPaid();
        return Task.CompletedTask;
    }
}
