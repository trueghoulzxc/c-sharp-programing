using System.Collections;
using System.Collections.Immutable;

namespace CSharpPrograming;

internal class Cqueue : IEnumerable
{
    private int?[] _items = new int?[16];
    private int _lastPosition = 0;

    public Cqueue()
    {
        Console.WriteLine("Вызван конструктор без параметров");
    }

    public Cqueue(IEnumerable<int> items)
    {
        Console.WriteLine("Вызван конструктор с параметрами");
        
        foreach (var item in items)
        {
            this.Add(item);
        }
    }

    public void Add(int newItem)
    {
        if (_lastPosition + 1 >= _items.Length)
        {
            _items = IncreaseCapacity();
        }

        _items[_lastPosition] = newItem;
        _lastPosition++;
    }

    private int?[] IncreaseCapacity()
    {
        int?[] newItems = new int?[_items.Length * 2];

        int startIdx = GetFirstNotNullIndex();
        int endIdx = _lastPosition;

        Array.Copy(_items, startIdx, newItems, 0, endIdx - startIdx);
        return newItems;
    }

    public int? Get() 
    {
        int notNullIdx = GetFirstNotNullIndex();
        
        if (notNullIdx == -1)
            return null;

        int? res = _items[notNullIdx];
        _items[notNullIdx] = null;
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

    public IEnumerator GetEnumerator()
    {
        return _items.Where(x => x != null).GetEnumerator();
    }

    public static Cqueue operator +(Cqueue queue, int newItem)
    {
        queue.Add(newItem);
        return queue;
    }

    public static int? operator -(Cqueue queue)
    {
        return queue.Get();
    }

    /// <summary>
    /// копирование одной очереди в другую с сортировкой в убывающем порядке
    /// </summary>
    public static Cqueue operator <(Cqueue queue1, Cqueue queue2)
    {
        foreach (var item in queue2._items.OrderByDescending(x => x))
        {
            if (item != null)
                queue1.Add((int)item);
        }

        return queue1;
    }

    public static Cqueue operator >(Cqueue queue1, Cqueue queue2)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Проверка на пустоту очереди. Возвращает true если очередь пустая, иначе false
    /// </summary>
    public static explicit operator bool(Cqueue queue)
    {
        return queue.GetFirstNotNullIndex() == -1;
    }

    /// <summary>
    /// Мощность (количество уникальных элементов)
    /// </summary>
    public static explicit operator int(Cqueue queue)
    {
        return queue._items.Where(x => x != null).Distinct().Count();
    }

    ~Cqueue()
    {
        Console.WriteLine("Вызван деструктор");
    }
}
