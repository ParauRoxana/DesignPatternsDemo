namespace DesignPatternsDemo;

// Mediator + Command patterns: caller knows only the request and mediator.
public sealed class CheckoutMediator(CheckoutFacade checkout)
{
    public Task<Guid> SendAsync(PlaceOrderCommand command) => checkout.PlaceOrderAsync(command);
}
