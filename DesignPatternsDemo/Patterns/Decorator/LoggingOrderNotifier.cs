namespace DesignPatternsDemo;

// Decorator pattern applied to notifications.
public sealed class LoggingOrderNotifier(IOrderNotifier inner) : IOrderNotifier
{
    public void Subscribe(Order order)
    {
        Console.WriteLine($"Notification subscriber attached to order {order.Id}.");
        inner.Subscribe(order);
    }
}
