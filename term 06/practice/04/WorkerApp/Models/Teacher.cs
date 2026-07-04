namespace WorkerApp.Models;


/// <summary>
/// Преподаватель университета.
/// </summary>
public class Teacher : Worker
{
    private static readonly Dictionary<string, decimal> RankBonus = new()
    {
        ["Ассистент"] = 0m,
        ["Старший преподаватель"] = 5_000m,
        ["Доцент"] = 12_000m,
        ["Профессор"] = 25_000m,
    };

    public Teacher(string fullName, int birthYear, decimal salary, int hireYear,
                   string department, string academicRank, int subjectCount)
        : base(fullName, birthYear, "Преподаватель", salary, hireYear)
    {
        Department = department;
        AcademicRank = academicRank;
        SubjectCount = subjectCount;
    }

    public string Department { get; set; }
    public string AcademicRank { get; set; }
    public int SubjectCount { get; set; }

    /// <inheritdoc/>
    public override decimal CalculateSalary()
    {
        var bonus = RankBonus.TryGetValue(AcademicRank, out var b) ? b : 0m;
        return Salary + bonus + SubjectCount * 2_000m;
    }

    public override void Display()
    {
        base.Display();
        Console.WriteLine($"Кафедра:    {Department}");
        Console.WriteLine($"Звание:     {AcademicRank}");
        Console.WriteLine($"Дисциплин:  {SubjectCount}");
        Console.WriteLine("--------------------------------");
    }
}