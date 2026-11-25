namespace DesignPatternsDemo;

// Decorator pattern: add cross-cutting behavior without changing the processor.
public sealed class PaymentProcessorDecorator(IPaymentProcessor inner, IPaymentProcessor audit)
    : IPaymentProcessor
{
    public async Task ProcessAsync(Order order, CancellationToken cancellationToken = default)
    {
        await inner.ProcessAsync(order, cancellationToken);
        await audit.ProcessAsync(order, cancellationToken);
    }
}
