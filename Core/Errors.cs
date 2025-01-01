namespace ConsoleAppTestProject.Core;

/// <summary>
/// Перечисление всех ошибок приложения
/// </summary>
public static class Errors
{
    public static class General
    {
        /// <summary>
        /// Общие ошибки приложения
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Error NotFound(string entityName, long id) =>
            new Error("record.not.found", $"'{entityName}' not found for Id '{id}'");

        public static Error ValueIsInvalid(string entityName) =>
            new Error("Значение ошибочно", $"'{entityName}' имеет некорректное значение");


        /* other general errors go here */
    }

    /*
    /// Ошибки класса Student
    public static class Student
    {
        public static Error EmailIsTaken(string email) =>
            new Error("student.email.is.taken", $"Student email '{email}' is taken");

        // other errors specific to students go here
    }

    // ...

    // можете создать еще больше подкатегорий, введя классы внутри Studentи General.

    */
}
