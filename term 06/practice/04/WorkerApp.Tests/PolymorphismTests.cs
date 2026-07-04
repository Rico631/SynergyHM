using WorkerApp.Models;

namespace WorkerApp.Tests;

public class PolymorphismTests
{
    [Fact]
    public void AllWorkers_CanBeTreatedAsBaseClass()
    {
        List<Worker> workers = new()
        {
            new Worker("Иванов И.И.", 1980, "Инженер", 50_000m, 2010),
            new Teacher("Петров П.П.", 1970, 60_000m, 2000, "Каф. ИТ", "Профессор", 3),
            new Manager("Сидоров С.С.", 1975, 70_000m, 2005, 4),
            new Administrator("Кузнецова К.К.", 1985, 45_000m, 2015, "Бух.", "Полный"),
        };

        foreach (var w in workers)
        {
            Assert.True(w.CalculateSalary() > 0);
            Assert.False(string.IsNullOrEmpty(w.GetInfo()));
        }
    }
}