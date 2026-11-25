namespace DesignPatternsDemo;

public static class Program
{
    public static async Task Main()
    {
        // Composition root: Dependency Injection / Inversion of Control.
        var repository = new InMemoryOrderRepository();
        var unitOfWork = new InMemoryUnitOfWork();
        var paymentFactory = new PaymentProcessorFactory(new Dictionary<string, IPaymentProcessor>
        {
            ["card"] = new PaymentProcessorDecorator(
                new CardPaymentProcessor(),
                new AuditPaymentProcessor())
        });

        IShippingStrategy shipping = new ExpressShippingStrategy();
        IOrderNotifier notifier = new LoggingOrderNotifier(
            new EmailOrderNotifier());

        var checkout = new CheckoutFacade(
            repository,
            unitOfWork,
            paymentFactory,
            shipping,
            notifier,
            new CheckoutPipeline(new ICheckoutStep[]
            {
                new CustomerValidationStep(),
                new ProductAvailabilityStep()
            }));

        var mediator = new CheckoutMediator(checkout);
        var orderId = await mediator.SendAsync(new PlaceOrderCommand(
            CustomerId: "customer-42",
            ProductId: "keyboard-pro",
            Quantity: 2,
            PaymentMethod: "card"));

        Console.WriteLine($"Order {orderId} completed.\n");

        // Specification pattern: compose business rules into reusable predicates.
        var paidOrders = repository.Find(new OrderIsPaidSpecification());
        Console.WriteLine($"Paid orders found by specification: {paidOrders.Count}");
    }
}
