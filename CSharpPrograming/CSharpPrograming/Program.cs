namespace CSharpPrograming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Cqueue q1 = new Cqueue([1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
            Cqueue q2 = new Cqueue([20, 21, 22, 27, 26, 25, 23, 22, 21]);

            PrintCqueue(q1);


            Console.WriteLine("Добавление + 1");
            q1 = q1 + 1;
            PrintCqueue(q1);

            Console.WriteLine("Получение элемента");
            int? item = - q1;
            Console.WriteLine("Получен элемент " + item);
            PrintCqueue(q1);

            Console.WriteLine("Копирование одной очереди в другую с сортировкой в убывающем порядке");
            q1 = q1 < q2;
            PrintCqueue(q1);

            Console.WriteLine("Проверка пустая ли очередь: " + (bool)q1);
            Console.WriteLine("Мощность: " + (int)q1);
        }

        static void PrintCqueue(Cqueue queue)
        {
            Console.Write("Элементы очереди ");
            foreach (var item in queue)
            {
                Console.Write(item + "; ");
            }
            Console.Write("\n\n");
        }
    }
}
