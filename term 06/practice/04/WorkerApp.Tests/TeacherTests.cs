using WorkerApp.Models;

namespace WorkerApp.Tests;

public class TeacherTests
{
    [Fact]
    public void CalculateSalary_Professor_IncludesAllBonuses()
    {
        var t = new Teacher("Петров П.П.", 1970, 60_000m, 2000, "Каф. ИТ", "Профессор", 3);
        // 60000 + 25000 + 3*2000 = 91000
        Assert.Equal(91_000m, t.CalculateSalary());
    }

    [Fact]
    public void Display_DoesNotThrow()
    {
        var t = new Teacher("Петров П.П.", 1970, 60_000m, 2000, "Каф. ИТ", "Доцент", 2);
        Assert.Null(Record.Exception(() => t.Display()));
    }
}
