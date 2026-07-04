namespace WorkerApp.Models;

/// <summary>
/// Административный сотрудник университета.
/// </summary>
public class Administrator : Worker
{
    public Administrator(string fullName, int birthYear, decimal salary, int hireYear,
                         string department, string accessLevel)
        : base(fullName, birthYear, "Администратор", salary, hireYear)
    {
        Department = department;
        AccessLevel = accessLevel;
    }

    public string Department { get; set; }
    public string AccessLevel { get; set; }

    public override decimal CalculateSalary() => Salary + 3_000m;

    public override void Display()
    {
        base.Display();
        Console.WriteLine($"Отдел:      {Department}");
        Console.WriteLine($"Уровень доступа: {AccessLevel}");
        Console.WriteLine("--------------------------------");
    }
}