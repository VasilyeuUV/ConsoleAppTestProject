using System.Text.RegularExpressions;
using ConsoleAppTestProject.Core;
using CSharpFunctionalExtensions;


namespace ConsoleAppTestProject.Objects.ValueObjects.Examples;

/// <summary>
/// ValueObject для номера телефона.
/// </summary>
public class PhoneNumber : ValueObject
{
    private const string phoneRegex = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

    /// <summary>
    /// Приватный Конструктор.
    /// </summary>
    /// <param name="number"></param>
    private PhoneNumber(string number)
    {
        Number = number;
    }

    /// <summary>
    /// Свойство, которое будет хранить номер телефона.
    /// </summary>
    public string Number { get; }


    /// <summary>
    /// Фабричный метод для создания экземпляра объекта.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Result<PhoneNumber, Error> Create(string input)              // классы из CSharpFunctionalExtensions
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Errors.General.ValueIsInvalid(nameof(PhoneNumber));
        }

        if (Regex.IsMatch(input, phoneRegex) == false)
        {
            return Errors.General.ValueIsInvalid(nameof(PhoneNumber));
        }

        return new PhoneNumber(input);
    }


    /// <summary>
    /// Условия сравненения экземпляров данного объектов.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;        // - значение, по которому будут сравниваться объекты. Т.е. те, по которым будет определяться уникальность ValueObject.
    }
}
