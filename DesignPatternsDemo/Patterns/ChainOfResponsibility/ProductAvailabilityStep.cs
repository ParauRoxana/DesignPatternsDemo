namespace DesignPatternsDemo;

public sealed class ProductAvailabilityStep : ICheckoutStep
{
    public void Execute(CheckoutContext context)
    {
        if (context.Command.Quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");
        context.Subtotal = context.Command.Quantity * 75m;
    }
}
