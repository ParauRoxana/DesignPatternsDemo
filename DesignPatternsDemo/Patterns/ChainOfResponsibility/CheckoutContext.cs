namespace DesignPatternsDemo;

public sealed class CheckoutContext(PlaceOrderCommand command)
{
    public PlaceOrderCommand Command { get; } = command;
    public decimal Subtotal { get; set; }
}
