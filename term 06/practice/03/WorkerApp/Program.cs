using WorkerApp;

List<Worker> workers = [];

Console.WriteLine("==========================================");
Console.WriteLine("   Учет сотрудников Университета");
Console.WriteLine("==========================================\n");

int count = ConsoleExt.ReadPositiveInt("Введите количество сотрудников: ");

for (int i = 0; i < count; i++)
{
    Console.WriteLine($"\nСотрудник №{i + 1}");

    string name = ConsoleExt.ReadString("Фамилия и инициалы: ");
    string position = ConsoleExt.ReadString("Должность: ");
    decimal salary = ConsoleExt.ReadSalary("Зарплата: ");
    int hireYear = ConsoleExt.ReadHireYear("Год поступления на работу: ");

    workers.Add(new Worker(name, position, salary, hireYear));
}

Console.WriteLine("\n==========================================");
Console.WriteLine("Список сотрудников");
Console.WriteLine("==========================================");

foreach (Worker worker in workers)
{
    worker.Display();
}

int experience = ConsoleExt.ReadPositiveInt(
    "\nВведите минимальный стаж работы (лет): ");

bool found = false;

Console.WriteLine($"\nСотрудники со стажем более {experience} лет:");

foreach (Worker worker in workers)
{
    if (worker.GetExperience() > experience)
    {
        Console.WriteLine(worker.GetName());
        found = true;
    }
}

if (!found)
{
    Console.WriteLine("Сотрудников с указанным стажем не найдено.");
}

Console.WriteLine("\nНажмите любую клавишу для завершения программы...");
Console.ReadKey();
        