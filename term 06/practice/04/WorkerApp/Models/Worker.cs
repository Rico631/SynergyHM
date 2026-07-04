namespace WorkerApp.Models;

/// <summary>
/// Сотрудник университета. Наследуется от <see cref="Person"/>.
/// Содержит данные о должности, зарплате и стаже.
/// Является базовым для специализированных ролей (Teacher, Manager, ...).
/// </summary>
public class Worker : Person
{
    private string _position = string.Empty;
    private decimal _salary;
    private int _hireYear;

    public Worker(string fullName, int birthYear, string position,
                  decimal salary, int hireYear)
        : base(fullName, birthYear)
    {
        Position = position;
        Salary = salary;
        HireYear = hireYear;
    }

    /// <summary>
    /// Должность.
    /// </summary>
    public string Position
    {
        get => _position;
        set => _position = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Должность не может быть пустой.", nameof(value))
            : value.Trim();
    }

    /// <summary>
    /// Базовая заработная плата (&gt;= 0).
    /// </summary>
    public decimal Salary
    {
        get => _salary;
        set => _salary = value < 0
            ? throw new ArgumentOutOfRangeException(nameof(value),
                "Зарплата не может быть отрицательной.")
            : value;
    }

    /// <summary>
    /// Год приёма на работу [1990..Now].
    /// </summary>
    public int HireYear
    {
        get => _hireYear;
        set
        {
            if (value < 1990 || value > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Некорректный год приёма на работу.");
            _hireYear = value;
        }
    }

    /// <summary>
    /// Стаж работы в годах (виртуальный — можно переопределить).
    /// </summary>
    public virtual int Experience => DateTime.Now.Year - HireYear;

    /// <summary>
    /// Итоговая зарплата с учётом надбавок (полиморфный метод).
    /// </summary>
    public virtual decimal CalculateSalary() => Salary;

    /// <inheritdoc/>
    public override string GetInfo() =>
        $"{FullName} | {Position} | стаж {Experience} лет | з/п {CalculateSalary():C}";

    /// <summary>
    /// Выводит информацию о сотруднике в консоль.
    /// </summary>
    public virtual void Display()
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"ФИО:        {FullName}");
        Console.WriteLine($"Возраст:    {Age}");
        Console.WriteLine($"Должность:  {Position}");
        Console.WriteLine($"Зарплата:   {CalculateSalary():C}");
        Console.WriteLine($"Год приёма: {HireYear}");
        Console.WriteLine($"Стаж:       {Experience} лет");
        Console.WriteLine("--------------------------------");
    }
}