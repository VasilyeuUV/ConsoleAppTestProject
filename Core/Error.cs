using ConsoleAppTestProject.Objects.ValueObjects;

namespace ConsoleAppTestProject.Core;

/// <summary>
/// Класс для возврата ошибок.
/// </summary>
public sealed class Error : ValueObject
{
    public string Code { get; }
    public string Message { get; }

    internal Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
