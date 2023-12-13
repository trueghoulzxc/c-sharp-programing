namespace CSharpPrograming.UniversityStructure;

internal class Faculty
{
    public required string Name { get; set; }
    public required University University { get; init; }

    public bool TryAcceptStudent(Student student, Group group) 
    {
        return group.TryAcceptStudent(student);
    }

    public bool TryExpelStudent(Student student, Group group)
    {
        return group.TryExpelStudent(student);
    }

    public bool TryTransferStudent(Student student, Group groupFrom, Group groupTo)
    {
        if (groupTo.CanAcceptStudent(student) && groupFrom.TryExpelStudent(student))
            return groupTo.TryAcceptStudent(student);

        return false;
    }

    public override string ToString() => $"{Name} {University.ShortName}";
}
