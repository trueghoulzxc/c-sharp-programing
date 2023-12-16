using CSharpPrograming.UniversityStructure.Interfaces;

namespace CSharpPrograming.UniversityStructure;

internal abstract class Group : ICollectible
{
    private static Group? _first;
    private static Group? _last;
    private Group? _next;
    public required string GroupNumber { get; set; }
    public Student? GroupLeader { get; protected set; }
    public required int CourseNumber { get; set; }
    public Faculty Faculty { get; init; }
    public HashSet<Student> Students { get; } = new();
    public List<Subgroup> Subgroups { get; init; }

    public Group(Faculty faculty)
    {
        Subgroups = new List<Subgroup>(1);
        _ = new Subgroup() { SubgroupNumber = 1, Group = this };

        Faculty = faculty;
        faculty.Groups.Add(this);

        AddGroupToList(this);
    }

    private static void AddGroupToList(Group group)
    {
        if (_first == null)
        {
            _first = group;
            _last = group;
        }
        else
        {
            _last!._next = group;
            _last = group;
        }
    }

    public static void PrintAllObjects()
    {
        foreach (Group group in GetEnumerable())
        {
            Console.WriteLine(group.ToString());
        }
    }

    /// <summary>
    /// Возвращает итератор всех созданных групп
    /// </summary>
    private static IEnumerable<Group> GetEnumerable()
    {
        Group? current = _first;

        while (current != null)
        {
            yield return current;
            current = current._next;
        }
    }

    /// <summary>
    /// Возвращает количество групп заданного курса
    /// </summary>
    /// <param name="course">Номер курса</param>
    public static int GetGroupsCount(int course)
    {
        return GetEnumerable().Where(x => x.CourseNumber == course).Count();
    }

    /// <summary>
    /// Возвращает итератор старост всех групп определенного факультета
    /// </summary>
    /// <param name="faculty">Факультет</param>
    public static IEnumerable<Student?> GetGroupLeaders(Faculty faculty)
    {
        return GetEnumerable().Where(x => x.Faculty == faculty).Select(x => x.GroupLeader);
    }

    public virtual bool TrySetGroupLeader(Student? student = null)
    {
        if (student == null)
        {
            int index = new Random().Next(0, Students.Count - 1);
            GroupLeader = Students.ElementAt(index);
            return true;
        }

        if (Students.Contains(student))
        {
            GroupLeader = student;
            return true;
        }

        return false;
    }

    public virtual void PrintStudents()
    {
        Console.Write($"{ToString()}: староста {GroupLeader?.Name} (подгруппа {GroupLeader?.Subgroup.SubgroupNumber}); ");
        
        foreach (var student in Students.Except([GroupLeader]))
        {
            Console.Write($"{student.Name} (подгруппа {student.Subgroup.SubgroupNumber}); ");
        }
        
        Console.WriteLine();
    }

    public virtual bool TryExpelStudent(Student student)
    {
        if (Students.Remove(student))
        {
            if (GroupLeader == student) 
                GroupLeader = null;

            return true;
        }

        return false;
    }

    public virtual bool CanAcceptStudent(Student student)
    {
        return !Students.Contains(student);
    }

    public virtual bool TryAcceptStudent(Student student)
    {
        if (Students.Add(student))
        {
            if (!Subgroups.Contains(student.Subgroup))
                student.Subgroup = this.Subgroups.First();
            
            return true;
        }

        return false;
    }

    public virtual void DistributeStudentsInSubgroups()
    {
        int count = Subgroups.Count;

        if (count < 2)
            return;

        for (int i = 0, j = 0; i < Students.Count; i++)
        {
            Students.ElementAt(i).Subgroup = Subgroups[j];

            if (j + 1 == count)
                j = 0;
            else
                j++;
        }
    }

    public override string ToString() => $"Группа {GroupNumber}";
}