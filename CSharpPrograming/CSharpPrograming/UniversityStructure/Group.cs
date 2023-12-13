namespace CSharpPrograming.UniversityStructure;

internal abstract class Group
{
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

    public void PrintStudents()
    {
        Console.Write($"{ToString()}: староста {GroupLeader?.Name} (подгруппа {GroupLeader?.Subgroup.SubgroupNumber}); ");
        
        foreach (var student in Students.Except([GroupLeader]))
        {
            Console.Write($"{student.Name} (подгруппа {student.Subgroup.SubgroupNumber}); ");
        }
        
        Console.WriteLine();
    }

    public bool TryExpelStudent(Student student)
    {
        if (Students.Remove(student))
        {
            if (GroupLeader == student) 
                GroupLeader = null;

            return true;
        }

        return false;
    }

    public bool CanAcceptStudent(Student student)
    {
        return !Students.Contains(student);
    }

    public bool TryAcceptStudent(Student student)
    {
        if (Students.Add(student))
        {
            if (!Subgroups.Contains(student.Subgroup))
                student.Subgroup = this.Subgroups.First();
            
            return true;
        }

        return false;
    }

    public void DistributeStudentsInSubgroups()
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