namespace WorkerApp;

public class Worker
{
    // Поля класса
    private string fullName;
    private string position;
    private decimal salary;
    private int hireYear;

    // Конструктор по умолчанию
    public Worker()
    {
        fullName = "Не задано";
        position = "Не задано";
        salary = 0;
        hireYear = DateTime.Now.Year;
    }

    // Конструктор с двумя параметрами
    public Worker(string fullName, string position)
    {
        this.fullName = fullName;
        this.position = position;
        salary = 0;
        hireYear = DateTime.Now.Year;
    }

    // Конструктор со всеми параметрами
    public Worker(string fullName, string position, decimal salary, int hireYear)
    {
        this.fullName = fullName;
        this.position = position;
        this.salary = salary;
        this.hireYear = hireYear;
    }

    // Деструктор
    ~Worker()
    {
        Console.WriteLine($"Объект {fullName} уничтожен.");
    }

    // Изменение данных
    public void Edit(string fullName, string position, decimal salary, int hireYear)
    {
        this.fullName = fullName;
        this.position = position;
        this.salary = salary;
        this.hireYear = hireYear;
    }

    // Вывод информации
    public void Display()
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"ФИО:        {fullName}");
        Console.WriteLine($"Должность:  {position}");
        Console.WriteLine($"Зарплата:   {salary:C}");
        Console.WriteLine($"Год приема: {hireYear}");
        Console.WriteLine($"Стаж:       {GetExperience()} лет");
        Console.WriteLine("--------------------------------");
    }

    // Получение стажа
    public int GetExperience()
    {
        return DateTime.Now.Year - hireYear;
    }

    // Получение ФИО
    public string GetName()
    {
        return fullName;
    }
}