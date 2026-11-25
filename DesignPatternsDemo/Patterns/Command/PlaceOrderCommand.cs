namespace DesignPatternsDemo;

public sealed record PlaceOrderCommand(
    string CustomerId,
    string ProductId,
    int Quantity,
    string PaymentMethod);
