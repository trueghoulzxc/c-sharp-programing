namespace CSharpPrograming.UniversityStructure;

internal class Student
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; init; }
    public Subgroup Subgroup { get; set; }

    public Student(string name, Subgroup subgroup)
    {
        Name = name;
        Subgroup = subgroup;
    }

    public Student(string name, Group group)
    {
        Name = name;
        Subgroup = group.Subgroups.First();
    }

    public override string ToString() => "Студент " + Name;

    public override bool Equals(object? obj)
    {
        if (obj is Student otherStudent)
            return this.Id == otherStudent.Id;    
            
        return false;        
    }

    public override int GetHashCode() => Id.GetHashCode();
}
