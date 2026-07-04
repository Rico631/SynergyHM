using WorkerApp.Models;

namespace WorkerApp.Tests;

public class ManagerTests
{
    [Fact]
    public void CalculateSalary_IncludesSubordinateBonus()
    {
        var m = new Manager("Сидоров С.С.", 1975, 70_000m, 2005, 4);
        Assert.Equal(70_000m + 4 * 1_500m, m.CalculateSalary());
    }
}