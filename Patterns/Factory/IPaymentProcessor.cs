namespace DesignPatternsDemo;

public interface IPaymentProcessor
{
    Task ProcessAsync(Order order, CancellationToken cancellationToken = default);
}
