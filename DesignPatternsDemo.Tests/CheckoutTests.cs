using DesignPatternsDemo;
namespace DesignPatternsDemo.Tests;

public sealed class CheckoutTests
{
    [Theory]
    [InlineData(100, 0)]
    [InlineData(99, 15)]
    public void Express_shipping_applies_free_shipping_at_the_threshold(
        decimal subtotal,
        decimal expectedShipping)
    {
        var strategy = new ExpressShippingStrategy();


        var actual = strategy.Calculate(subtotal);

        Assert.Equal(expectedShipping, actual);
    }

    [Fact]
    public void Payment_factory_rejects_unknown_payment_methods()
    {
        var factory = new PaymentProcessorFactory(
            new Dictionary<string, IPaymentProcessor>());

        var exception = Assert.Throws<InvalidOperationException>(
            () => factory.Create("crypto"));

        Assert.Contains("Unsupported payment method", exception.Message);
    }

    [Fact]
    public void Checkout_pipeline_rejects_non_positive_quantities()
    {
        var pipeline = new CheckoutPipeline([new ProductAvailabilityStep()]);
        var context = new CheckoutContext(new PlaceOrderCommand("customer", "product", 0, "card"));

        Assert.Throws<ArgumentException>(() => pipeline.Run(context));
    }

    [Fact]
    public async Task Checkout_facade_persists_a_paid_order_and_commits()
    {
        var repository = new InMemoryOrderRepository();
        var unitOfWork = new SpyUnitOfWork();
        var notifier = new SpyNotifier();
        var paymentFactory = new PaymentProcessorFactory(
            new Dictionary<string, IPaymentProcessor>
            {
                ["card"] = new CardPaymentProcessor()
            });
        var facade = new CheckoutFacade(
            repository,
            unitOfWork,
            paymentFactory,
            new ExpressShippingStrategy(),
            notifier,
            new CheckoutPipeline([
                new CustomerValidationStep(),
                new ProductAvailabilityStep()
            ]));

        var id = await facade.PlaceOrderAsync(
            new PlaceOrderCommand("customer", "keyboard", 2, "card"));

        var order = repository.Get(id);
        Assert.NotNull(order);
        Assert.Equal(OrderStatus.Paid, order!.Status);
        Assert.Equal(150m, order.Total);
        Assert.Same(order, notifier.SubscribedOrder);
        Assert.Equal(1, unitOfWork.CommitCount);
    }

    [Fact]
    public void Paid_order_specification_matches_only_paid_orders()
    {
        var pending = new Order("customer", "product", 1, 75);
        var paid = new Order("customer", "product", 1, 75);
        paid.MarkPaid();
        var repository = new InMemoryOrderRepository();
        repository.Add(pending);
        repository.Add(paid);

        var result = repository.Find(new OrderIsPaidSpecification());

        Assert.Single(result);
        Assert.Same(paid, result[0]);
    }

    [Fact]
    public async Task Adapter_translates_legacy_gateway_and_marks_order_paid()
    {
        var order = new Order("customer", "product", 1, 75);
        var adapter = new LegacyBankGatewayAdapter(new LegacyBankGateway());

        await adapter.ProcessAsync(order);

        Assert.Equal(OrderStatus.Paid, order.Status);
    }

    [Fact]
    public async Task Decorator_runs_inner_processor_then_audit_processor()
    {
        var calls = new List<string>();
        var decorator = new PaymentProcessorDecorator(
            new RecordingProcessor("inner", calls),
            new RecordingProcessor("audit", calls));

        await decorator.ProcessAsync(new Order("customer", "product", 1, 75));

        Assert.Equal(["inner", "audit"], calls);
    }

    private sealed class SpyUnitOfWork : IUnitOfWork
    {
        public int CommitCount { get; private set; }
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            CommitCount++;
            return Task.CompletedTask;
        }
    }

    private sealed class SpyNotifier : IOrderNotifier
    {
        public Order? SubscribedOrder { get; private set; }
        public void Subscribe(Order order) => SubscribedOrder = order;
    }

    private sealed class RecordingProcessor(string name, List<string> calls) : IPaymentProcessor
    {
        public Task ProcessAsync(Order order, CancellationToken cancellationToken = default)
        {
            calls.Add(name);
            return Task.CompletedTask;
        }
    }
}
