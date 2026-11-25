namespace DesignPatternsDemo;

public sealed class LegacyBankGateway
{
    public bool Charge(string account, double amount) => amount > 0;
}
