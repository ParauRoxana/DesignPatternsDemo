namespace DesignPatternsDemo;

public interface ICheckoutStep
{
    void Execute(CheckoutContext context);
}
