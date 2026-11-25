namespace DesignPatternsDemo;

public interface IShippingStrategy
{
    decimal Calculate(decimal subtotal);
}
