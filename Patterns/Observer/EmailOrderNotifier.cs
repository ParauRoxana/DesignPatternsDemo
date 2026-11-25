namespace DesignPatternsDemo;

// Observer pattern: subscribers react to a domain event.
public sealed class EmailOrderNotifier : IOrderNotifier
{
    public void Subscribe(Order order) => order.Placed += OnOrderPlaced;

    private static void OnOrderPlaced(object? sender, OrderPlacedEvent e) =>
        Console.WriteLine($"Email notification sent for order {e.OrderId}.");
}
