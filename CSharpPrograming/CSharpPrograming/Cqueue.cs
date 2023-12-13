using System.Collections;

namespace CSharpPrograming;

internal class Cqueue<T> : IEnumerable<T>
{
    private T?[] _items = new T?[16];
    private int _lastPosition = 0;

    public Cqueue()
    {
        //Console.WriteLine("Вызван конструктор без параметров");
    }

    public Cqueue(IEnumerable<T> items)
    {
        //Console.WriteLine("Вызван конструктор с параметрами");
        
        foreach (var item in items)
        {
            this.Add(item);
        }
    }

    public void Add(T newItem)
    {
        if (_lastPosition + 1 >= _items.Length)
        {
            _items = IncreaseCapacity();
        }

        _items[_lastPosition] = newItem;
        _lastPosition++;
    }

    private T?[] IncreaseCapacity()
    {
        T?[] newItems = new T?[_items.Length * 2];

        int startIdx = GetFirstNotNullIndex();
        int endIdx = _lastPosition;

        Array.Copy(_items, startIdx, newItems, 0, endIdx - startIdx);
        return newItems;
    }

    public T? Get() 
    {
        int notNullIdx = GetFirstNotNullIndex();
        
        if (notNullIdx == -1)
            return default(T?);

        T? res = _items[notNullIdx];
        _items[notNullIdx] = default(T?);
        return res;
    }

    private int GetFirstNotNullIndex()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] != null)
                return i;
        }

        return -1;
    }

    public T? FindMax()
    {
        return _items.Max();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _items.Where(x => x != null && !x.Equals(default(T?))).GetEnumerator()!;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static Cqueue<T> operator +(Cqueue<T> queue, T newItem)
    {
        queue.Add(newItem);
        return queue;
    }

    public static T? operator -(Cqueue<T> queue)
    {
        return queue.Get();
    }

    /// <summary>
    /// копирование одной очереди в другую с сортировкой в убывающем порядке
    /// </summary>
    public static Cqueue<T> operator <(Cqueue<T> queue1, Cqueue<T> queue2)
    {
        foreach (var item in queue2._items.OrderByDescending(x => x))
        {
            if (item != null)
                queue1.Add(item);
        }

        return queue1;
    }

    public static Cqueue<T> operator >(Cqueue<T> queue1, Cqueue<T> queue2)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Проверка на пустоту очереди. Возвращает true если очередь пустая, иначе false
    /// </summary>
    public static explicit operator bool(Cqueue<T> queue)
    {
        return queue.GetFirstNotNullIndex() == -1;
    }

    /// <summary>
    /// Мощность (количество уникальных элементов)
    /// </summary>
    public static explicit operator int(Cqueue<T> queue)
    {
        return queue._items.Where(x => x != null).Distinct().Count();
    }

    ~Cqueue()
    {
        Console.WriteLine("Вызван деструктор");
    }
}
