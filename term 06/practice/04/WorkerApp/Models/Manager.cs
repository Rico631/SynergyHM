namespace WorkerApp.Models;

/// <summary>
/// Руководитель подразделения.
/// </summary>
public class Manager : Worker
{
    public Manager(string fullName, int birthYear, decimal salary, int hireYear,
                   int subordinatesCount)
        : base(fullName, birthYear, "Менеджер", salary, hireYear)
    {
        SubordinatesCount = subordinatesCount;
    }

    public int SubordinatesCount { get; set; }

    public override decimal CalculateSalary() => Salary + SubordinatesCount * 1_500m;

    public override void Display()
    {
        base.Display();
        Console.WriteLine($"Подчинённых: {SubordinatesCount}");
        Console.WriteLine("--------------------------------");
    }
}