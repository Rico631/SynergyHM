using WorkerApp.Extensions;
using WorkerApp.Models;

List<Worker> workers = [];

Console.WriteLine("==========================================");
Console.WriteLine("   Учёт сотрудников Университета");
Console.WriteLine("==========================================\n");

int count = ConsoleExt.ReadPositiveInt("Введите количество сотрудников: ");

for (int i = 0; i < count; i++)
{
    Console.WriteLine($"\n--- Сотрудник №{i + 1} ---");
    Console.WriteLine("Тип: 1 — Преподаватель, 2 — Администратор, 3 — Менеджер, 4 — Простой сотрудник");
    int type = ConsoleExt.ReadChoice(1, 4);

    string name = ConsoleExt.ReadString("Фамилия и инициалы: ");
    int birthYear = ConsoleExt.ReadBirthYear("Год рождения: ");
    decimal salary = ConsoleExt.ReadSalary("Базовая зарплата: ");
    int hireYear = ConsoleExt.ReadHireYear("Год приёма на работу: ");

    Worker worker = type switch
    {
        1 => new Teacher(name, birthYear, salary, hireYear,
                         ConsoleExt.ReadString("Кафедра: "),
                         ConsoleExt.ReadString("Звание (Ассистент/Старший преподаватель/Доцент/Профессор): "),
                         ConsoleExt.ReadPositiveInt("Количество дисциплин: ")),
        2 => new Administrator(name, birthYear, salary, hireYear,
                               ConsoleExt.ReadString("Отдел: "),
                               ConsoleExt.ReadString("Уровень доступа: ")),
        3 => new Manager(name, birthYear, salary, hireYear,
                         ConsoleExt.ReadPositiveInt("Количество подчинённых: ")),
        _ => new Worker(name, birthYear,
                        ConsoleExt.ReadString("Должность: "), salary, hireYear),
    };

    workers.Add(worker);
}

Console.WriteLine("\n==========================================");
Console.WriteLine("Список сотрудников (полиморфный вывод)");
Console.WriteLine("==========================================");

foreach (Worker w in workers) // полиморфизм: вызывается Display соответствующего подкласса
    w.Display();

int minExp = ConsoleExt.ReadPositiveInt("\nМинимальный стаж работы (лет): ");
bool found = false;

Console.WriteLine($"\nСотрудники со стажем более {minExp} лет:");
foreach (Worker w in workers)
{
    if (w.Experience > minExp)
    {
        Console.WriteLine($" • {w.GetInfo()}"); // полиморфный вызов
        found = true;
    }
}

if (!found) Console.WriteLine("Сотрудников с указанным стажем не найдено.");

Console.WriteLine("\nНажмите любую клавишу для выхода...");
Console.ReadKey();