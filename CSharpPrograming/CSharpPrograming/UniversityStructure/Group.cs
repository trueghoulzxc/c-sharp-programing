namespace CSharpPrograming.UniversityStructure;

internal class Group
{
    public required string GroupNumber { get; set; }
    public string? GroupLeader { get; private set; }
    public required int CourseNumber { get; set; }
    public required Faculty Faculty { get; init; }
    public HashSet<string> Students { get; } = new HashSet<string>();

    public virtual bool TrySetGroupLeader(string? name = null)
    {
        if (name == null)
        {
            int index = new Random().Next(0, Students.Count - 1);
            GroupLeader = Students.ElementAt(index);
            return true;
        }

        if (Students.Contains(name))
        {
            GroupLeader = name;
            return true;
        }

        return false;
    }

    public void PrintStudents()
    {
        Console.Write($"{ToString()}: староста {GroupLeader}; ");
        
        foreach (var student in Students.Except([GroupLeader]))
        {
            Console.Write(student + "; ");
        }
        
        Console.WriteLine();
    }

    public bool TryExpelStudent(string studentName)
    {
        if (Students.Remove(studentName))
        {
            if (GroupLeader == studentName) 
                GroupLeader = null;

            return true;
        }

        return false;
    }

    public bool CanAcceptStudent(string studentName)
    {
        return !Students.Contains(studentName);
    }

    public override string ToString() => $"Группа {GroupNumber}";
}