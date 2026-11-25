namespace DesignPatternsDemo;

public sealed class CustomerValidationStep : ICheckoutStep
{
    public void Execute(CheckoutContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Command.CustomerId))
            throw new ArgumentException("Customer is required.");
    }
}
