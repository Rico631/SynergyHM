namespace WorkerApp.Models;


/// <summary>
/// Абстрактный базовый класс, описывающий человека.
/// Инкапсулирует общие для любого человека данные: ФИО и год рождения.
/// </summary>
public abstract class Person
{
    private string _fullName = string.Empty;
    private int _birthYear;

    protected Person(string fullName, int birthYear)
    {
        FullName = fullName;
        BirthYear = birthYear;
    }

    /// <summary>
    /// Полное имя. Не может быть пустым или состоять из пробелов.
    /// </summary>
    /// <exception cref="ArgumentException">Если значение пустое.</exception>
    public string FullName
    {
        get => _fullName;
        set => _fullName = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("ФИО не может быть пустым.", nameof(value))
            : value.Trim();
    }

    /// <summary>
    /// Год рождения (1900..текущий).
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Если значение вне диапазона.</exception>
    public int BirthYear
    {
        get => _birthYear;
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Некорректный год рождения.");
            _birthYear = value;
        }
    }

    /// <summary>
    /// Полный возраст в годах.
    /// </summary>
    public int Age => DateTime.Now.Year - BirthYear;

    /// <summary>
    /// Возвращает краткое строковое описание.
    /// </summary>
    public abstract string GetInfo();
}