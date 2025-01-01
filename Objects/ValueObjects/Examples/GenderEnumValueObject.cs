
using ConsoleAppTestProject.Core;
using CSharpFunctionalExtensions;

namespace ConsoleAppTestProject.Objects.ValueObjects.Examples;


/// <summary>
/// ValueObject вместо Enum. Вариант замены.
/// </summary>
public class GenderEnumValueObject : ValueObject
{
    // Фиксированные значения как у enum-а.
    // Т.е. у этого класса могут быть только такие значения и никакие другие.
    public static readonly GenderEnumValueObject Male = new(nameof(Male));
    public static readonly GenderEnumValueObject Female = new(nameof(Female));

    public static readonly GenderEnumValueObject[] _all = [Male, Female];         // - список всех возможных значений.

    /// <summary>
    /// CTOR
    /// </summary>
    private GenderEnumValueObject(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Свойство, представляющее значение этого класса.
    /// </summary>
    public string Value { get; }


    /// <summary>
    /// Фабричный метод создания экземпляра класса.
    /// </summary>
    /// <returns></returns>
    public static Result<GenderEnumValueObject, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            // return Errors.General.ValueIsRequired();
        }

        string gender = input.Trim().ToLower();

        // - проверка на соответствие возможным значениям.
        if (_all.Any(g => g.Value.ToLower() == gender) == false)
        {
            return Errors.General.ValueIsInvalid(nameof(GenderEnumValueObject));
        }

        return new GenderEnumValueObject(input);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
       yield return Value;
    }
}
