namespace CSharpPrograming;

internal class Program
{
    static void Main(string[] args)
    {
        TestDates();

        //явно вызываем сборку мусора для демонстрации работы деструктора
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    static void TestDates()
    {
        MyDate date1 = new();
        Console.Write("date1: ");
        date1.Print();

        MyDate date2 = new(11, 12, 2023);
        Console.Write("date2: ");
        date2.PrintWithMonthName();

        MyDate date3 = new(date2);
        Console.Write("date3: ");
        date3.Print();

        date2.Day = 31;
        Console.WriteLine(date2.Day);

        date2.Day = 32;
        Console.WriteLine(date2.Day);
    }
}
