namespace CSharpPrograming.UniversityStructure;

internal class Faculty
{
    public required string Name { get; set; }
    public required University University { get; init; }

    public bool TryAcceptStudent(string studentName, Group group) 
    {
        return group.Students.Add(studentName);
    }

    public bool TryExpelStudent(string studentName, Group group)
    {
        return group.TryExpelStudent(studentName);
    }

    public bool TryTransferStudent(string studentName, Group groupFrom, Group groupTo)
    {
        if (groupTo.CanAcceptStudent(studentName) && groupFrom.TryExpelStudent(studentName))
            return groupTo.Students.Add(studentName);

        return false;
    }

    public override string ToString() => $"{Name} {University.ShortName}";
}
