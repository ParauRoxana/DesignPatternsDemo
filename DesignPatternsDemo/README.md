# .NET Design Patterns Demo

This is a .NET 8 console application showing how several patterns can work together in a small checkout flow. The companion `DesignPatternsDemo.Tests` project contains xUnit unit tests for the core behaviors.

## Run

```powershell
dotnet run --project .\DesignPatternsDemo.csproj

dotnet test ..\DesignPatternsDemo.Tests\DesignPatternsDemo.Tests.csproj
```

The repository pins the .NET 8 SDK in `..\global.json` so the sample uses the same SDK version as its `net8.0` target.

## Patterns demonstrated

- Dependency Injection / Inversion of Control: composition root in `Program.cs`
- Command and Mediator: `PlaceOrderCommand`, `CheckoutMediator`
- Facade: `CheckoutFacade`
- Chain of Responsibility: `CheckoutPipeline` and checkout steps
- Factory: `PaymentProcessorFactory`
- Strategy: `IShippingStrategy`
- Decorator: payment audit and notification logging
- Adapter: `LegacyBankGatewayAdapter`
- Observer: `Order.Placed` event and email notifier
- Repository and Unit of Work: in-memory persistence abstractions
- Specification: `OrderIsPaidSpecification`

The implementations are intentionally small. Each type is kept in its own source file to make the pattern boundaries easy to explore. In a production application, these abstractions would normally also be split into Domain, Application, Infrastructure, and API projects.

## Folder layout

```text
Domain/
Patterns/
  Adapter/
  ChainOfResponsibility/
  Command/
  Decorator/
  Factory/
  Facade/
  Mediator/
  Observer/
  Repository/
  Specification/
  Strategy/
  UnitOfWork/
Program.cs
```
