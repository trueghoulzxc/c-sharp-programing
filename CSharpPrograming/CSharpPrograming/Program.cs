namespace CSharpPrograming;

internal class Program
{
    static void Main(string[] args)
    {
        TestFloatContainer();
        Console.WriteLine();
        TestCustomDateContainer();
    }

    static void TestFloatContainer()
    {
        LinkedList<float> container = new();

        Console.WriteLine("Заполнение коллекции данными");
        container.AddFirst(0.5f);
        container.AddFirst(1.1f);
        container.AddFirst(2.2f);
        container.AddFirst(3.3f);
        container.AddFirst(4.4f);
        container.AddLast(5.5f);
        container.AddLast(6.6f);
        container.AddLast(7.7f);
        container.AddLast(8.8f);
        container.AddLast(9.9f);
        Print(container);

        Console.WriteLine("Удаление и изменение элементов");
        container.RemoveFirst();
        container.RemoveLast();
        container.First!.Value *= 2;
        container.Last!.Value /= 2;
        Print(container);

        Console.WriteLine("Удаление 3 элементов после 5,5");
        var node = container.Find(5.5f);
        int n = 3;
        while (n > 0)
        {
            n--;
            container.Remove(node!.Next!);
        }
        Print(container);

        Console.WriteLine("Сортировка контейнера по убыванию");
        IEnumerable<float> sortedItems = container.OrderByDescending(x => x);
        container = new();
        foreach (float item in sortedItems)
        {
            container.AddLast(item);
        }
        Print(container);
    }

    static void TestCustomDateContainer()
    {
        LinkedList<MyDate> container = new();

        Console.WriteLine("Заполнение коллекции данными");
        container.AddFirst(new MyDate(1, 1, 2020));
        container.AddFirst(new MyDate(2, 2, 2020));
        container.AddFirst(new MyDate(3, 3, 2020));
        container.AddFirst(new MyDate(4, 4, 2020));
        container.AddFirst(new MyDate(5, 5, 2020));
        container.AddFirst(new MyDate(6, 6, 2020));
        container.AddLast(new MyDate(7, 7, 2020));
        container.AddLast(new MyDate(8, 8, 2020));
        container.AddLast(new MyDate(9, 9, 2020));
        container.AddLast(new MyDate(10, 10, 2020));
        container.AddLast(new MyDate(11, 11, 2020));
        container.AddLast(new MyDate(12, 12, 2020));
        Print(container);

        Console.WriteLine("\nУдаление и изменение элементов");
        container.RemoveFirst();
        container.RemoveLast();
        container.First!.Value = new MyDate(01, 01, 2023);
        container.Last!.Value = new MyDate(31, 12, 2023);
        Print(container);

        Console.WriteLine("\nУдаление 3 элементов после 03.03.2020");
        var node = container.Find(new MyDate(3, 3, 2020));
        int n = 3;
        while (n > 0)
        {
            n--;
            container.Remove(node!.Next!);
        }
        Print(container);

        Console.WriteLine("\nСортировка контейнера по убыванию");
        IEnumerable<MyDate> sortedItems = container.OrderByDescending(x => x);
        container = new();
        foreach (MyDate item in sortedItems)
        {
            container.AddLast(item);
        }
        Print(container);

        Console.WriteLine("\nПоиск даты, где день больше 8");
        MyDate? res = container.FirstOrDefault(x => x.Day > 8);
        Console.WriteLine("Найденная дата: " + res);

        int count = container.Where(x => x.Day > 8).Count();
        Console.WriteLine("Всего элементов, удовлетворяющих этому условию: " + count);
    }

    static void Print<T>(LinkedList<T> container)
    {
        foreach (var item in container)
        {
            Console.Write(item + "; ");
        }

        Console.WriteLine();
    }
}
