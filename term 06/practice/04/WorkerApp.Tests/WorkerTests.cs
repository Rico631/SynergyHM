using WorkerApp.Models;

namespace WorkerApp.Tests;


public class WorkerTests
{
    [Fact]
    public void Constructor_ValidArguments_SetsProperties()
    {
        var w = new Worker("Иванов И.И.", 1980, "Инженер", 50_000m, 2010);
        Assert.Equal("Иванов И.И.", w.FullName);
        Assert.Equal("Инженер", w.Position);
        Assert.Equal(50_000m, w.Salary);
        Assert.Equal(2010, w.HireYear);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void FullName_Invalid_Throws(string name) =>
        Assert.Throws<ArgumentException>(() => new Worker(name, 1980, "x", 100, 2010));

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void Salary_Negative_Throws(decimal s) =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Worker("Иванов И.И.", 1980, "x", s, 2010));

    [Theory]
    [InlineData(1899)]
    [InlineData(2100)]
    public void HireYear_OutOfRange_Throws(int year) =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Worker("Иванов И.И.", 1980, "x", 100, year));

    [Fact]
    public void Experience_CalculatedCorrectly()
    {
        var w = new Worker("Иванов И.И.", 1980, "Инженер", 50_000m, 2010);
        Assert.Equal(DateTime.Now.Year - 2010, w.Experience);
    }

    [Fact]
    public void CalculateSalary_Base_EqualsSalary() =>
        Assert.Equal(50_000m,
            new Worker("И.", 1980, "И.", 50_000m, 2010).CalculateSalary());
}