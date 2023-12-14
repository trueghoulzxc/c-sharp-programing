using CSharpPrograming.Exceptions;

namespace CSharpPrograming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SuccessCase();
            Console.WriteLine("==========");
            ExceptionHandlingTest();
        }

        static void SuccessCase()
        {
            Directory.CreateDirectory(@"C:/queues");

            Console.WriteLine("Очередь char");
            Cqueue<char> charQ1 = ['a', 'b', 'c', 'd'];
            PrintCqueue(charQ1);
            SaveCqueue(charQ1, @"C:/queues/charQ.json");
            Cqueue<char> charQ2 = LoadCqueue<char>(@"C:/queues/charQ.json");
            PrintCqueue(charQ2);

            Console.WriteLine("\nОчередь MyDate");
            Cqueue<MyDate> dateQ1 = [new(11, 12, 2023), new(12, 12, 2023), new(13, 12, 2023)];
            PrintCqueue(dateQ1);
            SaveCqueue(dateQ1, @"C:/queues/dateQ.json");
            Cqueue<MyDate> dateQ2 = LoadCqueue<MyDate>(@"C:/queues/dateQ.json");
            PrintCqueue(dateQ2);
        }

        static void ExceptionHandlingTest()
        {
            Console.WriteLine("Сохранение в несуществующую директорию");
            Cqueue<char> charQ1 = ['a', 'b', 'c', 'd'];
            SaveCqueue(charQ1, @"C:/queues1/charQ.json");

            Console.WriteLine("Загрузка из несуществующего файла");
            _ = LoadCqueue<char>(@"C:/charQ.json");

            Console.WriteLine("Вызов загрузки без указания файла");
            _ = LoadCqueue<char>(null!);

            Console.WriteLine("Попытка загрузить очередь другого типа данных");
            _ = LoadCqueue<char>(@"C:/queues/dateQ.json");

            Console.WriteLine("Попытка загрузить очередь из файла с невалидным json");
            Cqueue<MyDate> charQ2 = LoadCqueue<MyDate>(@"C:/queues/dateQ с невалидными данными.json");
        }

        static void SaveCqueue<T>(Cqueue<T> q, string filePath)
        {
            string mainExceptionMessage = "Ошибка при сохранении очереди";

            try
            {
                q.SaveToFile(filePath);
                Console.WriteLine("Очередь успешно сохранена в файл " + filePath);
            }
            catch(DirectoryNotFoundException ex)
            {
                Console.WriteLine(mainExceptionMessage + ": Не найдена директория " + filePath);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(mainExceptionMessage + ": Не найден файл " + filePath);
            }
            catch (SaveCqueueException ex)
            {
                string? innerExceptionMessage = ex.InnerException?.Message;
                if (innerExceptionMessage != null)
                    innerExceptionMessage = ": " + innerExceptionMessage;

                Console.WriteLine(mainExceptionMessage + ": " + ex.Message + innerExceptionMessage);
            }
            catch (CqueueException ex)
            {
                Console.WriteLine(mainExceptionMessage + (ex.Message != null ? ex.Message : ": неизвестная ошибка при работе с очередью"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static Cqueue<T> LoadCqueue<T>(string filePath)
        {
            string mainExceptionMessage = "Ошибка при загрузке очереди";
            Cqueue<T> q = new();

            try
            {                
                q.LoadFromFile(filePath);
                Console.WriteLine("Очередь успешно загружена из файла " + filePath);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(mainExceptionMessage + ": Не найдена директория " + filePath);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(mainExceptionMessage + ": Не найден файл " + filePath);
            }
            catch (LoadCqueueException ex)
            {
                string? innerExceptionMessage = ex.InnerException?.Message;
                if (innerExceptionMessage != null)
                    innerExceptionMessage = ": " + innerExceptionMessage;

                Console.WriteLine(mainExceptionMessage + ": " + ex.Message + innerExceptionMessage);
            }
            catch (CqueueException ex)
            {
                Console.WriteLine(mainExceptionMessage + (ex.Message != null ? ex.Message : ": неизвестная ошибка при работе с очередью"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return q;
        }

        static void PrintCqueue<T>(Cqueue<T> queue)
        {
            Console.Write("Элементы очереди ");
            foreach (var item in queue)
            {
                Console.Write(item + "; ");
            }
            Console.WriteLine();
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

            Console.WriteLine("Получение еще одного элемента");
            T? item2 = -q1;
            Console.WriteLine("Получен элемент " + item2);
            PrintCqueue(q1);

            Console.WriteLine("Копирование одной очереди в другую с сортировкой в убывающем порядке");
            q1 = q1 < q2;
            PrintCqueue(q1);

            Console.WriteLine("Проверка пустая ли очередь: " + (bool)q1);
            Console.WriteLine("Мощность: " + (int)q1);
            Console.WriteLine("Максимальный элемент: " + q1.FindMax());

            Console.WriteLine("Делаем очередь пустой");
            int count = q1.Count();
            while (count > 0)
            {
                count--;
                Console.WriteLine(q1.Get());
            }
            Console.WriteLine("Проверка пустая ли очередь: " + (bool)q1);
        }

        static void TestFindMax<T>(Cqueue<T> q)
        {
            PrintCqueue(q);
            Console.WriteLine("Максимальный элемент: " + q.FindMax());
        }
    }
}
