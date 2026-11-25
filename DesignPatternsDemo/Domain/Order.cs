namespace DesignPatternsDemo;

public sealed class Order
{
    public Guid Id { get; } = Guid.NewGuid();
    public string CustomerId { get; }
    public string ProductId { get; }
    public int Quantity { get; }
    public decimal Total { get; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public event EventHandler<OrderPlacedEvent>? Placed;

    public Order(string customerId, string productId, int quantity, decimal total)
    {
        CustomerId = customerId;
        ProductId = productId;
        Quantity = quantity;
        Total = total;
    }

    public void MarkPaid()
    {
        Status = OrderStatus.Paid;
        Placed?.Invoke(this, new OrderPlacedEvent(Id, CustomerId, Total));
    }
}
