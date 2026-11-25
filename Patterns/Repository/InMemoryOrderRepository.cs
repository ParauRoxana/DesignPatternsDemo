namespace DesignPatternsDemo;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> orders = [];

    public void Add(Order order) => orders.Add(order);

    public Order? Get(Guid id) => orders.SingleOrDefault(x => x.Id == id);

    public IReadOnlyList<Order> Find(ISpecification<Order> specification) =>
        orders.Where(specification.IsSatisfiedBy).ToList();
}
