// Пример реализации с https://github.com/vkhorikov/CSharpFunctionalExtensions

namespace ConsoleAppTestProject.Objects.ValueObjects;

/// <summary>
/// Абстрактный класс объекта-значения.
/// </summary>
public abstract class ValueObject
{
    private int? _cachedHashCode;


    protected abstract IEnumerable<object> GetEqualityComponents();             // Основной метод, который в классе-наследнике будет определять, как именно сравнивать. 


    /// <summary>
    /// Сравнить объекты по значению (описывает, как именно сравниваем).
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj == null
            //|| GetUnproxiedType(this) != GetUnproxiedType(obj))
            || GetType() != obj.GetType())
        {
            return false;
        }

        var valueObject = (ValueObject)obj;
        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());            // - сравнение каждого элемента одной коллекции с каждым элементом другой.
    }


    /// <summary>
    /// Получить хэш-код объекта.
    /// Берет каждый компонент и использует его для построения результирующего хэш-кода.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        //if (!_cachedHashCode.HasValue)
        //{
        //    _cachedHashCode = GetEqualityComponents()
        //        .Aggregate(1, (current, obj) =>
        //        {
        //            unchecked
        //            {
        //                return current * 23 + (obj?.GetHashCode() ?? 0);
        //            }
        //        });
        //}
        //return _cachedHashCode.Value;

        return GetEqualityComponents()
            .Aggregate(default(int), (hashCode, value) => HashCode.Combine(hashCode, value.GetHashCode()));     // комбинация хэш-кодов
    }


    public static bool operator ==(ValueObject a, ValueObject b)
        => a is null && b is null
            || a is not null
                && b is not null
                && a.Equals(b);


    public static bool operator !=(ValueObject a, ValueObject b)
        => !(a == b);


    //    internal static Type GetUnproxiedType(object obj)
    //    {
    //        const string EFCoreProxyPrefix = "Castle.Proxies.";
    //        const string NHibernateProxyPostfix = "Proxy";

    //        Type type = obj.GetType();
    //        string typeString = type.ToString();

    //        return typeString.Contains(EFCoreProxyPrefix)
    //            || typeString.EndsWith(NHibernateProxyPostfix)
    //            ? type.BaseType!
    //            : type;
    //    }
}
