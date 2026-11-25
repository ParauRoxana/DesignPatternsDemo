namespace DesignPatternsDemo;

public sealed class OrderIsPaidSpecification : ISpecification<Order>
{
    public bool IsSatisfiedBy(Order order) => order.Status == OrderStatus.Paid;
}
