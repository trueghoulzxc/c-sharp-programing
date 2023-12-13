namespace CSharpPrograming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Очередь char");
            Cqueue<char> charQ1 = ['a', 'b', 'c', 'd', 'e', 'f'];
            Cqueue<char> charQ2 = ['q', 'r', 's', 't', 'u', 'v'];
            TestQueue(charQ1, charQ2, 'z');

            Console.WriteLine("\nОчередь MyDate");
            Cqueue<MyDate> dateQ1 = [new(11, 12, 2023), new(12, 12, 2023), new(13, 12, 2023)]; 
            Cqueue<MyDate> dateQ2 = [new(11, 01, 2023), new(12, 01, 2023), new(13, 01, 2023)];
            TestQueue(dateQ1, dateQ2, new(31, 12, 2023));

            Console.WriteLine();
            TestFindMax(new Cqueue<int> { 1, 3, 4, 2, 6, 2 });
            Console.WriteLine();
            TestFindMax(new Cqueue<double> { 1.6, 3.22, 4.20, 8.8, 2.28 });
        }

        static void PrintCqueue<T>(Cqueue<T> queue)
        {
            Console.Write("Элементы очереди ");
            foreach (var item in queue)
            {
                Console.Write(item + "; ");
            }
            Console.Write("\n\n");
        }

        static void TestQueue<T>(Cqueue<T> q1, Cqueue<T> q2, T newItem)
        {
            PrintCqueue(q1);

            Console.WriteLine("Добавление элемента через + ");
            q1 = q1 + newItem;
            PrintCqueue(q1);

            Console.WriteLine("Получение элемента");
            T? item = - q1;
            Console.WriteLine("Получен элемент " + item);
            PrintCqueue(q1);

            Console.WriteLine("Копирование одной очереди в другую с сортировкой в убывающем порядке");
            q1 = q1 < q2;
            PrintCqueue(q1);

            Console.WriteLine("Проверка пустая ли очередь: " + (bool)q1);
            Console.WriteLine("Мощность: " + (int)q1);
            Console.WriteLine("Максимальный элемент: " + q1.FindMax());
        }

        static void TestFindMax<T>(Cqueue<T> q)
        {
            PrintCqueue(q);
            Console.WriteLine("Максимальный элемент: " + q.FindMax());
        }
    }
}
