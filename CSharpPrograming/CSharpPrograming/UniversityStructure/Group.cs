namespace CSharpPrograming.UniversityStructure;

internal abstract class Group
{
    private static Group? _first;
    private static Group? _last;
    private Group? _next;
    public required string GroupNumber { get; set; }
    public Student? GroupLeader { get; private set; }
    public required int CourseNumber { get; set; }
    public required Faculty Faculty { get; init; }
    public HashSet<Student> Students { get; } = new();
    public List<Subgroup> Subgroups { get; init; }

    public Group()
    {
        Subgroups = new List<Subgroup>(1);
        _ = new Subgroup() { SubgroupNumber = 1, Group = this };

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
        Group? current = _first;

        while (current != null)
        {
            Console.WriteLine(current.ToString());
            current = current._next;
        }
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