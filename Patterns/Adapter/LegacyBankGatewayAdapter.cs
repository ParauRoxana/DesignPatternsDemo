namespace DesignPatternsDemo;

// Adapter pattern: translate a legacy API into the application abstraction.
public sealed class LegacyBankGatewayAdapter(LegacyBankGateway gateway) : IPaymentProcessor
{
    public Task ProcessAsync(Order order, CancellationToken cancellationToken = default)
    {
        if (!gateway.Charge("customer-account", (double)order.Total))
            throw new InvalidOperationException("Legacy bank declined the payment.");
        order.MarkPaid();
        return Task.CompletedTask;
    }
}
