using CSharpPrograming.Exceptions;
using System.Collections;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace CSharpPrograming;

internal class Cqueue<T> : IEnumerable<T>
{
    private T?[] _items = new T?[16];
    private int _lastPosition = 0;
    private int _firstPosition = 0;

    public T? this[int index]
    {
        get { return _items[index]; }
        set { _items[index] = value; }
    }

    public Cqueue()
    {
        //Console.WriteLine("Вызван конструктор без параметров");
    }

    public Cqueue(IEnumerable<object> items)
    {
        //Console.WriteLine("Вызван конструктор с параметрами");
        
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        if (items is not IEnumerable<T>)
            throw new ArgumentException(nameof(items));

        var itemsT = items as IEnumerable<T>;

        foreach (var item in itemsT!)
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

        int startIdx = _firstPosition;
        int endIdx = _lastPosition;

        Array.Copy(_items, startIdx, newItems, 0, endIdx - startIdx);

        _firstPosition = 0;
        _lastPosition = endIdx - startIdx;

        return newItems;
    }

    public T? Get() 
    {
        if (_firstPosition == _lastPosition)
            throw new CqueueException("Очередь пуста");

        T? res = _items[_firstPosition];
        _items[_firstPosition] = default;
        _firstPosition++;
        return res;
    }

    public bool TryGet(out T? value)
    {
        if (_firstPosition == _lastPosition)
        {
            value = default;
            return false;
        }   

        value = _items[_firstPosition];
        _items[_firstPosition] = default;
        _firstPosition++;
        return true;
    }

    public T? FindMax()
    {
        return _items.Max();
    }

    public void SaveToFile(string filePath)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        using FileStream fileStream = new(filePath, FileMode.Create);

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };

        try
        {
            JsonSerializer.Serialize(fileStream, this.ToArray(), options);
        }
        catch (Exception ex)
        {
            throw new SaveCqueueException("Ошибка при сохранении очереди", ex);
        }
    }

    public void LoadFromFile(string filePath)
    {
        if (filePath == null)
            throw new ArgumentNullException(nameof(filePath));

        using FileStream fileStream = new(filePath, FileMode.Open);

        T?[]? newItems;
        try
        {
            newItems = JsonSerializer.Deserialize<T?[]>(fileStream);
        }
        catch (Exception ex)
        {
            throw new LoadCqueueException("Ошибка при загрузке очереди", ex);
        }

        if (newItems == null)
            throw new LoadCqueueException("Ошибка при десериализации очереди");

        _items = newItems;
        _firstPosition = 0;
        _lastPosition = newItems.Length;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _items.Skip(_firstPosition).SkipLast(_items.Length - _lastPosition).GetEnumerator()!;
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
        foreach (var item in queue2.OrderByDescending(x => x))
        {
            if (item != null)
                queue1.Add(item);
        }

        return queue1;
    }

    /// <summary>
    /// копирование одной очереди в другую с сортировкой в возрастающем порядке 
    /// </summary>
    public static Cqueue<T> operator >(Cqueue<T> queue1, Cqueue<T> queue2)
    {
        foreach (var item in queue2.OrderBy(x => x))
        {
            if (item != null)
                queue1.Add(item);
        }

        return queue1;
    }

    /// <summary>
    /// Проверка на пустоту очереди. Возвращает true если очередь пустая, иначе false
    /// </summary>
    public static explicit operator bool(Cqueue<T> queue)
    {
        return queue._firstPosition == queue._lastPosition;
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
