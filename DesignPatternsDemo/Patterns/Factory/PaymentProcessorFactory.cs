namespace DesignPatternsDemo;

// Factory pattern: select an implementation based on a business choice.
public sealed class PaymentProcessorFactory(IReadOnlyDictionary<string, IPaymentProcessor> processors)
{
    public IPaymentProcessor Create(string method) =>
        processors.TryGetValue(method, out var processor)
            ? processor
            : throw new InvalidOperationException($"Unsupported payment method: {method}");
}
