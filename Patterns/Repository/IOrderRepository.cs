namespace DesignPatternsDemo;

public interface IOrderRepository
{
    void Add(Order order);
    Order? Get(Guid id);
    IReadOnlyList<Order> Find(ISpecification<Order> specification);
}
