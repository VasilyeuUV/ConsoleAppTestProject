Value Object — это концепция DDD, которая:
- нет идентификатора (т.е. это не сущность, ее следует сравнивать по значению, а не по Id);
- неизменна (если нужно изменить такой объект, создается новый экземпляр на основе существующего объекта, а не изменяется существующий);
- имеют нулевую (короткую) продолжительность жизни (жизненный цикл);
- не могут существовать сами по себе, без родительской сущности (они всегда должны принадлежать одной или нескольким сущностям);

Содержат логику и, как правило, не используются для передачи данных между границами приложения.

Это основа любой насыщенной модели предметной области.
Всегда отдавайте предпочтение объектам значений, а не сущностям. 

Подчиняется тем же правилам, что и Entity, кроме того, что сущность имеет идентификатор и, следовательно, жизненный цикл (историю изменений).




1. Сравнение объектов:
	object object1 = new object();
	object object2 = object1;
- равенство ссылок (два объекта считаются равными, если они ссылаются на один и тот же адрес в памяти):
	bool areEqual = object.ReferenceEquals(object1, object2);		// returns true
- равенство идентификаторов (класс имеет поле id, два экземпляра такого класса будут равны, если у них одинаковые идентификаторы):
- структурное равенство (два объекта равными, если все их члены совпадают).

Концепция равенства идентификаторов относится к сущностям, тогда как концепция структурного равенства — к объектам-значениям.


2. Сравнение ValueObject с Record

- record не реализуют IComparable интерфейс, а это значит, что следующий код не работает:
    var address1 = new Address("1234 Main St", "20012");
    var address2 = new Address("1235 Main St", "20012");
    Address[] addresses = new[] { address1, address2 }.OrderBy(x => x).ToArray();


- Инкапсуляция. Преимущество краткой записи record для доменного класса.
Инкапсуляция является важной частью любого класса домена.
Она подразумевает, что не должна имется возможность создать экземпляр объекта значения в недопустимом состоянии.
Следовательно конструктор не должен быть публичным, а для создания объекта должен использоваться фабричный метод.
    public record Address
    {
        public string Street { get; }
        public string ZipCode { get; }

        // C-tor is private
        private Address(string street, string zipCode)
        {
            Street = street;
            ZipCode = zipCode;
        }

        // Фабричный метод.
        public static Result<Address> Create(string street, string zipCode)
        {
            // Check street and zipCode validity
            // ...

            return Result.Success(new Address(street, zipCode));    // - возвращает результат только в том случае, если все проверки пройдены
        }
    }
При такой реализации,самое важное преимущество записей C#, их краткость, исчезает.
Для определения объектов-значений нельзя использовать однострочные записи.

В структуре нельзя скрыть конструктор без параметров


- Точный контроль проверок равенства
Версия с ValueObject базовым классом имеет дополнительный GetEqualityComponents метод,
в котором можно:
• задать проверки по конкретным свойствам, а не по всем, как в Record;
• исключить из проверки необязательные свойства. В Record единственный способ исключить свойство — переопределить все члены равенства в классе;
• настроить точность сравнения (например, количество знаков после запятой, или учет верхнего/нижнего регистра в строках);
• сравнивать коллекции. В Record сравнение не работает, если один из его членов не следует семантике сравнения по значению.
    например, для 
        public record Address(string Street, string ZipCode, string[] Comments);
    сравнение не работает:
        var address1 = new Address("1234 Main St", "20012", new [] { "Comment1", "Comment2" });
        var address2 = new Address("1234 Main St", "20012", new [] { "Comment1", "Comment2" });
        bool result = address1 == address2; // false
    т.к. в .NET коллекции сравниваются по ссылке - два string массива считаются разными, даже если их содержимое одинаково.


- Наличие базового класса.
Базовые классы ValueObject и Entity не только предоставляют полезность в виде полезных методов, 
но и служат маркерами для указания роли наследующего класса.


- проблема с with.
Эта функция работает аналогично Fluent Interface, позволяя создавать новые объекты на основе старых:
    Address address1 = new Address("1234 Main St", "20012");
    Address address2 = address1 with { Street = "1234 Second St" };
    bool result = address1.ZipCode == address2.ZipCode;                 // true
Она не работает в сценариях, где инкапсуляция является обязательным требованием.
В таких сценариях вам нужен сеттер, который, в свою очередь, позволяет обойти инварианты объекта значения. Это позволит создать экземпляр с обходом валидации.
with-функция хороша для создания тестовых данных, запросов и т. д. Они не подходят для доменных классов, которые обычно требуют инкапсуляции.


Т.о. у Record есть только одно преимущество перед подходом с ValueObject базовым классом: не нужно реализовывать GetEqualityComponentsметод. 
Это также является недостатком: отсутствие этого метода не позволяет настраивать поведение сравнения на равенство, что может быть важно в некоторых случаях.



3. Пример

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();


    public override bool Equals(object? obj)
    {
        if (obj == null
            || GetType() != obj.GetType())
        {
            return false;
        }

        ValueObject valueObject = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }


    /// <summary>
    /// Берет каждый компонент и использует его для построения результирующего хэш-кода.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
        => GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });

    public static bool operator ==(ValueObject a, ValueObject b)
        => a is null && b is null
            || (a is not null
                && b is not null
                && a.Equals(b));

    public static bool operator !=(ValueObject a, ValueObject b)
        => !(a == b);
}





// Entity
public class Person
{
    public int Id { get; set; }                     // - идентификатор определяет сущность
    public string Name { get; set; }
    public Address Address { get; set; }
}
 
// Value Object
public class Address
{
    public string Street { get; set; }
    public string ZipCode { get; set; }
}

=>
public class Address : ValueObject
{
    public string Street { get; }
    public string ZipCode { get; }

    public Address(string street, string zipCode)
    {
        Street = street;
        ZipCode = zipCode;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return ZipCode;
    }
}



Не вводите отдельные таблицы для объектов значений , просто встраивайте их в таблицу родительской сущности.