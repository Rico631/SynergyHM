using WorkerApp.Models;

namespace WorkerApp.Tests;

public class AdministratorTests
{
    [Fact]
    public void CalculateSalary_IncludesFixedBonus()
    {
        var a = new Administrator("Кузнецова К.К.", 1985, 45_000m, 2015, "Бухгалтерия", "Полный");
        Assert.Equal(48_000m, a.CalculateSalary());
    }
}