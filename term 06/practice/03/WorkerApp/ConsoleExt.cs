namespace WorkerApp;

/// <summary>
/// Методы расширения Console
/// </summary>
public class ConsoleExt
{

    /// <summary>
    /// Ввод непустой строки.
    /// </summary>
    public static string ReadString(string message)
    {
        while (true)
        {
            Console.Write(message);

            string? value = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            Console.WriteLine("Ошибка! Поле не может быть пустым.\n");
        }
    }

    /// <summary>
    /// Ввод целого числа.
    /// </summary>
    public static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }

            Console.WriteLine("Ошибка! Введите целое число.\n");
        }
    }

    /// <summary>
    /// Ввод положительного целого числа.
    /// </summary>
    public static int ReadPositiveInt(string message)
    {
        while (true)
        {
            int value = ReadInt(message);

            if (value > 0)
            {
                return value;
            }

            Console.WriteLine("Ошибка! Значение должно быть больше нуля.\n");
        }
    }

    /// <summary>
    /// Ввод десятичного числа.
    /// </summary>
    public static decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (decimal.TryParse(Console.ReadLine(), out decimal value))
            {
                return value;
            }

            Console.WriteLine("Ошибка! Введите корректное число.\n");
        }
    }

    /// <summary>
    /// Ввод зарплаты.
    /// </summary>
    public static decimal ReadSalary(string message)
    {
        while (true)
        {
            decimal salary = ReadDecimal(message);

            if (salary > 0)
            {
                return salary;
            }

            Console.WriteLine("Ошибка! Зарплата должна быть больше нуля.\n");
        }
    }

    /// <summary>
    /// Ввод года поступления на работу.
    /// </summary>
    public static int ReadHireYear(string message)
    {
        while (true)
        {
            int year = ReadInt(message);

            if (year >= 1950 && year <= DateTime.Now.Year)
            {
                return year;
            }

            Console.WriteLine(
                $"Ошибка! Год должен быть в диапазоне от 1950 до {DateTime.Now.Year}.\n");
        }
    }
}
