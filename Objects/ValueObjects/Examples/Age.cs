
using ConsoleAppTestProject.Core;
using CSharpFunctionalExtensions;

namespace ConsoleAppTestProject.Objects.ValueObjects.Examples;

public class Age : ValueObject
{
    /// <summary>
    /// CTOR
    /// </summary>
    private Age(int year, int months)
    {
        Years = year;
        Months = months;
    }


    public int Years { get; }
    public int Months { get; }


    /// <summary>
    /// Фабричный метод создания экземпляра объекта.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="months"></param>
    /// <returns></returns>
    public static Result<Age, Error> Create(int year, int months)
    {
        if (year < 0)
        {
            //return Errors.General.InvalidLength(nameof(Years));
        }

        if (months is < 0 or > 12)
        {
            //return Errors.General.InvalidLength(nameof(Months));
        }

        return new Age(year, months);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Years;             // свойства, по которым будут сравниваться объекты.
        yield return Months;
    }
}
