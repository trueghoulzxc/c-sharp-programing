using System.Reflection;

namespace CSharpPrograming;

internal class MyDate
{
    private int _day;
    public int Day
    {
        get => _day;
        set
        {
            if (1 <= value && value <= 31)
                _day = value;
        }
    }

    private int _month;
    public int Month
    {
        get => _month;
        set
        {
            if (1 <= value && value <= 12)
                _month = value;
        }
    }

    private int _year;
    public int Year
    {
        get => _year;
        set
        {
            if (value >= 1)
                _year = value;
        }
    }

    private Dictionary<int, string> _monthNames = new Dictionary<int, string>
    {
        { 1, "января" },
        { 2, "февраля" },
        { 3, "марта" },
        { 4, "апреля" },
        { 5, "мая" },
        { 6, "июня" },
        { 7, "июля" },
        { 8, "августа" },
        { 9, "сентября" },
        { 10, "октября" },
        { 11, "ноября" },
        { 12, "декабря" }
    };

    public MyDate()
    {
        Console.WriteLine("Вызван конструктор без параметров");

        Day = 1;
        Month = 1;
        Year = 1;
    }

    public MyDate(int day, int month, int year)
    {
        Console.WriteLine("Вызван конструктор с параметрами");

        Day = day;
        Month = month;
        Year = year;
    }

    public MyDate(MyDate date)
    {
        Console.WriteLine("Вызван конструктор копирования");

        Day = date.Day;
        Month = date.Month;
        Year = date.Year;
    }

    public void Print()
    {
        string res = "";

        if (Day / 10 == 0)
            res += $"0{Day}";
        else
            res += $"{Day}";

        if (Month / 10 == 0)
            res += $".0{Month}.";
        else
            res += $".{Month}.";

        string yearStr = Year.ToString();
        
        while (yearStr.Length < 4)
            yearStr = "0" + yearStr;

        Console.WriteLine(res + yearStr);
    }

    public void PrintWithMonthName() => Console.WriteLine($"{Day} {_monthNames[Month]} {Year} года");

    ~MyDate()
    {
        Console.WriteLine("Вызван деструктор");
    }
}
