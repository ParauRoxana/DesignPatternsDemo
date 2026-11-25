namespace DesignPatternsDemo;

public sealed record OrderPlacedEvent(Guid OrderId, string CustomerId, decimal Total);
