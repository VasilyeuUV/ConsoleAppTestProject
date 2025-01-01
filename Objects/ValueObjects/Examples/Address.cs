using ConsoleAppTestProject.Objects.ValueObjects;

namespace ConsoleAppTestProject.Models.ValueObjects.Examples;

public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }

    /// <summary>
    /// Список арендаторов
    /// </summary>
    public List<Tenant> Tenants { get; }

    public Address(string street, string city, string zipCode, List<Tenant> tenants)
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
        Tenants = tenants;
    }

    /// <summary>
    /// Для стравнения сущностей
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        // - условия для сравнения можно удалять, или добавлять по мере появления новых свойств.
        yield return Street;
        yield return City;
        yield return ZipCode;

        foreach (Tenant tenant in Tenants)
        {
            yield return tenant;
        }
    }
}
