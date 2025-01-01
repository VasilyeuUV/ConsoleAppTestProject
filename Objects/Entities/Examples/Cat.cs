using ConsoleAppTestProject.Constants.Enums;
using ConsoleAppTestProject.Core;
using ConsoleAppTestProject.Objects.ValueObjects.Examples;
using CSharpFunctionalExtensions;

namespace ConsoleAppTestProject.Objects.Entities.Examples;


/// <summary>
/// Сущность кота.
/// </summary>
public class Cat // : Entity<Guid>
{
    /// <summary>
    /// Приватный конструктор.
    /// </summary>
    private Cat(
        Guid id,
        string name,
        //string phoneNumber,
        PhoneNumber phoneNumber,
        Age age,
        //string years,
        //string months,
        GenderEnumValueObject gender
        //GenderEnum gender
        // ...
        )
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        //Years = years;
        //Months = months;
        Gender = gender;
        // ...
    }

    public Guid Id { get; }
    public string Name { get; } = string.Empty;

    public PhoneNumber PhoneNumber { get; }         // - string в недостаточной степени описывает номер телефона, используем для него ValueObject
    //public string PhoneNumber { get; }            // - string в недостаточной степени описывает номер телефона, используем для него ValueObject

    public GenderEnumValueObject Gender { get; }
    //public GenderEnum Gender { get; }

    public Age Age { get; }
    //public string Years { get; }                  // Years и Months меняем на Age ValueObject
    //public string Months { get; }

    // ...


    /// <summary>
    /// Фабричный метод для создания экземпляра кота.
    /// </summary>
    /// <returns></returns>
    public static Result<Cat, Error> Create(
        Guid id,
        string name,
        //string phoneNumber,
        PhoneNumber phoneNumber,
        //string years,
        //string months,
        Age age,
        GenderEnumValueObject gender
        //GenderEnum gender
        // ...
        )
    {
        if (id == Guid.Empty)
        {
            // return Errors.General.ValueIsRequired();
        }

        // другие проверки

        return new Cat(
            id,
            name,
            phoneNumber,
            //years,
            //months,
            age,
            gender
            // ...
            );
    }

}
