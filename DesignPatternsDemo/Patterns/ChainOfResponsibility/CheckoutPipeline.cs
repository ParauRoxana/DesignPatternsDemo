namespace DesignPatternsDemo;

// Chain of Responsibility: each step can stop the checkout pipeline.
public sealed class CheckoutPipeline(IEnumerable<ICheckoutStep> steps)
{
    public void Run(CheckoutContext context)
    {
        foreach (var step in steps)
            step.Execute(context);
    }
}
