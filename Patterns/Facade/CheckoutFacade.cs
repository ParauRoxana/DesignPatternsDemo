namespace DesignPatternsDemo;

// Facade pattern: one simple API coordinates multiple subsystems.
public sealed class CheckoutFacade(
    IOrderRepository repository,
    IUnitOfWork unitOfWork,
    PaymentProcessorFactory paymentFactory,
    IShippingStrategy shipping,
    IOrderNotifier notifier,
    CheckoutPipeline pipeline)
{
    public async Task<Guid> PlaceOrderAsync(PlaceOrderCommand command)
    {
        var context = new CheckoutContext(command);
        pipeline.Run(context);

        var total = context.Subtotal + shipping.Calculate(context.Subtotal);
        var order = new Order(command.CustomerId, command.ProductId, command.Quantity, total);
        notifier.Subscribe(order);
        repository.Add(order);

        await paymentFactory.Create(command.PaymentMethod).ProcessAsync(order);
        await unitOfWork.CommitAsync();
        return order.Id;
    }
}
