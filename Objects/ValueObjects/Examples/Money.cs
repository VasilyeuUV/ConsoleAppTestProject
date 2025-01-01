using ConsoleAppTestProject.Objects.ValueObjects;

namespace ConsoleAppTestProject.Models.ValueObjects.Examples;

public class Money : ValueObject
{
    public string Currency { get; }
    public decimal Amount { get; }

    public Money(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }

    /// <summary>
    /// Переопределение Equals() поведения по умолчанию.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency.ToUpper();        // реализовать сравнение без учета регистра для строки
        yield return Math.Round(Amount, 2);     // указать точность для типа float/double/decimal
    }
}
