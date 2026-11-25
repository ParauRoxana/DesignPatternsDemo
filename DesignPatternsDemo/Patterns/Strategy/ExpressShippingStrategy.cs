namespace DesignPatternsDemo;

public sealed class ExpressShippingStrategy : IShippingStrategy
{
    public decimal Calculate(decimal subtotal) => subtotal >= 100 ? 0 : 15;
}
