
using ConsoleAppTestProject.Objects.ValueObjects.Examples;

namespace ConsoleAppTestProject;

internal class Program
{
    static void Main(string[] args)
    {
        DoValueObjectWorkExample();
    }



    /// <summary>
    /// Пример работы с ValueObject.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private static void DoValueObjectWorkExample()
    {
        var age1 = Age.Create(1, 1).Value;
        var age2 = Age.Create(1, 1).Value;

        Console.WriteLine(age1 == age2);
    }
}
